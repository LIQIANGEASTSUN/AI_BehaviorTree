using System.Collections.Generic;

namespace BehaviorTree
{
    public enum LanguageType
    {
        CN = 0,
        EN = 1,
    }

    public struct LocalizationData
    {
        public string cn;
        public string en;
    }

    public class Localization
    {
        private static Localization Instance;
        private const string tableName = "table_text_localization";
        private Dictionary<string, LocalizationData> _dataDic = new Dictionary<string, LocalizationData>();

        private static object lockObj = new object();
        public static Localization GetInstance()
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
            
        }

        public string Format(string key)
        {
            LocalizationData data;
            if (!DataDic.TryGetValue(key, out data))
            {
                return key;
            }

            if (BehaviorDataController.Instance.LanguageType == LanguageType.CN)
            {
                return data.cn;
            }
            return data.en;
        }

        private Dictionary<string, LocalizationData> DataDic
        {
            get
            {
                if (_dataDic.Count <= 0)
                {
                    LoadLocalization();
                }
                return _dataDic;
            }
        }

        private void LoadLocalization()
        {
            //if (null == BehaviorDataController.Instance)
            //{
            //    return;
            //}
            _dataDic.Clear();
            string csvPath = BehaviorDataController.Instance.GetCsvPath();
            TableRead.Instance.ReadCustomPath(csvPath);
            List<int> idList = TableRead.Instance.GetKeyList(tableName);
            foreach (var id in idList)
            {
                LocalizationData data = new LocalizationData();
                string key = TableRead.Instance.GetData(tableName, id, "Key");
                data.cn = TableRead.Instance.GetData(tableName, id, "CN");
                data.en = TableRead.Instance.GetData(tableName, id, "EN");
                _dataDic[key] = data;
            }
        }

    }
}
