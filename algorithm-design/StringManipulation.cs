using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeetCode
{
    public class StringManipulation
    {
        // 316
        public string RemoveDuplicateLetters(string s)
        {
            // count show up times
            var count = new Dictionary<char, int>();
            foreach (var ch in s)
            {
                if (!count.ContainsKey(ch))
                    count.Add(ch, 1);
                else
                    count[ch]++;
            }

            // remove duplicate
            bool[] inStack = new bool[26];
            var stack = new Stack<char>();
            foreach (var ch in s)
            {
                count[ch]--;
                if (inStack[ch - 'a']) continue;
                while (stack.Count != 0 && stack.Peek() > ch && count[stack.Peek()] != 0)
                {
                    inStack[stack.Pop() - 'a'] = false;
                }
                stack.Push(ch);
                inStack[ch - 'a'] = true;
            }

            // get result
            var sb = new StringBuilder();
            foreach (var ch in stack)
            {
                sb.Append(ch);
            }
            return new string(sb.ToString().Reverse().ToArray());
        }

        public string RemoveDuplicateLetters2(string s)
        {
            // use last index to replace count
            int[] lastIndex = new int[26];
            for (int i = 0; i < s.Length; i++)
                lastIndex[s[i] - 'a'] = i;

            // remove duplicate
            bool[] inStack = new bool[26];
            var stack = new Stack<char>();
            for (int i = 0; i < s.Length; i++)
            {
                char ch = s[i];
                if (inStack[ch - 'a']) continue;
                while (stack.Count != 0 && stack.Peek() > ch && lastIndex[stack.Peek() - 'a'] > i)
                {
                    inStack[stack.Pop() - 'a'] = false;
                }
                stack.Push(ch);
                inStack[ch - 'a'] = true;
            }

            // get result
            var sb = new StringBuilder();
            foreach (var ch in stack)
            {
                sb.Append(ch);
            }
            return new string(sb.ToString().Reverse().ToArray());
        }
    }
}