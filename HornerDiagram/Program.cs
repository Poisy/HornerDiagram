using System;
using System.Collections.Generic;

namespace HornerDiagram
{
    class Program
    {
        static void Main()
        {
            Data.Add();
            Data.Calculate();
            Data.Print();
        }
    }
    static class Data
    {
        private static List<int> coefficient = new List<int>();
        private static List<List<int>> results = new List<List<int>>();
        private static List<int> dividers = new List<int>();
        private static int lastNumber;

        public static void Add()
        {
            string input = Console.ReadLine();
            while (!string.IsNullOrEmpty(input))
            {
                coefficient.Add(Convert.ToInt32(input));
                input = Console.ReadLine();
            }
            lastNumber = coefficient[coefficient.Count - 1];
        }
        public static void Calculate()
        {
            for (int i = 1; i < Math.Abs(lastNumber); i++)
            {
                if (lastNumber % i == 0)
                {
                    List<int> tempList = new List<int>();
                    int temp = coefficient[0];
                    tempList.Add(temp);
                    for (int x = 1; x < coefficient.Count; x++)
                    {
                        temp = i * temp + coefficient[x];
                        tempList.Add(temp);
                    }
                    if (temp == 0)
                    {
                        results.Add(tempList);
                        dividers.Add(i);
                    }
                }
            }
        }
        public static void Print()
        {
            for (int i = 0; i < dividers.Count; i++)
            {
                string result = "";
                if (dividers[i] > 0) result = $"(x - {dividers[i]})(";
                else result = $"(x + {dividers[i]})(";
                for (int k = results[i].Count; k > 1; k--)
                {
                    int tempNum = results[i][results[i].Count - k];
                    if (tempNum > 0 && k == results[i].Count)
                    {
                        result += $"{tempNum}{(k-2 > 1 ? $"x^{k-2}" : "x")} ";
                        continue;
                    }
                    else if (tempNum > 0) result += $"+ {tempNum}{(k - 2 > 1 ? $"x^{k - 2}" : "x")} ";
                    else result += $"- {-tempNum}{(k - 2 > 1 ? $"x^{k - 2}" : "x")} ";
                }
                result = result.Substring(0, result.Length - 2);
                result += ")";
                Console.WriteLine(result);
            }
        }
    }
}
