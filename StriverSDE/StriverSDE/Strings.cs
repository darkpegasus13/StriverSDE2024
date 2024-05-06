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

        #region Roman to Integer

        //Naive solution 
        //filling dictionary with numerals and subtracting the lower value
        //from higher

        public int RomanToInt(string s)
        {
            int l = s.Length,i = 0, val = 0;
            Dictionary<char, int> m =
            new Dictionary<char, int> { { 'I', 1 }, { 'V', 5 }, { 'X', 10 }, 
                { 'L', 50 }, { 'C', 100 }, { 'D', 500 }, { 'M', 1000 } };
            while (i < l - 1)
            {
                if (m[s[i]] < m[s[i + 1]])
                {
                    val += (m[s[i + 1]] - m[s[i]]);
                    i = i + 2;
                }
                else
                {
                    val += m[s[i]];
                    i += 1;
                }
            }
            if (i < l)
                val += m[s[i]];
            return val;
        }
        #endregion

        #region String to ATOI

        //Solution S=>O(N) and T=>O(1)
        //
        public int MyAtoi(string s)
        {
            long res = 0;
            var sign = 1;
            s = s.Trim();
            if (string.IsNullOrEmpty(s)) return 0;
            int index = 0;
            if (s[0] == '+' || s[0] == '-')
            {
                sign = s[0] == '+' ? 1 : -1;
                index++;
            }
            while (index < s.Length)
            {
                if (char.IsNumber(s[index]))
                {
                    res = res * 10 + s[index] - '0';
                    if (res * sign > int.MaxValue) return int.MaxValue;
                    if (res * sign < int.MinValue) return int.MinValue;
                }
                else
                {
                    break;
                }
                index++;
            }
            return (int)res * sign;
        }

        #endregion
        #region LongestCommonPrefix

        //Solution S=>O(N*M) and T=>O(min(strs.len))
        public string LongestCommonPrefix(string[] strs)
        {
            StringBuilder ans = new StringBuilder("");
            for (int i = 0; i < strs[0].Length; i++)
            {
                foreach (string s in strs)
                {
                    if (i == s.Length || s[i] != strs[0][i])
                    {
                        return ans.ToString();
                    }
                }
                ans.Append(strs[0][i]);
            }
            return ans.ToString();
        }
        #endregion

        #region Longest Palindromic Substring

        //Naive Solution S=>O(1) and T=>O(N^3)
        //calculating all subsequence and then verifying if it is 
        //palindrome

        //Better Solution S=>O(1) and T=>O(N^2)
        //by expanding from the point of considerations
        public string LongestPalindromeSubstring(string s)
        {
            int resl = 0,resr = 0,len = 0;
            for (int i = 0; i < s.Length; i++)
            {
                //checking for odd length
                int l = i;
                int r = i;
                while (l >= 0 && r < s.Length && s[l] == s[r])
                {
                    if (len < r - l + 1)
                    {
                        resr = r;
                        len = r - l + 1;
                        resl = l;
                    }
                    l -= 1;
                    r += 1;
                }
                //checking for even length
                l = i;
                r = i + 1;
                while (l >= 0 && r < s.Length && s[l] == s[r])
                {
                    if (len < r - l + 1)
                    {
                        resr = r + 1;
                        len = r - l + 1;
                        resl = l;
                    }
                    l -= 1;
                    r += 1;
                }
            }
            return s.Substring(resl, len);
        }
        #endregion

        #region repeated string match
        //find whether the string A can contain B by repeating adding the A

        //Solution 
        //by calculating the number of times a string is already present and add 2 to it
        //as the string can be added before and after the string

        public int RepeatedStringMatch(string a, string b)
        {
            int m = b.Length / a.Length;
            string temp = a;
            int cnt = 1;
            for (int i = 0; i < m + 2; i++)
            {
                if (a.Contains(b))
                {
                    return cnt;
                }
                else
                {
                    //concatenate the string to itself
                    a += temp;
                    cnt += 1;
                }
            }
            return -1;
        }

        #endregion

        #region Return first index of occurence 

        //Naive Solution S=>O(1) and T=>O(N^2)
        //using two loops

        //Better solution S=>O(1) and T=>O(~N*M) performs better on average(N+M)
        //comparing the size and first character and then 
        //checking

        public int StrStr(string haystack, string needle)
        {
            for (int i = 0; i < haystack.Length; i++)
            {
                if (haystack[i] == needle[0] && i + needle.Length - 1 < haystack.Length)
                {
                    bool flag = false;
                    for (int j = 0; j < needle.Length; j++)
                    {
                        if (haystack[i + j] != needle[j])
                            flag = true;
                    }
                    if (!flag)
                        return i;
                }
            }
            return -1;
        }

        //Optimal Solution S=>O(M) and T=>O(N+M)
        //using KMP algo

        public int StrStrOptimal(string haystack, string needle)
        {
            if (needle == "")
                return 0;
            int[] LPS = new int[needle.Length];
            int prev_LPS = 0;
            int i = 1;
            while (i < needle.Length)
            {
                if (needle[i] == needle[prev_LPS])
                {
                    LPS[i] += prev_LPS + 1;
                    prev_LPS += 1;
                    i += 1;
                }
                else if (prev_LPS == 0)
                {
                    LPS[i] = 0;
                    i += 1;
                }
                else
                {
                    prev_LPS = LPS[prev_LPS - 1];
                }
            }
            i = 0;
            int j = 0;
            while (i < haystack.Length)
            {
                if (haystack[i] == needle[j])
                {
                    i += 1;
                    j += 1;
                }
                else
                {
                    if (j == 0)
                    {
                        i += 1;
                    }
                    else
                    {
                        j = LPS[j - 1];
                    }
                }
                if (j == needle.Length)
                    return i - needle.Length;
            }
            return -1;
        }

        #endregion
    }
}
