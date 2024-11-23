using System;
using System.Linq;

namespace ProjetoB2_OrdenacaoBusca
{
    public class RandomListGenerator
    {
        public static int[] GenerateRandomList(int size, int maxValue = 1000)
        {
            Random random = new Random();
            return Enumerable.Range(0, size).Select(_ => random.Next(maxValue)).ToArray();
        }

        public static int[] GenerateSortedList(int size)
        {
            return Enumerable.Range(1, size).ToArray();
        }

        public static int[] GenerateReverseSortedList(int size)
        {
            return Enumerable.Range(1, size).OrderByDescending(x => x).ToArray();
        }
    }
}
