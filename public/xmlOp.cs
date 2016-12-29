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
            //��ȡһ��XML�ļ�
            xmlDoc = new XmlDocument();
            xmlLoc = thisXmlLoc;
            xmlDoc.Load(thisXmlLoc);

            //���ڵ�
            theRootNode = xmlDoc.DocumentElement;

            dt = new DataTable();
            dt.Columns.Add();
            dt.Columns.Add();
        }

        //��ȡһ���ڵ�
        public string readOneNode(string nodeName)
        {
            XmlNode theQueryNode = theRootNode.SelectSingleNode(nodeName);
            return theQueryNode.InnerText;
        }

        //����һ���ڵ�
        public bool addOneNode(string parNodeName, string nodeName, string nodeValue)
        {
            try
            {
                XmlElement newNode = xmlDoc.CreateElement(nodeName);

                if (parNodeName == "")
                {
                    //���ڵ�������һ���ڵ�
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
        //�޸�һ���ڵ�
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

        //ɾ��һ���ڵ�
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

        //��ȡָ���ڵ�������ӽڵ�
        public Hashtable traverseNode(string elemName)
        {
            XmlNodeList nodelist;
            Hashtable table = new Hashtable();
            //���ڵ�
            if (elemName == "")
            {
                //�õ�����ڵ��б�
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