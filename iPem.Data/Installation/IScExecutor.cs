using System;
using System.IO;

namespace iPem.Data.Installation {
    public partial interface IScExecutor {
        void Execute(Stream stream);

        void Execute(string file);
    }
}
