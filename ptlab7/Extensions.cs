using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ptlab7
{
    public static class Extensions
    {
        public static DateTime GetDateOfOldestElement(this DirectoryInfo directory)
        {
            DateTime oldestDate = DateTime.Now;
            DateTime tmp;
            foreach(FileInfo fi in directory.GetFiles())
            {
                if (fi.CreationTime < oldestDate) oldestDate = fi.CreationTime;
            }
            foreach (DirectoryInfo di in directory.GetDirectories())
            {
                if (di.CreationTime < oldestDate) oldestDate = di.CreationTime;
                tmp = di.GetDateOfOldestElement();
                if (tmp < oldestDate) oldestDate = tmp;
            }
            return oldestDate;
        }

        public static string GetRahs(this FileSystemInfo fsi)
        {
            string rahs = "";
            if ((fsi.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly) rahs = rahs + "r";
            else rahs += "-";
            if ((fsi.Attributes & FileAttributes.Archive) == FileAttributes.Archive) rahs = rahs + "a";
            else rahs += "-";
            if ((fsi.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) rahs = rahs + "h";
            else rahs += "-";
            if ((fsi.Attributes & FileAttributes.System) == FileAttributes.System) rahs = rahs + "s";
            else rahs += "-";
            return rahs;
        }
    }
}
