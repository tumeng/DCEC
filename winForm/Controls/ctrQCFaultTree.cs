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

namespace Rmes.WinForm.Controls
{
    public partial class ctrQCFaultTree : BaseControl
    {
        public string VIN;
        public ProductEntity product;

        public ctrQCFaultTree()
        {
            InitializeComponent();
            this.RMesDataChanged += new Rmes.WinForm.Base.RMesEventHandler(ctrQCFaultTree_RMesDataChanged);
            listFault1.DataSource = FaultTreeFactory.GetByLever(1);
            listFault1.ValueMember = "FAULT_CODE";
            listFault1.DisplayMember = "FAULT_NAME";
        }

        void ctrQCFaultTree_RMesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            if (e.MessageHead.ToString() != "VIN") return;
            product = e.MessageBody as ProductEntity;
            VIN = product.SN;
            //VIN = e.MessageBody.ToString();
        }

        private void listFault1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listFault2.DataSource = FaultTreeFactory.GetNextLever(listFault1.SelectedValue.ToString());
            listFault2.ValueMember = "FAULT_CODE";
            listFault2.DisplayMember = "FAULT_NAME";
        }

        private void listFault2_DoubleClick(object sender, EventArgs e)
        {
            FaultListEntity fl = new FaultListEntity();
            
            //fl.RMES_ID = DB.GetInstance().ExecuteScalar<string>("SELECT SYS_GUID() FROM DUAL");

            fl.RMES_ID = DateTime.Now.ToString();
            fl.SN = VIN;
            fl.FAULT_CODE = listFault2.SelectedValue.ToString();
            fl.COMPANY_CODE = LoginInfo.CompanyInfo.COMPANY_CODE.ToString();
            fl.PLINE_CODE = LoginInfo.StationInfo.RMES_ID.ToString();
            fl.STATION_CODE = LoginInfo.StationInfo.RMES_ID.ToString();
            fl.SHIFT_CODE = LoginInfo.ShiftInfo.SHIFT_CODE.ToString();
            fl.TEAM_CODE = LoginInfo.TeamInfo.TEAM_CODE.ToString();
            fl.EMPLOYEE_CODE = LoginInfo.UserInfo.USER_ID.ToString();
            fl.WORK_TIME = System.DateTime.Now;
            fl.REPAIR_FLAG = "N";
            fl.DELETE_FLAG = "N";

            FaultListFactory.Save(fl);

            RMESEventArgs arg = new RMESEventArgs();
            arg.MessageBody = product;
            arg.MessageHead = "qcADD";
            UiFactory.CallDataChanged(this, arg);
        }
    }
}
