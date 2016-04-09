using iPem.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iPem.Keys {
    public partial class DbCleanKey : Form {
        public DbCleanKey() {
            InitializeComponent();
        }

        private void GetKeyButton_Click(object sender, EventArgs e) {
            Password.Text = CommonHelper.GetCleanKey();
        }
    }
}
