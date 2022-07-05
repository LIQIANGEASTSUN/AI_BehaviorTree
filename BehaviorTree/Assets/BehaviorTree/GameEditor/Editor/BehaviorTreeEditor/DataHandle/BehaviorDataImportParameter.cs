using GraphicTree;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class BehaviorDataImportParameter
    {
        public void ImportParameter(BehaviorTreeData behaviorData)
        {
            string fileName = "table_behaviortree";
            TableRead.Instance.Init();

            string csvPath = BehaviorDataController.Instance.GetCsvPath();
            TableRead.Instance.ReadCustomPath(csvPath);

            // Debug.LogError(filePath + "   " + fileName);
            List<int> keyList = TableRead.Instance.GetKeyList(fileName);

            Dictionary<string, NodeParameter> parameterDic = new Dictionary<string, NodeParameter>();
            for (int i = 0; i < behaviorData.parameterList.Count; ++i)
            {
                NodeParameter parameter = behaviorData.parameterList[i];
                parameterDic[parameter.parameterName] = parameter;
            }

            for (int i = 0; i < keyList.Count; ++i)
            {
                int key = keyList[i];
                string EnName = TableRead.Instance.GetData(fileName, key, "EnName");
                string cnName = TableRead.Instance.GetData(fileName, key, "CnName");
                string typeName = TableRead.Instance.GetData(fileName, key, "Type");
                int type = int.Parse(typeName);

                string floatContent = TableRead.Instance.GetData(fileName, key, "FloatValue");
                float floatValue = float.Parse(floatContent, System.Globalization.CultureInfo.InvariantCulture);

                string intContent = TableRead.Instance.GetData(fileName, key, "IntValue");
                int intValue = int.Parse(intContent);

                string longContent = TableRead.Instance.GetData(fileName, key, "LongValue");
                long longValue = long.Parse(intContent);

                string boolContent = TableRead.Instance.GetData(fileName, key, "BoolValue");
                bool boolValue = (int.Parse(boolContent) == 1);

                string stringValue = TableRead.Instance.GetData(fileName, key, "StringValue");

                NodeParameter parameter = new NodeParameter();
                parameter.parameterName = EnName;
                parameter.CNName = cnName;
                parameter.compare = (int)ParameterCompare.EQUALS;
                parameter.parameterType = type;

                parameter.floatValue = floatValue;
                parameter.intValue = intValue;
                parameter.longValue = longValue;
                parameter.boolValue = boolValue;
                parameter.stringValue = stringValue;

                if (parameterDic.ContainsKey(EnName))
                {
                    if (parameterDic[EnName].parameterType != type)
                    {
                        Debug.LogError("已经存在参数:" + EnName + "   type:" + (ParameterType)parameterDic[EnName].parameterType + "   newType:" + (ParameterType)type);
                    }

                    parameterDic[EnName].CloneFrom(parameter);
                }
                else
                {
                    behaviorData.parameterList.Add(parameter);
                }
            }
        }

    }

}