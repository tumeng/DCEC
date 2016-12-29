using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PetaPoco;
using Rmes.DA.Base;
using System.IO;

namespace Rmes.Workstation.ConfigTools
{
    public partial class frm_Gen_Entities : Form
    {
        public frm_Gen_Entities()
        {
            InitializeComponent();
            refreshdatagridview();
            dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(dataGridView1_CellEndEdit);
            dataGridView1.AllowUserToAddRows = false;
        }

        void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow r = dataGridView1.Rows[e.RowIndex];
            if (r.IsNewRow) return;
            string RMES_ID = r.Cells["RMES_ID"].Value as string;
            string EntityName = r.Cells["ENTITY_NAME"].Value as string;
            if (EntityName == null || EntityName.Equals(string.Empty)) return;
            if (RMES_ID == null || RMES_ID.Equals(string.Empty)) return;
            code_config_tables edititem = DB.GetInstance().First<code_config_tables>("where RMES_ID=@0",RMES_ID);
            if (edititem == null) return;
            if (edititem.ENTITY_NAME!=null && edititem.ENTITY_NAME.Equals(EntityName)) return;
            edititem.ENTITY_NAME = EntityName;
            DB.GetInstance().Update(edititem);
                refreshdatagridview();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<code_config_tables> tables = DB.GetInstance().Fetch<code_config_tables>(@"
                select null as RMES_ID, t.TABLE_NAME as TABLE_NAME_EN,'' as ENTITY_NAME,b.comments as REMARK1, null as LASTGEN_TIME from user_tables t 
                left join user_tab_comments b on b.table_name = t.TABLE_NAME
                where t.TABLE_NAME not in (select table_name_en from code_config_tables)    
            ");
            if (tables.Count > 0)
            {
                DB.GetInstance().BeginTransaction();
                foreach (code_config_tables tb in tables)
                {
                    DB.GetInstance().Insert(tb);
                }
                DB.GetInstance().CompleteTransaction();
            }
            refreshdatagridview();
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                object checkitem = r.Cells["LASTGEN_TIME"].Value;
                if (r.Cells["ENTITY_NAME"].Value == null)
                {
                    r.DefaultCellStyle.BackColor = Color.LightPink;
                    continue;
                }
                if (r.Cells["ENTITY_NAME"].Value == null)
                {
                    r.DefaultCellStyle.BackColor = Color.Red;
                    continue;
                }
                DateTime LASTDDL_TIME = DB.GetInstance().First<DateTime>(
                        @"Select c.LAST_DDL_TIME from user_objects c where c.OBJECT_NAME = @0 and c.OBJECT_TYPE=@1",
                        r.Cells["TABLE_NAME_EN"].Value.ToString(), "TABLE");
                if (LASTDDL_TIME > Convert.ToDateTime(r.Cells["LASTGEN_TIME"].Value))
                {
                    r.DefaultCellStyle.BackColor = Color.Red;
                    continue;
                }
                r.DefaultCellStyle.BackColor = Color.LightGreen;
            }

        }
        private void refreshdatagridview()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = DB.GetInstance().Fetch<code_config_tables>("order by TABLE_NAME_EN");
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView1.Columns["RMES_ID"].ReadOnly = true;
            dataGridView1.Columns["TABLE_NAME_EN"].ReadOnly = true;
            dataGridView1.Columns["LASTGEN_TIME"].ReadOnly = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<code_config_tables> tbls = DB.GetInstance().Fetch<code_config_tables>("");
            ClassMaker cm = new ClassMaker();
            int total = 0;
            foreach (code_config_tables tb in tbls)
            {
                bool genit = false;
                if (tb.LASTGEN_TIME == null) 
                    genit = true;
                else
                {
                    DateTime LASTDDL_TIME = DB.GetInstance().First<DateTime>(
                        @"Select c.LAST_DDL_TIME from user_objects c where c.OBJECT_NAME = @0 and c.OBJECT_TYPE=@1",
                        tb.TABLE_NAME_EN, "TABLE");
                    if (LASTDDL_TIME > tb.LASTGEN_TIME) genit = true;
                }
                if (genit) //开始生成相关的文件
                {
                    total+=cm.GenEntityFile(tb);
                }
            }
            MessageBox.Show("共生成了"+total+"个Entity类", "提示");
            refreshdatagridview();
        }
    }

    [PetaPoco.TableName("CODE_CONFIG_TABLES")]
    [PetaPoco.PrimaryKey("RMES_ID", sequenceName = "SEQ_RMES_ID")]
    public class code_config_tables
    {
        public string RMES_ID { get; set; }
        public string TABLE_NAME_EN { get; set; }
        public string ENTITY_NAME { get; set; }
        public string REMARK1 { get; set; }
        public DateTime LASTGEN_TIME { get; set; }
    }

    public class ClassMaker
    {
        private  List<Syscolumns> Columns;
        public int GenEntityFile(code_config_tables row)
        {
            if(row.TABLE_NAME_EN.Equals(string.Empty)) return 0 ;
            if(row.ENTITY_NAME==null || row.ENTITY_NAME.Equals(string.Empty)) return 0 ;

            string strFile = "";
            strFile += generateHeader();
            strFile += generateClass(row.TABLE_NAME_EN, row.ENTITY_NAME);


            string path = System.Windows.Forms.Application.StartupPath;
            Directory.SetCurrentDirectory(path);
            if (!Directory.Exists(path + "\\Entity"))
            {
                Directory.CreateDirectory("Entity");
            }

            string destination = path + "\\Entity\\" + row.ENTITY_NAME + "Entity.cs";
            FileStream desFS = new FileStream(destination, FileMode.Create);
            byte[] byteArray = System.Text.UnicodeEncoding.Default.GetBytes(strFile);
            int len = byteArray.Length;
            desFS.Write(byteArray, 0, len);
            desFS.Close();
            row.LASTGEN_TIME = DB.GetServerTime();
            return DB.GetInstance().Update(row);            
        }
        private string generateHeader()
        {
            string outstr = "";

            outstr += "using System;\r\n";
            outstr += "using System.Collections.Generic;\r\n";
            outstr += "using System.Linq;\r\n";
            outstr += "using System.Text;\r\n";
            outstr += "#region 北自所Rmes命名空间引用\r\n";
            outstr += "using Rmes.DA.Base;\r\n";
            outstr += "using Rmes.DA.Entity;\r\n";
            outstr += "using Rmes.DA.Dal;\r\n";
            outstr += "using Rmes.DA.Factory;\r\n";
            outstr += "#endregion\r\n";
            outstr += "\r\n\r\n";

            outstr += "#region 自动生成实体类工具生成，by 北自所自控中心信息部\r\n";
            outstr += "//From XYJ\r\n";
            outstr += "//时间：" + DateTime.Now.Date.ToShortDateString() + "\r\n";
            outstr += "//\r\n";
            outstr += "#endregion\r\n";
            outstr += "\r\n\r\n";

            return outstr;
        }
        private string generateClass(string tbname, string classStr)
        {
            string outstr = "";

            outstr += "#region 自动生成实体类\r\n";
            outstr += "namespace Rmes.DA.Entity\r\n";
            outstr += "{\r\n";
            outstr += "\t[PetaPoco.TableName(\"" + tbname + "\")]\r\n";
            outstr += "\t//下面这行请自行填写关键字段并取消注释\r\n";
            outstr += "\t//[PetaPoco.PrimaryKey(\"RMES_ID\", sequenceName = \"SEQ_RMES_ID\")]\r\n";
            outstr += "\tpublic class " + classStr + "Entity : IEntity\r\n";
            outstr += "\t{\r\n";
            ReadColumns(tbname);

            foreach(Syscolumns sc in Columns)
            {
                outstr += "\t\tpublic " + SqlReplace.dbtype2c(sc.DATA_TYPE) + " " + sc.COLUMN_NAME.ToUpper() + "{ get; set; }\r\n";
            }

            outstr += "\r\n";
            outstr += "\t\t//字段名称\t\t\t\t字段类型\t\t长度\t\t关键字\t是否为空\t\t中文注释\r\n";

            foreach(Syscolumns sc in Columns)
            {
                if (sc.COLUMN_NAME.Length < 6)
                {
                    outstr += "\t\t//" + sc.COLUMN_NAME + "\t\t\t\t\t" + sc.DATA_TYPE;
                }
                else if (sc.COLUMN_NAME.Length < 10)
                {
                    outstr += "\t\t//" + sc.COLUMN_NAME + "\t\t\t\t" + sc.DATA_TYPE;
                }
                else if (sc.COLUMN_NAME.Length < 14)
                {
                    outstr += "\t\t//" + sc.COLUMN_NAME + "\t\t\t" + sc.DATA_TYPE;
                }
                else if (sc.COLUMN_NAME.Length < 18)
                {
                    outstr += "\t\t//" + sc.COLUMN_NAME + "\t\t" + sc.DATA_TYPE;
                }
                else
                {
                    outstr += "\t\t//" + sc.COLUMN_NAME + "\t" + sc.DATA_TYPE;
                }

                if (sc.DATA_TYPE != "VARCHAR2")
                {
                    outstr += "\t\t" + sc.DATA_LENGTH + "\t\t" + sc.IsPrimaryKey.ToString() + "\t" + sc.NULLABLE.ToString() + "\t\t" + sc.COMMENTS + "\r\n";
                }
                else
                {
                    outstr += "\t" + sc.DATA_LENGTH + "\t\t" + sc.IsPrimaryKey.ToString() + "\t" + sc.NULLABLE.ToString() + "\t\t" + sc.COMMENTS + "\r\n";
                }
            }

            outstr += "\t}\r\n";
            outstr += "}\r\n";
            outstr += "#endregion\r\n";
            return outstr;
        }
        private void ReadColumns(string TableStr)
        {
            string colstr = @"SELECT t.COLUMN_NAME,t.DATA_TYPE,t.DATA_LENGTH,t.NULLABLE,c.comments FROM user_tab_columns t, user_col_comments c 
                 WHERE t.table_name = c.table_name 
                 AND t.column_name = c.column_name 
                 AND t.table_name = @0 order by t.column_id";
            Columns = DB.GetInstance().Fetch<Syscolumns>(colstr, TableStr);
            string primarystr = "select count(*) " +
                "FROM user_cons_columns cu, user_constraints au " +
                "WHERE cu.constraint_name = au.constraint_name " +
                "AND au.constraint_type = 'P' " +
                "AND au.table_name = @0 " +
                "AND cu.column_name = @1";
            foreach (Syscolumns c in Columns)
            {
                string p = DB.GetInstance().ExecuteScalar<string>(primarystr, TableStr, c.COLUMN_NAME);
                c.IsPrimaryKey = (p.Equals("0"))?false:true;
            }
        }
    }
    public class Syscolumns
    {
        private bool _isprimarykey;

        public string COLUMN_NAME { get; set; }
        public string DATA_TYPE { get; set; }
        public string DATA_LENGTH { get; set; }
        public string NULLABLE { get; set; }
        public string COMMENTS { get; set; }
        [PetaPoco.Ignore()]
        public bool IsPrimaryKey
        {
            get { return _isprimarykey; }
            set { _isprimarykey = value; }
        }

    }
    public class SqlReplace
    {
        public static string Rp(string te)
        {
            te = te.Replace("System.String", "string");
            te = te.Replace("System.Int32", "int");
            te = te.Replace("System.Int16", "short");
            te = te.Replace("System.DateTime", "DateTime");
            te = te.Replace("System.Boolean", "bool");
            return te;
        }
        //将oracle数据类型转换为C#的数据类型
        public static string dbtype2c(string dbtype)
        {
            if (dbtype.ToUpper() == "VARCHAR2")
                dbtype = "string";
            if (dbtype.ToUpper() == "NUMBER")
                dbtype = "int";

            //dbtype = "decimal";

            if (dbtype.ToUpper() == "DECIMAL")
                dbtype = "decimal";
            if (dbtype.ToUpper() == "CHAR")
                dbtype = "string";
            if (dbtype.ToUpper() == "FLOAT")
                dbtype = "float";
            if (dbtype.ToUpper() == "DATE")
                dbtype = "DateTime";
            if (dbtype.ToUpper() == "INT")
                dbtype = "int";
            if (dbtype.ToUpper() == "INTEGTER")
                dbtype = "int";
            if (dbtype.ToUpper() == "BLOB")
                dbtype = "Byte[]";
            //有待添加新类型
            return dbtype;
        }
        private string ParaChange(string s, int length)
        {
            switch (s)
            {
                case "int":
                    s = "Int";
                    break;
                case "varchar":
                    s = "VarChar, " + length;
                    break;
                case "bit":
                    s = "Bit";
                    break;
                case "datetime":
                    s = "DateTime";
                    break;
                case "decimal":
                    s = "Decimal";
                    break;
                case "float":
                    s = "Float";
                    break;
                case "image":
                    s = "Image";
                    break;
                case "money":
                    s = "Money";
                    break;
                case "ntext":
                    s = "NText";
                    break;
                case "nvarchar":
                    s = "NVarChar";
                    break;
                case "smalldatetime":
                    s = "SmallDateTime";
                    break;
                case "smallint":
                    s = "SmallInt";
                    break;
                case "text":
                    s = "Text";
                    break;
            }
            return s;
        }
        public static string ConvertToSqlType(string type)
        {

            //  SqlDbType sqlType = SqlDbType.VarChar; 
            string sqlType = "System.String";
            switch (type.ToString())
            {
                case "System.String":
                    sqlType = "SqlDbType.VarChar";
                    break;
                case "System.Int32":
                    sqlType = "SqlDbType.Int";
                    break;
                case "System.Boolean":
                    sqlType = "SqlDbType.Bit";
                    break;
                case "System.DateTime":
                    sqlType = "SqlDbType.DateTime";
                    break;
                case "System.Guid":
                    sqlType = "SqlDbType.UniqueIdentifier";
                    break;
                case "System.Double":
                    sqlType = "SqlDbType.Float";
                    break;
                case "System.Byte[]":
                    sqlType = "SqlDbType.Binary";
                    break;
                case "System.Decimal":
                    sqlType = "SqlDbType.Money";
                    break;
                default:
                    break;
            }
            return sqlType;
        }
    }

}
