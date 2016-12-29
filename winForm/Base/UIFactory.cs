using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;
using System.Reflection;
using Rmes.DA.Base;
using System.Windows.Forms;

namespace Rmes.WinForm.Base
{
    public static class UiFactory
    { 
        static BaseForm mainform;
        static List<BaseControl> _controls;
        public static BaseForm GetMainFormInstance(ConfigFormEntity FormConfig, List<ConfigControlsEntity> ControlConfigs)
        {
            if (mainform == null) 
            {
                object _form = null;
                if (FormConfig.AssembleFile ==null && FormConfig.AssembleFile.Equals(string.Empty))
                {
                    string[] _t = FormConfig.AssembleString.Split('.');
                    _form = System.Reflection.Assembly.Load(_t[0]).CreateInstance(FormConfig.AssembleString);
                }
                else
                {
                    _form = System.Reflection.Assembly.LoadFrom(FormConfig.AssembleFile).CreateInstance(FormConfig.AssembleString);
                }
                mainform = _form as BaseForm;
                if (mainform == null)
                    throw new Exception("Init MainForm failed, please check your config;");
                else
                    mainform.LoadConfig(FormConfig);
                loadControls(ControlConfigs);
            }
            return mainform;
        }
        public static BaseForm GetMainFormInstance()
        {
            if (mainform == null)
                throw new Exception("MainForm is NULL! You should use another CREATE FUNCTION first.");
            return mainform;
        }
        private static void loadControls(List<ConfigControlsEntity> ControlConfigs)
        {
            if (ControlConfigs != null && ControlConfigs.Count() > 0)
            {
                _controls = new List<BaseControl>();
                foreach (ConfigControlsEntity config in ControlConfigs)
                {
                    setControl(config);
                }
                mainform.Controls.AddRange(_controls.ToArray());
            }
            //CallDataChanged(mainform, new RMESEventArgs() { MessageHead="APP_INITED", MessageBody=null });
        }
        private static void setControl(ConfigControlsEntity c)
        {
            if (_controls != null)
            {
                object controlobj;
                try
                {
                    if (string.IsNullOrWhiteSpace(c.AssembleFile))
                    {
                        string[] _t = c.AssembleString.Split('.');
                        controlobj = System.Reflection.Assembly.Load(_t[0]).CreateInstance(c.AssembleString);
                    }
                    else
                    {
                        controlobj = System.Reflection.Assembly.LoadFrom(c.AssembleFile).CreateInstance(c.AssembleString);
                    }
                    if (controlobj == null)
                    {
                        controlobj = new ctrlBaseDebug();
                        string msg = string.Format("创建控件{0}失败：\r\n无法找到该控件!", c.AssembleString);
                        ((ctrlBaseDebug)controlobj).setText(msg);
                    }
                }
                catch(Exception ex)
                {
                    controlobj = new ctrlBaseDebug();
                    string msg = string.Format("创建控件{0}失败：\r\n{1}",c.AssembleString,ex.Message);
                    ((ctrlBaseDebug)controlobj).setText(msg);
                }

                Rmes.WinForm.Base.BaseControl control = controlobj as BaseControl;
                if (c.Width < 0) 
                    c.Width = mainform.ClientRectangle.Width - c.Left;
                if (c.Height < 0) 
                    c.Height = mainform.ClientRectangle.Height - c.Top;
                if (c.Top < 0)
                    c.Top = mainform.ClientRectangle.Height - c.Height;
                if (c.Left < 0)
                    c.Left = mainform.ClientRectangle.Width - c.Width;
                control.Bounds = new Rectangle(c.Left, c.Top, c.Width, c.Height);
                control.Name = c.AssembleString;
                _controls.Add(control);
            }
        }

        public static BaseForm ReLoad(ConfigFormEntity FormConfig, List<ConfigControlsEntity> ControlConfigs)
        {
            _controls.Clear();
            object _form = null;
            if (FormConfig.AssembleFile == null && FormConfig.AssembleFile.Equals(string.Empty))
            {
                string[] _t = FormConfig.AssembleString.Split('.');
                _form = System.Reflection.Assembly.Load(_t[0]).CreateInstance(FormConfig.AssembleString);
            }
            else
            {
                _form = System.Reflection.Assembly.LoadFrom(FormConfig.AssembleFile).CreateInstance(FormConfig.AssembleString);
            }
            mainform = _form as BaseForm;
            if (mainform == null)
                throw new Exception("Init MainForm failed, please check your config;");
            else
                mainform.LoadConfig(FormConfig);
            loadControls(ControlConfigs);

            mainform.FormClosing += new System.Windows.Forms.FormClosingEventHandler(mainform_FormClosing);
            mainform.FormClosed += new System.Windows.Forms.FormClosedEventHandler(mainform_FormClosed);
            return mainform;
        }

        static void mainform_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            Application.ExitThread();
        }

        static void mainform_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (MessageBox.Show("确认要退出系统吗？", "退出提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                RMESEventArgs args = new RMESEventArgs();
                args.MessageHead = "EXIT";
                (mainform.Controls[0] as BaseControl).SendDataChangeMessage(args);
                e.Cancel = false;
            }
            else
            {


                e.Cancel = true;
            }
        }

        public static void CallDataChanged(Object obj, RMESEventArgs e)
        {
            if (_controls.Count() > 0)
            {
                BaseControl co = obj as BaseControl;
                RMESEventArgs newe = e;
                foreach (BaseControl c in _controls)
                {
                    if(co!=null)
                        c.OnRMesDataChanged(newe);
                }
            }
        }

    }

    public delegate void RMesEventHandler(object obj, RMESEventArgs e);
    public class RMESEventArgs: EventArgs
    {
        public bool isCommand = false;
        public string MessageHead = "";
        public object MessageBody = null;
    }

}
