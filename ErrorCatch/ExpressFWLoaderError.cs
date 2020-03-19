using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExpressGangLoader.ErrorCatch
{
    public class ExpressFWLoaderError
    {
        // This class is to help catching all error and null execption while the program is running.
        // This will out put a text file that will show Date, Time, and error. 
        // This file will help make troubleshooting better and easier to detect error and type of Error
        // Will add this to Azure in the future to better understand error type and how to handle the Error
        public static void ExpressFWLoaderErrorMessenger(string Running_Method_or_Class, string ErrorMessage,string InnerErrorMessage, string User)
        {
            string ver = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string recordsDir = AppDomain.CurrentDomain.BaseDirectory + "Record" + "\\";
            string recordPath = recordsDir + Running_Method_or_Class + "_" + unixTimeStampUTC() + ".txt";
            string sw = "ExpressGangLoader";
            string Username = User;
            string station = Environment.MachineName;
            List<string> LogContent = new List<string>
            {
                               "----------------------------------------------------------------------------",
                string.Format( "EXPRESS_GANG_LOADER SOFTWARE ERROR RECORD REPORT                                  "),
                               "----------------------------------------------------------------------------",
                string.Format( "Date: {0,-22}", DateTime.UtcNow),
                string.Format( "Method or Class: " + " " + Running_Method_or_Class),
                string.Format( "ErrorMessage 1:  " + " " + ErrorMessage),
                string.Format( "ErrorMessage 2:  " + " " + InnerErrorMessage),
                               "----------------------------------------------------------------------------",
                string.Format( "SW: {0,-24} Ver: {1,-12} Station: {2,-15} Operator: {2,-18}", sw, ver, station, Username),
                               "---------------------------------------------------------------------------- ",
                "Error DETAILS::"
            };

            // if Record directory does not exist
            if (!Directory.Exists(recordsDir))
            {
                Directory.CreateDirectory(recordsDir);
            }

            File.WriteAllLines(recordPath, LogContent);
        }

        //remove any special charactor
        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        //UTC timestamp
        public static string unixTimeStampUTC()
        {
            DateTime currentTime = DateTime.UtcNow;
            DateTime epoch = DateTime.MinValue;
            TimeSpan tempTS = currentTime - epoch;
            return Convert.ToString(Math.Truncate(tempTS.TotalSeconds + 172800));
        }
    }
}




