using System;
using System.IO;

namespace iPem.Data.Installation {
    public partial class CsExecutor : ICsExecutor {

        #region Fields

        private readonly string _databaseConnectionString;
        private readonly IDbInstaller _dbInstaller;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public CsExecutor(string databaseConnectionString, IDbInstaller dbInstaller) {
            this._databaseConnectionString = databaseConnectionString;
            this._dbInstaller = dbInstaller;
        }

        #endregion

        #region Methods

        public void Execute(Stream stream) {
            _dbInstaller.ExecuteScript(this._databaseConnectionString, stream);
        }

        public void Execute(string file) {
            _dbInstaller.ExecuteScript(this._databaseConnectionString, file);
        }

        #endregion

    }
}
