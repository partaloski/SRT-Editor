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
    public partial class EditSubtitle : Form
    {
        public Subtitle current { get; set; }
        public EditSubtitle(Subtitle current)
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            InitializeComponent();
            this.current = current;
            tbPrev.Lines = current.lines.ToArray();
            tbCurrent.Lines = current.lines.ToArray();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string[] lines = tbCurrent.Text.Split("\n");
            if(tbCurrent.Text.Length == 0)
            {
                MessageBox.Show("Subtitle cannot be empty");
                return;
            }
            List<String> subs = new List<String>();
            foreach (string line in lines) subs.Add(line);
            current.lines = subs;
            DialogResult = DialogResult.OK;
        } 

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            btnSave_Click(sender, e);
        }

        private void EditSubtitle_Load(object sender, EventArgs e)
        {

        }
    }
}
