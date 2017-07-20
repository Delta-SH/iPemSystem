using System;
using System.IO;

namespace iPem.Data.Installation {
    public partial interface IRsExecutor {
        void Execute(Stream stream);

        void Execute(string file);
    }
}
