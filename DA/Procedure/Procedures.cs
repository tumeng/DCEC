using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using System.Data;

namespace Rmes.DA.Procedures
{
    [Procedure.SPName("MW_HANDLE_LOGIN")] //如果类名称和存储过程名称一致，该行可以省略
    public class MW_HANDLE_LOGIN //
    {
        [Procedure.Direction(ParameterDirection.Input)] // Input参数可以省略不写该行，属性名称需和存储过程参数名称一致
        public string THECOMPANYCODE1 { get; set; }

        public string THEUSERID1 { get; set; }
        public string THEPASSWORD1 { get; set; }
        public string THECLIENTIP1 { get; set; }
        public string THEPLINECODE1 { get; set; }

        [Procedure.Direction(ParameterDirection.InputOutput)] //InputOutput/Output/Return必须在这里写明
        public string THERETSTR1 { get; set; }
        [Procedure.Direction(ParameterDirection.InputOutput)]
        public string THERETSESSIONCODE1 { get; set; }
        [Procedure.Direction(ParameterDirection.InputOutput)]
        public string THERETUSERNAME1 { get; set; }
        [Procedure.Direction(ParameterDirection.InputOutput)]
        public string THERETUSERCODE1 { get; set; }
    }
    [Procedure.SPName("VEPS_SDGX")]
    public class VEPS_SDGX
    {
        public string SO1 { get; set; }
    }
    [Procedure.SPName("MW_MODIFY_USER_PLINE")]
    public class MW_MODIFY_USER_PLINE
    {
        public string THEFUNCTION1 { get; set; }
        public string THECOMPANYCODE1 { get; set; }
        public string THEUSERID1 { get; set; }
        public string THEPLINECODE1 { get; set; }
    }

    public class MW_MODIFY_USER_ROLE
    {
        public string THEFUNCTION1 { get; set; }
        public string THECOMPANYCODE1 { get; set; }
        public string THEUSERID1 { get; set; }
        public string THEROLECODE1 { get; set; }
    }

    public class MW_MODIFY_USER_PROGRAM
    {
        public string THEFUNCTION1 { get; set; }
        public string THECOMPANYCODE1 { get; set; }
        public string THEPROGRAMCODE1 { get; set; }
        public string THEUSERID1 { get; set; }
        public string THEPLINECODE1 { get; set; }
    }

    public class MW_MODIFY_ROLE_PROGRAM
    {
        public string THEFUNCTION1 { get; set; }
        public string THECOMPANYCODE1 { get; set; }
        public string THEPROGRAMCODE1 { get; set; }
        public string THEROLECODE1 { get; set; }
        public string THEPLINECODE1 { get; set; }
    }

    public class MW_CHECK_USERRIGHT
    {
        public string THECOMPANYCODE1 { get; set; }
        public string THEUSERID1 { get; set; }
        public string THECLIENTIP1 { get; set; }
        public string THEPROGRAMVALUE1 { get; set; }

        [Procedure.Direction(ParameterDirection.InputOutput)]
        public string THERETSTR1 { get; set; }

        [Procedure.Direction(ParameterDirection.InputOutput)]
        public string THERETPROGRAMCODE1 { get; set; }

        [Procedure.Direction(ParameterDirection.InputOutput)]
        public string THERETPROGRAMNAME1 { get; set; }
    }

    public class MW_HANDLE_LOGOUT
    {
        public string THECOMPANYCODE1 { get; set; }
        public string THESESSIONCODE1 { get; set; }
    }

    [Procedure.SPName("MW_HANDLE_RELOGIN")]
    public class MW_HANDLE_RELOGIN
    {
        public string THECOMPANYCODE1 { get; set; }
        public string THEUSERID1 { get; set; }
        public string THEPASSWORD1 { get; set; }
        public string THECLIENTIP1 { get; set; }
        public string THESESSIONCODE1 { get; set; }
        public string THEPLINECODE1 { get; set; }

        [Procedure.Direction(ParameterDirection.InputOutput)]
        public string THERETSTR1 { get; set; }

        [Procedure.Direction(ParameterDirection.InputOutput)]
        public string THERETSESSIONCODE1 { get; set; }

        [Procedure.Direction(ParameterDirection.InputOutput)]
        public string THERETUSERNAME1 { get; set; }

        [Procedure.Direction(ParameterDirection.InputOutput)]
        public string THERETUSERCODE1 { get; set; }
    }

    [Procedure.SPName("PL_CREATE_PLAN_ISSUE")]
    public class PL_CREATE_PLAN_ISSUE 
    {
        //public string THECOMPANYCODE { get; set; }
        //public string THEUSERCODE { get; set; }
        //public string THEPLANS { get; set; }
    }
    [Procedure.SPName("MS_MODIFY_OVER_MAT")]
    public class MS_MODIFY_OVER_MAT
    {
        public string FUNC1 { get; set; }
        public string MATERIALCODE1 { get; set; }
        public string LINESIDENUM1 { get; set; }
        public string GZDD1 { get; set; }
        public string YHDM1 { get; set; }
        public string QADSITE1 { get; set; }
    }

    [Procedure.SPName("MW_GHTM_TH")]
    public class MW_GHTM_TH
    {
        public string THEOLDLSH1 { get; set; }
        public string THENEWLSH1 { get; set; }
        public string THEGZDD1 { get; set; }
        public string THEYGDM1 { get; set; }
        [Procedure.Direction(ParameterDirection.InputOutput)]
        public string THERETSTR1 { get; set; }
    }
    [Procedure.SPName("MW_GHTM_TH_DD")]
    public class MW_GHTM_TH_DD
    {
        public string THELSHA { get; set; }
        public string THEMIDLSH { get; set; }
        public string THELSHB { get; set; }
        public string THEGZDD1 { get; set; }
        public string THEYGDM1 { get; set; }
        [Procedure.Direction(ParameterDirection.InputOutput)]
        public string THERETSTR1 { get; set; }
    }
    [Procedure.SPName("MW_CREATE_GHTMFJB")]
    public class MW_CREATE_GHTMFJB
    {
        public string GZDD1 { get; set; }
        public string MACHINENAME1 { get; set; }
        public string RQSJ1 { get; set; }

    }


    [Procedure.SPName("MW_CREATE_RSTZZPQD_NEW")]
    public class MW_CREATE_RSTZZPQD_NEW
    {
        public string FUNC1 { get; set; }
        public DateTime RQ1 { get; set; }
        public DateTime RQ2 { get; set; }
        public string BC1 { get; set; }
        public string ZD1 { get; set; }
        public string GZDD1 { get; set; }
        public string MACHINENAME1 { get; set; }

    }

    [Procedure.SPName("PL_CREATE_REPORT")]
    public class PL_CREATE_REPORT
    {
        public string SO1 { get; set; }
        public string YEAR1 { get; set; }
        public string MONTH1 { get; set; }
        public string GZDD1 { get; set; }
        public string FROMRQ1 { get; set; }
        public string TORQ1 { get; set; }
        public string MONTHDATE1 { get; set; }
        [Procedure.Direction(ParameterDirection.InputOutput)]
        public string OUTSTR1 { get; set; }

    }
    [Procedure.SPName("MW_RST_MonthBB")]
    public class MW_RST_MonthBB
    {
        public string ZDMC1 { get; set; }
        public string GZRQ1 { get; set; }
        public string GZRQ2 { get; set; }
        public string MACHINENAME1 { get; set; }

    }
    [Procedure.SPName("PL_QUERY_BOMZJTS3")]
    public class PL_QUERY_BOMZJTS3
    {
        public string SO1 { get; set; }
        public string ZDDM1 { get; set; }
        public string GZDD1 { get; set; }
        public string FDJXL1 { get; set; }
        public string JHDM1 { get; set; }
    }
    [Procedure.SPName("PL_UPDATE_BOMZJTS_CRM3")]
    public class PL_UPDATE_BOMZJTS_CRM3
    {
        public string SO1 { get; set; }
        public string ZDDM1 { get; set; }
        public string JHDM1 { get; set; }
        public string GZDD1 { get; set; }
    }
    [Procedure.SPName("PL_UPDATE_BOMLSHTS3")]
    public class PL_UPDATE_BOMLSHTS3
    {
        public string LSH1 { get; set; }
        public string ZDDM1 { get; set; }
    }
    [Procedure.SPName("PL_UPDATE_BOMSOTHTS3")]
    public class PL_UPDATE_BOMSOTHTS3
    {
        public string SO1 { get; set; }
        public string JHDM1 { get; set; }
        public string ZDDM1 { get; set; }
    }
    [Procedure.SPName("PL_QUERY_BOMZJTS_MWS")]
    public class PL_QUERY_BOMZJTS_MWS
    {
        public string SO1 { get; set; }
        public string ZDDM1 { get; set; }
        public string GZDD11 { get; set; }
        public string FDJXL1 { get; set; }
        public string JHDM1 { get; set; }
        public string MACHINENAME1 { get; set; }

    }
    [Procedure.SPName("PL_UPDATE_BOMZJTS_MWS")]
    public class PL_UPDATE_BOMZJTS_MWS
    {
        public string SO1 { get; set; }
        public string ZDDM1 { get; set; }
        public string JHDM1 { get; set; }
        public string GZDD11 { get; set; }
        public string MACHINENAME1 { get; set; }

    }
    [Procedure.SPName("PL_UPDATE_BOMSOTHTS_MWS")]
    public class PL_UPDATE_BOMSOTHTS_MWS
    {
        public string SO1 { get; set; }
        public string JHDM1 { get; set; }
        public string ZDDM1 { get; set; }
        public string MACHINENAME1 { get; set; }

    }
    [Procedure.SPName("MW_COMPARE_ONE_BOM")]
    public class MW_COMPARE_ONE_BOM
    {
        public string GHTM1 { get; set; }
        public string GHTM2 { get; set; }
        public string JHDM1 { get; set; }
        public string JHDM2 { get; set; }
        public string ONEFLAG1 { get; set; }

    }
    [Procedure.SPName("MW_CREATE_BOMCOMP")]
    public class MW_CREATE_BOMCOMP
    {
        public string FUNC1 { get; set; }
        public string SO1 { get; set; }
        public string JHDM1 { get; set; }
        public string SO2 { get; set; }
        public string JHDM2 { get; set; }
        public string USER1 { get; set; }

    }
    [Procedure.SPName("MW_CREATE_RSTZZPTJ_NEW")]
    public class MW_CREATE_RSTZZPTJ_NEW
    {
        public string FUNC1 { get; set; }
        public DateTime RQ1 { get; set; }
        public DateTime RQ2 { get; set; }
        public string BCMC1 { get; set; }
        public string ZDMC1 { get; set; }
        public string MACHINENAME1 { get; set; }


    }

    [Procedure.SPName("RST_CREATE_BOM_ZZPWLQD")]
    public class RST_CREATE_BOM_ZZPWLQD
    {
        public string GHTM1 { get; set; }

    }

    [Procedure.SPName("PL_CHECK_FDJLS")]
    public class PL_CHECK_FDJLS
    {
        public string GHTM1 { get; set; }
        public string ZDMC1 { get; set; }
        public string GZDD1 { get; set; }
        [Procedure.Direction(ParameterDirection.InputOutput)]
        public string OUTSTR1 { get; set; }
    }
    [Procedure.SPName("MW_CREATE_ZDLSHTJ")]
    public class MW_CREATE_ZDLSHTJ
    {
        public DateTime GZRQ1 { get; set; }
        public DateTime GZRQ2 { get; set; }
        public string GZDD1 { get; set; }

    }
    [Procedure.SPName("MW_COMPARE_GHTMBOM")]
    public class MW_COMPARE_GHTMBOM
    {
        public string GHTM1 { get; set; }
        public string GZDD1 { get; set; }
        public string JHDM1 { get; set; }
        public string JHSO1 { get; set; }
        public string GZFLAG1 { get; set; }
        public string GZDD2 { get; set; }
        public string JHDM2 { get; set; }
        public string JHSO2 { get; set; }
        public string GZFLAG2 { get; set; }
        public string YHDM1 { get; set; }
    }

    [Procedure.SPName("VEPS_CHECK_SO")]
    public class VEPS_CHECK_SO
    {
        public string GZDD1 { get; set; }
        public string SO1 { get; set; }
        [Procedure.Direction(ParameterDirection.InputOutput)]
        public string RETSTR1 { get; set; }
    }
    [Procedure.SPName("PL_QUERY_ITEM8")]
    public class PL_QUERY_ITEM8
    {
        public string PLINECODE1 { get; set; }
        public string SCODE1 { get; set; }
        public DateTime BEGINDATE1 { get; set; }
        public DateTime ENDDATE1 { get; set; }
        public string PART1 { get; set; }
        public string MACHINENAME1 { get; set; }

    }
    [Procedure.SPName("PL_INSERT_SJZJQD")]
    public class PL_INSERT_SJZJQD
    {
        public string SN1 { get; set; }
        public string MACHINENAME1 { get; set; }

    }

    [Procedure.SPName("PL_BOMZJTS_ITEM8")]
    public class PL_BOMZJTS_ITEM8
    {
        public string PLANSO1 { get; set; }
        public string SCODE1 { get; set; }
        public string PLANCODE1 { get; set; }
        public string PART1 { get; set; }
        public string MACHINENAME1 { get; set; }

    }
    [Procedure.SPName("PL_UPDATE_BOMZJTS_ITEM8")]
    public class PL_UPDATE_BOMZJTS_ITEM8
    {
        public string PLANSO1 { get; set; }
        public string SCODE1 { get; set; }
        public string PLANCODE1 { get; set; }
        public string PLINECODE1 { get; set; }
        public string MACHINENAME1 { get; set; }

    }
    [Procedure.SPName("PL_UPDATE_ITEM8")]
    public class PL_UPDATE_ITEM8
    {
        public string PLANSO1 { get; set; }
        public string PLANCODE1 { get; set; }
        public string SCODE1 { get; set; }

    }
    //[Procedure.SPName("MW_COMPARE_GHTMBOM")]
    //public class MW_COMPARE_GHTMBOM
    //{
    //    public string GHTM1 { get; set; }
    //    public string GZDD1 { get; set; }
    //    public string JHDM1 { get; set; }
    //    public string JHSO1 { get; set; }
    //    public string GZFLAG1 { get; set; }
    //    public string GZDD2 { get; set; }
    //    public string JHDM2 { get; set; }
    //    public string JHSO2 { get; set; }
    //    public string GZFLAG2 { get; set; }
    //    public string YHDM1 { get; set; }
    //}
}
