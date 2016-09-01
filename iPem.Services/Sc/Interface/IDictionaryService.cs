using iPem.Core;
using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial interface IDictionaryService {

        Dictionary GetDictionary(int id);

        IPagedList<Dictionary> GetDictionaries(int pageIndex = 0, int pageSize = int.MaxValue);

        List<Dictionary> GetDictionariesAsList();

        void Update(Dictionary dictionary);

        void Update(List<Dictionary> dictionaries);

    }
}
