using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Collections;
using System.Text;

namespace Rmes.Public
{
    //public class PublicClass
    //{
    //    public static XmlNode CreateNode(int theIndent, XmlNode theParent, string theNodeName, String theNodeValue, XmlDocument doc)
    //    {
    //        XmlNode theNewNode;
    //        StringBuilder sb = new StringBuilder();
    //        theParent.AppendChild(theParent.OwnerDocument.CreateTextNode(sb.Append('\t', theIndent).ToString()));
    //        theNewNode = theParent.OwnerDocument.CreateElement(theNodeName);

    //        XmlAttribute attr = doc.CreateAttribute("value");
    //        attr.Value = theNodeValue;
    //        theNewNode.Attributes.Append(attr);

    //        theParent.AppendChild(theNewNode);
    //        theParent.AppendChild(theParent.OwnerDocument.CreateTextNode("\r\n"));
    //        return theParent;
    //    }
    //    public static XmlNode CreateNode2(int theIndent, XmlNode theParent, string theNodeName, String theNodeValue, string theNodeValue2, XmlDocument doc)
    //    {
    //        XmlNode theNewNode;
    //        StringBuilder sb = new StringBuilder();
    //        theParent.AppendChild(theParent.OwnerDocument.CreateTextNode(sb.Append('\t', theIndent).ToString()));
    //        theNewNode = theParent.OwnerDocument.CreateElement(theNodeName);

    //        XmlAttribute attr = doc.CreateAttribute("name");
    //        attr.Value = theNodeValue2;
    //        theNewNode.Attributes.Append(attr);

    //        XmlAttribute attr1 = doc.CreateAttribute("value");
    //        attr1.Value = theNodeValue;
    //        theNewNode.Attributes.Append(attr1);

    //        theParent.AppendChild(theNewNode);
    //        theParent.AppendChild(theParent.OwnerDocument.CreateTextNode("\r\n"));
    //        return theParent;
    //    }

    //    public static int XCopyFile(string from_file, string to_file)
    //    {
    //        try
    //        {
    //            if (File.Exists(from_file))
    //            {
    //                // 是文件
    //                File.Copy(from_file, to_file);
    //                return 1;
    //            }
    //            else if (Directory.Exists(from_file))
    //            {
    //                // 是文件夹
    //                int count1 = 1;
    //                //if (!from_file.EndsWith("\\"))
    //                //{
    //                //    from_file = from_file + "\\";
    //                //}
    //                //if (!to_file.EndsWith("\\"))
    //                //{
    //                //    to_file = to_file + "\\";
    //                //}
    //                if (!Directory.Exists(to_file))
    //                {
    //                    Directory.CreateDirectory(to_file);
    //                }
    //                count1 = count1 + CopyFiles(from_file, to_file);
    //                return count1;
    //            }
    //            else
    //            {
    //                return 0;
    //            }
    //            //return 0;
    //        }
    //        catch
    //        {
    //            return 0;
    //        }

    //    }

    //    public static int CopyFiles(string srcdir, string desdir)
    //    {
    //        int count1 = 0;
    //        //string folderName = srcdir.Substring(srcdir.LastIndexOf("\\") + 1);

    //        //string desfolderdir = desdir + "\\" + folderName;

    //        //if (desdir.LastIndexOf("\\") == (desdir.Length - 1))
    //        //{
    //        //    desfolderdir = desdir + folderName;
    //        //}
    //        string desfolderdir = desdir;
    //        string[] filenames = Directory.GetFileSystemEntries(srcdir);

    //        foreach (string file in filenames)// 遍历所有的文件和目录
    //        {
    //            if (Directory.Exists(file))// 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
    //            {
    //                //是文件夹
    //                string currentdir = desfolderdir + "\\" + file.Substring(file.LastIndexOf("\\") + 1);
    //                if (!Directory.Exists(currentdir))
    //                {
    //                    Directory.CreateDirectory(currentdir);
    //                }

    //                CopyFiles(file, desfolderdir);
    //            }

    //            else // 否则直接copy文件
    //            {
    //                string srcfileName = file.Substring(file.LastIndexOf("\\") + 1);

    //                srcfileName = desfolderdir + "\\" + srcfileName;


    //                if (!Directory.Exists(desfolderdir))
    //                {
    //                    Directory.CreateDirectory(desfolderdir);
    //                }
    //                File.Copy(file, srcfileName);
    //                count1++;
    //            }
    //        }//foreach 
    //        return count1;
    //    }
    //}
}
