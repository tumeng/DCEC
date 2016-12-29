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
    public partial class ctrl_Fx : BaseControl
    {
        private string CompanyCode, PlineCode, PlineID, StationCode, stationname, TheSo, TheSn, ThePlancode;
        //dataConn dc = new dataConn();
        ProductInfoEntity product;
        public ctrl_Fx()
        {
            InitializeComponent();
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            PlineID = LoginInfo.ProductLineInfo.RMES_ID;
            PlineCode = LoginInfo.ProductLineInfo.PLINE_CODE;
            StationCode = LoginInfo.StationInfo.RMES_ID;
            stationname = LoginInfo.StationInfo.STATION_NAME;
            this.RMesDataChanged += new RMesEventHandler(ctrl_Fx_RMesDataChanged);
            cmbStation.ValueMember = "STATION_CODE";
            cmbStation.DisplayMember = "STATION_NAME";
            DataTable dt = dataConn.GetTable("select * from (select ' ' station_code,' ' station_name from dual union select station_code,station_name from vw_code_station where pline_code ='" + PlineCode + "' order by station_name ) order by station_name ");
            cmbStation.DataSource = dt;
        }
        void ctrl_Fx_RMesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            try
            {
                RMESEventArgs arg = new RMESEventArgs();
                arg.MessageHead = "";
                arg.MessageBody = "";
                if (e.MessageHead == null) return;
                if (e.MessageHead == "SN")
                {
                    TheSn = e.MessageBody.ToString();
                    product = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, TheSn);//获取sn信息
                    if (product == null) return;
                    TheSo = product.PLAN_SO;
                    ThePlancode = product.PLAN_CODE;

                }
                if (e.MessageHead == "CHECKFX")
                {
                    //检查返修站点提交的流水号信息 做如下处理： 获取流水号对应的信息；线下返修做返修上线；
                    TheSn = e.MessageBody.ToString();
                    PlanSnEntity ent = PlanSnFactory.GetBySnPline(TheSn, PlineCode);//获取sn信息
                    product = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, TheSn);//获取sn信息
                    if (product == null) return;
                    TheSo = product.PLAN_SO;
                    ThePlancode = product.PLAN_CODE;

                    //判断是否线下返修未上线
                    if (ent.SN_FLAG == "P" || ent.SN_FLAG == "N")//未上线 初始为N 打印为P  上线未Y
                    {
                        if (LoginInfo.StationInfo.STATION_TYPE == "ST05")//当前站点是返修站点
                        {
                            arg.MessageHead = "ONLINE";
                            arg.MessageBody = TheSn;
                            SendDataChangeMessage(arg);
                        }
                        else
                        {
                            MessageBox.Show("此发动机流水号未上线");
                            return;
                        }
                    }
                    else
                    {
                        arg.MessageHead = "SN";
                        arg.MessageBody = TheSn;
                        SendDataChangeMessage(arg);
                    }

                    //显示BOM 显示装机提示 
                    arg.MessageHead = "SHOWBOMFX1";
                    arg.MessageBody = TheSn;
                    SendDataChangeMessage(arg);
                    //显示检测数据
                    arg.MessageHead = "SHOWDETECTFX1";
                    arg.MessageBody = TheSn;
                    SendDataChangeMessage(arg);

                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void cmbStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            object val = cmbStation.SelectedValue;
            if (val != null)
            {
                RMESEventArgs arg = new RMESEventArgs();
                arg.MessageHead = "";
                arg.MessageBody = "";
                string stationcode = cmbStation.SelectedValue.ToString().Trim();
                string stationname = cmbStation.Text.ToString().Trim();
                if (stationcode == "")
                    return;
                if (TheSn == "" || TheSn == null)
                {
                    MessageBox.Show("请输入流水号！");
                    arg.MessageHead = "INIT";
                    arg.MessageBody = "";
                    SendDataChangeMessage(arg);
                    return;
                }

                //根据选择的站点加载对应的装机档案

                //显示BOM 显示装机提示 
                arg.MessageHead = "SHOWBOM_FX";
                arg.MessageBody = TheSn + "^" + stationcode + "^" + stationname;
                SendDataChangeMessage(arg);
                //显示检测数据
                arg.MessageHead = "SHOWDETECT_FX";
                arg.MessageBody = TheSn + "^" + stationcode + "^" + stationname;
                SendDataChangeMessage(arg);

            }
        }
    }
}
