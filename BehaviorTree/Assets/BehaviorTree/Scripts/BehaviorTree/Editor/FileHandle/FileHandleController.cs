using UnityEngine;
using System.IO;

namespace BehaviorTree
{
    public class FileHandleController
    {
        private FileHandleView _fileHandleView;
        private const string BEHAVIOUR_FILE_KEY = "BEHAVIOUR_FILE_KEY";

        public FileHandleController()
        {
            Init();
        }

        public void Init()
        {
            _fileHandleView = new FileHandleView();
        }

        public void OnDestroy()
        {

        }

        public static string GetFileFolder()
        {
            if (!PlayerPrefs.HasKey(BEHAVIOUR_FILE_KEY))
            {
                return string.Empty;
            }
            return PlayerPrefs.GetString(BEHAVIOUR_FILE_KEY);
        }

        public static string SaveFilePath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return string.Empty;
            }
            if (File.Exists(filePath))
            {
                FileAttributes attr = File.GetAttributes(filePath);
                if (!attr.HasFlag(FileAttributes.Directory))
                {
                    filePath = Path.GetDirectoryName(filePath);
                }
            }
            int index = filePath.IndexOf("Assets");
            index += ("Assets").Length + 1;
            if (index > filePath.Length)
            {
                return string.Empty;
            }
            string subPath = filePath.Substring(index);
            PlayerPrefs.SetString(BEHAVIOUR_FILE_KEY, subPath);
            return subPath;
        }

        public void OnGUI()
        {
            _fileHandleView.Draw();
        }
    }
}
