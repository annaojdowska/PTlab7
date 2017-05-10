using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ptlab7
{
    [Serializable]
    class NameComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x.Length != y.Length)
            {
                if (x.Length > y.Length) return 1;
                else return -1;
            }
            else
            {
                if (string.Compare(x, y, true) > 0) return 1;
                else return -1;
            }
        }
    }
}
