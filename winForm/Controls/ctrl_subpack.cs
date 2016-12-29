using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rmes.WinForm.Base;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.Pub.Data1;

namespace Rmes.WinForm.Controls
{
    //未用
    //分装处理站点 分装站点在此控件判断是否存在分装总成零件，存在则生成分装条码并打印
    public partial class ctrl_subpack : BaseControl
    {
        private string CompanyCode, PlineID, PlineCode, StationCode, StationID, StationName,SN;
        dataConn dc = new dataConn();
        public ctrl_subpack()
        {
            InitializeComponent();
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            PlineID = LoginInfo.ProductLineInfo.RMES_ID;
            PlineCode = LoginInfo.ProductLineInfo.PLINE_CODE;
            StationCode = LoginInfo.StationInfo.STATION_CODE;
            StationID = LoginInfo.StationInfo.RMES_ID;
            StationName = LoginInfo.StationInfo.STATION_NAME;
            this.RMesDataChanged += new RMesEventHandler(ctrl_sub_RMesDataChanged);
        }

        void ctrl_sub_RMesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            try
            {
                RMESEventArgs arg = new RMESEventArgs();
                arg.MessageHead = "";
                arg.MessageBody = "";
                if (e.MessageHead == null) return;
                if (e.MessageHead == "SN")
                {
                    SN = e.MessageBody.ToString();
                }
                //BOM展示完成后校验总成零件是否存在
                //if (e.MessageHead == "SHOWBOMOK")
                //{
                //    string subzc = "";
                //    try
                //    {
                //        subzc = dc.GetValue("select sub_zc from code_station_sub where substation_code='" + StationCode + "' and pline_code='" + PlineID + "'");
                //    }
                //    catch { subzc = ""; }
                //    if (subzc != "")
                //    {
                //        arg.MessageHead = "SUBCHECK";
                //        arg.MessageBody = SN + "^" + subzc;
                //        SendDataChangeMessage(arg);
                //    }
                //}
                //if (e.MessageHead == "SUBCHECKOK")
                //{
                //    string item_info = e.MessageBody.ToString();//消息体是三段码
                //    string[] cmd_info = item_info.Split('^');
                //    string subzc = cmd_info[1];
                //    string subItem = "";
                //    if (subzc != "")
                //    {
                //        //判断bom中是否包含该总成零件 
                //        //分装总成零件不为空  则打印分装总成零件条码
                //        subItem = subzc + "^SUB^" + SN;
                //        //Print 
                //        PublicClass.PrintSub(SN, "", subItem);
                //    }
                //}

            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }



    }
}
