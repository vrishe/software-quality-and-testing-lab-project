using System;
using System.Collections.Generic;
using System.Text;

namespace BinDecConverter
{
    public static class BinDecConverter
    {
        internal static string DecToBin(string Decimal)
        {
            int index = Decimal.IndexOf(',');
            if (index < 0)
            {
                Decimal += ',';
                index = Decimal.Length - 1;
            }
            Int64 intPart = Int64.Parse(Decimal.Substring(0, index));
            
            Double decPart = 0;
            bool hasDec = (Decimal.Length - index > 1 ? Double.TryParse(Decimal.Substring(index, Decimal.Length - index), out decPart) : false);
            if (decPart == 0)
                hasDec = false;

            string result = (intPart % 2).ToString();
            intPart >>= 1;
            while (intPart > 0)
            {
                result = (intPart % 2).ToString() + result;
                intPart >>= 1;
            }

            if (!hasDec)
                return result;

            StringBuilder dec_string = new StringBuilder();
            while (decPart > 0)
            {
                if ((result.Length + dec_string.Length) > 64)
                    break;
                if (decPart == 1)
                {
                    dec_string.Append(decPart);
                    break;
                }
                decPart = decPart * 2;
                if (decPart >= 1)
                {
                    dec_string.Append(1);
                    decPart -= 1;
                }
                else
                    dec_string.Append(0);
            }

            return (result + "," + dec_string);
        }
        
        internal static string BinToDec(string Binary)
        {
            int length = Binary.Length;
            int index = Binary.IndexOf(',');
            if (index < 0)
            {
                Binary += ',';
                index = length - 1;
            }
            Int64 intPart = Convert.ToInt64(Binary.Substring(0, index), 2);
            string result = intPart.ToString();

            if (length - index < 1)
                return result;

            int decPartLength = length - index - 1;
            string decPart = Binary.Substring(index + 1);

            double decResult = 0;
            for (int i = 1; i <= decPartLength; i++)
            {
                if (decPart[i - 1] == '1')
                    decResult += Math.Pow(2, -i);
            }

            return result + "," + decResult.ToString().Substring(2);
        }
    }
}
