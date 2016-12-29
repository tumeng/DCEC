using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Xml;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Net; 



/// <summary>
/// 写一些在C#里面常用的但是系统本身未提供的公用函数
/// </summary>
public class PubCs
{
	public PubCs()
	{

	}

    //根据分隔符分解字符串生成数组，因为C#本身提供的Split()函数只提供分隔符是一位字符的处理，故写此函数
    //因为c#中不存在动态数组，所以这里加一个转换，先生成ArrayList对象，然后对此对象进行转换，转换成string[]对象
    //注意此函数的两个参数都不能为空，这里不加判断了，在调用之前加上判断。
    public ArrayList SplitBySeparator(string thisCharacterStr, string thisSeparator) 
    {
        int i = 0;
        int j = 0;
        string theTempStr = "";
        ArrayList theArrayList = new ArrayList();

        //如果目标字符串中没有分隔符，则返回目标字符串
        i = thisCharacterStr.IndexOf(thisSeparator);
        if (i == -1) {
            theArrayList.Add(thisCharacterStr);
            return theArrayList;
        }
        
        //循环分解判断

        j = thisSeparator.Length;

        while (i != -1) {
            theTempStr = thisCharacterStr.Substring(0, i);
            thisCharacterStr = thisCharacterStr.Substring(i + j, thisCharacterStr.Length - i - j);
            i = thisCharacterStr.IndexOf(thisSeparator);
            theArrayList.Add(theTempStr);
           
        }
        if (i == -1) {
            theArrayList.Add(thisCharacterStr);          
        }
        return theArrayList;
        
    }

    //写一个函数，实现ArrayList到string[]对象的转换，转换成别的类型的数组可以仿照这个写。
    public string[] ArrayListToString(ArrayList thisArrayList) {

        string[] theString = (string[])thisArrayList.ToArray(typeof(string));
        return theString;
    }

    //写一个函数，读取xml文件指定节点值，系统自带一个xml配置文件，只是一个简单的单层的读写。
    public string ReadFromXml(string thisFileName, string thisNodeName) {

        try
        {

            //参数是文件路径加名称，要读取的节点名称
            XmlDocument theXmlDoc = new XmlDocument();
            theXmlDoc.Load(thisFileName);

            //得到根节点
            XmlNode theRootNode = theXmlDoc.DocumentElement;

            //从根节点出发得到要找的节点值

            XmlNode theQueryNode = theRootNode.SelectSingleNode(thisNodeName);

            return theQueryNode.InnerText;

        }
        catch (Exception e)
        {
            return null;
        }
        //下面是获得属性值的方法
        //XmlElement theQueryElement = (XmlElement)theQueryNode;
        //string theRetStr = theQueryElement.GetAttribute(thisNodeName);
        //return theRetStr;
       
    }

    //在根目录下增加指定节点和节点值，如果没有增加，如果已经存在，修改值
    public string WriteToXml(string thisFileName, string thisNodeName,string thisNodeValue)
    {
        try
        {

            //参数是文件路径加名称，要读取的节点名称
            XmlDocument theXmlDoc = new XmlDocument();
            theXmlDoc.Load(thisFileName);

            //得到根节点
            XmlNode theRootNode = theXmlDoc.DocumentElement;

            //从根节点出发得到要找的节点值

            XmlNode theQueryNode = theRootNode.SelectSingleNode(thisNodeName);
            if (theQueryNode != null)
            {
                //存在该节点，修改值
                theQueryNode.InnerText = thisNodeValue;

            }
            else
            {
                //不存在，增加该节点
                XmlElement theAddElement = theXmlDoc.CreateElement(thisNodeName);
                theAddElement.InnerText = thisNodeValue;
                theRootNode.AppendChild(theAddElement);
            }

            theXmlDoc.Save(thisFileName);
            return "true";
        }
        catch  {
            return "false";
        }

    }

    //在根目录下的指定节点
    public string DeleteFromXml(string thisFileName, string thisNodeName)
    {
        try
        {

            //参数是文件路径加名称，要读取的节点名称
            XmlDocument theXmlDoc = new XmlDocument();
            theXmlDoc.Load(thisFileName);

            //得到根节点
            XmlNode theRootNode = theXmlDoc.DocumentElement;

            //从根节点出发得到要找的节点值

            XmlNode theQueryNode = theRootNode.SelectSingleNode(thisNodeName);
            if (theQueryNode != null)
            {
                //存在该节点，删除
                theRootNode.RemoveChild(theQueryNode);

            }

            theXmlDoc.Save(thisFileName);
            return "true";
        }
        catch 
        {
            return "false";
        }

    }

    //判断输入的是否满足HH:MM:SS的时间格式
    public bool CheckStrTime(string thisTimeStr) { 
        //对传入的参数进行判断处理，看是否满足上面说的时间条件，HH 00-23  MM 00-59 SS 00-59
        try
        {
            //if have two :
            int i = 0;
            int j = 0;
            string theTempStr = "";

            i = thisTimeStr.IndexOf(':');
            if (i == -1) {
                return false;
            }


            while (i != -1) {
                j = j + 1;

 
                theTempStr = thisTimeStr.Substring(0, i);
                if (j == 3) {
                    return false;
                }
                if (j == 1)
                {
                    if (theTempStr.Length == 1 || theTempStr.Length == 2)
                    {
                        if (Convert.ToInt32(theTempStr) > 23)
                        {
                            return false;
                        }

                    }
                    else
                    {
 
                            return false;
 
                    }
                }
                if (j == 2)
                {
                    if (theTempStr.Length == 1 || theTempStr.Length == 2)
                    {
                        if (Convert.ToInt32(theTempStr) > 59)
                        {
                            return false;
                        }

                    }
                    else
                    {
 
                            return false;
 
                    }
                }
                thisTimeStr = thisTimeStr.Substring(i + 1, thisTimeStr.Length - i - 1);
                i = thisTimeStr.IndexOf(':');           
            }
            if (j != 2) {
                return false;
            }

            if (i == -1) {

                if (thisTimeStr.Length == 1 || thisTimeStr.Length == 2)
                {
                    if (Convert.ToInt32(thisTimeStr) > 59)
                    {
                        return false;
                    }

                }
                else
                {
 
                        return false;
 
                }
            }
            return true;

        }
        catch { 
           return false;
        }
    }

    //校验整型数
    public bool IsNumeric(string Value)
    {
        try
        {
            int i = int.Parse(Value);
            return true;
        }
        catch
        {
            return false;
        }
    }
    //校验浮点数
    public bool IsValidNumber(String value)
    {
        int i = value.Length;
        String REGEXP_IS_VALID_DEMICAL = @"^-?(0|\d+)(\.\d+)?$";

        return new Regex(REGEXP_IS_VALID_DEMICAL).IsMatch(value);
    }


    //检验日期
    public bool IsValidDate(String value)
    {
        String REGEXP_IS_VALID_DATE = @"^(?:(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(\-|\.)(?:0?2\1(?:29))$)|(?:(?:1[6-9]|[2-9]\d)?\d{2})(\-|\.)(?:(?:(?:0?[13578]|1[02])\2(?:31))|(?:(?:0?[1,3-9]|1[0-2])\2(29|30))|(?:(?:0?[1-9])|(?:1[0-2]))\2(?:0?[1-9]|1\d|2[0-8]))$";
        return new Regex(REGEXP_IS_VALID_DATE).IsMatch(value);
    }

    public bool IsValidDateTime(String value)
    { 
        String REGEXP_IS_VALID_DATE_TIME=@"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d$";
        return new Regex(REGEXP_IS_VALID_DATE_TIME).IsMatch(value);

    }

    public bool IsValidYearMonth(String value)
    {
        String REGEXP_IS_VALID_YEAR_MONTH = @"^(\d{4}[\-\/]?((0[1-9])|(1[012])))$";
        return new Regex(REGEXP_IS_VALID_YEAR_MONTH).IsMatch(value);
    }

    //检验整数
    public bool IsPositiveInt(String value)
    {
        String REGEXP_IS_VALID_DEMICAL = @"^\d+$"; //鏁板€兼牎楠屽父閲?
        return new Regex(REGEXP_IS_VALID_DEMICAL).IsMatch(value);
    }


    //校验密码：长度8－20，必须包含字母，数字和特殊字符，不能包含空格

public bool isUserPasswd(String s)
{

    String REGEXP_IS_VALID_DEMICAL = @"^.{6,20}$";
    if (!new Regex(REGEXP_IS_VALID_DEMICAL).IsMatch(s))
    {
        return false;
        
    }


    REGEXP_IS_VALID_DEMICAL = @"\W+";
    if (!new Regex(REGEXP_IS_VALID_DEMICAL).IsMatch(s))
    {
        return false;
    }


    REGEXP_IS_VALID_DEMICAL = @"\d+";
    if (!new Regex(REGEXP_IS_VALID_DEMICAL).IsMatch(s))
    {
        return false;
    }

    REGEXP_IS_VALID_DEMICAL = @"[A-Z]+";
    if (!new Regex(REGEXP_IS_VALID_DEMICAL).IsMatch(s))
    {
        return false;
    }

    REGEXP_IS_VALID_DEMICAL = @"[a-z]+";
    if (!new Regex(REGEXP_IS_VALID_DEMICAL).IsMatch(s))
    {
        return false;
    }
   
    if (s == "")
    {
        return false;
    }

    return true;
}
    public string stringToDateTime(object value)
    { 
        if (value.ToString is DBNull)
            return "";
        else
            try{return Convert.ToDateTime(value).ToString("yyyy-MM-dd");}
        catch{return ""; }
    }
    //从文件完整路径返回包括后缀在内的文件名
    public string getFullFileName(string filePath)
    {
        //取最后一个.
        //取最后一个\\
        int lineLocation;
        if (filePath.Contains("\\"))
        {
            lineLocation = filePath.LastIndexOf("\\") + 1;
        }
        else
        {
            lineLocation = 0;
        }

        return filePath.Substring(lineLocation);
    }
    public void m_ExportExcel(System.Data.DataTable dt)
    {
        System.IO.StringWriter stringWriter = new System.IO.StringWriter();
        HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);
        DataGrid excel = new DataGrid();
        System.Web.UI.WebControls.TableItemStyle AlternatingStyle = new TableItemStyle();
        System.Web.UI.WebControls.TableItemStyle headerStyle = new TableItemStyle();
        System.Web.UI.WebControls.TableItemStyle itemStyle = new TableItemStyle();
        AlternatingStyle.BackColor = System.Drawing.Color.LightGray;
        headerStyle.BackColor = System.Drawing.Color.LightGray;
        headerStyle.Font.Bold = true;
        headerStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        itemStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center; ;
        excel.AlternatingItemStyle.MergeWith(AlternatingStyle);
        excel.HeaderStyle.MergeWith(headerStyle);
        excel.ItemStyle.MergeWith(itemStyle);
        excel.GridLines = GridLines.Both;
        excel.HeaderStyle.Font.Bold = true;
        excel.DataSource = dt.DefaultView;   //输出DataTable的内容
        excel.DataBind();
        excel.RenderControl(htmlWriter);

        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=Excel.xls");
        //HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
        //HttpContext.Current.Response.ContentType=".xls/.txt/.doc";image/JPEG;text/HTML;image/GIF;vnd.ms-excel/msword 
        HttpContext.Current.Response.ContentType = ".xls";

        HttpContext.Current.Response.Write(stringWriter.ToString());
        HttpContext.Current.Response.End();
        //string filestr = "d:\\data\\" + filePath; //filePath是文件的路径
        //int pos = filestr.LastIndexOf("\\");
        //string file = filestr.Substring(0, pos);
        //if (!Directory.Exists(file)) {
        //    Directory.CreateDirectory(file);
        //}
        //System.IO.StreamWriter sw = new StreamWriter(filestr);
        //sw.Write(stringWriter.ToString());
        //sw.Close();

    }

    public string DESEncryptMethod(string rs)
    {
        byte[] desKey = new byte[] { 0x16, 0x09, 0x14, 0x15, 0x07, 0x01, 0x05, 0x08 };
        byte[] desIV = new byte[] { 0x16, 0x09, 0x14, 0x15, 0x07, 0x01, 0x05, 0x08 };

        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        try
        {
            byte[] inputByteArray = Encoding.Default.GetBytes(rs);
            //byte[] inputByteArray=Encoding.Unicode.GetBytes(rs);

            des.Key = desKey;  // ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = desIV;   //ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(),CryptoStreamMode.Write);
            //Write the byte array into the crypto stream
            //(It will end up in the memory stream)
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            //Get the data back from the memory stream, and into a string
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                //Format as hex
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }
        catch
        {
            return rs;
        }
        finally
        {
            des = null;
        }
    }

    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="rs"></param>
    /// <returns></returns>
    public string DESDecryptMethod(string rs)
    {
        byte[] desKey = new byte[] { 0x16, 0x09, 0x14, 0x15, 0x07, 0x01, 0x05, 0x08 };
        byte[] desIV = new byte[] { 0x16, 0x09, 0x14, 0x15, 0x07, 0x01, 0x05, 0x08 };

        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        try
        {
            //Put the input string into the byte array
            byte[] inputByteArray = new byte[rs.Length / 2];
            for (int x = 0; x < rs.Length / 2; x++)
            {
                int i = (Convert.ToInt32(rs.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = desKey;   //ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = desIV;    //ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            //Flush the data through the crypto stream into the memory stream
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            //Get the decrypted data back from the memory stream
            StringBuilder ret = new StringBuilder();

            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }
        catch
        {
            return rs;
        }
        finally
        {
            des = null;
        }
    }
    /// <summary>
    /// 获取密钥
    /// </summary>
    private string Key
    {
        get { return @")O[NB]6,YF}+efcaj{+oESb9d8>Z'e9M"; }
    }

    /// <summary>
    /// 获取向量
    /// </summary>
    private string IV
    {
        get { return @"L+\~f4,Ir)b$=pkf"; }
    }

    /// <summary>
    /// AES加密
    /// </summary>
    /// <param name="plainStr">明文字符串</param>
    /// <returns>密文</returns>
    public string AESEncrypt(string plainStr)
    {
        byte[] bKey = Encoding.UTF8.GetBytes(Key);
        byte[] bIV = Encoding.UTF8.GetBytes(IV);
        byte[] byteArray = Encoding.UTF8.GetBytes(plainStr);

        string encrypt = null;
        Rijndael aes = Rijndael.Create();
        using (MemoryStream mStream = new MemoryStream())
        {
            using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateEncryptor(bKey, bIV), CryptoStreamMode.Write))
            {
                cStream.Write(byteArray, 0, byteArray.Length);
                cStream.FlushFinalBlock();
                encrypt = Convert.ToBase64String(mStream.ToArray());
            }
        }
        aes.Clear();
        return encrypt;
    }
    /// <summary>
    /// AES解密
    /// </summary>
    /// <param name="encryptStr">密文字符串</param>
    /// <returns>明文</returns>
    public string AESDecrypt(string encryptStr)
    {
        byte[] bKey = Encoding.UTF8.GetBytes(Key);
        byte[] bIV = Encoding.UTF8.GetBytes(IV);
        byte[] byteArray = Convert.FromBase64String(encryptStr);

        string decrypt = null;
        Rijndael aes = Rijndael.Create();
        using (MemoryStream mStream = new MemoryStream())
        {
            using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateDecryptor(bKey, bIV), CryptoStreamMode.Write))
            {
                cStream.Write(byteArray, 0, byteArray.Length);
                cStream.FlushFinalBlock();
                decrypt = Encoding.UTF8.GetString(mStream.ToArray());
            }
        }
        aes.Clear();
        return decrypt;
    }
    //获取客户端ip
    public string GetClientIp()
    {
        try
        {
            return System.Web.HttpContext.Current.Request.UserHostAddress;
        }
        catch
        {
            return "";
        }
    }

    //获取客户端机器名称
    public string GetClientName()
    {
        try
        {
            //IPHostEntry hostInfo = Dns.GetHostByAddress(GetClientIp());
            //return hostInfo.HostName;
            return System.Net.Dns.GetHostName();
        }
        catch
        {
            return "";
        }
    }
}
