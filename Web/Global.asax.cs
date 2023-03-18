using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web.Utils;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure();
        }
        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            // Configurar  Culture, formato fechas y separadores decimales.
            CultureInfo myCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            myCulture.NumberFormat.NumberDecimalSeparator = ".";
            myCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            myCulture.DateTimeFormat.DateSeparator = "/";
            Thread.CurrentThread.CurrentCulture = myCulture;
        }


        protected void Application_Error()
        {
            var ex = Server.GetLastError();

            //  Control de bit�cora de errores con log4Net ..
            Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod());

        }
    }
}
