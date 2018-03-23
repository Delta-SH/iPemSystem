using System;

namespace iPem.Services.Common {
    public interface IServiceGetter {
        T GetByName<T>(string name);

        T GetByKey<T>(string key);
    }
}