using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SetOpenWithCommand
{

    public static class Recorder
    {
        private static readonly string RecordPath = MiscCfg.UserAppDataPath + "Record.byte";

        [Serializable]
        public class RecordInfo
        {
            private List<string> _exeHistory = new List<string>();
            private List<string> _commandHistory = new List<string>();
            private List<string> _fileExtensionHistory = new List<string>();

            public List<string> GetExeHistory()
            {
                return new List<string>(_exeHistory);
            }

            public List<string> GetCommandHistory()
            {
                return new List<string>(_commandHistory);
            }

            public List<string> GetFileExtensions()
            {
                return new List<string>(_fileExtensionHistory);
            }

            public void AddExeHistory(string exePath)
            {
                if (!_exeHistory.Contains(exePath))
                {
                    _exeHistory.Add(exePath);
                }
            }

            public void AddRangeExeHistory(IEnumerable<string> histories)
            {
                foreach (var history in histories)
                {
                    AddExeHistory(history);
                }
            }

            public void AddCommandHistory(string command)
            {
                if (!_commandHistory.Contains(command))
                {
                    _commandHistory.Add(command);
                }
            }

            public void AddRangeCommandHistory(IEnumerable<string> histories)
            {
                foreach (var history in histories)
                {
                    AddCommandHistory(history);
                }

            }

            public void AddFileExtension(string fileExtension)
            {
                if (!_fileExtensionHistory.Contains(fileExtension))
                {
                    _fileExtensionHistory.Add(fileExtension);
                }
            }

            public void AddRangeFileExtension(IEnumerable<string> histories)
            {
                foreach (var history in histories)
                {
                    AddFileExtension(history);
                }
            }
        }

        public static RecordInfo Info
        {
            get
            {
                if (_info == null)
                {
                    Load();
                }
                return _info;
            }
        }
        private static RecordInfo _info;

        private static void Load()
        {
            if (!File.Exists(RecordPath))
            {
                _info = new RecordInfo();
                _info.AddRangeFileExtension(new string[] { ".json", ".txt", ".mp4", ".exe", ".dll", "*", "Folder" });
            }
            else
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream record = File.Open(RecordPath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
                _info = formatter.Deserialize(record) as RecordInfo;
                record.Close();
            }
        }

        public static void Quit()
        {
            Write();
        }

        private static void Write()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream writer = File.Open(RecordPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
            formatter.Serialize(writer, Info);
            writer.Close();
        }
    }
}
