using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Collections;
namespace Rmes.Public.Base
{
    /// <summary>
    /// Summary description for xmlOp
    /// </summary>
    public class xmlOp
    {
        private XmlDocument xmlDoc;
        private XmlNode theRootNode;
        private DataTable dt;
        private string xmlLoc;

        public xmlOp(string thisXmlLoc)
        {
            //读取一个XML文件
            xmlDoc = new XmlDocument();
            xmlLoc = thisXmlLoc;
            xmlDoc.Load(thisXmlLoc);

            //根节点
            theRootNode = xmlDoc.DocumentElement;

            dt = new DataTable();
            dt.Columns.Add();
            dt.Columns.Add();
        }

        //读取一个节点
        public string readOneNode(string nodeName)
        {
            XmlNode theQueryNode = theRootNode.SelectSingleNode(nodeName);
            return theQueryNode.InnerText;
        }

        //新增一个节点
        public bool addOneNode(string parNodeName, string nodeName, string nodeValue)
        {
            try
            {
                XmlElement newNode = xmlDoc.CreateElement(nodeName);

                if (parNodeName == "")
                {
                    //根节点下新增一个节点
                    theRootNode.AppendChild(newNode);
                }
                else
                {
                    newNode.InnerText = nodeValue;
                    XmlElement elem = xmlDoc.GetElementsByTagName(parNodeName)[0] as XmlElement;
                    elem.AppendChild(newNode);
                }

                xmlDoc.Save(xmlLoc);
                return true;
            }
            catch
            {
                return false;
            }
        }
        //修改一个节点
        public bool modOneNode(string parNodeName, string nodeName, string nodeValue)
        {
            try
            {
                XmlElement xn = xmlDoc.GetElementsByTagName(parNodeName)[0] as XmlElement;
                XmlElement xnChild = xn.GetElementsByTagName(nodeName)[0] as XmlElement;
                xnChild.InnerText = nodeValue;

                xmlDoc.Save(xmlLoc);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //删除一个节点
        public bool delOneNode(string nodeName)
        {
            try
            {
                XmlElement xnChild = xmlDoc.GetElementsByTagName(nodeName)[0] as XmlElement;
                theRootNode.RemoveChild(xnChild);
                xmlDoc.Save(xmlLoc);
                //xnChild.RemoveAll();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //获取指定节点的所有子节点
        public Hashtable traverseNode(string elemName)
        {
            XmlNodeList nodelist;
            Hashtable table = new Hashtable();
            //根节点
            if (elemName == "")
            {
                //得到顶层节点列表
                nodelist = xmlDoc.DocumentElement.ChildNodes;
            }
            else
            {
                XmlElement elem = xmlDoc.GetElementsByTagName(elemName)[0] as XmlElement;
                nodelist = elem.ChildNodes;
            }

            foreach (XmlElement element in nodelist)
            {
                table[element.Name.ToString()] = element.InnerText.ToString();
            }

            return table;
        }
    }
}