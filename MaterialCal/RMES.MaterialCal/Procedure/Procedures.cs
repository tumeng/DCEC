using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Rmes.DA.Base;
using System.Data;

namespace RMES.MaterialCal
{
    [Procedure.SPName("MS_SF_JIT_SINGLE_R")] //如果类名称和存储过程名称一致，该行可以省略
    public class MS_SF_JIT_SINGLE_R //
    {
        [Procedure.Direction(ParameterDirection.Input)] // Input参数可以省略不写该行，属性名称需和存储过程参数名称一致
        public string GZQY1 { get; set; }
        [Procedure.Direction(ParameterDirection.Input)] //InputOutput/Output/Return必须在这里写明
        public string QADSITE1 { get; set; }
        [Procedure.Direction(ParameterDirection.InputOutput)]
        public string RESULT1 { get; set; }
    }
    [Procedure.SPName("MS_SF_JIT_SINGLE_MAT_R")] //如果类名称和存储过程名称一致，该行可以省略
    public class MS_SF_JIT_SINGLE_MAT_R //
    {
        [Procedure.Direction(ParameterDirection.Input)] // Input参数可以省略不写该行，属性名称需和存储过程参数名称一致
        public string GZQY1 { get; set; }
        [Procedure.Direction(ParameterDirection.Input)] //InputOutput/Output/Return必须在这里写明
        public string QADSITE1 { get; set; }
        [Procedure.Direction(ParameterDirection.InputOutput)]
        public string RESULT1 { get; set; }
    }
    [Procedure.SPName("MS_SF_JIT_R")] //如果类名称和存储过程名称一致，该行可以省略
    public class MS_SF_JIT_R //
    {
        [Procedure.Direction(ParameterDirection.Input)] // Input参数可以省略不写该行，属性名称需和存储过程参数名称一致
        public string GZQY1 { get; set; }
        [Procedure.Direction(ParameterDirection.Input)] //InputOutput/Output/Return必须在这里写明
        public string QADSITE1 { get; set; }
        [Procedure.Direction(ParameterDirection.Input)] //InputOutput/Output/Return必须在这里写明
        public string MANUALFLAG1 { get; set; }
        [Procedure.Direction(ParameterDirection.InputOutput)]
        public string RESULT1 { get; set; }
    }
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
    }

    public class MW_MODIFY_ROLE_PROGRAM
    {
        public string THEFUNCTION1 { get; set; }
        public string THECOMPANYCODE1 { get; set; }
        public string THEPROGRAMCODE1 { get; set; }
        public string THEROLECODE1 { get; set; }
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
}
