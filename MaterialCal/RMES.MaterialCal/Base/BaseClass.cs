using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Linq;
using System.Xml;
using System.Text;
using PetaPoco;
using System.Configuration;
using Oracle.DataAccess.Client;

namespace RMES.MaterialCal
{

    public interface IEntity
    {
    }

    public interface IFactory
    {
        //List<IEntity> GetAll();
        //List<IEntity> GetSome(int index, int size, string OrderPropertyName);
        //List<IEntity> GetBySql(string sql);

        //IEntity GetByKey(object keyvalue);

        //IEntity New();
        //int Save(IEntity entity);
        //void Remove(IEntity entity);
        //void RemoveByKey(Object PrimaryKey);

        //RmesExtendInfo GetExtendInfo(IEntity entity);
    }

    public class RmesExtendInfo
    {
        public RmesExtendInfo(string str){}
        public object GetValue(string key) { return null; }
        public void SetValue(string key, string value){}
    }

    public abstract class BaseDalClass
    {
        protected Database db;
        public BaseDalClass() { db = DB.GetInstance(); }
        public List<T> FindAll<T>()
        {
            return db.Fetch<T>("");
        }
        public List<T> FindBySql<T>(string sql,params object[] args)
        {
            return db.Fetch<T>(sql, args);
        }
        public List<T> FindBySql<T>(Sql sql)
        {
            return db.Fetch<T>(sql);
        }
        public Page<T> Page<T>(long page,long itemsperpage,Sql sql)
        {
            return db.Page<T>(page, itemsperpage, sql);
        }
        public Page<T> Page<T>(long page, long itemsperpage, string sql,params object[] objs)
        {
            return db.Page<T>(page, itemsperpage, sql,objs);
        }
        public object Insert(IEntity entity)
        {
            return db.Insert(entity);
        }
        public int Update(IEntity entity)
        {
            return db.Update(entity);
        }
        public int Remove(IEntity entity)
        {
            return db.Delete(entity);
        }
        public int RemoveByKey<T>(object PrimaryKey)
        {            
            return db.Delete<T>(PrimaryKey);
        }
        public bool Exsits<T>(object primaryKey)
        {
            return db.Exists<T>(primaryKey);
        }
        public PetaPoco.Database DataBase
        {
            get { return db; }
        }
    }

    [PetaPoco.TableName("CODE_CONFIG_SYSTEM")]
    [PetaPoco.PrimaryKey("RMES_ID",sequenceName="SEQ_RMES_ID")]
    public class ConfigServerEntity : IEntity
    {
        public string RMES_ID { get; set; }
        public string CONFIG_CODE { get; set; }
        public string CONFIG_VALUE { get; set; }
        public string REMARK { get; set; }
    }

    public class ConfigServerDal : BaseDalClass
    {
        public string GetValue(string KeyCode)
        {
            List<ConfigServerEntity> vals = db.Fetch<ConfigServerEntity>("WHERE CONFIG_CODE=@0 order by RMES_ID desc",KeyCode);
            if (vals.Count < 1) return "";
            else return vals.First().CONFIG_VALUE;
        }

        public void SetValue(string KeyCode, string KeyValue)
        {
            ConfigServerEntity entity;
            if (this.GetValue(KeyCode).Equals(string.Empty))
            {
                entity = new ConfigServerEntity();
                entity.CONFIG_CODE = KeyCode;
                entity.CONFIG_VALUE = KeyValue;
                base.Insert(entity);
            }
            else
            {
                entity = db.First<ConfigServerEntity>("WHERE CONFIG_CODE=@0", KeyCode);
                entity.CONFIG_VALUE = KeyValue;
                base.Update(entity);
            }
        }

    }

    public static class DB
    {
        //public static string SEPRATER = "_";
        public static string DataConnectionName = "oracle";
        //static string orastring = ConfigurationManager.AppSettings["connectionString"].ToString().Replace("#userid#", "imfmuser").Replace("#password#", "imfmuser").Replace("#host#", "192.168.56.2").Replace("#port#", "1521").Replace("#servicename#", "imfm");
        private static Database _db;
        static Hashtable dbServerSettings = new Hashtable();
        static string configFileName = "\\RMES.config.xml";
        static string strTimeSQL = "TIMESQL";

        public static Database GetInstance()
        {
            string dbConnName = DataConnectionName;
            if (_db == null)
            {
                try
                {
                    _db = new Database(dbConnName);
                }
                catch
                {
                    string connstr = System.Configuration.ConfigurationManager.ConnectionStrings[dbConnName].ToString();
                    Oracle.DataAccess.Client.OracleConnection conn = new Oracle.DataAccess.Client.OracleConnection(connstr);
                    conn.Open();
                    _db = new Database(conn);
                }
            }
            else
            {
                //重试连接，以下代码尚未测试，请进行测试。。。by liuzhy 20140421
                int n = 3;//设定重试多少次；
                int delay = 200;//设定每次重试间隔多少毫秒；
                string connstr = System.Configuration.ConfigurationManager.ConnectionStrings[dbConnName].ToString();
                Oracle.DataAccess.Client.OracleConnection conn = new Oracle.DataAccess.Client.OracleConnection(connstr);

                while (_db.Connection.State != System.Data.ConnectionState.Open)
                {
                    try
                    {
                        conn.Open();
                        _db = new Database(conn);
                    }
                    catch(Exception ex)
                    { 
                        //可以记录一下访问失败的一些信息，比如时间、ip地址、ex.Message等
                    }
                    System.Threading.Thread.Sleep(delay);
                    n--;
                    if (n.Equals(0)) break;
                }
            }
            _db.OpenSharedConnection();
            return _db;
        }

        private static XmlDocument getLocalXML()
        {
            string xmlpath = LocalXMLFile;
            XmlDocument xml = new XmlDocument();
            if (!System.IO.File.Exists(xmlpath))
            {
                XmlNode titlenode = xml.CreateNode(XmlNodeType.XmlDeclaration, "", "");
                xml.AppendChild(titlenode);
                XmlElement el = xml.CreateElement("","ROOT","");
                xml.AppendChild(el);
                xml.Save(xmlpath);
            }
            xml.Load(xmlpath);
            return xml;
        }

        private static void setLocalXML(XmlDocument xml1)
        {
            string xmlpath = LocalXMLFile;
            xml1.Save(xmlpath);
        }

        public static string LocalXMLFile
        {
            get {
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + configFileName;
            }
        }

        public static string ReadConfigLocal(string KeyName)
        {
            try
            {
                XmlDocument xml = getLocalXML();
                string query = string.Format("//setting[@id='{0}']", KeyName); // or "//book[@id='{0}']"
                XmlElement el = (XmlElement)xml.SelectSingleNode(query);
                if (el == null || !el.HasAttribute("value"))
                {
                    return "";
                }
                return el.GetAttribute("value");
            }
            catch
            {
                return "";
            }
        }
        public static void WriteConfigLocal(string KeyName, string KeyValue)
        {
            try
            {
                XmlDocument xml = getLocalXML();
                string query = string.Format("//setting[@id='{0}']", KeyName); // or "//book[@id='{0}']"
                XmlElement el = (XmlElement)xml.SelectSingleNode(query);
                if (el == null)
                {
                    el = xml.CreateElement("setting", xml.NamespaceURI);
                    el.SetAttribute("id", KeyName);
                    xml.DocumentElement.AppendChild(el);
                }
                el.SetAttribute("value", KeyValue);
                setLocalXML(xml);
            }
            catch
            { }
        }

        /// <summary>
        /// 从数据库中读取服务器的全局配置
        /// </summary>
        /// <param name="KeyName">参数为配置的名字</param>
        /// <returns>返回该配置的值，没有则返回空字符串</returns>
        public static string ReadConfigServer(string KeyName)
        {
            if(GetInstance()==null)    throw new Exception("必须先连接数据库才能读取服务器设置");
            if (!dbServerSettings.Contains(KeyName))
                dbServerSettings.Add(KeyName, new ConfigServerDal().GetValue(KeyName));
            else
                dbServerSettings[KeyName] = new ConfigServerDal().GetValue(KeyName);
            return dbServerSettings[KeyName] as string;
        }
        /// <summary>
        /// 设置某个配置的值，存放到CODE_CONFIG_SYSTEM表中，如果没有该配置则创建并赋值。
        /// </summary>
        /// <param name="KeyName">配置的名字</param>
        /// <param name="KeyValue">配置的值</param>
        public static void WriteConfigServer(string KeyName, string KeyValue)
        {
            if (GetInstance() == null) throw new Exception("必须先连接数据库才能修改服务器设置");
            new ConfigServerDal().SetValue(KeyName, KeyValue);
        }

        public static DateTime GetServerTime()
        {
            if (_db != null)
            {
                string timesql = ReadConfigServer(strTimeSQL);
                if (timesql.Equals(string.Empty))
                    return DateTime.Now;
                return _db.ExecuteScalar<DateTime>(timesql);
            }
            else
                return DateTime.Now;
        }

        public static bool CheckAllowRemove(string tablename, string companycode, string productlinecode, string temp1, string temp2, string primarykeyvalue)
        { 
            string removechecksql = DB.ReadConfigServer("REMOVECHECKSQL");
            string removevalue = DB.ReadConfigServer("REMOVECHECKSQLTRUEVALUE");
            if (removechecksql.Equals(string.Empty) || removevalue.Equals(string.Empty)) return true;
            string returnval = DB.GetInstance().ExecuteScalar<string>(removechecksql, tablename, companycode, productlinecode, temp1, temp2, primarykeyvalue);
            if (returnval.Equals(removevalue)) 
                return true;
            Rmes.Public.ErrorHandle.EH.LASTMSG = returnval;
            return false;
        }

        public static int oracleCallProcedure(string ProcedureName,params object[] args)
        {
            string strSql = String.Format("CALL {0}(@args)", ProcedureName);
            int ret = DB.GetInstance().Execute(strSql,args);
            return ret;
        }
    }
}
