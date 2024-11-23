using System.Windows;

namespace ProjetoB2_OrdenacaoBusca
{
    public partial class SettingsWindow : Window
    {
        public int SortingDelay { get; private set; }

        public SettingsWindow(int currentDelay)
        {
            InitializeComponent();
            SortingDelay = currentDelay;
            DelayInput.Text = currentDelay.ToString(); // Exibe o delay atual no campo de texto
        }

        private void SaveSettings(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(DelayInput.Text, out int delay))
            {
                SortingDelay = delay;
                MessageBox.Show("Configurações salvas com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Por favor, insira um número válido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
