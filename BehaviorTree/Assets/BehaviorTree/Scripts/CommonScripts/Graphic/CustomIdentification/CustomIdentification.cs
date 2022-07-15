using System;
using System.Collections.Generic;
using System;

namespace GraphicTree
{
    public class CustomIdentification
    {
        private List<string> _defaultParameterList = new List<string>();
        private Type type;

        public CustomIdentification(string name, Type t)
        {
            Name = name;
            type = t;
            IdentificationName = GetIdentification();
            SetNodeType(t);
        }

        public CustomIdentification(string name, Type t, int nodeType)
        {
            Name = name;
            type = t;
            IdentificationName = GetIdentification();
            NodeType = nodeType;
        }

        public string GetIdentification()
        {
            return type.Name;
        }

        public static string GetIdentification<T>()
        {
            return typeof(T).Name;
        }

        public object Create()
        {
            object o = Activator.CreateInstance(type);
            return o;
        }

        public string IdentificationName
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int NodeType
        {
            get;
            set;
        }

        private void SetNodeType(Type t)
        {
            NodeType = (Create() as AbstractNode).NodeType();
        }

        public List<string> DefaultParameterList
        {
            get { return _defaultParameterList; }
            set { _defaultParameterList = value; }
        }
    }
}


/*
 using System;
using System.Collections.Generic;

namespace GraphicTree
{
    public interface ICustomIdentification<out T> where T : AbstractNode
    {
        T Create();

        string IdentificationName
        {
            get;
            set;
        }

        string Name
        {
            get;
            set;
        }

        int NodeType
        {
            get;
            set;
        }

        List<string> DefaultParameterList
        {
            get;
            set;
        }
    }

    public class CustomIdentification<T> : ICustomIdentification<T> where T : AbstractNode, new()
    {
        private List<string> _defaultParameterList = new List<string>();

        public CustomIdentification(string name, Type t)
        {
            Name = name;
            IdentificationName = GetIdentification();
            SetNodeType(t);
        }

        public CustomIdentification(string name, Type t, int nodeType)
        {
            Name = name;
            IdentificationName = GetIdentification();
            NodeType = nodeType;
        }

        public static string GetIdentification()
        {
            return typeof(T).Name;
        }

        public T Create()
        {
            return new T();
        }

        public string IdentificationName
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int NodeType
        {
            get;
            set;
        }

        private void SetNodeType(Type t)
        {
            NodeType = Create().NodeType();
        }

        public List<string> DefaultParameterList
        {
            get { return _defaultParameterList; }
            set { _defaultParameterList = value; }
        }
    }
}
*/
