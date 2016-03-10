using Banking.Api;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SessionAuthenticationConfig), "PreAppStart")]

namespace Banking.Api
{
    public static class SessionAuthenticationConfig
    {
        public static void PreAppStart()
        {
            //DynamicModuleUtility.RegisterModule(typeof(System.IdentityModel.Services.SessionAuthenticationModule));
        }
    }
}