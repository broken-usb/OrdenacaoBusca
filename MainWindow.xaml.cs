using ProjetoB2_OrdenacaoBusca.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ProjetoB2_OrdenacaoBusca
{
    public partial class MainWindow : Window
    {
        private bool isDarkTheme = false;
        private List<int> originalValues;
        private List<int> currentValues;
        private int sortingDelay = 10; // Delay padrão em milissegundos
        private List<SortingStatistics> statistics = new List<SortingStatistics>();

        public MainWindow()
        {
            InitializeComponent();
            GenerateValuesButton.Click += GenerateValuesButton_Click;
            SortValuesButton.Click += SortValuesButton_Click;
            OriginalValuesButton.Click += OriginalValuesButton_Click;
            ClearButton.Click += ClearButton_Click;
            ExitButton.Click += ExitButton_Click;
            SettingsButton.Click += SettingsButton_Click;
            StatisticsButton.Click += StatisticsButton_Click;
        }
        private void ToggleThemeButton_Click(object sender, RoutedEventArgs e)
        {
            if (isDarkTheme)
            {
                ((App)Application.Current).ApplyLightTheme();
                isDarkTheme = false;
            }
            else
            {
                ((App)Application.Current).ApplyDarkTheme();
                isDarkTheme = true;
            }
        }

        public partial class App : Application
        {
            public void ApplyDarkTheme()
            {
                try
                {
                    var darkTheme = new ResourceDictionary
                    {
                        Source = new Uri("DarkTheme.xaml", UriKind.Relative)
                    };

                    // Remove os dicionários antigos e aplica o novo tema
                    Resources.MergedDictionaries.Clear();
                    Resources.MergedDictionaries.Add(darkTheme);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao aplicar o tema escuro: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            public void ApplyLightTheme()
            {
                try
                {
                    var lightTheme = new ResourceDictionary
                    {
                        Source = new Uri("LightTheme.xaml", UriKind.Relative)
                    };

                    // Remove os dicionários antigos e aplica o novo tema
                    Resources.MergedDictionaries.Clear();
                    Resources.MergedDictionaries.Add(lightTheme);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao aplicar o tema claro: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void GenerateValuesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int quantity = int.Parse(((ComboBoxItem)QuantityComboBox.SelectedItem).Content.ToString());
                originalValues = RandomListGenerator.GenerateRandomList(quantity, 100).ToList();
                currentValues = new List<int>(originalValues);

                ResultsTextBlock.Text = $"Valores Gerados: {string.Join(", ", currentValues)}";
                DrawGraph(currentValues);
            }
            catch
            {
                MessageBox.Show("Selecione uma quantidade válida.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OriginalValuesButton_Click(object sender, RoutedEventArgs e)
        {
            if (originalValues == null || !originalValues.Any())
            {
                MessageBox.Show("Gere os valores primeiro.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            currentValues = new List<int>(originalValues);
            ResultsTextBlock.Text = $"Valores Originais: {string.Join(", ", currentValues)}";
            DrawGraph(currentValues);
        }

        private async void SortValuesButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentValues == null || !currentValues.Any())
            {
                MessageBox.Show("Gere os valores primeiro.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                string selectedMethod = ((ComboBoxItem)SortMethodComboBox.SelectedItem).Content.ToString();
                var stopwatch = Stopwatch.StartNew();
                int comparisons = 0, swaps = 0;

                switch (selectedMethod)
                {
                    case "Bubble Sort":
                        var resultBubble = await SortingAlgorithms.BubbleSort(
                            currentValues.ToArray(),
                            async updatedArray =>
                            {
                                // Atualizar o gráfico na interface
                                await Dispatcher.InvokeAsync(() => DrawGraph(updatedArray.ToList()));
                            },
                            sortingDelay // Passar o delay configurado
                        );

                        comparisons = resultBubble.comparisons;
                        swaps = resultBubble.swaps;
                        currentValues = resultBubble.sortedArray.ToList();
                        break;

                    case "Selection Sort":
                        var resultSelection = await SortingAlgorithms.SelectionSort(
                            currentValues.ToArray(),
                            async updatedArray =>
                            {
                                // Atualizar o gráfico na interface
                                await Dispatcher.InvokeAsync(() => DrawGraph(updatedArray.ToList()));
                            },
                            sortingDelay
                        );

                        comparisons = resultSelection.comparisons;
                        swaps = resultSelection.swaps;
                        currentValues = resultSelection.sortedArray.ToList();
                        break;

                    case "Insertion Sort":
                        var resultInsertion = await SortingAlgorithms.InsertionSort(
                            currentValues.ToArray(),
                            async updatedArray =>
                            {
                                // Atualizar o gráfico na interface
                                await Dispatcher.InvokeAsync(() => DrawGraph(updatedArray.ToList()));
                            },
                            sortingDelay
                        );

                        comparisons = resultInsertion.comparisons;
                        swaps = resultInsertion.swaps;
                        currentValues = resultInsertion.sortedArray.ToList();
                        break;

                    case "Quick Sort":
                        var resultQuick = await SortingAlgorithms.QuickSort(
                            currentValues.ToArray(),
                            async updatedArray =>
                            {
                                // Atualizar o gráfico na interface
                                await Dispatcher.InvokeAsync(() => DrawGraph(updatedArray.ToList()));
                            },
                            sortingDelay
                        );

                        comparisons = resultQuick.comparisons;
                        swaps = resultQuick.swaps;
                        currentValues = resultQuick.sortedArray.ToList();
                        break;

                    case "Merge Sort":
                        var resultMerge = await SortingAlgorithms.MergeSort(
                            currentValues.ToArray(),
                            async updatedArray =>
                            {
                                // Atualizar o gráfico na interface
                                await Dispatcher.InvokeAsync(() => DrawGraph(updatedArray.ToList()));
                            },
                            sortingDelay
                        );

                        comparisons = resultMerge.comparisons;
                        swaps = resultMerge.swaps;
                        currentValues = resultMerge.sortedArray.ToList();
                        break;

                    default:
                        MessageBox.Show("Método de ordenação não reconhecido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }

                stopwatch.Stop();
                TimeSpan elapsed = stopwatch.Elapsed;

                ResultsTextBlock.Text = $"Estatísticas do metodo " + selectedMethod + " forma armazenadas com sucesso!";

                DrawGraph(currentValues);
                statistics.Add(new SortingStatistics(selectedMethod, comparisons, swaps, (long)elapsed.TotalMilliseconds));
            }
            catch
            {
                MessageBox.Show("Erro ao ordenar os valores.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            originalValues = null;
            currentValues = null;
            ResultsTextBlock.Text = "Resultados aparecerão aqui...";
            GraphCanvas.Children.Clear();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow(sortingDelay);

            if (settingsWindow.ShowDialog() == true)
            {
                sortingDelay = settingsWindow.SortingDelay;
                ResultsTextBlock.Text = $"Velocidade de Ordenação Alterada: {(sortingDelay == 0 ? "Sem Delay" : $"{sortingDelay} ms")}";
            }
        }

        private void StatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!statistics.Any())
            {
                MessageBox.Show("Não há estatísticas disponíveis. Realize uma ordenação primeiro.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var statisticsWindow = new StatisticsWindow(statistics);
            statisticsWindow.ShowDialog();
        }

        private void DrawGraph(List<int> values)
        {
            GraphCanvas.Children.Clear();
            if (values == null || !values.Any()) return;

            double canvasWidth = GraphCanvas.ActualWidth;
            double canvasHeight = GraphCanvas.ActualHeight;
            double barWidth = canvasWidth / values.Count;

            double maxValue = values.Max();

            for (int i = 0; i < values.Count; i++)
            {
                double barHeight = (values[i] / maxValue) * canvasHeight;

                var rect = new Rectangle
                {
                    Width = barWidth - 2,
                    Height = barHeight,
                    Fill = Brushes.SteelBlue
                };

                Canvas.SetLeft(rect, i * barWidth);
                Canvas.SetTop(rect, canvasHeight - barHeight);
                GraphCanvas.Children.Add(rect);
            }
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentValues == null || !currentValues.Any())
            {
                MessageBox.Show("Gere ou ordene os valores primeiro.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(SearchValueTextBox.Text, out int searchValue))
            {
                MessageBox.Show("Insira um número válido para buscar.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string selectedMethod = ((ComboBoxItem)SearchMethodComboBox.SelectedItem)?.Content?.ToString();
            if (string.IsNullOrEmpty(selectedMethod))
            {
                MessageBox.Show("Selecione um método de busca.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                int comparisons = 0;
                int index = -1;

                if (selectedMethod == "Binary Search (Iterative)")
                {
                    index = await PerformBinarySearchIterative(currentValues.ToArray(), searchValue, comparisons);
                }
                else if (selectedMethod == "Binary Search (Recursive)")
                {
                    index = await PerformBinarySearchRecursive(currentValues.ToArray(), searchValue, 0, currentValues.Count - 1, comparisons);
                }

                if (index >= 0)
                {
                    ResultsTextBlock.Text = $"Valor {searchValue} encontrado na posição {index}. Comparações: {comparisons}.";
                }
                else
                {
                    ResultsTextBlock.Text = $"Valor {searchValue} não encontrado. Comparações: {comparisons}.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro durante a busca: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<int> PerformBinarySearchIterative(int[] array, int target, int comparisons)
        {
            int left = 0, right = array.Length - 1;

            while (left <= right)
            {
                int mid = (left + right) / 2;

                // Atualiza o gráfico para destacar o valor atual
                HighlightValueInGraph(mid, Brushes.Red);
                await Task.Delay(sortingDelay);

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

        private async Task<int> PerformBinarySearchRecursive(int[] array, int target, int left, int right, int comparisons)
        {
            if (left > right)
                return -1;

            int mid = (left + right) / 2;

            // Atualiza o gráfico para destacar o valor atual
            HighlightValueInGraph(mid, Brushes.Red);
            await Task.Delay(sortingDelay);

            comparisons++;
            if (array[mid] == target)
                return mid;

            if (array[mid] > target)
                return await PerformBinarySearchRecursive(array, target, left, mid - 1, comparisons);

            return await PerformBinarySearchRecursive(array, target, mid + 1, right, comparisons);
        }

        private void HighlightValueInGraph(int index, Brush color)
        {
            GraphCanvas.Children.Clear();

            if (currentValues == null || !currentValues.Any()) return;

            double canvasWidth = GraphCanvas.ActualWidth;
            double canvasHeight = GraphCanvas.ActualHeight;
            double barWidth = canvasWidth / currentValues.Count;

            double maxValue = currentValues.Max();

            for (int i = 0; i < currentValues.Count; i++)
            {
                double barHeight = (currentValues[i] / maxValue) * canvasHeight;

                var rect = new Rectangle
                {
                    Width = barWidth - 2,
                    Height = barHeight,
                    Fill = i == index ? color : Brushes.SteelBlue
                };

                Canvas.SetLeft(rect, i * barWidth);
                Canvas.SetTop(rect, canvasHeight - barHeight);
                GraphCanvas.Children.Add(rect);
            }
        }

    }
}
