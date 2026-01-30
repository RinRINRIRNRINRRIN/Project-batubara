using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckScale.functions
{
    internal class VersionF
    {


        private Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

        /*
         * How to use
         * 1. Unload the project
         * 2. find a key word <Deterministic>true</Deterministic>
         * 3. change the value true -> false
         * 4. Reload the project
         * 5. Go to file ProjectName->Properties->AssemblyInfo.cs
         * 6. Find a key word [assembly: AssemblyVersion("1.0.0.0")]
         * 7. Change to [assembly: AssemblyVersion("1.0.*")]
         * 8. Save and rebuild project
         */

        public int GetMajor()
        {
            return version.Major;
        }


        public int GetMinor()
        {
            return version.Minor;
        }

        public int GetBuild()
        {
            return version.Build;
        }

        public string GetFullVersion()
        {
            Console.WriteLine($"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}");
            return $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }


        public int GetRevision()
        {
            return version.Revision;
        }

        public string GetBuildDate()
        {
            // 🟢 ใช้ Build สำหรับคำนวณวันที่
            var buildDate = new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);

            // 🟢 บังคับใช้ Calendar แบบ Gregorian (ค.ศ.)
            var culture = new System.Globalization.CultureInfo("en-US");
            culture.DateTimeFormat.Calendar = new System.Globalization.GregorianCalendar();

            string dateBuile = buildDate.ToString("yyyy-MM-dd HH:mm:ss", culture);
            Console.WriteLine(dateBuile);
            return dateBuile;
        }
    }
}
