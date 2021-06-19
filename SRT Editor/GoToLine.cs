using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SRT_Editor
{
    public partial class GoToLine : Form
    {
        public int goTo { get; set; }
        public GoToLine(int max)
        {
            InitializeComponent();
            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = max;
            label1.Text = "Range [" + 1 + ", " + max + "]";
        }

        private void GoToLine_Load(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            goTo = Convert.ToInt32(numericUpDown1.Value - 1);
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
