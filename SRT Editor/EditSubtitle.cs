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
        public List<Subtitle> allSubs { get; set; }
        public int index { get; set; }
        public EditSubtitle(List<Subtitle> subs, int current)
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            InitializeComponent();
            this.current = subs[current];
            this.allSubs = subs;
            this.index = current;
            update();
        }
        private void update()
        {
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
        public void saveChanges() {
            string[] lines = tbCurrent.Text.Split("\n");
            if (tbCurrent.Text.Length == 0)
            {
                MessageBox.Show("Subtitle cannot be empty");
                return;
            }
            List<String> subs = new List<String>();
            foreach (string line in lines) subs.Add(line);
            current.lines = subs;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            saveChanges();
            allSubs[index] = current;
            index--;
            if (index < 0)
            {
                index = allSubs.Count - 1;
            }
            current = allSubs[index]; update();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            saveChanges();
            allSubs[index] = current;
            index++;
            if (index > allSubs.Count - 1)
            {
                index = 0;
            }
            current = allSubs[index]; update();
        }

        private void prevToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnPrev_Click(sender, e);
        }

        private void nextToolStripMenuItem_Click(object sender, EventArgs e)
        {

            btnNext_Click(sender, e);
        }
    }
}
