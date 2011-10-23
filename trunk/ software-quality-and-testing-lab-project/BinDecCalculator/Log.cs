using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BinDecCalculator
{
    static public class Log
    {
        private static int logLines { get; set; }
        private static int activeLine { get; set; }
        static Log() { logLines = 0; activeLine = -1; }

        public static void addDigit(RichTextBox tbOut, string digit, bool binary)
        {
            if (activeLine < 0)
            {
                tbOut.Text = digit;
                logLines++;
                activeLine = 0;
            }
            else
            {
                if (binary && tbOut.Lines[logLines - 1].Length >= 64)
                    throw new ArgumentOutOfRangeException("Binary number representation can't use more that 64 digits.");
                string s = tbOut.Lines[activeLine] + digit;
                if (!binary && Int64.Parse(tbOut.Lines[activeLine] + digit) < 0)
                    throw new ArgumentOutOfRangeException(string.Format("Number can't be more that {0}.", Int64.MaxValue));

                tbOut.Text = tbOut.Text.Insert(tbOut.GetFirstCharIndexFromLine(activeLine), digit);
            }
        }
        public static void delDigit(RichTextBox tbOut)
        {
            if (logLines == 0 || tbOut.Lines[activeLine].Length == 0)
                throw new NullReferenceException();

            if (tbOut.Lines[activeLine].Length > 1)
                tbOut.Text = tbOut.Text.Remove(tbOut.GetFirstCharIndexFromLine(activeLine), 1);
            else
            {
                tbOut.Text = tbOut.Text.Insert(tbOut.GetFirstCharIndexFromLine(activeLine) + 1, "\n");
                tbOut.Text = tbOut.Text.Remove(tbOut.GetFirstCharIndexFromLine(activeLine), 1);
            }
        }

        internal static void newNummber(RichTextBox tbOut)
        {
            if (logLines == 0 || tbOut.Lines[activeLine].Length == 0)
                throw new NullReferenceException();

            tbOut.Text = tbOut.Text.Insert(
                tbOut.GetFirstCharIndexFromLine(activeLine) + tbOut.Lines[activeLine].Length,
                "\n");
            logLines++;
            activeLine++;
        }
    }
}
