using System;
using System.Xml;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting;
using System.Collections;

namespace bt
{
    public class BtTools
    {
        public const string SELECT = "select";

        public const string SEQUENCE = "sequence";

        public const string RANDOM = "random";

        public const string CONDITION = "condition";

        public const string ACTION = "action";

        public const string RANDOM_VALUE = "randomValue";

        public const string LOOP = "loop";

        public static BtRoot<T, U, V> Create<T, U, V>(string _str, string _assemblyName)
        {
            XmlDocument xmlDoc = new XmlDocument();

            string xmlStr = XmlFix(_str);

            if (string.IsNullOrEmpty(xmlStr))
            {
                throw new Exception("xml error!");
            }

            xmlDoc.LoadXml(xmlStr);

            INode<T, U, V> rootNode = ParseNode<T, U, V>(xmlDoc.DocumentElement, _assemblyName);

            BtRoot<T, U, V> result = new BtRoot<T, U, V>();

            result.Init(rootNode);

            return result;
        }

        private static INode<T, U, V> ParseNode<T, U, V>(XmlNode _node, string _assemblyName)
        {
            INode<T, U, V> node;

            bool isCompositeNode = false;

            bool isRandomNode = false;

            switch (_node.Name)
            {
                case SELECT:

                    node = new SelectNode<T, U, V>();

                    isCompositeNode = true;

                    break;

                case SEQUENCE:

                    node = new SequenceNode<T, U, V>();

                    isCompositeNode = true;

                    break;

                case RANDOM:

                    node = new RandomNode<T, U, V>();

                    isCompositeNode = true;

                    isRandomNode = true;

                    break;

                case CONDITION:

                    node = GetNode<T, U, V>(_node, _assemblyName);

                    break;

                case ACTION:

                    node = GetNode<T, U, V>(_node, _assemblyName);

                    break;

                default:

                    throw new Exception("node type error:" + _node.Name);
            }

            if (isCompositeNode && _node.HasChildNodes)
            {
                List<INode<T, U, V>> nodeList = new List<INode<T, U, V>>();

                List<int> randomList = null;

                if (isRandomNode)
                {
                    randomList = new List<int>();
                }

                XmlNodeList children = _node.ChildNodes;

                foreach (XmlNode child in children)
                {
                    if (child.NodeType == XmlNodeType.Element)
                    {
                        INode<T, U, V> childNode = ParseNode<T, U, V>(child, _assemblyName);

                        nodeList.Add(childNode);

                        if (isRandomNode)
                        {
                            XmlAttribute att = child.Attributes[RANDOM_VALUE];

                            if (att == null)
                            {
                                throw new Exception(RANDOM_VALUE + " att can not be found!");
                            }

                            randomList.Add(int.Parse(att.InnerXml));
                        }
                    }
                }

                XmlAttribute loopAtt = _node.Attributes[LOOP];

                bool isLoop = loopAtt != null && bool.Parse(loopAtt.InnerText);

                (node as CompositeNode<T, U, V>).Init(nodeList, isLoop);

                if (isRandomNode)
                {
                    (node as RandomNode<T, U, V>).InitRandomValue(randomList);
                }
            }

            return node;
        }

        private static INode<T, U, V> GetNode<T, U, V>(XmlNode _node, string _assemblyName)
        {
            XmlAttribute typeAtt = _node.Attributes["type"];

            ObjectHandle oh = Activator.CreateInstance(_assemblyName, typeAtt.InnerText);

            INode<T, U, V> actionNode = oh.Unwrap() as INode<T, U, V>;

            IEnumerator ie = _node.Attributes.GetEnumerator();

            while (ie.MoveNext())
            {
                XmlAttribute xa = ie.Current as XmlAttribute;

                if (xa.Name != "type")
                {
                    FieldInfo fi = actionNode.GetType().GetField(xa.Name, BindingFlags.NonPublic | BindingFlags.Instance);

                    if (fi != null)
                    {
                        SetData(fi, actionNode, xa.Value);
                    }
                }
            }

            return actionNode;
        }

        private static void SetData<T, U, V>(FieldInfo _info, INode<T, U, V> _obj, string _data)
        {
            switch (_info.FieldType.Name)
            {
                case "Int32":

                    _info.SetValue(_obj, int.Parse(_data));

                    break;

                case "String":

                    _info.SetValue(_obj, _data);

                    break;

                case "Boolean":

                    _info.SetValue(_obj, _data == "1" ? true : false);

                    break;

                case "Single":

                    _info.SetValue(_obj, float.Parse(_data));

                    break;

                case "Double":

                    _info.SetValue(_obj, double.Parse(_data));

                    break;

                case "Int16":

                    _info.SetValue(_obj, short.Parse(_data));

                    break;

                default:

                    throw new Exception("Node的属性不支持反射  setData:" + _info.Name + "   " + _info.FieldType.Name + "   " + _data);
            }
        }

        private static string XmlFix(string _str)
        {
            int index = _str.IndexOf("<");

            if (index == -1)
            {
                return string.Empty;
            }
            else
            {
                return _str.Substring(index, _str.Length - index);
            }
        }
    }
}
