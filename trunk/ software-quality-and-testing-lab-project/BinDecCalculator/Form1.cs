using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace BinDecCalculator
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }

        #region Interface manipulations
//      textBox.Text=texBox.Text.Remove(texBox.GetFirstCharIndexFromLine(LineIndex),texBox.Lines[LineIndex].Length+1);
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
            tbOut.Clear();
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
        #endregion

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                Log.newNummber(tbOut);
            }
            catch
            {
                SystemSounds.Beep.Play();
            }
        }
    }
}
