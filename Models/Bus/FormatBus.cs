using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Bus
{
    public class FormatBus
    {
        public String FormatDecimalDisplay(decimal? myNumber)
        {
            var s = myNumber.GetValueOrDefault().ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("de"));
            var s1 = s + " vnd";

            return s1;
        }

        public String ColorFormat(string color)
        {
            String s;
            if (color == "White")
            {
                s = "#f2f2f2";
            }
            else if (color == "Copper")
            {
                s = "#B87333";
            }
            else if (color == "Chrome")
            {
                s = "silver";
            }
            else
            {
                s = color;
            }
            return s;
        }

    }
}
