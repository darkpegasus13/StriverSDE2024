﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StriverSDE
{
    class Trie
    {
        // Alphabet size (# of symbols)
        static readonly int ALPHABET_SIZE = 26;

        // trie node
        class TrieNode
        {
            public TrieNode[] children = new TrieNode[ALPHABET_SIZE];

            // isEndOfWord is true if the node represents
            // end of a word
            public bool isEndOfWord;

            public TrieNode()
            {
                isEndOfWord = false;
                for (int i = 0; i < ALPHABET_SIZE; i++)
                    children[i] = null;
            }
        };
        static TrieNode root;
        public Trie()
        {
                root = new TrieNode();
                root2 = new TrieNode2();
        }

        public void Insert(string word)
        {
            var currTieNode = root;
            foreach (var curr in word)
            {
                if (currTieNode.children[curr - 'a'] == null)
                    currTieNode.children[curr - 'a'] = new TrieNode();
                currTieNode = currTieNode.children[curr - 'a'];
            }
            currTieNode.isEndOfWord = true;
        }

        public bool Search(string word)
        {
            var currTieNode = root;
            foreach (var curr in word)
            {
                if (currTieNode.children[curr - 'a'] == null)
                    return false;
                currTieNode = currTieNode.children[curr - 'a'];
            }
            return currTieNode.isEndOfWord;
        }

        public bool StartsWith(string prefix)
        {
            var currTieNode = root;
            foreach (var curr in prefix)
            {
                if (currTieNode.children[curr - 'a'] == null)
                    return false;
                currTieNode = currTieNode.children[curr - 'a'];
            }
            return true;
        }

        #region Implement Trie -2
        //In this implementation we need to display counts of words and prefixes
        //so we are going to save endsWithcount and prefixcount
        //endswith count starting with 0 gets incremented and prefix count will increase 
        //next nodes prefix count on delete we do the opposite
        class TrieNode2
        {
            public TrieNode2[] children = new TrieNode2[ALPHABET_SIZE];

            // isEndOfWord is true if the node represents
            // end of a word
            public int endsWithCnt;
            public int prefixCnt;

            public TrieNode2()
            {
                endsWithCnt = 0;
                prefixCnt = 0;
                for (int i = 0; i < ALPHABET_SIZE; i++)
                    children[i] = null;
            }
        };
        static TrieNode2 root2;

        public void Insert2(string word)
        {
            var currTieNode = root2;
            foreach (var curr in word)
            {
                if (currTieNode.children[curr - 'a'] == null)
                    currTieNode.children[curr - 'a'] = new TrieNode2();
                else
                    currTieNode.children[curr - 'a'].prefixCnt += 1;
                currTieNode = currTieNode.children[curr - 'a'];
            }
            currTieNode.endsWithCnt += 1;
        }

        public void Delete2(string word)
        {
            var currTieNode = root2;
            foreach (var curr in word)
            {
                if (currTieNode.children[curr - 'a'] == null)
                    return;
                else
                    currTieNode.children[curr - 'a'].prefixCnt-= 1;
                currTieNode = currTieNode.children[curr - 'a'];
            }
            currTieNode.endsWithCnt -= 1;
        }

        public int CountWords(string word)
        {
            var currTieNode = root2;
            foreach (var curr in word)
            {
                if (currTieNode.children[curr - 'a'] == null)
                    return 0;
                currTieNode = currTieNode.children[curr - 'a'];
            }
            return currTieNode.endsWithCnt;
        }

        public int CountWordsPrefix(string word)
        {
            var currTieNode = root2;
            foreach (var curr in word)
            {
                if (currTieNode.children[curr - 'a'] == null)
                    return 0;
                currTieNode = currTieNode.children[curr - 'a'];
            }
            return currTieNode.prefixCnt;
        }

        #endregion

# region Longest Prefix with All prefixes
        //S=>O(1) and T=>O(NlogN+N)
        public string LongestPrefixInArray(string[] arr)
        {
            if (arr.Length == 1)
                return arr[0];
            string ans="";
            Array.Sort(arr);
            foreach (var curr in arr)
                LongestPrefixHelper(curr,ref ans);
            return ans;
        }

        public string LongestPrefixHelper(string word,ref string ans)
        {
            var flag = true;
            var currTieNode = root;
            foreach (var curr in word)
            {
                if (currTieNode.children[curr - 'a'] == null)
                {
                    currTieNode.children[curr - 'a'] = new TrieNode();
                    flag = false;
                }
                currTieNode = currTieNode.children[curr - 'a'];
            }
            currTieNode.isEndOfWord = true;
            if (flag)
                ans = word;
            return ans;
        }

        #endregion
        #region Number of distinct substrings
        //Naive Solution S=>O((N/2)*N^2) and T=>O(N^2*(logM)) M->number of element in set
        //by using set and running a nested loop on the word

        //Optimal Solution  S=>O(~~n^2)will depend on input and T=>O(N^2)
        //using trie

        public int CountDistict(string word)
        {
            int cnt = 0;
            int n = word.Length;
            for(int i = 0; i < n; i++)
            {
                for(int j = i; j < n;j++)
                {
                    this.InsertCountDinstinct(word.Substring(i, (j - i) + 1),ref cnt);
                }
            }
            return cnt+1; //as empty string is also considered
        }

        public void InsertCountDinstinct(string word, ref int cnt)
        {
            var currTieNode = root;
            foreach (var curr in word)
            {
                if (currTieNode.children[curr - 'a'] == null)
                {
                    currTieNode.children[curr - 'a'] = new TrieNode();
                    cnt++;
                }
                currTieNode = currTieNode.children[curr - 'a'];
            }
            currTieNode.isEndOfWord = true;
        }
        #endregion

    }
}
