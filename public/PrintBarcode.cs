using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rmes.Public.Base
{
    //此类是提供给其它应用程序的接口
    public class PrintBarcode
    {
        private string thisXmlLoc;
        private string thisTemplateName;

        public PrintBarcode(string xmlLoc,string templateName)
        {
            thisXmlLoc = xmlLoc;
            thisTemplateName=templateName;
        }
        public string printing()
        {
            PrintDocument pd = new PrintDocument();
            StandardPrintController controler = new StandardPrintController();

            try
            {
                pd.PrintPage += new PrintPageEventHandler(this.PrintCustomLable);
                pd.PrintController = controler;
                pd.Print();
                return "SUCCESS"; 
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return "ERROR";
            }
            finally
            {
                pd.Dispose();
            }

        }
        private void PrintCustomLable(Object Sender, PrintPageEventArgs av)
        {
            //从xml文件读取数据
            xmlOp xo = new xmlOp(thisXmlLoc);
            Hashtable hs = xo.traverseNode(thisTemplateName);

            //颜色
            Brush br = new SolidBrush(Color.Black);
            //边距
            Margins margins = new Margins(50, 50, 50, 145);
            av.PageSettings.Margins = margins;
            //读取所有label
            foreach (DictionaryEntry de in hs)
            {
                Hashtable dtLabelPro = xo.traverseNode(de.Key.ToString());
                Font ft = new System.Drawing.Font(dtLabelPro["fontName"].ToString(),
                    Convert.ToInt32(dtLabelPro["fontSize"].ToString()), FontStyle.Regular, GraphicsUnit.World);
                av.Graphics.DrawString(dtLabelPro["labelText"].ToString(), ft, br, Convert.ToInt32(dtLabelPro["left"].ToString()), Convert.ToInt32(dtLabelPro["top"].ToString()));
            }
            av.HasMorePages = false;
        }
    }
}
