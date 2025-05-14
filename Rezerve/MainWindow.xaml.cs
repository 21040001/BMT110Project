using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Rezerve
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DoktorlariYukle();
            Saatler();
        }

        string connectionString = "Server=localhost\\SQLEXPRESS,1433;Database=HastaneRS;User Id=sa;Password=12345;Encrypt=True;TrustServerCertificate=True;";
        private void DoktorlariYukle()
        {
            try
            {

                string query = "SELECT name, surname, id FROM doctors";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd); 
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboBox1.ItemsSource = dt.DefaultView;
                    comboBox1.DisplayMemberPath = "name";      // Kullanıcıya görünen
                    comboBox1.SelectedValuePath = "id";        // Arka planda tutulacak (primary key gibi)

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void Saatler()
        {
            comboBox2.Items.Add("09:00:00");
            comboBox2.Items.Add("10:00:00");
            comboBox2.Items.Add("11:00:00");
            comboBox2.Items.Add("12:00:00");
            comboBox2.Items.Add("13:00:00");
            comboBox2.Items.Add("14:00:00");
            comboBox2.Items.Add("15:00:00");
            comboBox2.Items.Add("16:00:00");
            comboBox3.Items.Add("Monday");
            comboBox3.Items.Add("Tuesday");
            comboBox3.Items.Add("Wednesday");
            comboBox3.Items.Add("Thursday");
            comboBox3.Items.Add("Friday");

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string query = "INSERT INTO rezerves(name, surname, day, hour, doctor, id_user, rezerve_type) VALUES (@name, @surname, @day, @hour, @doctor,@tcnumber, @type);";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", name.Text);
                    cmd.Parameters.AddWithValue("@surname", surname.Text);
                    cmd.Parameters.AddWithValue("@day", comboBox3.Text);
                    cmd.Parameters.AddWithValue("@hour", comboBox2.Text);
                    cmd.Parameters.AddWithValue("@doctor", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@tcnumber", Convert.ToInt32(tcnumber.Text));
                    cmd.Parameters.AddWithValue("@type", "Face to Face");
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Successful.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void Button_Click12(object sender, RoutedEventArgs e)
        {
            string query = "INSERT INTO rezerves(name, surname, day, hour, doctor, id_user, rezerve_type) VALUES (@name, @surname, @day, @hour, @doctor,@tcnumber, @type);";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", name.Text);
                    cmd.Parameters.AddWithValue("@surname", surname.Text);
                    cmd.Parameters.AddWithValue("@day", comboBox3.Text);
                    cmd.Parameters.AddWithValue("@hour", comboBox2.Text);
                    cmd.Parameters.AddWithValue("@doctor", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@tcnumber", Convert.ToInt32(tcnumber.Text));
                    cmd.Parameters.AddWithValue("@type", "Online");
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Successful.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Menu main = new Menu();
            main.Show();
            this.Close(); // Menü penceresini kapat
        }
    }
}
