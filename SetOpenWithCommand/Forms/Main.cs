using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SetOpenWithCommand.Forms
{
    public partial class Main : Form
    {
        private static readonly string ExePath = Process.GetCurrentProcess().MainModule.FileName;
        private static readonly string SelfCommand = "Set As Usual Open Command";

        private static readonly Dictionary<OperationType, string> Translation = new Dictionary<OperationType, string>()
        {
            {OperationType.KeyCheck, "注册表检查"},
            {OperationType.Register, "注册"},
            {OperationType.Unregister, "注销"}
        };

        public Main(params string[] args)
        {
            InitializeComponent();
            RegisterSelf();
            ShowDefaults(args);
            RegisterListener();
        }

        private void RegisterSelf()
        {
            var type = MouseRightMenuManager.GetRegistryKeyExistType(".exe", SelfCommand, out Microsoft.Win32.RegistryKey menu);
            if (type != RegistryKeyExistType.Exist)
            {
                if (Recorder.Info.ShowRegisterSelf)
                {
                    return;
                }
                if (MessageBox.Show($"将{SelfCommand}注册到鼠标右键菜单, 方便将任意exe设置为打开方式, 不需要时, 可以通过本软件注销", "Tips", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    MouseRightMenuManager.RegisterRightMenu(".exe", SelfCommand, ExePath);
                }
            }
            else
            {
                var icon = menu.GetValue("Icon").ToString();
                if (icon != ExePath)
                {
                    if (MessageBox.Show("检测到该exe的路径可能发生变化，请问是否需要重新注册快捷命令", "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        MouseRightMenuManager.RegisterRightMenu(".exe", SelfCommand, ExePath);
                    }
                }
            }
        }

        private void ShowDefaults(params string[] args)
        {
            _fileTypeList.Items.AddRange(Recorder.Info.GetFileExtensions().ToArray());
            _commandNameText.Items.AddRange(Recorder.Info.GetCommandHistory().ToArray());
            _pathText.Items.AddRange(Recorder.Info.GetExeHistory().ToArray());
            _pathText.Text = args.FirstOrDefault();
            _leftExtraParams.Visible = false;
            _leftParamLabel.Visible = false;
            _rightExtraParams.Visible = false;
            _rightParamLabel.Visible = false;
        }

        private void RegisterListener()
        {
            MouseRightMenuManager.OnError += OnMouseRightMenuManagerError;
            MouseRightMenuManager.OnRegister += OnMouseRightMenuManagerRegister;
            MouseRightMenuManager.OnUnregister += OnMouseRightMenuManagerUnregister;
            LogWriter.Instance.Start();
        }

        private void OnMouseRightMenuManagerError(object sender, ErrorMsgEventArgs e)
        {
            LogWriter.Instance.LogError($"{Translation[e.Operation]}失败, 原因为: {e.Msg}");
            MessageBox.Show($"{Translation[e.Operation]}失败, 原因为: {e.Msg}");
        }

        private void OnMouseRightMenuManagerRegister(object sender, RegisterRightMenuEventArgs e)
        {
            LogWriter.Instance.Log($"注册成功, 地址为{e.Address}");
            Recorder.Info.AddCommandHistory(e.ItemName);
            Recorder.Info.AddExeHistory(e.ExePath);
            Recorder.Info.AddFileExtension(e.FileType);
        }

        private void OnMouseRightMenuManagerUnregister(object sender, RegisterRightMenuEventArgs e)
        {
            LogWriter.Instance.Log($"注销成功, 菜单为{e.ItemName}");
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            string fileType = _fileTypeList.Text;
            string commandName = _commandNameText.Text;
            string exePath = _pathText.Text;
            string leftExtraParams = _leftExtraParams.Text;
            string rightExtraParams = _rightExtraParams.Text;
            if (!File.Exists(exePath))
            {
                MessageBox.Show("可执行程序不存在");
                return;
            }
            if (string.IsNullOrEmpty(commandName))
            {
                MessageBox.Show("命令名为空");
                return;
            }
            if (string.IsNullOrEmpty(fileType))
            {
                MessageBox.Show("文件类型为空");
                return;
            }
            if (!MouseRightMenuManager.RegisterRightMenuWithFullArgs(fileType, commandName, exePath, leftExtraParams.Split(';'), "%1", rightExtraParams.Split(';')))
            {
                MessageBox.Show("注册失败");
                return;
            }
            MessageBox.Show("注册成功");
        }

        private void UnregisterBtn_Click(object sender, EventArgs e)
        {
            string fileType = this._fileTypeList.Text;
            string menuName = this._commandNameText.Text;
            if (MouseRightMenuManager.UnregisterRightMenu(fileType, menuName))
            {
                MessageBox.Show("注销成功");
                return;
            }
            MessageBox.Show("注销失败");
        }

        private void ExtraParamTog_CheckedChanged(object sender, EventArgs e)
        {
            _leftExtraParams.Visible = _extraParamTog.CheckState == CheckState.Checked;
            _leftParamLabel.Visible = _extraParamTog.CheckState == CheckState.Checked;
            _rightExtraParams.Visible = _extraParamTog.CheckState == CheckState.Checked;
            _rightParamLabel.Visible = _extraParamTog.CheckState == CheckState.Checked;
        }

        private void _browserBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "选择使用的软件";
                dialog.Filter = "exe文件|*.exe|EXE文件|*.EXE";
                dialog.Multiselect = false;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    _pathText.Text = dialog.FileName;
                }
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            LogWriter.Instance.Quit();
            Recorder.Quit();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            using (Settings settings = new Settings())
            {
                settings.ShowDialog(this);
            }

        }
    }
}
