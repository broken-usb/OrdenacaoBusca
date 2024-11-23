using ProjetoB2_OrdenacaoBusca.Classes;
using System.Collections.Generic;
using System.Windows;

namespace ProjetoB2_OrdenacaoBusca
{
    public partial class StatisticsWindow : Window
    {
        public StatisticsWindow(List<SortingStatistics> statistics)
        {
            InitializeComponent();
            StatisticsGrid.ItemsSource = statistics;
            StatisticsListBox.ItemsSource = statistics;
            LoadStatistics();
        }

        private void LoadStatistics()
        {
            // Example statistics for demonstration
            var statistics = new List<object>
            {
                new { Algorithm = "Bubble Sort", Comparisons = 120, Swaps = 50 },
                new { Algorithm = "Quick Sort", Comparisons = 90, Swaps = 40 },
            };

            StatisticsGrid.ItemsSource = statistics;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
