using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.DA.Dal;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
namespace Rmes.DA.Factory
{
    public class LedProcFactory
    {
        AndonAlertDal dal = new AndonAlertDal();
        PlanDal dalPlan = new PlanDal();
        TeamDal dalTeam = new TeamDal();
        Thread td;
        string SourcePicturePath = System.Windows.Forms.Application.StartupPath;
        Graphics g;
        Bitmap a;
        Font Font1 = new Font("宋体", 25, FontStyle.Bold);
        Font Font2 = new Font("宋体", 16, FontStyle.Bold);
        Font Font3 = new Font("宋体", 14, FontStyle.Regular);//自动线工位字段
        Font Fonttime = new Font("宋体", 9, FontStyle.Regular);
        Brush Brush1 = new SolidBrush(Color.FromArgb(255, 0, 0));//red
        Brush Brush2 = new SolidBrush(Color.FromArgb(0, 255, 0));//green
        AndonAlertEntity AlertEntity;
        public LedProcFactory(AndonAlertEntity AEntity)
        {
            AlertEntity = AEntity;
        }

        //public LedProc()
        //{ }
        public void ThreadPic()
        {
            if (td == null)
                td = new Thread(this.CreatAndonPic);
            td.Start();
        }


        public string PicSource;
        // public string PicSave;
        private void CreatAndonPic()
        {
            a = new Bitmap(PicSource);
            g = Graphics.FromImage(a);
            string text = "";

            List<AndonAlertEntity> list = dal.GetAndon(AlertEntity.COMPANY_CODE, AlertEntity.PLINE_CODE);
            if (list.Count < 1)
            {
                text = "无andon信息";
                g.DrawString(text, Font1, Brush1, 20, 20);
            }
            else
            {
                int i = 0;
                foreach (AndonAlertEntity list1 in list)
                {
                    g.DrawString(list1.PLINE_CODE, Font1, Brush1, 20, 20 + i * 20);
                    g.DrawString(list1.ANDON_ALERT_CONTENT, Font1, Brush1, 40, 20 + i * 20);
                    g.DrawString(list1.ANDON_ALERT_TIME.ToString(), Font1, Brush1, 60, 20 + i * 20);
                    i++;
                    if (i == 5) break;
                }
            }
            g.Save();

            a.Save("1" + PicSource);
        }

        //public List<string> CreatPlanPic(string pline_code)
        //{
        //    //a = new Bitmap(Application.StartupPath + "");
        //    string[] _plines = pline_code.Split('^');
        //    if (_plines.Contains("MCPL0001"))
        //        a = new Bitmap(Application.StartupPath + @"\led图片\总装二.bmp");
        //    else if (_plines.Contains("MCPL0002"))
        //        a = new Bitmap(Application.StartupPath + @"\led图片\自动线.bmp");
        //    else if (_plines.Contains("MCPL0003"))
        //        a = new Bitmap(Application.StartupPath + @"\led图片\装二灭弧室.bmp");
        //    else if (_plines.Contains("MCPL0004"))
        //        a = new Bitmap(Application.StartupPath + @"\led图片\装二灭弧室.bmp");
        //    else if (_plines.Contains("MCPL0005"))
        //        a = new Bitmap(Application.StartupPath + @"\led图片\装二灭弧室.bmp");
        //    else if (_plines.Contains("MCPL0011"))
        //        a = new Bitmap(Application.StartupPath + @"\led图片\总装一.bmp");
        //    else if (_plines.Contains("MCPL0012"))
        //        a = new Bitmap(Application.StartupPath + @"\led图片\总装一.bmp");
        //    else if (_plines.Contains("MCPL0013"))
        //        a = new Bitmap(Application.StartupPath + @"\led图片\总装一.bmp");
        //    else if (_plines.Contains("MCPL0014"))
        //        a = new Bitmap(Application.StartupPath + @"\led图片\总装一.bmp");
        //    else if (_plines.Contains("MCPL0015"))
        //        a = new Bitmap(Application.StartupPath + @"\led图片\总装一.bmp");
        //    else if (_plines.Contains("MCPL0031"))
        //        a = new Bitmap(Application.StartupPath + @"\led图片\总装三.bmp");
        //    else if (_plines.Contains("MCPL0032"))
        //        a = new Bitmap(Application.StartupPath + @"\led图片\装三灭弧室.bmp");
        //    else if (_plines.Contains("MCPL0033"))
        //        a = new Bitmap(Application.StartupPath + @"\led图片\装三灭弧室.bmp");
        //    else if (_plines.Contains("MCPL0034"))
        //        a = new Bitmap(Application.StartupPath + @"\led图片\装三灭弧室.bmp");
        //    else if (_plines.Contains("MCPL0035"))
        //        a = new Bitmap(Application.StartupPath + @"\led图片\装三灭弧室.bmp");
        //    // a = new Bitmap(@"E:\总装.bmp");
        //    g = Graphics.FromImage(a);
        //    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //    string text = "";
        //    List<string> strsave = new List<string>();
        //    List<PlanEntity> listPlan = PlanFactory.GetByLED(pline_code);
        //    DateTime Mutime = Rmes.DA.Base.DB.GetServerTime();
        //    string time = Mutime.ToString("yyyy.MM.dd  HH:mm");
            
        //    if (listPlan.Count < 1)
        //    {
        //        if (_plines.Contains("MCPL0001"))
        //        {
        //            text = "无计划信息";
        //            g.DrawString(text, Font1, Brush1, 10, 80);
        //            g.DrawString(time, Fonttime, Brush1, 850, 10);
        //            // a.Save(@"E:\总装1.bmp");
        //            a.Save(Application.StartupPath + @"\led图片\总装二a.bmp");
        //            strsave.Add(Application.StartupPath + @"\led图片\总装二a.bmp");
        //            // strsave.Add("E:\\总装1.bmp");
        //        }
        //        else if (_plines.Contains("MCPL0002"))
        //        {
        //            text = "无计划信息";
        //            g.DrawString(text, Font1, Brush1, 10, 80);
        //            g.DrawString(time, Fonttime, Brush1, 850, 10);
        //            // a.Save(@"E:\总装1.bmp");
        //            a.Save(Application.StartupPath + @"\led图片\自动线a.bmp");
        //            strsave.Add(Application.StartupPath + @"\led图片\自动线a.bmp");
        //        }
        //        else if (_plines.Contains("MCPL0003") || _plines.Contains("MCPL0004") || _plines.Contains("MCPL0005"))
        //        {
        //            text = "无计划信息";
        //            g.DrawString(text, Font1, Brush1, 10, 80);
        //            g.DrawString(time, Fonttime, Brush1, 850, 10);
        //            // a.Save(@"E:\总装1.bmp");
        //            a.Save(Application.StartupPath + @"\led图片\装二灭弧室a.bmp");
        //            strsave.Add(Application.StartupPath + @"\led图片\装二灭弧室a.bmp");
        //        }
        //        else if (_plines.Contains("MCPL0011") || _plines.Contains("MCPL0012") || _plines.Contains("MCPL0013") || _plines.Contains("MCPL0014") || _plines.Contains("MCPL0015"))
        //        {
        //            text = "无计划信息";
        //            g.DrawString(text, Font1, Brush1, 10, 80);
        //            g.DrawString(time, Fonttime, Brush1, 850, 10);
        //            // a.Save(@"E:\总装1.bmp");
        //            a.Save(Application.StartupPath + @"\led图片\总装一a.bmp");
        //            strsave.Add(Application.StartupPath + @"\led图片\总装一a.bmp");
        //        }
        //        else if (_plines.Contains("MCPL0031"))
        //        {
        //            text = "无计划信息";
        //            g.DrawString(text, Font1, Brush1, 10, 80);
        //            g.DrawString(time, Fonttime, Brush1, 850, 10);
        //            // a.Save(@"E:\总装1.bmp");
        //            a.Save(Application.StartupPath + @"\led图片\总装三a.bmp");
        //            strsave.Add(Application.StartupPath + @"\led图片\总装三a.bmp");
        //        }
        //        else if (_plines.Contains("MCPL0032") || _plines.Contains("MCPL0033") || _plines.Contains("MCPL0034") || _plines.Contains("MCPL0035"))
        //        {
        //            text = "无计划信息";
        //            g.DrawString(text, Font1, Brush1, 10, 80);
        //            g.DrawString(time, Fonttime, Brush1, 850, 10);
        //            // a.Save(@"E:\总装1.bmp");
        //            a.Save(Application.StartupPath + @"\led图片\装三灭弧室a.bmp");
        //            strsave.Add(Application.StartupPath + @"\led图片\装三灭弧室a.bmp");
        //        }
        //        return strsave;
        //    }
        //    else
        //    {
        //        if (_plines.Contains("MCPL0001") || _plines.Contains("MCPL0011") || _plines.Contains( "MCPL0012") || _plines.Contains( "MCPL0013") || _plines.Contains("MCPL0014" )||_plines.Contains( "MCPL0015") || _plines.Contains( "MCPL0031"))
        //        {
        //            int i = 0;
        //            int n = 0;
        //            foreach (PlanEntity list1 in listPlan)
        //            {

        //                TeamEntity TeamEn = dalTeam.GetByKey(list1.TEAM_CODE);
        //                string runflag = "";
        //                //switch (list1.RUN_FLAG)
        //                //{

        //                //    case "Y":
        //                //        {
        //                //            runflag = "执行";
        //                //        }
        //                //        break;
        //                //    case "P":
        //                //        {
        //                //            runflag = "暂停";
        //                //        }
        //                //        break;
        //                //    case "F":
        //                //        {
        //                //            runflag = "完成";
        //                //        }
        //                //        break;
        //                //    case "N":
        //                //        {
        //                //            if (list1.ITEM_FLAG=="B")
        //                //            {
        //                //                    runflag="要料";
        //                //            }
        //                //        }
        //                //    default:
        //                //        runflag = "未知";
        //                //        break;
        //                //}
        //                if (list1.RUN_FLAG == "Y")
        //                    runflag = "执行";
        //                else if (list1.RUN_FLAG == "P")
        //                    runflag = "暂停";
        //                else if (list1.RUN_FLAG == "F")
        //                    runflag = "完成";
        //                else if (list1.RUN_FLAG == "N" && list1.ITEM_FLAG == "B")
        //                    runflag = "要料";
        //                else if (list1.RUN_FLAG == "N" && list1.ITEM_FLAG == "Y")
        //                    runflag = "收料";
        //                else runflag = "未知";
        //                if (list1.PLAN_SO_NAME.Length > 8)
        //                    list1.PLAN_SO_NAME = list1.PLAN_SO_NAME.Substring(0, 8);
        //                if (TeamEn.TEAM_NAME.Length > 5)
        //                    TeamEn.TEAM_NAME = TeamEn.TEAM_NAME.Substring(0, 4);


        //                g.DrawString(TeamEn.TEAM_NAME, Font1, Brush1, 0, 65 + i * 30);
        //                g.DrawString(list1.PROJECT_CODE.ToString(), Font1, Brush1, 110, 65 + i * 30);
        //                g.DrawString(list1.PRODUCT_MODEL, Font1, Brush1, 230, 65 + i * 30);
        //                //g.DrawString(list1.PLAN_SO, Font1, Brush1, 290, 65 + i * 30);
        //                g.DrawString(list1.PLAN_SO_NAME, Font1, Brush1, 360, 65 + i * 30);
        //                g.DrawString(list1.BEGIN_DATE.ToString("MM/dd"), Font1, Brush1, 640, 65 + i * 30);
        //                g.DrawString(list1.END_DATE.ToString("MM/dd"), Font1, Brush1, 760, 65 + i * 30);
        //                g.DrawString(runflag, Font1, Brush1, 900, 65 + i * 30);
        //                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
        //                i++;
        //                g.DrawString(time, Fonttime, Brush1, 850, 10);

        //                if (_plines.Contains( "MCPL0001"))
        //                {
        //                    if (i == 10)
        //                    {
        //                        g.Save();
        //                        a.Save(Application.StartupPath + @"\led图片\总装二" + n.ToString() + ".bmp");
        //                        strsave.Add(Application.StartupPath + @"\led图片\总装二" + n.ToString() + ".bmp");
        //                        i = 0; n++;
        //                        g.Dispose();
        //                        g = null;
        //                        a.Dispose();
        //                        a = null;
        //                        a = new Bitmap(Application.StartupPath + @"\led图片\总装二.bmp");
        //                        g = Graphics.FromImage(a);
        //                    }
        //                    else if (i == (listPlan.Count % 10) && n == listPlan.Count / 10)
        //                    {
        //                        g.Save();
        //                        a.Save(Application.StartupPath + @"\led图片\总装二" + n.ToString() + ".bmp");
        //                        strsave.Add(Application.StartupPath + @"\led图片\总装二" + n.ToString() + ".bmp");
        //                    }
        //                }
        //                else if (_plines.Contains("MCPL0011")||_plines.Contains("MCPL0012") || _plines.Contains("MCPL0013") || _plines.Contains("MCPL0014") || _plines.Contains("MCPL0015"))

        //                {
        //                    if (i == 10)
        //                    {
        //                        g.Save();
        //                        a.Save(Application.StartupPath + @"\led图片\总装一" + n.ToString() + ".bmp");
        //                        strsave.Add(Application.StartupPath + @"\led图片\总装一" + n.ToString() + ".bmp");
        //                        i = 0; n++;
        //                        g.Dispose();
        //                        g = null;
        //                        a.Dispose();
        //                        a = null;
        //                        a = new Bitmap(Application.StartupPath + @"\led图片\总装一.bmp");
        //                        g = Graphics.FromImage(a);
        //                    }
        //                    else if (i == (listPlan.Count % 10)  && n == listPlan.Count / 10)
        //                    {
        //                        g.Save();
        //                        a.Save(Application.StartupPath + @"\led图片\总装一" + n.ToString() + ".bmp");
        //                        strsave.Add(Application.StartupPath + @"\led图片\总装一" + n.ToString() + ".bmp");
        //                    }
        //                }
        //                //else if (_plines.Contains("MCPL0012") || _plines.Contains("MCPL0013") || _plines.Contains("MCPL0014") || _plines.Contains("MCPL0015"))

        //                //{
        //                //    if (i == 10)
        //                //    {
        //                //        g.Save();
        //                //        a.Save(Application.StartupPath + @"\led图片\老总装一" + n.ToString() + ".bmp");
        //                //        strsave.Add(Application.StartupPath + @"\led图片\老总装一" + n.ToString() + ".bmp");
        //                //        i = 0; n++;
        //                //        g.Dispose();
        //                //        g = null;
        //                //        a.Dispose();
        //                //        a = null;
        //                //        a = new Bitmap(Application.StartupPath + @"\led图片\总装一.bmp");
        //                //        g = Graphics.FromImage(a);
        //                //    }
        //                //    else if (i == (listPlan.Count % 10) && n == listPlan.Count / 10)
        //                //    {
        //                //        g.Save();
        //                //        a.Save(Application.StartupPath + @"\led图片\老总装一" + n.ToString() + ".bmp");
        //                //        strsave.Add(Application.StartupPath + @"\led图片\老总装一" + n.ToString() + ".bmp");
        //                //    }
        //                //}
        //                else if (_plines.Contains("MCPL0031"))
        //                {
        //                    if (i == 10)
        //                    {
        //                        g.Save();
        //                        a.Save(Application.StartupPath + @"\led图片\总装三" + n.ToString() + ".bmp");
        //                        strsave.Add(Application.StartupPath + @"\led图片\总装三" + n.ToString() + ".bmp");
        //                        i = 0; n++;
        //                        g.Dispose();
        //                        g = null;
        //                        a.Dispose();
        //                        a = null;
        //                        a = new Bitmap(Application.StartupPath + @"\led图片\总装三.bmp");
        //                        g = Graphics.FromImage(a);
        //                    }
        //                    else if (i == (listPlan.Count % 10)  && n == listPlan.Count / 10)
        //                    {
        //                        g.Save();
        //                        a.Save(Application.StartupPath + @"\led图片\总装三" + n.ToString() + ".bmp");
        //                        strsave.Add(Application.StartupPath + @"\led图片\总装三" + n.ToString() + ".bmp");
        //                    }
        //                }
        //            }
                    
        //        }
        //        else if (_plines.Contains("MCPL0002"))
        //        {
        //            int i = 0;
        //            int n = 0;
        //            foreach (PlanEntity list1 in listPlan)
        //            {

        //                TeamEntity TeamEn = dalTeam.GetByKey(list1.TEAM_CODE);
        //                string runflag = "";

        //                if (list1.RUN_FLAG == "Y")
        //                    runflag = "执行";
        //                else if (list1.RUN_FLAG == "P")
        //                    runflag = "暂停";
        //                else if (list1.RUN_FLAG == "F")
        //                    runflag = "完成";
        //                else if (list1.RUN_FLAG == "N" && list1.ITEM_FLAG == "B")
        //                    runflag = "要料";
        //                else if (list1.RUN_FLAG == "N" && list1.ITEM_FLAG == "Y")
        //                    runflag = "收料";
        //                else runflag = "未知";
        //                if (list1.PLAN_SO_NAME.Length > 8)
        //                    list1.PLAN_SO_NAME = list1.PLAN_SO_NAME.Substring(0, 8);
        //                if (TeamEn.TEAM_NAME.Length > 5)
        //                    TeamEn.TEAM_NAME = TeamEn.TEAM_NAME.Substring(0, 5);


        //                g.DrawString(TeamEn.TEAM_NAME, Font2, Brush1, 0, 40 + i * 18);
        //                g.DrawString(list1.PROJECT_CODE.ToString(), Font2, Brush1, 100, 40 + i * 18);
        //                //g.DrawString(list1.PRODUCT_MODEL, Font2, Brush1, 115, 65 + i * 18);
        //                //g.DrawString(list1.PLAN_SO, Font1, Brush1, 290, 65 + i * 30);
        //                g.DrawString(list1.PLAN_SO_NAME, Font2, Brush1, 210, 40 + i * 18);
        //                //g.DrawString(list1.BEGIN_DATE.ToString("MM/dd"), Font2, Brush1, 320, 65 + i * 18);
        //                // g.DrawString(list1.END_DATE.ToString("MM/dd"), Font2, Brush1, 380, 65 + i * 18);
        //                g.DrawString(runflag, Font2, Brush1, 385, 40 + i * 18);
        //                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
        //                i++;
        //                g.DrawString(time, Fonttime, Brush1, 380, 10);
        //                List<OPCStationEntity> OPCStations = OPCMessageFactory.GetOPCStations();
        //                //工位画在相应位置
        //                for (int j = 0; j < OPCStations.Count; j++)//每个工位对应的不同状态
        //                {
        //                    AndonAlertEntity andon = AndonFactory.GetAutoLine(OPCStations[j].RMES_ID);
        //                    g.DrawString(andon.LOCATION_CODE, Font3, Brush1, 10 + 48 * j, 155);//位置没调好
        //                    if (andon.ANDON_ALERT_CONTENT == "3")//green警报
        //                    {
        //                        g.DrawImage(Rmes.Public.Resource.green, 10 + j * 48, 170);
        //                    }
        //                        else if (andon.ANDON_ALERT_CONTENT=="2")//yellow报警
        //                        {
        //                            g.DrawImage(Rmes.Public.Resource.yellow, 10 + j * 48, 170);
        //                        }
        //                            else if (andon.ANDON_ALERT_CONTENT=="1")//red故障
        //                            {
        //                                g.DrawImage(Rmes.Public.Resource.red, 10 + j * 48, 170);
        //                            }
        //                }

        //                if (_plines.Contains("MCPL0002"))
        //                {
        //                    if (i == 5)
        //                    {
        //                        g.Save();
        //                        a.Save(Application.StartupPath + @"\led图片\自动线" + n.ToString() + ".bmp");
        //                        strsave.Add(Application.StartupPath + @"\led图片\自动线" + n.ToString() + ".bmp");
        //                        i = 0; n++;
        //                        g.Dispose();
        //                        g = null;
        //                        a.Dispose();
        //                        a = null;
        //                        a = new Bitmap(Application.StartupPath + @"\led图片\自动线.bmp");
        //                        g = Graphics.FromImage(a);
        //                    }
        //                    else if (i == (listPlan.Count % 7) && n == listPlan.Count / 7)
        //                    {
        //                        g.Save();
        //                        a.Save(Application.StartupPath + @"\led图片\自动线" + n.ToString() + ".bmp");
        //                        strsave.Add(Application.StartupPath + @"\led图片\自动线" + n.ToString() + ".bmp");
        //                    }
        //                }
        //            }
                        
 
        //        }
        //        else if ( _plines.Contains("MCPL0003") || _plines.Contains("MCPL0004") || _plines.Contains("MCPL0005") || _plines.Contains("MCPL0032") || _plines.Contains("MCPL0033") || _plines.Contains("MCPL0034") || _plines.Contains("MCPL0035"))
        //        {
        //            int i = 0;
        //            int n = 0;
        //            foreach (PlanEntity list1 in listPlan)
        //            {

        //                TeamEntity TeamEn = dalTeam.GetByKey(list1.TEAM_CODE);
        //                string runflag = "";

        //                if (list1.RUN_FLAG == "Y")
        //                    runflag = "执行";
        //                else if (list1.RUN_FLAG == "P")
        //                    runflag = "暂停";
        //                else if (list1.RUN_FLAG == "F")
        //                    runflag = "完成";
        //                else if (list1.RUN_FLAG == "N" && list1.ITEM_FLAG == "B")
        //                    runflag = "要料";
        //                else if (list1.RUN_FLAG == "N" && list1.ITEM_FLAG == "Y")
        //                    runflag = "收料";
        //                else runflag = "未知";
        //                if (list1.PLAN_SO_NAME.Length > 8)
        //                    list1.PLAN_SO_NAME = list1.PLAN_SO_NAME.Substring(0, 8);
        //                if (TeamEn.TEAM_NAME.Length > 5)
        //                    TeamEn.TEAM_NAME = TeamEn.TEAM_NAME.Substring(0, 5);


        //                g.DrawString(TeamEn.TEAM_NAME, Font2, Brush1, 0, 40 + i * 18);
        //                g.DrawString(list1.PROJECT_CODE.ToString(), Font2, Brush1, 100, 40 + i * 18);
        //                //g.DrawString(list1.PRODUCT_MODEL, Font2, Brush1, 115, 65 + i * 18);
        //                //g.DrawString(list1.PLAN_SO, Font1, Brush1, 290, 65 + i * 30);
        //                g.DrawString(list1.PLAN_SO_NAME, Font2, Brush1, 210, 40 + i * 18);
        //                //g.DrawString(list1.BEGIN_DATE.ToString("MM/dd"), Font2, Brush1, 320, 65 + i * 18);
        //                // g.DrawString(list1.END_DATE.ToString("MM/dd"), Font2, Brush1, 380, 65 + i * 18);
        //                g.DrawString(runflag, Font2, Brush1, 385, 40 + i * 18);
        //                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
        //                i++;
        //                g.DrawString(time, Fonttime, Brush1, 380, 10);

        //                if (_plines.Contains("MCPL0002"))
        //                {
        //                    if (i == 7)
        //                    {
        //                        g.Save();
        //                        a.Save(Application.StartupPath + @"\led图片\自动线" + n.ToString() + ".bmp");
        //                        strsave.Add(Application.StartupPath + @"\led图片\自动线" + n.ToString() + ".bmp");
        //                        i = 0; n++;
        //                        g.Dispose();
        //                        g = null;
        //                        a.Dispose();
        //                        a = null;
        //                        a = new Bitmap(Application.StartupPath + @"\led图片\自动线.bmp");
        //                        g = Graphics.FromImage(a);
        //                    }
        //                    else if (i == (listPlan.Count % 7) && n == listPlan.Count / 7)
        //                    {
        //                        g.Save();
        //                        a.Save(Application.StartupPath + @"\led图片\自动线" + n.ToString() + ".bmp");
        //                        strsave.Add(Application.StartupPath + @"\led图片\自动线" + n.ToString() + ".bmp");
        //                    }
        //                }
        //                else if (_plines.Contains("MCPL0003") || _plines.Contains("MCPL0004") || _plines.Contains("MCPL0005"))
        //                {
        //                    if (i == 7)
        //                    {
        //                        g.Save();
        //                        a.Save(Application.StartupPath + @"\led图片\装二灭弧室" + n.ToString() + ".bmp");
        //                        strsave.Add(Application.StartupPath + @"\led图片\装二灭弧室" + n.ToString() + ".bmp");
        //                        i = 0; n++;
        //                        g.Dispose();
        //                        g = null;
        //                        a.Dispose();
        //                        a = null;
        //                        a = new Bitmap(Application.StartupPath + @"\led图片\装二灭弧室.bmp");
        //                        g = Graphics.FromImage(a);
        //                    }
        //                    else if (i == (listPlan.Count % 7) && n == listPlan.Count / 7)
        //                    {
        //                        g.Save();
        //                        a.Save(Application.StartupPath + @"\led图片\装二灭弧室" + n.ToString() + ".bmp");
        //                        strsave.Add(Application.StartupPath + @"\led图片\装二灭弧室" + n.ToString() + ".bmp");
        //                    }
        //                }
        //                else if (_plines.Contains("MCPL0032") || _plines.Contains("MCPL0033") || _plines.Contains("MCPL0034") || _plines.Contains("MCPL0035"))
        //                {
        //                    if (i == 7)
        //                    {
        //                        g.Save();
        //                        a.Save(Application.StartupPath + @"\led图片\装三灭弧室" + n.ToString() + ".bmp");
        //                        strsave.Add(Application.StartupPath + @"\led图片\装三灭弧室" + n.ToString() + ".bmp");
        //                        i = 0; n++;
        //                        g.Dispose();
        //                        g = null;
        //                        a.Dispose();
        //                        a = null;
        //                        a = new Bitmap(Application.StartupPath + @"\led图片\装三灭弧室.bmp");
        //                        g = Graphics.FromImage(a);
        //                    }
        //                    else if (i == (listPlan.Count % 7) && n == listPlan.Count / 7)
        //                    {
        //                        g.Save();
        //                        a.Save(Application.StartupPath + @"\led图片\装三灭弧室" + n.ToString() + ".bmp");
        //                        strsave.Add(Application.StartupPath + @"\led图片\装三灭弧室" + n.ToString() + ".bmp");
        //                    }
        //                }
        //            }

        //        }
        //        return strsave;
        //    }
        //}
    }
}
