using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rmes.Pub.Data;

//放一些逻辑里公用的函数 2016.9.8 徐莹
public static class PubLogic
{
    //判断计划是否已上线
    public static bool isEngOnline(string planCode, string so, string oldPart, string newPart, string locationCode, string plineCode)
    {
        dataConn dc = new dataConn();
        string sql = "select IS_ENGONLINE('" + planCode + "','" + so + "','" + oldPart + "','" + newPart + "','" + locationCode + "','" + plineCode + "') from dual";
        string retVal = dc.GetValue(sql);
        dc.CloseConn();

        if (retVal == "0" || retVal == "3")
            return false;
        else
            return true;
    }
    //判断是否是供应商
    public static bool isZdgys(string planCode, string so, string oldPart, string plineCode)
    {
        dataConn dc = new dataConn();
        string sql = "select IS_ZDGYS('" + planCode + "','" + so + "','" + oldPart + "','" + plineCode + "') from dual";
        string retVal = dc.GetValue(sql);
        dc.CloseConn();

        if (retVal == "1")
            return true;
        else
            return false;
    }
}
