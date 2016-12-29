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
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Public;
using System.Threading;


namespace Rmes.WinForm.Controls
{
    public partial class ctrl_BroadCast : BaseControl
    {
        //RSpeechLib lib = new RSpeechLib();
        private Speech theSpeech;
        Thread workerThread;
        bool stopThread = false;
        List<string> RMESID = new List<string>();
        
        public ctrl_BroadCast()
        {
            InitializeComponent();
            //生产线信息
            ViewPline.View = View.Details;
            ViewPline.CheckBoxes = true;
            ViewPline.Columns.Add("  生产线名称",300, HorizontalAlignment.Right);
            ViewPline.Columns.Add("生产线代码", 200, HorizontalAlignment.Left);
           // ViewPline.Columns.Add("REMS_ID", 200, HorizontalAlignment.Left);
            //语音信息

            timer1.Interval = 5000;
            //axWindowsMediaPlayer1.settings.autoStart = true;
            //timer1.Start();
            Init();
        }
        private void ctrl_BroadCast_Load(object sender, EventArgs e)
        {
            this.SelectPline();
            workerThread = new Thread(this.doWork);
            workerThread.Start();
        }
        //private void Init()
        //{
           
        //    List<AndonAlertEntity> andon = AndonFactory.GetByTime();
        //    for (int i = 0; i < andon.Count; i++)
        //    {
        //        if (GridAndon.Rows.Count < andon.Count)
        //            GridAndon.Rows.Add();
        //        StationEntity Stationen = StationFactory.GetByPrimaryKey(andon[i].LOCATION_CODE);
        //        LocationEntity location = LocationFactory.GetByMasterKey(Stationen.RMES_ID);
        //        GridAndon.Rows[i].Cells[0].Value = andon[i].ANDON_ALERT_TIME.ToString("MM/dd/HH:mm");
        //        GridAndon.Rows[i].Cells[1].Value = Stationen.STATION_NAME;
        //       // GridAndon.Rows[i].Cells[2].Value = location.LOCATION_NAME;
        //        GridAndon.Rows[i].Cells[3].Value = andon[i].ANDON_ALERT_CONTENT.Substring(3, 4);
        //    }
        //}
        private void Init()
        {
            //textBox.Text = "123";
            List<AndonAlertEntity> andon = AndonFactory.GetByTime();
            string string1 = "";//, string2, string3;
            string string2 = "";
            string string3 = "";
            string string4 = "";
            string str1 = "";
            string[] str = new string[] { "", "","" };
            for (int i = 0; i < andon.Count; i++)
            {
               
                StationEntity Stationen = StationFactory.GetByPrimaryKey(andon[i].LOCATION_CODE);
                LocationEntity location = LocationFactory.GetByMasterKey(Stationen.RMES_ID);
                TeamEntity team = TeamFactory.GetByTeamCode(andon[i].TEAM_CODE);
                 string1 = andon[i].ANDON_ALERT_TIME.ToString("MM/dd/HH:mm");
                 string2 = Stationen.STATION_NAME;
                 string3 = team.TEAM_NAME;
                 string4 = andon[i].ANDON_ALERT_CONTENT.ToString();
                 str[i] = string1 +" ,"+ string2 + string3+string4;
                
                str1=str1 +str[i]+"\r\n";
                textBox.Text = str1;
            }
           
           
        }
        public void doWork()
        {
            while (!stopThread)
            {

                for (int i = 0; i < RMESID.Count; i++)
                {
                    //RMESID[i] = item1[i].SubItems[2].Text;
                    this.musicplay();
                    List<AndonAlertEntity> AEntity = AndonFactory.GetByPline(RMESID[i]);
                  
                    //{ axWindowsMediaPlayer1.settings.volume = 5; }
                    //else axWindowsMediaPlayer1.settings.volume = 60;
                   
                }
                this.BroadCasting();
                // MessageBox.Show("Hello, Chen Jun");
                Thread.Sleep(5000);
                //this.requeststop();
            }

            // Thread.Sleep(5000);
        }
        public void SelectPline()
        {
            Workshop_PlineEntity pline = Workshop_PlineFactory.GetByPline(LoginInfo.StationInfo.PLINE_CODE);
            List<ProductLineEntity> listline = ProductLineFactory.GetByWorkShopID(pline.WORKSHOP_CODE);
            foreach (ProductLineEntity LineEn in listline)
            {
                string[] str = new string[] { LineEn.PLINE_NAME, LineEn.PLINE_CODE, LineEn.RMES_ID };
                ListViewItem Item = new ListViewItem(str);

                ViewPline.Items.Add(Item);
            }

        }
        
      

        private void timer1_Tick(object sender, EventArgs e)
        {
            Init();   
        }

        private void ViewPline_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ViewPline_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            string _plinermesid = e.Item.SubItems[2].Text;
            if (e.Item.Checked)
                RMESID.Add(_plinermesid);
            else
                if (RMESID.Contains(_plinermesid))
                    RMESID.Remove(_plinermesid);
        }
        private void strToVoice(string val)
        {

            GC.Collect();

            theSpeech = new Speech();
            theSpeech.txtToSpeech(val);
            theSpeech = null;
            //Speech.txtToSpeech(val);
            GC.Collect();

        }
        public void BroadCasting()
        {
          

          
            for (int i = 0; i < RMESID.Count; i++)
            {
                //RMESID[i] = item1[i].SubItems[2].Text;

                List<AndonAlertEntity> AEntity = AndonFactory.GetByPline(RMESID[i]);
                foreach (AndonAlertEntity en in AEntity)
                {
                    BroadCastFactory cast = new BroadCastFactory(en);
                    List<string> listcall = cast.speaking();
                    if (listcall != null)
                    {
                        foreach (string sss in listcall)
                        {
                            strToVoice(sss);
                            strToVoice(sss);
                            //lib.Volume = 100;
                        }
                        en.REPORT_FLAG = "N";
                        AndonFactory.Update(en);

                    }
                    else continue;
                }
            }
        }



        public void musicplay()
        {

            DateTime Mutime = Rmes.DA.Base.DB.GetServerTime();
            string time = Mutime.ToString("HH:mm");
            //string hour = Mutime.Hour.ToString();
            //string munite = Mutime.Minute.ToString();
            // string time = hour + ":" + munite;
            //MessageBox.Show(time );


            List<MusicEntity> Mlist = MusicFactory.GetAllMusic();

            for (int i = 0; i < Mlist.Count; i++)
            {
                if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
                    return;
                //if (lib.MP3isPlaying == true)
                //    return;
              if  (time == Mlist[i].ANDON_TIME)
                {
                    axWindowsMediaPlayer1.URL = Mlist[i].ANDON_MUSIC;
                    //lib.MP3PlayFile(Mlist[i].ANDON_MUSIC);
                    //axWindowsMediaPlayer1.Ctlcontrols.play();
                }
            }
        }

       

      

     
       
    }
}
