using System;
using System.Collections.Generic;

namespace ProjetoB2_OrdenacaoBusca
{
    public class MetricsCollector
    {
        private class Metrics
        {
            public int Comparisons { get; set; }
            public int Swaps { get; set; }
        }

        private readonly Dictionary<string, Metrics> metrics = new Dictionary<string, Metrics>();

        public void IncrementComparison(string algorithm)
        {
            if (!metrics.ContainsKey(algorithm))
                metrics[algorithm] = new Metrics();

            metrics[algorithm].Comparisons++;
        }

        public void IncrementSwap(string algorithm)
        {
            if (!metrics.ContainsKey(algorithm))
                metrics[algorithm] = new Metrics();

            metrics[algorithm].Swaps++;
        }

        public (int Comparisons, int Swaps) GetMetrics(string algorithm)
        {
            if (!metrics.ContainsKey(algorithm))
                return (0, 0);

            var metric = metrics[algorithm];
            return (metric.Comparisons, metric.Swaps);
        }

        public void ResetMetrics(string algorithm)
        {
            if (metrics.ContainsKey(algorithm))
                metrics[algorithm] = new Metrics();
        }

        public void ResetAllMetrics()
        {
            metrics.Clear();
        }
    }
}

