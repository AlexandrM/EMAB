using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;
using Microsoft.Owin;
using System.Linq;

using EMAB.Data;
using EMAB.Models;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Autofac.Core;
using System.Data.Entity;
using Microsoft.Owin.Security;
using System.Web;

[assembly: OwinStartupAttribute(typeof(EMAB.Startup))]
namespace EMAB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var config = new HttpConfiguration();
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<ApplicationDbContext>().InstancePerRequest();
            builder.RegisterType<EFContext>().InstancePerRequest();

            var dbContextParameter = new ResolvedParameter((pi, ctx) => pi.ParameterType == typeof(DbContext), (pi, ctx) => ctx.Resolve<ApplicationDbContext>());
            builder.RegisterType<UserStore<ApplicationUser>>().As<IUserStore<ApplicationUser>>().WithParameter(dbContextParameter).Instanc‌​ePerRequest();
            builder.Register<IdentityFactoryOptions<ApplicationUserManag‌​er>>(c => new IdentityFactoryOptions<ApplicationUserManager>() { DataProtectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionPr‌​ovider("ApplicationN‌​ame") });
            builder.RegisterType<ApplicationUserManager>().AsSelf().Inst‌​ancePerRequest();
            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().Instanc‌​ePerRequest();
            //builder.RegisterType<IdentityRole>().As<IRole>().Instanc‌​ePerRequest();
            //builder.RegisterType<RoleStore<IdentityRole>>().As<IRoleStore<IdentityRole, string>>().WithParameter(dbContextParameter).Instanc‌​ePerRequest();
            builder.RegisterType<RoleStore<IdentityRole>>().As<IRoleStore<IdentityRole, string>>().WithParameter(dbContextParameter).Instanc‌​ePerRequest();
            builder.RegisterType<ApplicationRoleManager>().Instanc‌​ePerRequest();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseAutofacMvc();
            app.UseWebApi(config);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
