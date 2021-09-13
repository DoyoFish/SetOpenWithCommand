using System;
using System.IO;
using System.Windows.Forms;

namespace SetOpenWithCommand.Forms
{
    public partial class Main : Form
    {

        public Main()
        {
            InitializeComponent();
            _fileTypeList.Items.AddRange(Recorder.Info.GetFileExtensions().ToArray());
            _commandNameText.Items.AddRange(Recorder.Info.GetCommandHistory().ToArray());
            _pathText.Items.AddRange(Recorder.Info.GetExeHistory().ToArray());
            MouseRightMenuManager.OnError += OnMouseRightMenuManagerError;
            MouseRightMenuManager.OnError += LogWriter.Instance.LogError;
            MouseRightMenuManager.OnLog += LogWriter.Instance.Log;
            LogWriter.Instance.Start();
            _extraParams.Visible = false;
        }

        private void OnMouseRightMenuManagerError(object sender, RegisterRightMenuMsgEventArgs e)
        {
            MessageBox.Show(e.Msg);
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            string fileType = _fileTypeList.Text;
            string commandName = _commandNameText.Text;
            string exePath = _pathText.Text;
            string extraParam = _extraParams.Text;
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
            if (!MouseRightMenuManager.RegisterRightMenu(fileType, commandName, exePath, extraParam))
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
            _extraParams.Visible = _extraParamTog.CheckState == CheckState.Checked;
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
