using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;

namespace Rmes.WebApp.Rmes.Qms.qms2100
{
    public partial class qms2101 : System.Web.UI.Page
    {
        string rmesid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            rmesid = Request.QueryString["fid"] == null ? "" : Request.QueryString["fid"];
            if (string.IsNullOrWhiteSpace(rmesid))
            {
                Response.Write("不可下载");
                Response.End();
            }
            else
                ReadBlobToFile(rmesid);
        }

        public static bool ReadBlobToFile(string idValue)
        {
            try
            {
                //byte[] b = new byte[myReader.GetBytes(PictureCol, 0, null, 0, int.MaxValue) - 1];
                //myReader.GetBytes(PictureCol, 0, b, 0, b.Length);
               // myReader.Close();
                //FILE.FILE_BLOB

                //System.IO.FileStream fileStream = new System.IO.FileStream(
                    
                //    FILE.FILE_NAME, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                //byte[] bytes = new byte[(int)fileStream.Length];
                //fileStream.Write(bytes, 0, bytes.Length);
                //fileStream.Close();

                FileBlobEntity FILE = QualityFactory.GetByRMESID(idValue);
                FILE.FILE_NAME = FILE.FILE_NAME.Trim();
                
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                //通知浏览器下载文件而不是打开
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(FILE.FILE_NAME, System.Text.Encoding.ASCII));
                HttpContext.Current.Response.BinaryWrite(FILE.FILE_BLOB);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch
            {
                return false;
            }

            return true;
        }  
    }
}