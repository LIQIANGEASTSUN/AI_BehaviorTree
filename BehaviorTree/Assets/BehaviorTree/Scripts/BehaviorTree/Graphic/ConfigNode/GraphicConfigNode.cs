using System.Collections.Generic;

namespace GraphicTree
{
    public sealed class GraphicConfigNode
    {
        private Dictionary<string, CustomIdentification> nodeDic = new Dictionary<string, CustomIdentification>();
        public GraphicConfigNode() { }

        public void Config<T>(string name) where T : AbstractNode, new()
        {
            CustomIdentification customIdentification = new CustomIdentification(name, typeof(T));
            nodeDic[customIdentification.IdentificationName] = customIdentification;
        }

        public void Config<T>(string name, int nodeType) where T : AbstractNode, new()
        {
            CustomIdentification customIdentification = new CustomIdentification(name, typeof(T), nodeType);
            nodeDic[customIdentification.IdentificationName] = customIdentification;
        }
        public void Config(string name, string identificationName, int nodeType)
        {
            CustomIdentification customIdentification = new CustomIdentification(name, identificationName, nodeType);
            nodeDic[customIdentification.IdentificationName] = customIdentification;
        }

        public AbstractNode GetNode(string identificationName)
        {
            CustomIdentification info = GetIdentification(identificationName);
            if (null == info)
            {
                UnityEngine.Debug.LogError(identificationName);
            }
            AbstractNode obj = info.Create() as AbstractNode;
            return obj;
        }

        public CustomIdentification GetIdentification(string identificationName)
        {
            CustomIdentification info;
            if (nodeDic.TryGetValue(identificationName, out info))
            {
                return info;
            }

            return null;
        }

        public Dictionary<string, CustomIdentification> GetNodeDic()
        {
            return nodeDic;
        }
    }
}