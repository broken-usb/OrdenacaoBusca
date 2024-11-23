using System;
using System.Threading.Tasks;

namespace ProjetoB2_OrdenacaoBusca.Classes
{
    public static class SortingAlgorithms
    {
        // Bubble Sort
        public static async Task<(int[] sortedArray, int comparisons, int swaps)> BubbleSort(
            int[] array,
            Func<int[], Task> onStep,
            int delay)
        {
            int comparisons = 0;
            int swaps = 0;
            int[] sortedArray = (int[])array.Clone();

            for (int i = 0; i < sortedArray.Length - 1; i++)
            {
                for (int j = 0; j < sortedArray.Length - i - 1; j++)
                {
                    comparisons++;
                    if (sortedArray[j] > sortedArray[j + 1])
                    {
                        // Troca os elementos
                        (sortedArray[j], sortedArray[j + 1]) = (sortedArray[j + 1], sortedArray[j]);
                        swaps++;
                    }

                    // Chamar o callback e aplicar delay
                    if (onStep != null)
                    {
                        await onStep(sortedArray);
                    }
                    if (delay > 0)
                    {
                        await Task.Delay(delay);
                    }
                }
            }

            return (sortedArray, comparisons, swaps);
        }

        // Selection Sort
        public static async Task<(int[] sortedArray, int comparisons, int swaps)> SelectionSort(
            int[] array,
            Func<int[], Task> onStep,
            int delay)
        {
            int comparisons = 0;
            int swaps = 0;
            int[] sortedArray = (int[])array.Clone();

            for (int i = 0; i < sortedArray.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < sortedArray.Length; j++)
                {
                    comparisons++;
                    if (sortedArray[j] < sortedArray[minIndex])
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    // Troca os elementos
                    (sortedArray[i], sortedArray[minIndex]) = (sortedArray[minIndex], sortedArray[i]);
                    swaps++;
                }

                // Chamar o callback e aplicar delay
                if (onStep != null)
                {
                    await onStep(sortedArray);
                }
                if (delay > 0)
                {
                    await Task.Delay(delay);
                }
            }

            return (sortedArray, comparisons, swaps);
        }

        // Insertion Sort
        public static async Task<(int[] sortedArray, int comparisons, int swaps)> InsertionSort(
            int[] array,
            Func<int[], Task> onStep,
            int delay)
        {
            int comparisons = 0;
            int swaps = 0;
            int[] sortedArray = (int[])array.Clone();

            for (int i = 1; i < sortedArray.Length; i++)
            {
                int key = sortedArray[i];
                int j = i - 1;

                while (j >= 0 && sortedArray[j] > key)
                {
                    comparisons++;
                    sortedArray[j + 1] = sortedArray[j];
                    j--;
                    swaps++;

                    // Chamar o callback e aplicar delay
                    if (onStep != null)
                    {
                        await onStep(sortedArray);
                    }
                    if (delay > 0)
                    {
                        await Task.Delay(delay);
                    }
                }
                comparisons++; // Contabiliza a última comparação que falhou no `while`

                sortedArray[j + 1] = key;

                // Chamar o callback e aplicar delay
                if (onStep != null)
                {
                    await onStep(sortedArray);
                }
                if (delay > 0)
                {
                    await Task.Delay(delay);
                }
            }

            return (sortedArray, comparisons, swaps);
        }

        // Quick Sort
        public static async Task<(int[] sortedArray, int comparisons, int swaps)> QuickSort(
            int[] array,
            Func<int[], Task> onStep,
            int delay)
        {
            int comparisons = 0;
            int swaps = 0;
            int[] sortedArray = (int[])array.Clone();

            async Task QuickSortRecursive(int left, int right)
            {
                if (left < right)
                {
                    int pivotIndex = await Partition(left, right);
                    await QuickSortRecursive(left, pivotIndex - 1);
                    await QuickSortRecursive(pivotIndex + 1, right);
                }
            }

            async Task<int> Partition(int left, int right)
            {
                int pivot = sortedArray[right];
                int i = left - 1;

                for (int j = left; j < right; j++)
                {
                    comparisons++;
                    if (sortedArray[j] <= pivot)
                    {
                        i++;
                        (sortedArray[i], sortedArray[j]) = (sortedArray[j], sortedArray[i]);
                        swaps++;
                    }

                    // Chamar o callback e aplicar delay
                    if (onStep != null)
                    {
                        await onStep(sortedArray);
                    }
                    if (delay > 0)
                    {
                        await Task.Delay(delay);
                    }
                }

                (sortedArray[i + 1], sortedArray[right]) = (sortedArray[right], sortedArray[i + 1]);
                swaps++;

                // Chamar o callback e aplicar delay
                if (onStep != null)
                {
                    await onStep(sortedArray);
                }
                if (delay > 0)
                {
                    await Task.Delay(delay);
                }

                return i + 1;
            }

            await QuickSortRecursive(0, sortedArray.Length - 1);

            return (sortedArray, comparisons, swaps);
        }

        // Merge Sort
        public static async Task<(int[] sortedArray, int comparisons, int swaps)> MergeSort(
            int[] array,
            Func<int[], Task> onStep,
            int delay)
        {
            int comparisons = 0;
            int swaps = 0;
            int[] sortedArray = (int[])array.Clone();

            async Task MergeSortRecursive(int left, int right)
            {
                if (left < right)
                {
                    int mid = (left + right) / 2;

                    await MergeSortRecursive(left, mid);
                    await MergeSortRecursive(mid + 1, right);
                    await Merge(left, mid, right);
                }
            }

            async Task Merge(int left, int mid, int right)
            {
                int[] temp = new int[right - left + 1];
                int i = left, j = mid + 1, k = 0;

                while (i <= mid && j <= right)
                {
                    comparisons++;
                    if (sortedArray[i] <= sortedArray[j])
                    {
                        temp[k++] = sortedArray[i++];
                    }
                    else
                    {
                        temp[k++] = sortedArray[j++];
                        swaps++;
                    }

                    // Chamar o callback e aplicar delay
                    if (onStep != null)
                    {
                        await onStep(sortedArray);
                    }
                    if (delay > 0)
                    {
                        await Task.Delay(delay);
                    }
                }

                while (i <= mid) temp[k++] = sortedArray[i++];
                while (j <= right) temp[k++] = sortedArray[j++];

                for (int m = 0; m < temp.Length; m++)
                {
                    sortedArray[left + m] = temp[m];
                }

                // Chamar o callback e aplicar delay
                if (onStep != null)
                {
                    await onStep(sortedArray);
                }
                if (delay > 0)
                {
                    await Task.Delay(delay);
                }
            }

            await MergeSortRecursive(0, sortedArray.Length - 1);

            return (sortedArray, comparisons, swaps);
        }
    }
}
