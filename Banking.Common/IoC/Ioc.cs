using Microsoft.Practices.Unity;

namespace Banking.Common.IoC
{
    public class Ioc
    {
        private static UnityContainer _instance;
        private static readonly object _locker = new object();

        public static IUnityContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new UnityContainer();
                        }
                    }
                }
                return _instance;
            }
        }

        public static T Get<T>(params ResolverOverride[] overrides)
        {
            return Instance.Resolve<T>(overrides);
        }
    }
}
