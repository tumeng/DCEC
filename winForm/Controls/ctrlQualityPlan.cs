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
    //modify by thl 20140509 质量检验报工 记录每一个工序的质量检验结果
    public partial class ctrlQualityPlan : BaseControl
    {
        public string PlanCode = "", SN = "";
        public string CompanyCode = "", PlineCode = "", StationCode = "", UserID = "";
        private ProductInfoEntity _product = new ProductInfoEntity();
        public ctrlQualityPlan()
        {
            InitializeComponent();
            GridQuality.AutoGenerateColumns = false;
            GridQuality.RowHeadersVisible = false;

            //定义colCurrentResult检测结果  改为从BOM表引出的质量表data_detect_item中取
            DataGridViewComboBoxColumn dgvComboBoxColumn = GridQuality.Columns["coldx"] as DataGridViewComboBoxColumn;
            DataTable dt = new DataTable();
            dt.Columns.Add("DisplayField", typeof(string));
            dt.Columns.Add("ValueField", typeof(string));
            dt.Rows.Add("合格", "A");
            dt.Rows.Add("不合格", "B");
            dt.Rows.Add("未知状态", "C");


            dgvComboBoxColumn.DataSource = dt;
            dgvComboBoxColumn.DisplayMember = "DisplayField";
            dgvComboBoxColumn.ValueMember = "ValueField";

            //定义colType质量原因 用不到
            //DataGridViewComboBoxColumn dgvComboBoxColumn1 = GridQuality.Columns["colType"] as DataGridViewComboBoxColumn;
            //dgvComboBoxColumn1.DataSource = QualityFactory.GetAllType();
            //dgvComboBoxColumn1.DisplayMember = "FAULT_NAME";

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
                //质量点的sn与计划一一对应 此处只返回一条计划
                _product = product;
                ShowQualityList();
                
            }
        }

        private void ShowQualityList()
        {
            //根据当前站点LoginInfo.StationInfo.RMES_ID找到rel_station_location对应工位code_location
            List<LocationEntity> Location = new List<LocationEntity>();
            List<WorkProcessEntity> process_plan=new List<WorkProcessEntity>();//站点对应的所有工序集合
            List<DetectItemEntity> detect_item = new List<DetectItemEntity>();//工序对应的检验条目集合
            Location = LocationFactory.GetByStationCode(LoginInfo.StationInfo.RMES_ID);
            if (Location == null)
            {
                return;
            }
            else
            {
                if (_product==null)
                return;
                
                //根据站点对应的工位从bom表中取出每个工位对应的工序以及质量检验标准
                for(int i=0;i<Location.Count;i++)
                {
                    //每个工位对应的工序集合
                    List<WorkProcessEntity> Q = PlanStandardBOMFactory.GetByLocationCode(LoginInfo.StationInfo.COMPANY_CODE, _product.PLAN_CODE, LoginInfo.StationInfo.PLINE_CODE, Location[i].LOCATION_CODE);
                    process_plan.AddRange(Q);
                }
            }

            //根据工序process_plan从bom导出的data_detect_item中获取检测项目
            if (process_plan == null) return;
            else
            {
                //存每个工序对应的质检项目data_detect_item
                for (int i = 0; i < process_plan.Count; i++)
                {
                    List<DetectItemEntity> Q = DetectItemFactory.GetByPlanProcessCode(LoginInfo.StationInfo.COMPANY_CODE, _product.PLAN_CODE, process_plan[i].PROCESS_CODE);
                    detect_item.AddRange(Q);
                }
            }
            //每个工序的检测项目detect_item
            GridQuality.DataSource = detect_item;
            //关联data_sn_quality  取工序最新状态
            for (int i = 0; i < detect_item.Count;i++ )
            {
                QualitySnItem S = QualityFactory.GetProductInfoByBatchProcessPlan(_product.SN,detect_item[i].LTXA1,_product.PLAN_CODE);
                if (S == null)
                { }
                else
                {
                    if (S.CurrentResult != "")
                        GridQuality.Rows[i].Cells["coldx"].Value = S.CurrentResult;
                    if(S.CurrentValue!="")
                        GridQuality.Rows[i].Cells["colCurrentValue"].Value = S.CurrentValue;
                    GridQuality.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                }
                //if (!string.IsNullOrWhiteSpace(detect_item[i].QUALITAT as string))
                //{
                //    GridQuality.Rows[i].Cells["coldx"].Value = detect_item[i].QUALITAT;
                //}
            }
            for (int j = 0; j < GridQuality.Rows.Count; j++)
            {
                GridQuality.Rows[j].Height = 30;
                //根据值改变grid每行属性
                //if (!string.IsNullOrWhiteSpace(GridQuality.Rows[j].Cells["colRmesId"].Value as string))
                //{
                //    GridQuality.Rows[j].ReadOnly = true;
                //    GridQuality.Rows[j].DefaultCellStyle.BackColor = Color.LightGray;
                //} 
            }

        }


        private void GridQuality_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (GridQuality.Rows[e.RowIndex].ReadOnly == true) return;
            //string fileName = "";
            if (e.ColumnIndex == 5)
            {
                //colUnitType检测类型
                //if (GridQuality.Rows[e.RowIndex].Cells["colUnitType"].Value.ToString() == "F")
                //{
                //    OpenFileDialog MyFileDialog = new OpenFileDialog();//创建打开对话框对象
                //    MyFileDialog.ShowDialog();//显示打开对话框
                //    if (MyFileDialog.FileName.Trim() != "")//判断是否选择了文件
                //    {
                //        FileStream FStream = new FileStream(MyFileDialog.FileName, FileMode.Open, FileAccess.Read);//创建FileStream对象
                //        string fileName = MyFileDialog.FileName;
                //       // BinaryReader BReader = new BinaryReader(FStream);//创建BinaryReader读取对象
                //        //byte[] bytes = BReader.ReadBytes((int)FStream.Length);//读取二进制图片
                //        GridQuality.Rows[e.RowIndex].Cells["colCurrentValue"].Value = fileName;
                //        //quality.CurrentValue = fileName;
                //        FStream.Close();//关闭数据流
                //    }
                //}
                //else
                //{
                //    if (!string.IsNullOrWhiteSpace(GridQuality.Rows[e.RowIndex].Cells["colCurrentValue"].Value.ToString()))
                //       = GridQuality.Rows[e.RowIndex].Cells["colCurrentValue"].Value.ToString();
                //}
            }
            if (e.ColumnIndex == 0) //工序完成录入
            {
                //是否区分该工序是定性还是定量 如果不区分 则全部录入
                QualitySnItem quality = new QualitySnItem();

                if (string.IsNullOrWhiteSpace(GridQuality.Rows[e.RowIndex].Cells["colCurrentValue"].Value as string) && string.IsNullOrWhiteSpace(GridQuality.Rows[e.RowIndex].Cells["coldx"].Value as string))
                {
                    MessageBox.Show("至少输入一种检测结果：定性或定量");
                    return;
                }

                quality.BatchNo = _product.SN;//批次号 SN
                quality.ProcessCode = string.IsNullOrWhiteSpace(GridQuality.Rows[e.RowIndex].Cells["colProcessCode"].Value as string) ?"":GridQuality.Rows[e.RowIndex].Cells["colProcessCode"].Value.ToString();//工序号
                quality.ItemCode = string.IsNullOrWhiteSpace(GridQuality.Rows[e.RowIndex].Cells["coljytxdm"].Value as string)?"":GridQuality.Rows[e.RowIndex].Cells["coljytxdm"].Value.ToString(); //检测项代码

                quality.ItemName = string.IsNullOrWhiteSpace(GridQuality.Rows[e.RowIndex].Cells["colItemDescription"].Value as string) ? "": GridQuality.Rows[e.RowIndex].Cells["colItemDescription"].Value.ToString();//检测项名称
                quality.ItemDescription = string.IsNullOrWhiteSpace(GridQuality.Rows[e.RowIndex].Cells["colcheckrequire"].Value as string) ?"":GridQuality.Rows[e.RowIndex].Cells["colcheckrequire"].Value.ToString();//检测要求
                if (GridQuality.Rows[e.RowIndex].Cells["colMaxValue"].Value == null)
                    quality.MaxValue = -1;
                else
                    quality.MaxValue = Convert.ToDouble(GridQuality.Rows[e.RowIndex].Cells["colMaxValue"].Value);//最大值
                if (GridQuality.Rows[e.RowIndex].Cells["colMinValue"].Value == null)
                    quality.MinValue = -1;
                else
                    quality.MinValue = Convert.ToDouble(GridQuality.Rows[e.RowIndex].Cells["colMinValue"].Value);//最小值

                 //判断定量输入值是否在范围内
                if (!string.IsNullOrWhiteSpace(GridQuality.Rows[e.RowIndex].Cells["colCurrentValue"].Value as string))
                {
                    double inputvalue = Convert.ToDouble(GridQuality.Rows[e.RowIndex].Cells["colCurrentValue"].Value);
                    if (inputvalue > quality.MaxValue || inputvalue < quality.MinValue)
                    {
                        MessageBox.Show("定量输入值不在预定范围内，请修改");
                        return;
                    }
                }
                quality.StandardValue = 0;//标准值
                
                //if (string.IsNullOrWhiteSpace(GridQuality.Rows[e.RowIndex].Cells["colCurrentValue"].Value as string)) { }
                quality.CurrentValue = string.IsNullOrWhiteSpace(GridQuality.Rows[e.RowIndex].Cells["colCurrentValue"].Value as string) ?"":GridQuality.Rows[e.RowIndex].Cells["colCurrentValue"].Value.ToString();//定量值
                
                //if (GridQuality.Rows[e.RowIndex].Cells["coldx"].Value == null) { }
                quality.CurrentResult = string.IsNullOrWhiteSpace(GridQuality.Rows[e.RowIndex].Cells["coldx"].Value as string) ? "" : GridQuality.Rows[e.RowIndex].Cells["coldx"].Value.ToString();//定性值

                quality.UnitName = "";//检测项单位
                quality.UnitType = "";//检测项类型
                quality.Ordering = "";//排序字符串
                quality.TIMESTAMP1 = "";//时间戳
                quality.WORK_TIME = System.DateTime.Now;//完工时间
                quality.USER_ID = LoginInfo.UserInfo.USER_ID;//操作员
                //if (string.IsNullOrWhiteSpace(GridQuality.Rows[e.RowIndex].Cells["colEquipment"].Value as string)) return;
                //quality.TEST_EQUIPMENT = GridQuality.Rows[e.RowIndex].Cells["colEquipment"].Value.ToString();//检测设备
                quality.TEST_EQUIPMENT = "";//检测设备
                quality.FAULT_TYPE = "";
                //QualityType type = QualityFactory.GetByTypeName(GridQuality.Rows[e.RowIndex].Cells["colType"].Value.ToString());
                //if (GridQuality.Rows[e.RowIndex].Cells["colType"].Value == null) { }
                //else
                //    quality.FAULT_TYPE = (GridQuality.Rows[e.RowIndex].Cells["colType"] as DataGridViewComboBoxCell).Value.ToString();//质量原因
             
                if (string.IsNullOrWhiteSpace(GridQuality.Rows[e.RowIndex].Cells["Remark"].Value as string))
                    quality.TEMP = "";//
                else
                    quality.TEMP = GridQuality.Rows[e.RowIndex].Cells["Remark"].Value.ToString();//备注

                quality.plan_code = _product.PLAN_CODE;//计划号
                quality.item_code = string.IsNullOrWhiteSpace(GridQuality.Rows[e.RowIndex].Cells["colmaterialname"].Value as string)?"": GridQuality.Rows[e.RowIndex].Cells["colmaterialname"].Value.ToString();//检验物料编码
                quality.detect_seqno = string.IsNullOrWhiteSpace(GridQuality.Rows[e.RowIndex].Cells["coljytxxh"].Value as string)?"": GridQuality.Rows[e.RowIndex].Cells["coljytxxh"].Value.ToString();//检验特性序号
                

                //if (quality.UnitType == "N" && quality.UnitType == "B")
                //    QualityFactory.SaveItemOfProduct(quality, true);
                //else QualityFactory.SaveItemOfProduct(quality, false);
                if (QualityFactory.SaveProductItem(quality))//插入成功显示绿色 不成功为红色
                    GridQuality.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                else
                    GridQuality.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                GridQuality.DataSource = new List<QualitySnItem>();
                ShowQualityList();
            }

        }


        private void GridQuality_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (e.ColumnIndex == -1) return;
            //if (GridQuality.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly == true) return;
            //string fileName = "";
            string ProcessCode = GridQuality.Rows[e.RowIndex].Cells["colProcessCode"].Value.ToString();//工序
            string materialName = GridQuality.Rows[e.RowIndex].Cells["colmaterialname"].Value.ToString();//检验物料编码
            string ItemName = GridQuality.Rows[e.RowIndex].Cells["coljytxdm"].Value.ToString();//检验特性代码
            //string ItemName = "";
            if (e.ColumnIndex == 1)
            {
                FrmDetectPart test = new FrmDetectPart(SN, ProcessCode, materialName,ItemName);
                test.ShowDialog();
            }
        }
        
        
    }
}
