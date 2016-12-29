using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;
using Rmes.Pub.Data;


namespace Rmes.WebApp
{

    /// <summary>
    /// 实现自定义的 HttpModule类
    /// </summary>
    public class RmesHttpModule : IHttpModule, IRequiresSessionState
    {
        public RmesHttpModule()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 说明：实现IHttpModule接口的Init方法
        /// </summary>
        /// <param name="application">HttpApplication类型的参数</param>

        //将来根据不同的需要启动不同的进程，现在先都注释掉里面的代码20071025

        public void Init(HttpApplication application)
        {
            application.BeginRequest += (new EventHandler(this.Application_BeginRequest));
            application.EndRequest += (new EventHandler(this.Application_EndRequest));
            application.PreRequestHandlerExecute += (new EventHandler(this.Application_PreRequestHandlerExecute));
            application.PostRequestHandlerExecute += (new EventHandler(this.Application_PostRequestHandlerExecute));
            application.ReleaseRequestState += (new EventHandler(this.Application_ReleaseRequestState));
            application.AcquireRequestState += (new EventHandler(this.Application_AcquireRequestState));
            application.AuthenticateRequest += (new EventHandler(this.Application_AuthenticateRequest));
            application.AuthorizeRequest += (new EventHandler(this.Application_AuthorizeRequest));
            application.ResolveRequestCache += (new EventHandler(this.Application_ResolveRequestCache));
            application.PreSendRequestHeaders += (new EventHandler(this.Application_PreSendRequestHeaders));
            application.PreSendRequestContent += (new EventHandler(this.Application_PreSendRequestContent));
        }
        /// <summary>
        /// 说明：自己定义的用来做点事情的私有方法
        /// </summary>
        /// <param name="obj">传递进来的对象参数</param>
        /// <param name="e">事件参数</param>
        private void Application_PreRequestHandlerExecute(Object source, EventArgs e)
        {
            //HttpApplication application = (HttpApplication)source;
            //HttpContext context = application.Context;

            //context.Response.Write("Application_PreRequestHandlerExecute<br>");
        }
        /// <summary>
        /// 说明：自己定义的用来做点事情的私有方法
        /// </summary>
        /// <param name="obj">传递进来的对象参数</param>
        /// <param name="e">事件参数</param>
        private void Application_BeginRequest(Object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;

            //application.CompleteRequest();
            //context.Response.StatusCode = 500;
            //context.Response.StatusDescription = "error!";
            //context.Response.Write("Application_BeginRequest<br>");

 
      
        }

        private void Application_EndRequest(Object source, EventArgs e)
        {
            //HttpApplication application = (HttpApplication)source;
            //HttpContext context = application.Context;

            //context.Response.Write("Application_EndRequest<br>");

        }

        private void Application_PostRequestHandlerExecute(Object source, EventArgs e)
        {
            //HttpApplication application = (HttpApplication)source;
            //HttpContext context = application.Context;

            //context.Response.Write("Application_PostRequestHandlerExecute<br>");

        }

        private void Application_ReleaseRequestState(Object source, EventArgs e)
        {
            //HttpApplication application = (HttpApplication)source;
            //HttpContext context = application.Context;

            //context.Response.Write("Application_ReleaseRequestState<br>");

        }

        private void Application_UpdateRequestCache(Object source, EventArgs e)
        {
            //HttpApplication application = (HttpApplication)source;
            //HttpContext context = application.Context;

            //context.Response.Write("Application_UpdateRequestCache<br>");

        }

        private void Application_AuthenticateRequest(Object source, EventArgs e)
        {
            //HttpApplication application = (HttpApplication)source;
            //HttpContext context = application.Context;

            //context.Response.Write("Application_AuthenticateRequest<br>");

        }

        private void Application_AuthorizeRequest(Object source, EventArgs e)
        {
            //HttpApplication application = (HttpApplication)source;
            //HttpContext context = application.Context;

            //context.Response.Write("Application_AuthorizeRequest<br>");

        }

        private void Application_ResolveRequestCache(Object source, EventArgs e)
        {
            //HttpApplication application = (HttpApplication)source;
            //HttpContext context = application.Context;

            //context.Response.Write("Application_ResolveRequestCache<br>");

        }

        private void Application_AcquireRequestState(Object source, EventArgs e)
        {
            //HttpApplication application = (HttpApplication)source;
            //HttpContext context = application.Context;

            //context.Response.Write("Application_AcquireRequestState<br>");

        }

        private void Application_PreSendRequestHeaders(Object source, EventArgs e)
        {
            //HttpApplication application = (HttpApplication)source;
            //HttpContext context = application.Context;

            //context.Response.Write("Application_PreSendRequestHeaders<br>");

        }

        private void Application_PreSendRequestContent(Object source, EventArgs e)
        {
            //HttpApplication application = (HttpApplication)source;
            //HttpContext context = application.Context;

            //context.Response.Write("Application_PreSendRequestContent<br>");

        }


        /// <summary>
        /// 说明：实现IHttpModule接口的Dispose方法
        /// </summary>
        public void Dispose() { }

    }
}
