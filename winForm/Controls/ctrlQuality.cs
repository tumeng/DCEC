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
using System.IO;

namespace Rmes.WinForm.Controls
{

    public partial class ctrlQuality : BaseControl
    {
        public string PlanCode = "", SN = "";
        public string CompanyCode = "", PlineCode = "", StationCode = "", UserID = "";
        public ctrlQuality()
        {
            InitializeComponent();
            GridQuality.AutoGenerateColumns = false;
            GridQuality.RowHeadersVisible = false;

            DataGridViewComboBoxColumn dgvComboBoxColumn = GridQuality.Columns["colCurrentResult"] as DataGridViewComboBoxColumn;
            DataTable dt = new DataTable();
            dt.Columns.Add("DisplayField", typeof(string));
            dt.Columns.Add("ValueField", typeof(int));
            
            dt.Rows.Add("不合格", 0);
            dt.Rows.Add("未知状态", -1);
            dt.Rows.Add("合格", 1);

            dgvComboBoxColumn.DataSource = dt;
            dgvComboBoxColumn.DisplayMember = "DisplayField";
            dgvComboBoxColumn.ValueMember = "ValueField";


            DataGridViewComboBoxColumn dgvComboBoxColumn1 = GridQuality.Columns["colType"] as DataGridViewComboBoxColumn;
            dgvComboBoxColumn1.DataSource = QualityFactory.GetAllType();
            dgvComboBoxColumn1.DisplayMember = "FAULT_NAME";

            this.RMesDataChanged += new Rmes.WinForm.Base.RMesEventHandler(ctrQuality_RMesDataChanged);

            
        }
        void ctrQuality_RMesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
             string stationCode = LoginInfo.StationInfo.RMES_ID;
            if (e.MessageHead == null) return;
            
            if (e.MessageHead.ToString() == "SN")
            {
                SN = e.MessageBody as string;
                ProductInfoEntity product = ProductInfoFactory.GetByCompanyCodeSNSingle(LoginInfo.CompanyInfo.COMPANY_CODE, SN);
                if (product == null) return;               //SN = product.SN; 
                List<QualitySnItem> QualitySn = QualityFactory.GetAll();
                GridQuality.DataSource = QualitySn;
                //ShowQualityList();
                
            }
        }

        private void ShowQualityList()
        {
            List<QualitySnItem> QualitySn = new List<QualitySnItem>();
            List<QualityStandardItem> Standerd=new List<QualityStandardItem>();
            List<ProcessRoutingEntity> process = ProcessRoutingFactory.GetByStationCode(LoginInfo.StationInfo.RMES_ID);
            if (process == null)
            {
                List<ProcessRoutingEntity> process1 = ProcessRoutingFactory.GetByPlineCode(LoginInfo.StationInfo.PLINE_CODE);
                for (int j = 0; j < process1.Count; j++)//判断每道工序下的检验项目是否全部检验完成
                {
                    List<QualityStandardItem> StandardItem = QualityFactory.GetItemsOfProcess(process1[j].PROCESS_CODE);
                    List<QualitySnItem> snItem = QualityFactory.GetProductProcessItems(SN, process1[j].PROCESS_CODE);
                    int snItemFact = snItem.Count;
                    for (int k = 0; k < snItem.Count; k++)//判断相同ItemCode，移除
                    {
                        for (int m = k; m < snItem.Count-1; m++)
                        {
                            if (snItem[k].ItemCode == snItem[m + 1].ItemCode)
                                snItem.Remove(snItem[m + 1]);
                        }
                    }
                    if (snItem.Count == StandardItem.Count&&snItem.Count!=0)
                        Standerd.Add(StandardItem[0]);
                }
                for (int i = 0; i < process1.Count; i++)//未完成工序检验项目添加到列表中
                {
                    for (int j = 0; j < Standerd.Count; j++)
                    {
                        if (Standerd[j].ProcessCode == process1[i].PROCESS_CODE)
                        { }
                        else
                        {
                            List<QualitySnItem> Q = QualityFactory.GetProductProcessItems(SN, process1[i].PROCESS_CODE);
                            QualitySn.AddRange(Q);
                        }
                    }
                }

                GridQuality.DataSource = QualitySn;
                for (int j = 0; j < GridQuality.Rows.Count; j++)
                {
                    if (!string.IsNullOrWhiteSpace(GridQuality.Rows[j].Cells["colRmesId"].Value as string))
                    {
                        GridQuality.Rows[j].ReadOnly = true;
                        GridQuality.Rows[j].DefaultCellStyle.BackColor = Color.LightGray;
                    }
                }
            }
            else
            {
                for (int j = 0; j < process.Count; j++)//判断每道工序下的检验项目是否全部检验完成
                {
                    List<QualityStandardItem> StandardItem = QualityFactory.GetItemsOfProcess(process[j].PROCESS_CODE);
                    List<QualitySnItem> snItem = QualityFactory.GetProductProcessItems(SN, process[j].PROCESS_CODE);
                    int snItemFact = snItem.Count;
                    for (int k = 0; k < snItem.Count; k++)//判断相同ItemCode，移除
                    {
                        for (int m = k; m < snItem.Count - 1; m++)
                        {
                            if (snItem[k].ItemCode == snItem[m + 1].ItemCode)
                                snItem.Remove(snItem[m + 1]);
                        }
                    }
                    if (snItem.Count == StandardItem.Count)
                        Standerd.Add(StandardItem[0]);
                }
                for (int i = 0; i < process.Count; i++)//未完成工序检验项目添加到列表中
                {
                    for (int j = 0; j < Standerd.Count; j++)
                    {
                        if (Standerd[j].ProcessCode == process[i].PROCESS_CODE)
                        { }
                        else
                        {
                            List<QualitySnItem> Q = QualityFactory.GetProductProcessItems(SN, process[i].PROCESS_CODE);
                            QualitySn.AddRange(Q);
                        }
                    }
                }
                GridQuality.DataSource = QualitySn;
                for (int j = 0; j < GridQuality.Rows.Count; j++)
                {
                    if (!string.IsNullOrWhiteSpace(GridQuality.Rows[j].Cells["colRmesId"].Value as string))
                    {
                        GridQuality.Rows[j].ReadOnly = true;
                        GridQuality.Rows[j].DefaultCellStyle.BackColor = Color.LightGray;
                    }
                }
            }
        }


        private void GridQuality_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (GridQuality.Rows[e.RowIndex].ReadOnly == true) return;
            //string fileName = "";
            string rmesid="";
            if (e.ColumnIndex == 5)
            {
                if (GridQuality.Rows[e.RowIndex].Cells["colUnitType"].Value.ToString() == "F")
                {
                    OpenFileDialog MyFileDialog = new OpenFileDialog();//创建打开对话框对象
                    MyFileDialog.ShowDialog();//显示打开对话框
                    if (MyFileDialog.FileName.Trim() != "")//判断是否选择了文件
                    {
                        FileStream FStream = new FileStream(MyFileDialog.FileName, FileMode.Open, FileAccess.Read);//创建FileStream对象
                        string fileName = MyFileDialog.FileName;
                        string fileNamesub = MyFileDialog.SafeFileName;
                        BinaryReader BReader = new BinaryReader(FStream);//创建BinaryReader读取对象
                        byte[] bytes = BReader.ReadBytes((int)FStream.Length);//读取二进制
                        GridQuality.Rows[e.RowIndex].Cells["colCurrentValue"].Value = fileNamesub;
                        //quality.CurrentValue = fileName;
                        FileBlobEntity BE = new FileBlobEntity()
                        {
                            FILE_BLOB = bytes,
                            FILE_NAME = fileNamesub,
                            USER_ID = LoginInfo.UserInfo.USER_CODE,
                            CREAT_TIME = DateTime.Now
                        };
                        PetaPoco.Database db = DB.GetInstance();
                        rmesid = db.Insert("QMS_FILE_BLOB", "RMES_ID", BE).ToString();

                        //FileStream fs = new FileStream(@fileName, FileMode.CreateNew, FileAccess.Write, FileShare.Read);
                        //StreamWriter sw = new StreamWriter(fs);
                        //sw.Write(bytes);
                        FStream.Close();//关闭数据流
                    }
                }
                //else
                //{
                //    if (!string.IsNullOrWhiteSpace(GridQuality.Rows[e.RowIndex].Cells["colCurrentValue"].Value.ToString()))
                //       = GridQuality.Rows[e.RowIndex].Cells["colCurrentValue"].Value.ToString();
                //}
            }
            if (e.ColumnIndex == 0)
            {
                QualitySnItem quality = new QualitySnItem();
                if (GridQuality.Rows[e.RowIndex].Cells["colUnitType"].Value.ToString() == "F")
                {
                    quality.URL = rmesid;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(GridQuality.Rows[e.RowIndex].Cells["colCurrentValue"].Value as string)) return;
                    quality.URL = "";
                }
                if (string.IsNullOrWhiteSpace(GridQuality.Rows[e.RowIndex].Cells["colCurrentValue"].Value as string)) return;
                quality.CurrentValue = GridQuality.Rows[e.RowIndex].Cells["colCurrentValue"].Value.ToString();

                if (GridQuality.Rows[e.RowIndex].Cells["colCurrentResult"].Value == null) { }
                quality.CurrentResult = Convert.ToInt32((GridQuality.Rows[e.RowIndex].Cells["colCurrentResult"] as DataGridViewComboBoxCell).Value);

                quality.ProcessCode = GridQuality.Rows[e.RowIndex].Cells["colProcessCode"].Value.ToString();
                //quality.BatchNo = GridQuality.Rows[e.RowIndex].Cells["colBatchNo"].Value.ToString();
                quality.BatchNo = SN;
                //quality.RMES_ID = GridQuality.Rows[e.RowIndex].Cells["colRmesId"].Value.ToString();
                quality.ItemCode = GridQuality.Rows[e.RowIndex].Cells["colItemCode"].Value.ToString();
                quality.ItemName = GridQuality.Rows[e.RowIndex].Cells["colItemName"].Value.ToString();
                quality.ItemDescription = GridQuality.Rows[e.RowIndex].Cells["colItemDescription"].Value.ToString();
                quality.MaxValue = Convert.ToInt32(GridQuality.Rows[e.RowIndex].Cells["colMaxValue"].Value);
                quality.MinValue = Convert.ToInt32(GridQuality.Rows[e.RowIndex].Cells["colMinValue"].Value);
                if (quality.Ordering == null)
                    quality.Ordering = "";
                else
                    quality.Ordering = GridQuality.Rows[e.RowIndex].Cells["colOrdering"].Value.ToString();
                if (string.IsNullOrWhiteSpace(GridQuality.Rows[e.RowIndex].Cells["colUnitName"].Value as string))
                    quality.UnitName = "";
                else
                    quality.UnitName = GridQuality.Rows[e.RowIndex].Cells["colUnitName"].Value.ToString();
                quality.UnitType = GridQuality.Rows[e.RowIndex].Cells["colUnitType"].Value.ToString();
                
               
                //QualityType type = QualityFactory.GetByTypeName(GridQuality.Rows[e.RowIndex].Cells["colType"].Value.ToString());
                if (GridQuality.Rows[e.RowIndex].Cells["colType"].Value == null) { }
                else 
                quality.FAULT_TYPE = (GridQuality.Rows[e.RowIndex].Cells["colType"] as DataGridViewComboBoxCell).Value.ToString();
                
                if (string.IsNullOrWhiteSpace(GridQuality.Rows[e.RowIndex].Cells["colEquipment"].Value as string)) return;
                quality.TEST_EQUIPMENT = GridQuality.Rows[e.RowIndex].Cells["colEquipment"].Value.ToString();
                if (string.IsNullOrWhiteSpace(GridQuality.Rows[e.RowIndex].Cells["Remark"].Value as string))
                    quality.TEMP = "";
                else 
                quality.TEMP=GridQuality.Rows[e.RowIndex].Cells["Remark"].Value.ToString();
                quality.WORK_TIME = System.DateTime.Now;
                quality.USER_ID = LoginInfo.UserInfo.USER_ID;

                if (quality.UnitType == "N" && quality.UnitType == "B")
                    QualityFactory.SaveItemOfProduct(quality, true);
                else QualityFactory.SaveItemOfProduct(quality, false);
                GridQuality.DataSource = new List<QualitySnItem>();
                ShowQualityList();
                
            }

        }

        private void GridQuality_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (GridQuality.Rows[e.RowIndex].ReadOnly == true) return;
            //string fileName = "";
            string ProcessCode = GridQuality.Rows[e.RowIndex].Cells["colProcessCode"].Value.ToString();
            string ItemName = GridQuality.Rows[e.RowIndex].Cells["colItemName"].Value.ToString();
            if (e.ColumnIndex == 1)
            {
                FrmDetectPart test = new FrmDetectPart(SN,ProcessCode,ItemName);
                test.ShowDialog();
            }
        }
        
        
    }
}
