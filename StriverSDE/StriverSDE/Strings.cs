using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StriverSDE
{
    class Strings
    {
        #region Reverse Words in a string

        //Naive Solution S=>O(N) and T=>O(N)
        //plit the string in array and then print in reverse order

        //Optimal Solution S=>O(1) and T>O(N)
        //Without extra space by traversing backwards

        public string ReverseWords(string s)
        {
            //starting from last index
            int ptr = s.Length - 1;
            string ans = "", temp = "";
            while (ptr >= 0)
            {
                if (s[ptr] == ' ')
                {
                    if (!string.IsNullOrEmpty(temp))
                    {
                        //append to answer ans then clear the temp string
                        ans += temp + " ";
                        temp = "";
                    }
                }
                else
                    //if its a word prepend it to temp as we are coming backwards
                    temp = s[ptr] + temp;
                ptr--;
            }
            //appending the last word 
            if (!string.IsNullOrEmpty(temp))
                ans += temp;
            return ans.Trim();
        }
        #endregion
    }
}
