using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iPem.Keys {
    public partial class DbCleanKey : Form {
        public DbCleanKey() {
            InitializeComponent();
        }

        private void GetKeyButton_Click(object sender, EventArgs e) {
            Password.Text = this.CreateDynamicKeys();
        }

        private string CreateDynamicKeys(int length = 6) {
            var now = DateTime.Now;
            var factor = now.Year * 10000 + now.Month * 100 + now.Day;
            var salt = now.Day % 2 == 0 ? 621 : 325;
            var key = (factor + salt).ToString();

            var bytes = MD5.Create().ComputeHash(Encoding.Default.GetBytes(key));
            if(bytes != null) {
                key = string.Join("", bytes.Reverse());
                if(length < key.Length)
                    key = key.Substring(0, length);
            }
            
            return key;
        }
    }
}
