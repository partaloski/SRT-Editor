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
    partial class MoveTime : Form
    {
        public int ms { get; set; }
        public MoveTime()
        {
            InitializeComponent();
        }
        private bool checkForValid(string s)
        {
            foreach(char c in s)
            {
                if (!Char.IsDigit(c))
                    return false;

            }
            return true;

        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!checkForValid(textBox1.Text))
            {
                MessageBox.Show("There is something wrong with your input. Try again.");
                return;
            }
            ms = Convert.ToInt32(textBox1.Text.Trim());
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MoveTime_Load(object sender, EventArgs e)
        {

        }
    }
}
