using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

namespace SetOpenWithCommand
{
    public enum OperationType
    {
        KeyCheck,
        Register,
        Unregister,
    }

    [Flags]
    public enum RegistryKeyExistType
    {
        Unknown = -1,
        Unexist = 0,
        MenuUnexist = 1,
        CommandUnexist = 2,
        Exist = 4,
    }

    public delegate void ErrorMsgEventHandler(object sender, ErrorMsgEventArgs e);

    public delegate void RegisterRightMenuEventHandler(object sender, RegisterRightMenuEventArgs e);

    public class ErrorMsgEventArgs : EventArgs
    {
        public OperationType Operation { get; }
        public string Msg { get; }

        public ErrorMsgEventArgs(OperationType operation, string msg)
        {
            Operation = operation;
            Msg = msg;
        }
    }

    public class RegisterRightMenuEventArgs : EventArgs
    {
        public OperationType Operation { get; }
        public string Address { get; }
        public string ItemName { get; }
        public string ExePath { get; }
        public string FileType { get; }

        public RegisterRightMenuEventArgs(OperationType type, string addr, string itemName, string exePath, string fileType)
        {
            Operation = type;
            Address = addr;
            ItemName = itemName;
            ExePath = exePath;
            FileType = fileType;
        }
    }

    public static class MouseRightMenuManager
    {

        public static event ErrorMsgEventHandler OnError;
        public static event RegisterRightMenuEventHandler OnUnregister;
        public static event RegisterRightMenuEventHandler OnRegister;

        private const string SystemFileAssociations = "SystemFileAssociations\\";

        private static readonly string[] EmptyStringArr = new string[0];

        public static bool RegisterRightMenu(string fileType, string itemName, string exePath)
        {
            return RegisterRightMenuWithFullArgs(fileType, itemName, exePath, EmptyStringArr, "%1", EmptyStringArr);
        }

        public static bool RegisterRightMenuWithLeftArgs(string fileType, string itemName, string exePath, params string[] leftExtraParams)
        {
            return RegisterRightMenuWithFullArgs(fileType, itemName, exePath, leftExtraParams, "%1", EmptyStringArr);
        }

        public static bool RegisterRightMenuWithRightArgs(string fileType, string itemName, string exePath, params string[] rightExtraParams)
        {

            return RegisterRightMenuWithFullArgs(fileType, itemName, exePath, EmptyStringArr, "%1", rightExtraParams);
        }

        public static bool RegisterRightMenuWithFullArgs(string fileType, string itemName, string exePath, string[] leftExtraParams, string inputPath, string[] rightExtraParams)
        {
            string formattedFileType = FileTypeFormatter(fileType);
            bool result;
            try
            {
                string text = string.Format("{0}\\shell", formattedFileType);
                RegistryKey key = Registry.ClassesRoot.OpenSubKey(text, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.FullControl);
                if (key == null)
                {
                    key = Registry.ClassesRoot.CreateSubKey(text);
                }
                RegistryKey menu = key.CreateSubKey(itemName);
                RegistryKey command = menu.CreateSubKey("command");
                menu.SetValue("Icon", exePath);
                command.SetValue("", string.Format("\"{0}\" {1} \"{2}\" {3}", exePath, leftExtraParams.ToParams(), inputPath, rightExtraParams.ToParams()));
                command.Close();
                OnRegister?.Invoke(null, new RegisterRightMenuEventArgs(OperationType.Register, menu.Name, itemName, exePath, fileType));
                menu.Close();
                key.Close();
                result = true;
            }
            catch (Exception ex)
            {
                LogError(OperationType.Register, ex.Message);
                result = false;
            }
            return result;
        }

        public static bool UnregisterRightMenu(string fileType, string itemName)
        {
            fileType = FileTypeFormatter(fileType);
            try
            {
                string name = string.Format("{0}\\shell", fileType);
                RegistryKey key = Registry.ClassesRoot.OpenSubKey(name, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.FullControl);
                RegistryKeyExistType existType = GetRegistryKeyExistType(key, itemName, out RegistryKey menu);
                if (existType == RegistryKeyExistType.Unexist)
                {
                    LogError(OperationType.Unregister, $"{itemName}命令尚未注册");
                    return false;
                }
                else if (existType == RegistryKeyExistType.MenuUnexist)
                {
                    LogError(OperationType.Unregister, $"{itemName}命令未被注册");
                    return false;
                }
                else
                {
                    if (existType != RegistryKeyExistType.CommandUnexist)
                    {
                        menu.DeleteSubKey("command", true);
                    }
                    if (menu.SubKeyCount == 0)
                    {
                        key.DeleteSubKey(itemName, true);
                    }
                    OnUnregister?.Invoke(null, new RegisterRightMenuEventArgs(OperationType.Unregister, menu.Name, itemName, null, null));
                    key.Close();
                    return true;
                }
            }

            catch (Exception ex)
            {
                LogError(OperationType.Unregister, ex.Message);
                return false;
            }
        }

        public static RegistryKeyExistType GetRegistryKeyExistType(string fileType, string itemName, out RegistryKey menu)
        {
            fileType = FileTypeFormatter(fileType);
            menu = null;
            try
            {
                string name = string.Format("{0}\\shell", fileType);
                RegistryKey key = Registry.ClassesRoot.OpenSubKey(name, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.FullControl);
                return GetRegistryKeyExistType(key, itemName, out menu);
            }
            catch (Exception ex)
            {
                LogError(OperationType.KeyCheck, ex.Message);
                return RegistryKeyExistType.Unknown;
            }
        }

        private static RegistryKeyExistType GetRegistryKeyExistType(RegistryKey key, string itemName, out RegistryKey menu)
        {
            menu = null;
            if (key == null)
            {
                return RegistryKeyExistType.Unexist;
            }
            menu = key.OpenSubKey(itemName, true);
            if (menu == null)
            {
                return RegistryKeyExistType.MenuUnexist;
            }
            if (!menu.GetSubKeyNames().Contains("command"))
            {
                return RegistryKeyExistType.CommandUnexist;
            }
            return RegistryKeyExistType.Exist;
        }

        private static string FileTypeFormatter(string fileType)
        {
            if (fileType.ToLower() == "folder")
            {
                return "Folder";
            }
            else if (fileType == "*")
            {
                return "*";
            }
            fileType = SystemFileAssociations + fileType;
            return fileType;
        }

        private static void LogError(OperationType type, string error)
        {
            OnError?.Invoke(null, new ErrorMsgEventArgs(type, error));
        }

        private static string ToParams(this IList<string> extraParams)
        {
            if (extraParams == null)
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            foreach (var right in extraParams)
            {
                builder.AppendFormat("{0} ", right.ToParam());
            }
            return builder.ToString().Trim();
        }

        private static string ToParam(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return string.Empty;
            }
            return string.Format("\"{0}\"", source);
        }
    }
}
