using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ProjetoB2_OrdenacaoBusca
{
    public partial class App : Application
    {
        public void ApplyDarkTheme()
        {
            // Remove temas antigos
            RemoveTheme("LightTheme");

            // Adiciona o tema escuro
            AddTheme("DarkTheme");
        }

        public void ApplyLightTheme()
        {
            // Remove temas antigos
            RemoveTheme("DarkTheme");

            // Adiciona o tema claro
            AddTheme("LightTheme");
        }

        private void RemoveTheme(string themeName)
        {
            var existingTheme = Resources.MergedDictionaries.FirstOrDefault(d =>
                d.Source?.OriginalString.Contains(themeName) == true);

            if (existingTheme != null)
            {
                Resources.MergedDictionaries.Remove(existingTheme);
            }
        }

        private void AddTheme(string themeName)
        {
            try
            {
                var themeUri = new Uri($"/ProjetoB2_OrdenacaoBusca;component/Themes/{themeName}.xaml", UriKind.Relative);
                var themeResource = new ResourceDictionary { Source = themeUri };
                Resources.MergedDictionaries.Add(themeResource);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao aplicar tema '{themeName}': {ex.Message}",
                    "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}

