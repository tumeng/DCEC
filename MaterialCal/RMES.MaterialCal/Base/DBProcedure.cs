using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Oracle.DataAccess.Client;

//////////////////////////////////
/* 为了兼容存储过程使用的存储过程类，
 * 目前仅可以使用Oracle的存储过程（因为DBType所限）
 * 将来可以扩展到其他数据库的存储过程
 * 目前没处理性能问题，比直接调用可能略慢几十毫秒
 * 采用Property方式映射存储过程类，
 * 可以处理 INPUT、OUTPUT、INPUTOUTPUT、RETURN四类参数，暂时只能处理字符串、数字、时期时间形式的参数
 * 然后采用静态Procedure.run的方式处理存储过程并返回相应的参数
 * 
 * by liuzhy 2013-12-25 X-MAS @ xian, shaanxi. TAT
 */
 ///////////////////////////////////

namespace RMES.MaterialCal
{

    public static class Procedure
    {
        private static Dictionary<string, object> SPMapper = new Dictionary<string, object>();

        public static void run(object pocoProcedure)
        {
            Type t = pocoProcedure.GetType(); ;
            string spname = "";
            var a = t.GetCustomAttributes(typeof(SPNameAttribute), true);
            spname = a.Length == 0 ? t.Name : (a[0] as SPNameAttribute).Value;

            var cmd = DB.GetInstance().Connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = spname;
            Dictionary<string, ParamInfo> dicts;
            if (SPMapper.ContainsKey(spname))
                dicts = SPMapper[spname] as Dictionary<string, ParamInfo>;
            else
            {
                dicts = new Dictionary<string, ParamInfo>();
                foreach (var param in t.GetProperties())
                {
                    ParamInfo info = new ParamInfo();
                    var paraAttr = param.GetCustomAttributes(typeof(DirectionAttribute), true);
                    if (paraAttr.Length > 0)
                    {
                        DirectionAttribute at1 = paraAttr[0] as DirectionAttribute;
                        info.ParamDirection = at1.direction;
                    }
                    else
                    {
                        info.ParamDirection = ParameterDirection.Input;
                    }
                    info.ParamName = param.Name;
                    info.ParamType = getOracleType(param.GetGetMethod().ReturnType);
                    info.ParamValue = param.GetValue(pocoProcedure, null);
                    dicts.Add(param.Name, info);
                }
            }
            foreach (ParamInfo info in dicts.Values)
            {
                cmd.Parameters.Add(info.opm);
            }
            if(cmd.Connection.State!= ConnectionState.Open)
                cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            foreach (var param in t.GetProperties())
            {
                if (dicts[param.Name].ParamDirection == ParameterDirection.InputOutput || dicts[param.Name].ParamDirection == ParameterDirection.Output || dicts[param.Name].ParamDirection == ParameterDirection.ReturnValue)
                param.SetValue(pocoProcedure, dicts[param.Name].ResultValue, null);
            }
        }

        private static OracleDbType getOracleType(Type t)
        {
            if (t.Equals(typeof(int))) return OracleDbType.Int32;
            if (t.Equals(typeof(DateTime))) return OracleDbType.Date;
            if (t.Equals(typeof(byte[]))) return OracleDbType.Blob;
            if (t.Equals( typeof(long))) return OracleDbType.Long;
            if (t.Equals(typeof(float)) || t.Equals(typeof(double))) return OracleDbType.Double;
            return OracleDbType.Varchar2;            
        }

        [AttributeUsage(AttributeTargets.Class)]
        public class SPNameAttribute : Attribute
        {
            public SPNameAttribute(string StoredProcedureName)
            {
                Value = StoredProcedureName;
            }
            public string Value { get; private set; }
        }

        [AttributeUsage(AttributeTargets.Property)]
        public class DirectionAttribute : Attribute
        {
            public DirectionAttribute(ParameterDirection Direction)
            {
                direction = Direction;
            }
            public ParameterDirection direction { get; private set; }
            public string Value { get; private set; }
        }

        internal class ParamInfo
        {
            public string ParamName { get; set; }
            public ParameterDirection ParamDirection { get; set; }
            public OracleDbType ParamType { get; set; }
            public object ParamValue { get; set; }
            public object ResultValue
            {
                get
                {
                    if (ParamType == OracleDbType.Varchar2)
                        return _opm.Value.ToString();
                    if (ParamType == OracleDbType.Int32)
                        return Convert.ToInt32(_opm.Value.ToString());
                    if (ParamType == OracleDbType.Date)
                        return Convert.ToDateTime(_opm.Value.ToString());
                    return _opm.Value;
                }

            }
            OracleParameter _opm = null;
            public OracleParameter opm
            {
                get
                {
                    if (_opm == null)
                    {
                        _opm = new OracleParameter(ParamName, ParamType, ParamDirection);
                        _opm.Size = 300;
                        _opm.Value = ParamValue;
                    }
                    return _opm;
                }
            }
        }

    }
}
