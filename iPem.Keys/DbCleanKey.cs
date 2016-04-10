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
            Password.Text = this.GetCleanKey();
        }

        private string GetCleanKey() {
            var year = DateTime.Today.Year;
            var month = DateTime.Today.Month;
            var day = DateTime.Today.Day;

            var fractions = year * 10000 + month * 100 + day - month * day;
            var numerator = day % 2 == 0 ? 621 : 325;
            return (fractions / numerator).ToString();
        }
    }
}
