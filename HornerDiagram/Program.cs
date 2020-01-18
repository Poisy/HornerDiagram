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
            Console.WriteLine("Type only the coefficient and press Enter for each.\n" +
                "For example (x^4 + 2x^3 - 3x - 45) ==> (1, 2, 0, -3, -45)");
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
            for (int i = lastNumber > 0 ? -lastNumber : lastNumber; i < Math.Abs(lastNumber); i++)
            {
                if (i != 0 && lastNumber % i == 0)
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
            if (dividers.Count == 0)
            {
                Console.WriteLine("No possible polynomials or invalid input.");
                return;
            }
            for (int i = 0; i < dividers.Count; i++)
            {
                string result = "";
                if (dividers[i] > 0) result = $"(x - {dividers[i]})(";
                else result = $"(x + {Math.Abs(dividers[i])})(";
                for (int k = results[i].Count; k > 1; k--)
                {
                    int tempNum = results[i][results[i].Count - k];
                    if (tempNum > 0 && k == results[i].Count)
                    {
                        result += $"{(tempNum > 1 ? tempNum.ToString() : "")}{(k-2 > 1 ? $"x^{k-2}" : "x")} ";
                        continue;
                    }
                    else if (tempNum > 0) result += $"+ {(tempNum > 1 ? tempNum.ToString() : "")}{(k - 2 > 1 ? $"x^{k - 2}" : "x")} ";
                    else if (tempNum < 0) result += $"- {-tempNum}{(k - 2 > 1 ? $"x^{k - 2}" : "x")} ";
                }
                result = result.Substring(0, result.Length - 2);
                result += ")";
                Console.WriteLine(result);
            }
        }
    }
}
