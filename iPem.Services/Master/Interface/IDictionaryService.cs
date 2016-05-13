using iPem.Core;
using iPem.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace iPem.Services.Master {
    public partial interface IDictionaryService {

        Dictionary GetDictionary(int dictionaryId);

        IPagedList<Dictionary> GetDictionaries(int pageIndex = 0, int pageSize = int.MaxValue);

        void UpdateDictionary(Dictionary dictionary);

        void UpdateDictionaries(List<Dictionary> dictionaries);

    }
}
