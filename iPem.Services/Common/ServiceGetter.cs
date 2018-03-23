using Autofac;
using Autofac.Integration.Mvc;
using System;

namespace iPem.Services.Common {
    public class ServiceGetter : IServiceGetter {
        public T GetByName<T>(string name) {
            return AutofacDependencyResolver.Current.RequestLifetimeScope.ResolveNamed<T>(name);
        }

        public T GetByKey<T>(string key) {
            return AutofacDependencyResolver.Current.RequestLifetimeScope.ResolveKeyed<T>(key);
        }
    }
}
