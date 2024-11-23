using System.Windows;

namespace ProjetoB2_OrdenacaoBusca
{
    public partial class OptionsWindow : Window
    {
        public int? Delay { get; private set; } // Propriedade para retornar o valor do delay

        public OptionsWindow(int currentDelay)
        {
            InitializeComponent();
            DelayTextBox.Text = currentDelay.ToString(); // Preenche com o delay atual
            SaveButton.Click += SaveButton_Click;
            CancelButton.Click += (s, e) => Close(); // Fecha sem salvar
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(DelayTextBox.Text, out int delay) && delay >= 0)
            {
                Delay = delay;
                DialogResult = true; // Sinaliza que o valor foi salvo
                Close();
            }
            else
            {
                MessageBox.Show("Insira um número válido maior ou igual a 0.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
