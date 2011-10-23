using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace BinDecCalculator
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }

        private void tbOut_TextChanged(object sender, EventArgs e)
        {
            tbOut.SelectionStart = tbOut.Text.Length;
            tbOut.ScrollToCaret();
            if (tbOut.Text.Length > 0)
                gbSystems.Enabled = false;
            else
                gbSystems.Enabled = true;
        }

        private void rbBinary_CheckedChanged(object sender, EventArgs e)
        {
            btn2.Enabled = !btn2.Enabled;
            btn3.Enabled = !btn3.Enabled;
            btn4.Enabled = !btn4.Enabled;
            btn5.Enabled = !btn5.Enabled;
            btn6.Enabled = !btn6.Enabled;
            btn7.Enabled = !btn7.Enabled;
            btn8.Enabled = !btn8.Enabled;
            btn9.Enabled = !btn9.Enabled;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Log.Clear(tbOut);
        }

        private void btnNumeric_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbBinary.Checked)
                    Log.addDigit(tbOut, ((Button)sender).Text, true);
                else
                    Log.addDigit(tbOut, ((Button)sender).Text, false);
            }
            catch
            {
                SystemSounds.Beep.Play();
            }
            
        }

        private void btnPoint_Click(object sender, EventArgs e)
        {
            try
            {
                Log.addPoint(tbOut);
            }
            catch
            {
                SystemSounds.Beep.Play();
            }
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            try
            {
                Log.delDigit(tbOut);
            }
            catch
            {
                SystemSounds.Beep.Play();
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            try
            {
                Log.SaveResult(tbOut, rbDecimal.Checked);
                Log.newNummber(tbOut);
            }
            catch
            {
                SystemSounds.Beep.Play();
            }

        }

        private void btnLocalClear_Click(object sender, EventArgs e)
        {
            try
            {
                Log.delActive(tbOut);
            }
            catch
            {
                SystemSounds.Beep.Play();
            }
        }

        private void btnNegPos_Click(object sender, EventArgs e)
        {
            try
            {
                Log.NegPosChange(tbOut);
            }
            catch
            {
                SystemSounds.Beep.Play();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox fAbout = new AboutBox();
            fAbout.Show();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(saveFile.FileName);
                writer.Write(tbOut.Text);
                writer.Close();
            }
        }
    }
}
