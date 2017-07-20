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
            if (length > 16) throw new ArgumentException("长度超出范围（最大支持16个字符）。");
            var today = DateTime.Today;
            var even = today.Day % 2 == 0;
            var factor = today.Year * 10000 + today.Month * 100 + today.Day;
            var salt = even ? 621 : 325;
            var key = string.Format("TKac4SRTQi%rsDixVlfM3w{0}PVF+t^0iNOonZUyTZ5Hcxg=={2}==g+T3Cpu/FDpUjxOsUrt&xw{1}CuCEh2cH#kFqetg9Z+1wUw", even ? today.Day : today.Month, even ? today.Month : today.Day, factor + salt);
            var bytes = MD5.Create().ComputeHash(Encoding.Default.GetBytes(key));
            if (bytes == null) throw new Exception("生成密钥时发生错误。");
            key = string.Join("", bytes.Reverse());
            return key.Substring(key.Length > (today.Day + today.Month + length) ? (today.Day + today.Month) : (even ? 0 : (key.Length - length)), length);
        }
    }
}
