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
            int l = s.Length, i = 0, val = 0;
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
            int resl = 0, resr = 0, len = 0;
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
        public int[] LPSCalculate(string needle)
        {
            int[] LPS = new int[needle.Length];
            int prev_LPS = 0, i = 1;
            //calculating the LPS array
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
                    prev_LPS = LPS[prev_LPS - 1];
            }
            return LPS;
        }

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
        //using KMP algo study z algorithm as well

        public int StrStrOptimal(string haystack, string needle)
        {
            if (needle == "")
                return 0;
            var LPS = LPSCalculate(needle);
            int i = 0;
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
                        i += 1;
                    else
                        j = LPS[j - 1];
                }
                if (j == needle.Length)
                    return i - needle.Length;
            }
            return -1;
        }

        #endregion

        #region Minimum characters to be added at front to make string palindrome

        //Naive Solution S=>O(1) and T=>O(N^2)
        //deleting the last char until string is a palindrome
        //or empty string 

        public bool ispalindrome(String s)
        {
            int l = s.Length;

            for (int i = 0, j = l - 1; i <= j; i++, j--)
            {
                if (s[i] != s[j])
                    return false;
            }
            return true;
        }

        int minChar(string str)
        {
            int cnt = 0;
            while (str.Length > 0)
            {
                // if string becomes palindrome then break 
                if (ispalindrome(str))
                    break;
                else
                {
                    cnt++;
                    // erase the last element of the string 
                    str = str.Substring(0, str.Length - 1);
                    //s.erase(s.begin() + s.length() - 1); 
                }
            }
            return cnt;
        }

        //Naive Solution 2 S=>O(1) and T=>O(N^2)
        //using two pointers
        public int AddMinChar(string str1)
        {
            int n = str1.Length;
            int start = 0;
            int end = n - 1;
            int res = 0;
            while (start < end)
            {
                if (str1[start] == str1[end])
                {
                    start++;
                    end--;
                }
                else
                {
                    res++;
                    start = 0;
                    end = n - res - 1;
                }
            }
            return res;
        }

        //Optimal Approach S=>O(N) and T=>O(N)
        //using KMP alog

        public int AddMinCharOptimal(string str)
        {
            char[] s = str.ToCharArray();

            // Get concatenation of string, special character  
            // and reverse string  
            Array.Reverse(s);
            string rev = new string(s);

            string concat = str + "$" + rev;

            // Get LPS array of this concatenated string 
            int[] lps = LPSCalculate(concat);
            return str.Length - lps[concat.Length - 1];
        }
        #endregion

        #region valid anagram

        //Naive Solution S=>O(1) and T=>O(logN+LogM+N+M)
        //sorting and storing a new string in the input strings
        public bool IsAnagram(string s, string t)
        {
            if (s.Length != t.Length)
                return false;
            s = String.Concat(s.OrderBy(c => c));
            t = String.Concat(t.OrderBy(c => c));
            int ptr = 0;
            while (ptr < s.Length)
            {
                if (s[ptr] != t[ptr])
                    return false;
                ptr += 1;
            }
            return true;
        }

        //Better Solution S=>O(2N) and T=>O(2N)
        //using a hashmap

        //Optimal SOlution
        //maintaining a array which corresponds to ascii value of chars

        //Naive Solution S=>O(1) as it fixed size of 26 and T=>O(logN+LogM+N+M)
        //sorting and storing a new string in the input strings
        public bool IsAnagramOptimal(string s, string t)
        {
            if (s.Length != t.Length)
                return false;
            s = String.Concat(s.OrderBy(c => c));
            t = String.Concat(t.OrderBy(c => c));
            int ptr = 0;
            while (ptr < s.Length)
            {
                if (s[ptr] != t[ptr])
                    return false;
                ptr += 1;
            }
            return true;
        }
        #endregion

        #region Count and Say

        //Optimal Solution S=>O(1) and T=>O(N)
        //using concatenation logic of strings

        public string CountAndSay(int n)
        {
            if (n == 1)
                return "1";
            if (n == 2)
                return "11";
            string ans = "11";
            for (int i = 3; i <= n; i++)
            {
                //initialising counter with 1
                int c = 1;
                //adding a delimeter at the end 
                ans = ans + "&";
                string temp = "";
                for (int j = 1; j < ans.Length; j++)
                {
                    if (ans[j] != ans[j - 1])
                    {
                        //prepending  appending count of prev element and element
                        temp = temp + c.ToString() + ans[j - 1];
                        c = 1;
                    }
                    else
                        //char matched so increasing the counter
                        c++;
                }
                //putting the temp in the ans
                ans = temp;
            }
            return ans;
        }
        #endregion

        #region Compare version numbers

        //Optimal Solution S=>O(N+M) and T=>O(MAth.Max(N,M))
        //storing the values in the array and then comparing them
        public int CompareVersion(string version1, string version2)
        {
            string[] arr1 = version1.Split('.');
            string[] arr2 = version2.Split('.');
            int mx = Math.Max(arr1.Length, arr2.Length);
            for (int i = 0; i < mx; i++)
            {
                int n1 = i >= arr1.Length ? 0 : Convert.ToInt32(arr1[i]);
                int n2 = i >= arr2.Length ? 0 : Convert.ToInt32(arr2[i]);
                if (n1 > n2)
                    return 1;
                else if (n2 > n1)
                    return -1;
            }
            return 0;
        }

        //Optimal Solution
        //using char - operator

        public int CompareVersionOptimal(string version1, string version2)
        {
            int i = 0, j = 0;
            while (i < version1.Length || j < version2.Length)
            {
                //making nums 0 again helps in avoiding
                //1.01 and 1.1 test case 
                int num1 = 0, num2 = 0;
                while (i < version1.Length && version1[i] != '.')
                {
                    num1 = num1 * 10 + (version1[i++] - '0');
                }
                while (j < version2.Length && version2[j] != '.')
                {
                    num2 = num2 * 10 + (version2[j++] - '0');
                }
                if (num1 < num2)
                {
                    return -1;
                }
                if (num1 > num2)
                {
                    return 1;
                }
                i++;
                j++;
            }
            return 0;
        }

        #endregion
    }
}
