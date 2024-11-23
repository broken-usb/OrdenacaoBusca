using System;

namespace ProjetoB2_OrdenacaoBusca.Classes
{
    public class SortingStatistics
    {
        public string Algorithm { get; set; }
        public int Comparisons { get; set; }
        public int Swaps { get; set; }
        public long ExecutionTimeMs { get; set; }

        public SortingStatistics(string algorithm, int comparisons, int swaps, long executionTimeMs)
        {
            Algorithm = algorithm;
            Comparisons = comparisons;
            Swaps = swaps;
            ExecutionTimeMs = executionTimeMs;
        }

        public override string ToString()
        {
            return $"{Algorithm}: Comparisons={Comparisons}, Swaps={Swaps}, Time={ExecutionTimeMs}ms";
        }
    }
}
