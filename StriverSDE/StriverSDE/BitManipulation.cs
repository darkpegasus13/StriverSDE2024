using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StriverSDE
{
    class BitManipulation
    {
        #region power set

        public IList<string> PowerSet(string s)
        {
            List<string> ans = new List<string>();
            PowerSetHelperRecursive(s, 0, s.Length, ans);
            //this.PowerSetHelperBM(s, s.Length, ans); bit manipulation answer
            ans.Sort();
            return ans;
        }

        //S=>O(N) and T=>O(2^N)
        //using recursion and then sorting

        public void PowerSetHelperRecursive(string s, int indx, int n, IList<string> ans, string curr = "")
        {
            if (indx == n)
            {
                ans.Add(curr);
                return;
            }
            PowerSetHelperRecursive(s, indx + 1, n, ans, curr);
            PowerSetHelperRecursive(s, indx + 1, n, ans, curr + s[indx]);
        }

        //S=>O(1) and T=>O((2^n)*n)
        //using bit manipuation
        public void PowerSetHelperBM(string s,int n, IList<string> ans)
        {
            for(int num = 0; num < Math.Pow(2, n); num++)
            {
                string sub = "";
                for(int i = 0; i < n; i++)
                {if((num & (1<<i))!=0)
                    sub += s[i];
                }
                if (sub.Length > 0)
                    ans.Add(sub);
            }
        }

        #endregion
    }
}
