using iPem.Core;
using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial interface IDictionaryService {

        M_Dictionary GetDictionary(int id);

        IPagedList<M_Dictionary> GetDictionaries(int pageIndex = 0, int pageSize = int.MaxValue);

        List<M_Dictionary> GetDictionariesAsList();

        void Update(M_Dictionary dictionary);

        void Update(List<M_Dictionary> dictionaries);

    }
}
