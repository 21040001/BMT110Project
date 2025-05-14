using System.Windows;

namespace Rezerve
{
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void MainWindowButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close(); // Menü penceresini kapat
        }

        private void RezervesButton_Click(object sender, RoutedEventArgs e)
        {
            Rezerves rezerv = new Rezerves();
            rezerv.Show();
            this.Close(); // Menü penceresini kapat
        }
    }
}
