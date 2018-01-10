using System;
using System.Configuration;
using System.IO;

namespace GWiLi.Core
{
    public sealed class FileLog
    {
        #region Properties
        private const string FileLocationAppKey = "FileLogPath";
        private const string DateFormat = "yyyyMMdd";
        private const string DateTimeFormat = "HH:mm MM/dd/yy";
        private const string TimeFormat = "HH:mm:ss";
        private const string LineSep = "\r\n";
        #endregion

        #region Singleton
        private static volatile FileLog _instance;
        private static object syncRoot = new object();

        private FileLog() { }

        public static FileLog Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock(syncRoot)
                    {
                        if(_instance == null)
                            _instance = new FileLog();
                    }
                }

                return _instance;
            }
        }
        #endregion

        public bool WriteLine(string msg)
        {
            try
            {
                File.AppendAllText($"{GetFileLocation()}{GetFilename()}", $"{DateTime.Now.ToString(TimeFormat)}: {msg}{LineSep}");
                return true;
            }
            catch (Exception e)
            {
                // TODO
            }
            return false;
        }

        private string GetFilename()
        {
            return $"GWiLi_{DateTime.Today.ToString(DateFormat)}.log";
        }

        private string GetFileLocation()
        {
            return ConfigurationManager.AppSettings[FileLocationAppKey];
        }
    }
}