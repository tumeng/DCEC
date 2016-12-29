using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using PetaPoco;
using System.Configuration;
using Rmes.DA.Base;
using Rmes.WinForm.Base;
using System.Drawing;

namespace Rmes.WinForm.Base
{
    public class FormAppContext : ApplicationContext
    {
        private BaseForm _mainform;
        public FormAppContext()
        {
            try
            {
                Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
                Rmes.WinForm.Base.frmLogin form = new Rmes.WinForm.Base.frmLogin();
                form.FormClosing += new FormClosingEventHandler(form_FormClosing);
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化主窗体失败：\r\n" + ex.Message, "系统发生运行问题");
                this.ExitThread();
            }

        }

        void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (((Form)sender).DialogResult != DialogResult.OK)
                this.ExitThread();
            else
            {
                try
                {
                    DB.DataConnectionName = "oracle";
                    //Database db = DB.GetInstance();
                    List<ConfigFormEntity> formconfig = BaseControlFactory.GetFormConfigFromStationCode(LoginInfo.StationInfo.RMES_ID);
                    if (formconfig.Count < 1)
                    {
                        MessageBox.Show("该站点没有绑定任何执行窗体，请确认本地设置是否正确。");
                        e.Cancel = true;
                    }
                    else
                    {
                        Rectangle rect = new Rectangle();

                        rect = Screen.PrimaryScreen.Bounds;

                        string formID = "";
                        if (rect.Width >= 2000)
                        {
                            MessageBox.Show("请调整页面分辨率标准大小1024*768。");
                            e.Cancel = true;
                            return;
                            //formID = "FORM_MAN_ZONE";
                        }
                        else
                        {
                            //formID = "FORM_MAN_ZONE";
                            formID = formconfig[0].FORMID;
                        }

                        List<ConfigFormEntity> _formconfig = BaseControlFactory.GetFormConfigFromFormID(formID);

                        List<ConfigControlsEntity> configs = BaseControlFactory.GetFormControlConfigByFormID(formID);

                        //List<ConfigControlsEntity> configs = BaseControlFactory.GetFormControlConfigByFormID(formconfig.First().FORMID);
                        _mainform = Rmes.WinForm.Base.UiFactory.GetMainFormInstance(_formconfig.First(), configs.ToList<ConfigControlsEntity>());
                        _mainform.Text = LoginInfo.CompanyInfo.COMPANY_NAME_BRIEF + "MES系统";
                        _mainform.FormClosed += new FormClosedEventHandler(_mainform_FormClosed);
                        _mainform.FormClosing += new FormClosingEventHandler(_mainform_FormClosing);
                        _mainform.StartPosition = FormStartPosition.CenterScreen;
                        _mainform.Show();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "系统初始化产生问题！");
                    this.ExitThread();
                }
            }
        }

        void _mainform_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确认要退出系统吗？", "退出提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                RMESEventArgs args = new RMESEventArgs();
                args.MessageHead = "EXIT";
                (this._mainform.Controls[0] as BaseControl).SendDataChangeMessage(args);
                GC.Collect();
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        void _mainform_FormClosed(object sender, FormClosedEventArgs e)
        {

            this.ExitThread();
        }

        void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (_mainform != null)
                _mainform.Close();
        }
    }
}
