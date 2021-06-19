using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SRT_Editor
{
    public partial class Form1 : Form
    {
        List<Subtitle> subtitles;
        int index;
        Subtitle savedSubtitle;
        public Form1()
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            InitializeComponent();
            subtitles = new List<Subtitle>();
            index = -1;
        }

        private bool onlyDigits(string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                StreamReader file = new StreamReader(File.OpenRead(ofd.FileName));
                string line;
                while (true) {
                    line = file.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    if(line.Length == 0)
                            continue;
                    if (onlyDigits(line))
                    {
                        string timestamp = file.ReadLine();
                        string ll = file.ReadLine();
                        List<string> lines = new List<string>();
                        while (ll != null && ll.Length != 0)
                        {
                            lines.Add(ll);
                            ll = file.ReadLine();
                        }
                        subtitles.Add(new Subtitle(line, timestamp, lines));
                    }
                }
                index = 0;
                savedSubtitle = subtitles[index];
                switchPreview();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (subtitles.Count == 0)
                return;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".srt";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StreamWriter streamWriter = new StreamWriter(sfd.OpenFile());
                foreach (Subtitle sub in subtitles)
                {
                    streamWriter.WriteLine(sub.getToString() + "\n");
                    streamWriter.Flush();
                }
                streamWriter.Dispose();
                streamWriter.Close();
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            index--;
            if (index < 0)
            {
                index = subtitles.Count - 1;
            }
            savedSubtitle = subtitles[index];
            switchPreview();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            index++;
            if (index > subtitles.Count-1)
            {
                index = 0;
            }
            savedSubtitle = subtitles[index];
            switchPreview();
        }

        public void switchPreview()
        {
            tbPreview.Lines = savedSubtitle.lines.ToArray();
            lblLine.Text = "Line: " + (index + 1).ToString() + " of " + subtitles.Count.ToString();
            //label1.Text = savedSubtitle.getLines();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (index == -1)
                return;
            EditSubtitle editSubtitle = new EditSubtitle(subtitles, index);
            editSubtitle.ShowDialog();
            if(editSubtitle.DialogResult == DialogResult.OK)
            {
                index = editSubtitle.index;
                savedSubtitle = editSubtitle.current;
                subtitles = editSubtitle.allSubs;
                switchPreview();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (index == -1)
                return;
            DialogResult dr = MessageBox.Show("Are you sure you want to delete the currently selected subtitle?", "Confirm Deletion", MessageBoxButtons.YesNo);
            if (dr == DialogResult.No)
                return;
            this.subtitles.Remove(savedSubtitle);
            if(index > subtitles.Count)
            {
                index = subtitles.Count - 1;
            }
            savedSubtitle = subtitles[index];
            switchPreview();
            updateCounts();
        }

        private void updateCounts()
        {
            for(int i=0; i < subtitles.Count; i++)
            {
                subtitles[i].num = (i+1).ToString();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnOpen_Click(sender, e);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSave_Click(sender, e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void add10msDelayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Subtitle s in subtitles)
                s.add();
        }

        private void sub10msDelayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Subtitle s in subtitles)
                s.subtract();
        }

        private void btnGoTo_Click(object sender, EventArgs e)
        {
            GoToLine gt = new GoToLine(subtitles.Count);
            gt.ShowDialog();
            if(gt.DialogResult == DialogResult.OK)
            {
                index = gt.goTo;
                savedSubtitle = subtitles[index];
                switchPreview();
            }
        }

        private void customOffsetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveTime mt = new MoveTime();
            mt.ShowDialog();
            if(mt.DialogResult == DialogResult.OK)
            {
                foreach (Subtitle s in subtitles)
                    s.add(mt.ms);
            }
        }
    }
}
