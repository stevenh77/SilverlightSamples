using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSRS.Services
{
    internal class Settings
    {
        protected static char pathSeparator = '/';
        protected static char[] pathSeparatorArray = { pathSeparator };
        protected static string pathSeparatorString = new string(pathSeparator, 1);	
    
        public static string ReportServer { get { return "http://apollo/Reports_SQL2008"; } }
        public static string ReportPath { get { return "/SSRSProject"; } }
        public static char[] PathSeparatorArray { get { return pathSeparatorArray; } }
        public static string PathSeparatorString { get { return pathSeparatorString; } }
    }
}
