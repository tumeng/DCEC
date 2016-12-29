using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Rmes.Pub.Data
{
    /// <summary>
    /// 用户管理
    /// </summary>
    /// 
    [Serializable]
    public class userManager
    {
        public string theUserCode = "";  //用户代码
        public string theUserName = "";  //用户名称
        public string theCompanyCode="";  //公司代码
        public string theSessionCode=""; //会话号，数据库中按照一定规则生产的
        public Boolean  theLoginFlag=false ; //登录标识
        public string theProgValue = ""; //程序内容
        public string thePlineCode = ""; //生产线代码，针对具体业务操作，这个应该在每个会话中是固定的
        public string theProgCode = "";  //程序代码
        public string theProgName = "";  //程序名称

        //增加公司名称和生产线名称  20071219
        public string theCompanyName = "";
        public string thePlineName = "";

        //增加用户唯一ID
        public string theUserId = "";    //用户唯一ID

        public userManager(string thisSessionCode)
        {
            //
            // 构造函数
            //
            theSessionCode = thisSessionCode;
        }

        public void setUserCode(string thisUserCode) {
            theUserCode = thisUserCode;
        }
        public string getUserCode() {
            return theUserCode;
        }

        public void setUserName(string thisUserName)
        {
            theUserName = thisUserName;
        }
        public string getUserName()
        {
            return theUserName;
        }

        
        public void setCompanyCode(string thisCompanyCode)
        {
            theCompanyCode = thisCompanyCode;
        }
        
        public string getCompanyCode()
        {
            return theCompanyCode;
        }

        /*
        public void setSessionCode(string thisSessionCode)
        {
            theSessionCode = thisSessionCode;
        }
         */
        public string getSessionCode()
        {
            return theSessionCode;
        }

        public void setLoginFlag(Boolean  thisLoginFlag)
        {
            theLoginFlag  = thisLoginFlag;
        }
        public Boolean  getLoginflag()
        {
            return theLoginFlag;
        }

        public void setProgValue(string thisProgValue)
        {
            theProgValue  = thisProgValue;
        }
        public string getProgVlaue()
        {
            return theProgValue;
        }
        public void setPlineCode(string thisPlineCode)
        {
            thePlineCode  = thisPlineCode ;
        }
        public string getPlineCode()
        {
            return thePlineCode;
        }
        public void setProgCode(string thisProgCode)
        {
            theProgCode = thisProgCode;
        }
        public string getProgCode()
        {
            return theProgCode;
        }
        public void setProgName(string thisProgName)
        {
            theProgName = thisProgName;
        }
        public string getProgName()
        {
            return theProgName;
        }
        public void setPlineName(string thisPlineName)
        {
            thePlineName = thisPlineName;
        }
        public string getPlineName()
        {
            return thePlineName;
        }
        public void setCompanyName(string thisCompanyName)
        {
            theCompanyName = thisCompanyName;
        }
        public string getCompanyName()
        {
            return theCompanyName;
        }

        public void setUserId(string thisUserId)
        {
            theUserId = thisUserId;
        }
        public string getUserId()
        {
            return theUserId;
        }
    }
}
