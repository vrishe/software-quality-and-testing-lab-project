using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace BinDecConverter
{
    static public class Log
    {
        private static int logLines { get; set; }
        private static int activeLine { get; set; }
        private static bool activeIsNegative { get; set; }
        static Log() { logLines = 0; activeLine = -1; activeIsNegative = false; }

        internal static void addDigit(TextBox tbOut, string digit, bool binary)
        {
            if (activeLine < 0)
            {
                tbOut.Text = digit;
                logLines++;
                activeLine = 0;
                return;
            }
            if (tbOut.Lines[activeLine].Length == 0)
            {
                tbOut.Text = tbOut.Text.Insert(tbOut.Text.Length, digit);
            }
            else
            {
                if (binary && tbOut.Lines[logLines - 1].Length >= 64)
                    throw new ArgumentOutOfRangeException("Binary number representation can't use more that 64 digits.");
                if (!binary && Double.Parse(tbOut.Lines[activeLine] + digit, NumberStyles.AllowDecimalPoint) > Int64.MaxValue)
                    throw new ArgumentOutOfRangeException(string.Format("Number can't be more that {0}.", Int64.MaxValue));

                tbOut.Text = tbOut.Text.Insert(tbOut.Text.Length, digit);
            }
        }
        internal static void delDigit(TextBox tbOut)
        {
            int length = tbOut.Lines[activeLine].Length;
            int delta = (activeIsNegative ? 1 : 0);
            if (logLines == 0 || length == 0)
                throw new NullReferenceException();

            if (length > 1 + delta)
                tbOut.Text = tbOut.Text.Remove(length - 1, 1);
            else
            {
                tbOut.Text = tbOut.Text.Remove(length - 1 - delta, 1 + delta);
                if (length - 1 - delta == 0)
                {
                    logLines = 0;
                    activeLine = -1;
                }
                if (delta > 0)
                    activeIsNegative = !activeIsNegative;
            }
        }
        internal static void addPoint(TextBox tbOut)
        {
            if (logLines == 0 || tbOut.Lines[activeLine].Length == 0)
                throw new NullReferenceException();
            if (tbOut.Lines[activeLine].IndexOf(',') > 0)
                throw new ArithmeticException("Number can have only one separation point");

            tbOut.Text = tbOut.Text.Insert(tbOut.Text.Length, ",");
        }

        internal static void newNumber(TextBox tbOut)
        {
            if (logLines == 0 || tbOut.Lines[activeLine].Length == 0)
                throw new NullReferenceException();

            tbOut.Text = tbOut.Text.Insert(
                tbOut.Text.Length,
                "\r\n\r\n");
            logLines += 2;
            activeLine += 2;
        }

        internal static void SaveResult(TextBox tbOut, bool toBinary)
        {
            string correction = "";
            if (activeIsNegative)
            {
                tbOut.Text = tbOut.Text.Remove(tbOut.Text.Length - tbOut.Lines[activeLine].Length, 1);
                correction = "-";
            }
            if (toBinary)
            {
                string result = 
                    correction +
                    tbOut.Lines[activeLine] + 
                    " = " + correction +
                    BinDecConverter.DecToBin(tbOut.Lines[activeLine]);
                tbOut.Text = tbOut.Text.Remove(tbOut.Text.Length - tbOut.Lines[activeLine].Length, tbOut.Lines[activeLine].Length);
                tbOut.Text = tbOut.Text.Insert(tbOut.Text.Length, result);
            }
            else
            {
                string result =
                    correction +
                    tbOut.Lines[activeLine] + 
                    " = " + correction +
                    BinDecConverter.BinToDec(tbOut.Lines[activeLine]);
                tbOut.Text = tbOut.Text.Remove(tbOut.Text.Length - tbOut.Lines[activeLine].Length, tbOut.Lines[activeLine].Length);
                tbOut.Text = tbOut.Text.Insert(tbOut.Text.Length, result);
            }
        }

        internal static void delActive(TextBox tbOut)
        {
            if (logLines == 0 || tbOut.Lines[activeLine].Length == 0)
                throw new NullReferenceException();
            tbOut.Text = tbOut.Text.Remove(tbOut.Text.Length - tbOut.Lines[activeLine].Length, tbOut.Lines[activeLine].Length);
            if (tbOut.Text.Length == 0)
            {
                logLines = 0;
                activeLine = -1;
            }
        }

        internal static void Clear(TextBox tbOut)
        {
            tbOut.Clear();
            activeLine = -1;
            logLines = 0;
        }

        internal static void NegPosChange(TextBox tbOut)
        {
            if (activeIsNegative)
                tbOut.Text = tbOut.Text.Remove(tbOut.Text.Length - tbOut.Lines[activeLine].Length, 1);
            else
                if (tbOut.Lines[activeLine].Length > 0)
                    tbOut.Text = tbOut.Text.Insert(tbOut.Text.Length - tbOut.Lines[activeLine].Length, "-");
                else
                    throw new NullReferenceException();

            activeIsNegative = !activeIsNegative;
        }
    }
}
