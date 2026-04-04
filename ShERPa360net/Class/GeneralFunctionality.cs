using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShERPa360net.Class
{
    public static  class GeneralFunctionality
    {
        public static Double GetAGradeSuggestAmount(string sugamt , string Grade)
        {
            Double calsuggestAmt = 0;
            Double suggsAmt = 0;
            Double.TryParse(sugamt, out suggsAmt);
            try
            {
                if (Grade == "A")
                {
                    calsuggestAmt = suggsAmt;
                }
                else if (Grade == "B")
                {
                    calsuggestAmt = Math.Round((((suggsAmt * 100)) / 95), 0);
                }
                else if (Grade == "C")
                {
                    calsuggestAmt = Math.Round((((suggsAmt * 100)) / 90), 0);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return calsuggestAmt;
        }

        public static  String GetRandomGuid()
        {
            string GuidNumber = "";
            try
            {
                Guid g = Guid.NewGuid();
                GuidNumber = g.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GuidNumber;
        }


    }
}