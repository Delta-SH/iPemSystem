using System;
using System.IO;

namespace iPem.Data.Installation {
    public partial interface ICsExecutor {
        void Execute(Stream stream);

        void Execute(string file);
    }
}
