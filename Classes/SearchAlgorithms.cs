using System;

namespace ProjetoB2_OrdenacaoBusca
{
    public class SearchAlgorithms
    {
        public static int BinarySearchRecursive(int[] array, int target, int left, int right, ref int comparisons)
        {
            if (left > right) return -1;

            int mid = (left + right) / 2;
            comparisons++;

            if (array[mid] == target) return mid;
            if (array[mid] > target)
                return BinarySearchRecursive(array, target, left, mid - 1, ref comparisons);

            return BinarySearchRecursive(array, target, mid + 1, right, ref comparisons);
        }

        public static int BinarySearchIterative(int[] array, int target, ref int comparisons)
        {
            int left = 0, right = array.Length - 1;

            while (left <= right)
            {
                int mid = (left + right) / 2;
                comparisons++;

                if (array[mid] == target)
                    return mid;

                if (array[mid] < target)
                    left = mid + 1;
                else
                    right = mid - 1;
            }
            return -1;
        }
    }
}
