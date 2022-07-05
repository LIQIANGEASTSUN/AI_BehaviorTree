using System.Collections.Generic;

namespace BehaviorTree
{
    public struct LocalizationData
    {
        public string cn;
        public string en;
    }

    public class Localization
    {
        private static Localization Instance;
        private const string fileName = "table_text_localization";
        private Dictionary<string, LocalizationData> _dataDic = new Dictionary<string, LocalizationData>();

        private object lockObj = new object();
        public Localization GetInstance()
        {
            if (null == Instance)
            {
                lock (lockObj)
                {
                    Instance = new Localization();
                }
            }
            return Instance;
        }

        private Localization()
        {
            string csvPath = BehaviorDataController.Instance.GetCsvPath();
            TableRead.Instance.ReadCustomPath(csvPath);
            List<int> keyList = TableRead.Instance.GetKeyList(fileName);
            foreach(var key in keyList)
            {

            }
        }

        public string Format(string key)
        {


            // Debug.LogError(filePath + "   " + fileName);

            return key;
        }

    }
}
