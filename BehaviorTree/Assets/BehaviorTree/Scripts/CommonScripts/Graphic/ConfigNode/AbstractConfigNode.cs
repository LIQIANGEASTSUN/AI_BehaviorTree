using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphicTree
{

    public abstract class AbstractConfigNode
    {
        private Dictionary<string, CustomIdentification> nodeDic = new Dictionary<string, CustomIdentification>();

        public AbstractConfigNode()
        {
            Init();
        }

        public virtual void Init()
        {
            PrimaryNode();
        }

        protected void Config<T>(string name) where T : AbstractNode, new()
        {
            CustomIdentification customIdentification = new CustomIdentification(name, typeof(T));
            nodeDic[customIdentification.IdentificationName] = customIdentification;
        }

        protected void Config<T>(string name, int nodeType) where T : AbstractNode, new()
        {
            CustomIdentification customIdentification = new CustomIdentification(name, typeof(T), nodeType);
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

        protected abstract void PrimaryNode();

    }

}


/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphicTree
{

    public abstract class AbstractConfigNode
    {
        private Dictionary<string, ICustomIdentification<AbstractNode>> nodeDic = new Dictionary<string, ICustomIdentification<AbstractNode>>();

        public AbstractConfigNode()
        {
            Init();
        }

        public virtual void Init()
        {
            PrimaryNode();
        }

        protected void Config<T>(string name) where T : AbstractNode, new()
        {
            ICustomIdentification<AbstractNode> customIdentification = new CustomIdentification<T>(name, typeof(T));
            nodeDic[customIdentification.IdentificationName] = customIdentification;
        }

        protected void Config<T>(string name, int nodeType) where T : AbstractNode, new()
        {
            ICustomIdentification<AbstractNode> customIdentification = new CustomIdentification<T>(name, typeof(T), nodeType);
            nodeDic[customIdentification.IdentificationName] = customIdentification;
        }

        public AbstractNode GetNode(string identificationName)
        {
            ICustomIdentification<AbstractNode> info = GetIdentification(identificationName);
            if (null == info)
            {
                UnityEngine.Debug.LogError(identificationName);
            }
            AbstractNode obj = info.Create();
            return obj;
        }

        public ICustomIdentification<AbstractNode> GetIdentification(string identificationName)
        {
            ICustomIdentification<AbstractNode> info;
            if (nodeDic.TryGetValue(identificationName, out info))
            {
                return info;
            }

            return null;
        }

        public Dictionary<string, ICustomIdentification<AbstractNode>> GetNodeDic()
        {
            return nodeDic;
        }

        protected abstract void PrimaryNode();

    }

}
*/
