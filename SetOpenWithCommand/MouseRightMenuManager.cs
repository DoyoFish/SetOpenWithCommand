using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text.RegularExpressions;

namespace SetOpenWithCommand
{
    public delegate void RegisterRightMenuMsgEventHandler(object sender, RegisterRightMenuMsgEventArgs e);

    public class RegisterRightMenuMsgEventArgs : EventArgs
    {
        public string Msg { get; set; }

        public RegisterRightMenuMsgEventArgs(string msg)
        {
            Msg = msg;
        }
    }

    public static class MouseRightMenuManager
    {

        public static event RegisterRightMenuMsgEventHandler OnError;

        public static event RegisterRightMenuMsgEventHandler OnLog;

        private const string SystemFileAssociations = "SystemFileAssociations\\";

        public static bool RegisterRightMenu(string fileType, string itemName, string exePath, string extraParam)
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
                RegistryKey menuName = key.CreateSubKey(itemName);
                RegistryKey command = menuName.CreateSubKey("command");
                menuName.SetValue("Icon", exePath);
                command.SetValue("", string.Format("\"{0}\" {1} \"%1\"", exePath, extraParam));
                command.Close();
                Log($"注册成功, 地址为{menuName.Name}");
                Recorder.Info.AddCommandHistory(itemName);
                Recorder.Info.AddExeHistory(exePath);
                Recorder.Info.AddFileExtension(fileType);
                menuName.Close();
                key.Close();
                result = true;
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
                result = false;
            }
            return result;
        }

        public static bool UnregisterRightMenu(string fileType, string itemName)
        {
            fileType = FileTypeFormatter(fileType);
            bool result;
            try
            {
                string name = string.Format("{0}\\shell", fileType);
                RegistryKey key = Registry.ClassesRoot.OpenSubKey(name, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.FullControl);
                if (key == null)
                {
                    result = false;
                }
                else
                {
                    RegistryKey menuName = key.OpenSubKey(itemName, true);
                    if (menuName == null)
                    {
                        result = false;
                        LogError($"{itemName}命令未被注册");
                        return result;
                    }
                    if (menuName.GetSubKeyNames().Contains("command"))
                    {
                        menuName.DeleteSubKey("command", true);
                    }
                    if (menuName.SubKeyCount == 0)
                    {
                        key.DeleteSubKey(itemName, true);
                    }
                    key.Close();
                    result = true;
                    Log($"注销成功, 移除了{itemName}");
                }
            }
            catch (Exception ex)
            {
                LogError($"注销{itemName}失败, 原因为{ex.Message}");
                result = false;
            }
            return result;
        }

        private static string FileTypeFormatter(string fileType)
        {
            if(fileType.ToLower() == "folder")
            {
                return "Folder";
            }
            fileType = SystemFileAssociations + fileType;
            return fileType;
        }

        private static void Log(string log)
        {
            OnLog?.Invoke(null, new RegisterRightMenuMsgEventArgs(log));
        }

        private static void LogError(string error)
        {
            OnError?.Invoke(null, new RegisterRightMenuMsgEventArgs(error));
        }
    }
}
