﻿using System.Collections.Generic;
using GraphicTree;

namespace BehaviorTree
{
    public class Node_Draw_Info
    {
        public string _nodeTypeName;
        public List<KeyValuePair<string, Node_Draw_Info_Item>> _nodeArr = new List<KeyValuePair<string, Node_Draw_Info_Item>>();

        public Node_Draw_Info(string name)
        {
            _nodeTypeName = name;
        }

        public void AddNodeType(NODE_TYPE nodeType, string nodeName, string identificationName)
        {
            Node_Draw_Info_Item item = new Node_Draw_Info_Item(nodeType);
            item.SetName(nodeName);
            item.SetIdentification(identificationName);
            string name = string.Format("{0}/{1}", _nodeTypeName, nodeName);
            KeyValuePair<string, Node_Draw_Info_Item> kv = new KeyValuePair<string, Node_Draw_Info_Item>(name, item);
            _nodeArr.Add(kv);
        }

    }

    public class Node_Draw_Info_Item
    {
        public string _nodeName = string.Empty;
        public NODE_TYPE _nodeType;
        public string _identificationName = string.Empty;

        public Node_Draw_Info_Item(NODE_TYPE nodeType)
        {
            _nodeType = nodeType;
        }

        public void SetName(string name)
        {
            _nodeName = name;
        }

        public void SetIdentification(string identificationName)
        {
            _identificationName = identificationName;
        }
    }

    public class BehaviorNodeDrawInfoController
    {
        private List<Node_Draw_Info> infoList = new List<Node_Draw_Info>();
        public static BehaviorNodeDrawInfoController Instance;

        public static BehaviorNodeDrawInfoController GetInstance()
        {
            if (null == Instance)
            {
                Instance = new BehaviorNodeDrawInfoController();
            }
            return Instance;
        }

        public BehaviorNodeDrawInfoController()
        {
            DataController.languageChange += InitInfoList;
        }

        public void InitInfoList()
        {
            infoList.Clear();

            #region Node
            // composite node
            string compositeName = $"添加节点/组合节点";
            Node_Draw_Info compositeDrawInfo = new Node_Draw_Info(compositeName);
            infoList.Add(compositeDrawInfo);

            // decorator node
            string decoratorName = $"添加节点/修饰节点";
            Node_Draw_Info decoratorDrawInfo = new Node_Draw_Info(decoratorName);
            infoList.Add(decoratorDrawInfo);

            // condition node
            string conditionName = "添加节点/条件节点";
            Node_Draw_Info conditionDrawInfo = new Node_Draw_Info(conditionName);
            infoList.Add(conditionDrawInfo);

            // action node
            string actionName = "添加节点/行为节点";
            Node_Draw_Info actionDrawInfo = new Node_Draw_Info(actionName);
            infoList.Add(actionDrawInfo);

            Node_Draw_Info subTreeDrawInfo = new Node_Draw_Info("添加子树");
            infoList.Add(subTreeDrawInfo);

            Dictionary<string, CustomIdentification> nodeDic = BehaviorConfigNode.Instance.GetNodeDic();
            foreach (var kv in nodeDic)
            {
                CustomIdentification customIdentification = kv.Value;
                NODE_TYPE nodeType = (NODE_TYPE)customIdentification.NodeType;
                string name = customIdentification.Name;
                if ((int)nodeType >= (int)NODE_TYPE.SUB_TREE)
                {
                    subTreeDrawInfo.AddNodeType(nodeType, name, customIdentification.IdentificationName);
                }
                else if ((int)nodeType >= (int)NODE_TYPE.ACTION)
                {
                    actionDrawInfo.AddNodeType(nodeType, name, customIdentification.IdentificationName);
                }
                else if ((int)nodeType >= (int)NODE_TYPE.CONDITION)
                {
                    conditionDrawInfo.AddNodeType(nodeType, name, customIdentification.IdentificationName);
                }
                else if ((int)nodeType >= (int)NODE_TYPE.DECORATOR_INVERTER)
                {
                    decoratorDrawInfo.AddNodeType(nodeType, name, customIdentification.IdentificationName);
                }
                else if ((int)nodeType >= (int)NODE_TYPE.SELECT)
                {
                    compositeDrawInfo.AddNodeType(nodeType, name, customIdentification.IdentificationName);
                }
            }
            #endregion
        }

        public List<Node_Draw_Info> InfoList
        {
            get
            {
                return infoList;
            }
        }
    }

}