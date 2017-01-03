﻿using System;
using System.Xml;
using System.Collections.Generic;

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

        public static BtRoot<T, U> Create<T, U>(string _str, Func<XmlNode, ConditionNode<T, U>> _conditionNodeCallBack, Func<XmlNode, ActionNode<T, U>> _actionNodeCallBack, Random _random)
        {
            XmlDocument xmlDoc = new XmlDocument();

            string xmlStr = XmlFix(_str);

            if (string.IsNullOrEmpty(xmlStr))
            {
                throw new Exception("xml error!");
            }

            xmlDoc.LoadXml(xmlStr);

            INode<T, U> rootNode = ParseNode(xmlDoc.DocumentElement, _conditionNodeCallBack, _actionNodeCallBack, _random);

            BtRoot<T, U> result = new BtRoot<T, U>();

            result.Init(rootNode);

            return result;
        }

        private static INode<T, U> ParseNode<T, U>(XmlNode _node, Func<XmlNode, ConditionNode<T, U>> _conditionNodeCallBack, Func<XmlNode, ActionNode<T, U>> _actionNodeCallBack, Random _random)
        {
            INode<T, U> node;

            bool isCompositeNode;

            bool isRandomNode;

            switch (_node.Name)
            {
                case SELECT:

                    node = new SelectNode<T, U>();

                    isCompositeNode = true;

                    isRandomNode = false;

                    break;

                case SEQUENCE:

                    node = new SequenceNode<T, U>();

                    isCompositeNode = true;

                    isRandomNode = false;

                    break;

                case RANDOM:

                    node = new RandomNode<T, U>();

                    isCompositeNode = true;

                    isRandomNode = true;

                    break;

                case CONDITION:

                    node = _conditionNodeCallBack(_node);

                    isCompositeNode = false;

                    isRandomNode = false;

                    break;

                case ACTION:

                    node = _actionNodeCallBack(_node);

                    isCompositeNode = false;

                    isRandomNode = false;

                    break;

                default:

                    throw new Exception("node type error:" + _node.Name);
            }

            if (isCompositeNode)
            {
                if (_node.HasChildNodes)
                {
                    List<INode<T, U>> nodeList = new List<INode<T, U>>();

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
                            INode<T, U> childNode = ParseNode(child, _conditionNodeCallBack, _actionNodeCallBack, _random);

                            nodeList.Add(childNode);

                            if (isRandomNode)
                            {
                                XmlAttribute att = child.Attributes[RANDOM_VALUE];

                                if (att == null)
                                {
                                    throw new Exception("randomValue att can not be found!");
                                }

                                randomList.Add(int.Parse(att.InnerXml));
                            }
                        }
                    }

                    (node as CompositeNode<T, U>).Init(nodeList);

                    if (isRandomNode)
                    {
                        (node as RandomNode<T, U>).InitRandomValue(_random, randomList);
                    }
                }
                else
                {
                    throw new Exception("CompositeNode has no child!");
                }
            }

            return node;
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