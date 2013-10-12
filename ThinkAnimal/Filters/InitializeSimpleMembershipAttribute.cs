using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using System.Web.Security;
using ThinkAnimal.Model;
using ThinkAnimal.Repository;
using WebMatrix.WebData;

namespace ThinkAnimal.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<UsersContext>(null);

                try
                {
                    using (var context = new ProjectContext())
                    {
                        if (!context.Database.Exists())
                        {
                            // Create the SimpleMembership database without Entity Framework migration schema
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                        }
                    }

                    WebSecurity.InitializeDatabaseConnection("ProjectContext", "UserProfile", "UserId", "UserName", autoCreateTables: true);

                    const string adminRole = "Administrator";
                    const string adminName = "Administrator";
                    const string adminTempPass = "password1";

                    if (!Roles.RoleExists(adminRole))
                    {
                        Roles.CreateRole(adminRole);
                    }

                    if (!WebSecurity.UserExists(adminName))
                    {
                        WebSecurity.CreateUserAndAccount(adminName, adminTempPass);
                        Roles.AddUserToRole(adminName, adminRole);
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }
        }
    }
}
