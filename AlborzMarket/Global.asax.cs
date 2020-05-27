using System; 
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Alborz.DataLayer.Context; 
using Alborz.IocConfig;
using AlborzMarket;
using AlborzMarket.App_Start;
using StructureMap.Web.Pipeline;

namespace IdentitySample
{
    // Note: For instructions on enabling IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=301868
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            setDbInitializer();
            //Set current Controller factory as StructureMapControllerFactory
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
            Microsoft.AspNet.SignalR.GlobalHost.DependencyResolver = SmObjectFactory.Container.GetInstance<Microsoft.AspNet.SignalR.IDependencyResolver>();
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            HttpContextLifecycle.DisposeAndClearAll();
        }

        public class StructureMapControllerFactory : DefaultControllerFactory
        {
            protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
            {
                if (controllerType == null)
                {
                    throw new HttpException(404, $"Resource not found : {requestContext.HttpContext.Request.Path}");
                }
                return SmObjectFactory.Container.GetInstance(controllerType) as Controller;
            }
        }

        private static void setDbInitializer()
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Alborz.DataLayer.Migrations.Configuration>());
            SmObjectFactory.Container.GetInstance<IUnitOfWork>().ForceDatabaseInitialize();
        }

        //protected void Application_AuthenticateRequest(object sender, EventArgs args)
        //{
        //    if (Context.User != null)
        //    {
        //        IEnumerable<Role> roles = new UsersService.UsersClient().GetUserRoles(
        //                                                Context.User.Identity.Name);


        //        string[] rolesArray = new string[roles.Count()];
        //        for (int i = 0; i < roles.Count(); i++)
        //        {
        //            rolesArray[i] = roles.ElementAt(i).RoleName;
        //        }

        //        GenericPrincipal gp = new GenericPrincipal(Context.User.Identity, rolesArray);
        //        Context.User = gp;
        //    }
        //}
    }
}
