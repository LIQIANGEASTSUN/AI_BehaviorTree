using UnityEngine;
using LitJson;

namespace BehaviorTree
{
    public class BehaviorReadWrite
    {
        public bool WriteJson(BehaviorTreeData data, string filePath)
        {
            string content = JsonMapper.ToJson(data);
            bool value = FileReadWrite.Write(filePath, content);

            if (value)
            {
                Debug.Log("Write Sucess:" + filePath);
            }
            else
            {
                Debug.LogError("Write Fail:" + filePath);
            }

            return value;
        }

        public BehaviorTreeData ReadJson(string filePath)
        {
            Debug.Log("Read:" + filePath);
            string content = FileReadWrite.Read(filePath);
            BehaviorTreeData behaviorData = JsonMapper.ToObject<BehaviorTreeData>(content);
            return behaviorData;
        }
    }
}
