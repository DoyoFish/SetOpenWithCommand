using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetOpenWithCommand
{
    public class LogWriter
    {
        private enum LogType
        {
            Log,
            Warning,
            Error,
        }

        public static LogWriter Instance
        {
            get { return _instance; }
        }

        private static LogWriter _instance;
        static LogWriter()
        {
            _instance = new LogWriter();
        }

        private readonly Queue<string> _logs = new Queue<string>();

        private bool _quit = false;

        private StreamWriter _writer = null;

        public string LogPath
        {
            get
            {
                if (string.IsNullOrEmpty(_logPath))
                {
                    _logPath = MiscCfg.UserAppDataPath + "registerLog.txt";
                    string folder = Path.GetDirectoryName(_logPath);
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                }
                return _logPath;
            }
        }

        private string _logPath;

        public void Start()
        {
            Task.Run(new Action(LoopWrite));
        }

        public void Quit()
        {
            _quit = true;
        }

        private async void LoopWrite()
        {
            string log;
            _writer = new StreamWriter(LogPath, true);
            while (true)
            {
                await Task.Delay(10);
                while(_logs.Count != 0)
                {
                    log = _logs.Dequeue();
                    _writer.WriteLine(log);
                    _writer.Flush();
                    await Task.Delay(10);
                    if (_quit)
                    {
                        break;
                    }
                }
                if (_quit)
                {
                    _writer.WriteLine("==============================");
                    _writer.Flush();
                    _writer.Close();
                    break;
                }
            }
        }

        public void Log(object sender, RegisterRightMenuMsgEventArgs e)
        {
            _logs.Enqueue(AddTag(LogType.Log, e.Msg));
        }

        public void LogError(object sender, RegisterRightMenuMsgEventArgs e)
        {
            _logs.Enqueue(AddTag(LogType.Error, e.Msg));
        }

        private static string AddTag(LogType logType, string log)
        {
            log = string.Format("[{0} {1}] : {2}", DateTime.Now, logType, log);
            return log;
        }
    }
}
