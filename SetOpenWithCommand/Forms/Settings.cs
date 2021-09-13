using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SetOpenWithCommand.Forms
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void OpenLogBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(LogWriter.Instance.LogPath))
            {
                MessageBox.Show("日志路径为空, 配置错误");
                return;
            }
            if (!File.Exists(LogWriter.Instance.LogPath))
            {
                MessageBox.Show($"日志路径上不存在文件: {LogWriter.Instance.LogPath}");
                return;
            }
            System.Diagnostics.Process.Start("Explorer.exe", LogWriter.Instance.LogPath);
        }

        private void OpenLogFolderBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(LogWriter.Instance.LogPath))
            {
                MessageBox.Show("日志路径为空, 配置错误");
                return;
            }
            string folder = Path.GetDirectoryName(LogWriter.Instance.LogPath);
            if (!Directory.Exists(folder))
            {
                MessageBox.Show($"日志文件夹路径不存在: {folder}");
                return;
            }
            System.Diagnostics.Process.Start("Explorer.exe", folder);
        }
    }
}
