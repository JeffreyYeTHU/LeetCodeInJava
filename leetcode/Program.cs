﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeetCode
{
    class Program
    {
        static void Main(string[] args)
        {
            // var dt = new DelayTime();
            // var times = new int[3][] {
            //     new int[3] {2, 1, 1 },
            //     new int[3] {2, 3, 1},
            //     new int[3] {3, 4, 1}
            // };
            // int result = dt.Dijkstra(times, 4, 2);
            //int result = dt.BellFord(times, 4, 2);
            // int[] nums = new int[] { -1, 0, 1, 2, -1, -4 };
            // Array.Sort(nums);
            // var res = NSum(nums, 3, 0, 0);


            var maxAdv = new MaxAdvantage();
            var nums1 = new int[] { 2, 7, 11, 15 };
            var nums2 = new int[] { 1, 10, 4, 11 };
            var res = maxAdv.AdvantageCount(nums1, nums2);
        }

        // Problem 651 is not available in LeetCode, so I write my own
        private static int Problem651(int n)
        {
            // dp[i] : for i operations, the max A's can get
            if (n <= 6)
                return n;
            int[] dp = new int[n + 1];
            dp[0] = 0;
            for (int i = 1; i <= n; i++)
            {
                dp[i] = dp[i - 1] + 1;  // just insert A
                for (int j = i - 3; j >= 2; j--)
                {
                    dp[i] = Math.Max(dp[i], dp[j] * (i - j - 3 + 1));
                }
            }
            return dp[n];
        }

        private static void SortAlgoTest()
        {
            int[] arr = new int[] { 2, 5, 3, 1, 4 };
            var sroter = new Sorter();

            // bubble sort, and quick sort
            // sroter.BubbleSort(arr);
            // sroter.QuickSort(arr, 0, arr.Length - 1);

            // insert sort
            // sroter.InsertSort(arr);
            // sroter.ShellSort(arr);

            // select sort
            // sroter.SelectSort(arr);

            // heap sort
            // sroter.HeapSort(arr);

            // merger sort
            var res = sroter.MergeSort(arr);
        }

        private static IList<IList<int>> NSum(int[] nums, int n, int start, int target)
        {
            int sz = nums.Length;
            var res = new List<IList<int>>();
            if (n < 2 || sz < n)
                return res;

            if (n == 2)
            {
                int lo = start;
                int hi = sz - 1;
                while (lo < hi)
                {
                    int left = nums[lo];
                    int right = nums[hi];
                    int curr = left + right;
                    if (curr < target)
                    {
                        while (lo < hi && nums[lo] == left)
                            lo++;
                    }
                    else if (curr > target)
                    {
                        while (lo < hi && nums[hi] == right)
                            hi--;
                    }
                    else
                    {
                        var find = new List<int> { nums[lo], nums[hi] };
                        res.Add(find);
                        while (lo < hi && nums[lo] == left)
                            lo++;
                        while (lo < hi && nums[hi] == right)
                            hi--;
                    }
                }
                return res;
            }
            else
            {
                for (int i = start; i < sz;)
                {
                    var subSum = NSum(nums, n - 1, i + 1, target - nums[i]);
                    foreach (var sub in subSum)
                    {
                        sub.Add(nums[i]);
                        res.Add(sub);
                    }
                    while (i < sz - 1 && nums[i + 1] == nums[i])
                        i++;
                }
                return res;
            }
        }
    }

    // Surounded area: leetcode 130
    //class Solution
    //{
    //    char[][] board;
    //    int m, n;
    //    public void Solve(char[][] board)
    //    {
    //        this.board = board;
    //        m = board.Length;
    //        n = board[0].Length;

    //        // mark
    //        for (int i = 0; i < m; i++)
    //        {
    //            Dfs(i, 0);
    //            Dfs(i, n - 1);
    //        }
    //        for (int j = 0; j < n; j++)
    //        {
    //            Dfs(0, j);
    //            Dfs(m - 1, j);
    //        }

    //        // remark
    //        for (int i = 0; i < m; i++)
    //        {
    //            for (int j = 0; j < n; j++)
    //            {
    //                if (board[i][j] == 'O')
    //                    board[i][j] = 'X';
    //                else if (board[i][j] == 'Y')
    //                    board[i][j] = 'O';
    //            }
    //        }
    //    }

    //    // Mark all reachable region as 'Y'
    //    void Dfs(int row, int col)
    //    {
    //        if (row < 0 || col < 0 || row >= m || col >= n || board[row][col] == 'X' || board[row][col] == 'Y')
    //            return;

    //        board[row][col] = 'Y';
    //        Dfs(row, col - 1);
    //        Dfs(row, col + 1);
    //        Dfs(row - 1, col);
    //        Dfs(row + 1, col);
    //    }
    //}

    // example of union-find
    class Solution
    {
        public bool EquationsPossible(String[] equations)
        {
            var uf = new Uf(26);
            foreach (var eq in equations)
            {
                if (eq[1] == '=')
                {
                    uf.Union(eq[0] - 'a', eq[3] - 'a');
                }
            }
            foreach (var eq in equations)
            {
                if (eq[1] == '!')
                {
                    if (uf.Find(eq[0] - 'a') == uf.Find(eq[3] - 'a'))
                        return false;
                }
            }
            return true;
        }

        class Uf
        {
            int[] parent;
            public Uf(int n)
            {
                parent = new int[n];
                for (int i = 0; i < n; i++)
                    parent[i] = i;
            }

            public void Union(int x, int y)
            {
                int px = Find(x);
                int py = Find(y);
                if (px != py)
                    parent[py] = px;
            }

            public int Find(int x)
            {
                while (parent[x] != x)
                {
                    parent[x] = parent[parent[x]];
                    x = parent[x];
                }
                return x;
            }
        }
    }
}