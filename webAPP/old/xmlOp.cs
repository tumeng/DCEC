using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
using System.Windows.Forms;

/// <summary>
/// Summary description for xmlOp
/// </summary>
public class xmlOp
{
    private XmlDocument xmlDoc;
    private XmlNode theRootNode;
    private DataTable dt;

    public xmlOp()
    {
        //��ȡһ��XML�ļ�
        xmlDoc = new XmlDocument();
        string xmlLoc = System.Web.HttpContext.Current.Server.MapPath("~/RmesConfig.xml");
        xmlDoc.Load(xmlLoc);

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

            xmlDoc.Save(Application.StartupPath.ToString() + "\\XMLFile1.xml");
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

            xmlDoc.Save(Application.StartupPath.ToString() + "\\XMLFile1.xml");
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
            xmlDoc.Save(Application.StartupPath.ToString() + "\\XMLFile1.xml");
            //xnChild.RemoveAll();
            return true;
        }
        catch
        {
            return false;
        }
    }

    //��ȡָ���ڵ�������ӽڵ�
    public DataTable traverseNode(string elemName)
    {
        XmlNodeList nodelist;

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
            DataRow dr = dt.NewRow();
            dr[0] = element.Name.ToString();
            dr[1] = element.InnerText.ToString();
            dt.Rows.Add(dr);
        }

        return dt;
    }
}
