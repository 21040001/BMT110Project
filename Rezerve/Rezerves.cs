using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Rezerve
{
    public partial class Rezerves : Window
    {
        string connectionString = "Server=mssql-197313-0.cloudclusters.net,19998;Database=Rezerve;User Id=admin;Password=D12345678d;Encrypt=True;TrustServerCertificate=True;";
        private string secilenTip = "";
        public Rezerves()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<RezerveModel> rezerveler = new List<RezerveModel>();
            string idUser = "";
            string query = "";

            if (!string.IsNullOrWhiteSpace(TcNumber.Text))
            {
                int num;
                if (!int.TryParse(TcNumber.Text, out num))
                {
                    MessageBox.Show("TC numarası yalnızca rakamlardan oluşmalıdır.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    if (secilenTip == "Yes")
                    {
                        string doktorQuery = "SELECT name FROM doctors WHERE tc_number = @tc";
                        SqlCommand doktorCmd = new SqlCommand(doktorQuery, conn);
                        doktorCmd.Parameters.AddWithValue("@tc", num);

                        object result = doktorCmd.ExecuteScalar();
                        if (result != null)
                        {
                            idUser = Convert.ToString(result);
                            query = "SELECT * FROM rezerves WHERE doctor = @id";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@id", idUser);

                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                rezerveler.Add(new RezerveModel
                                {
                                    id = Convert.ToInt32(reader["id"]),
                                    name = reader["name"].ToString(),
                                    surname = reader["surname"].ToString(),
                                    day = reader["day"].ToString(),
                                    hour = reader["hour"].ToString(),
                                    doctor = reader["doctor"].ToString(),
                                    id_user = Convert.ToInt32(reader["id_user"]),
                                    rezerve_type = reader["rezerve_type"].ToString()
                                });
                            }
                        }
                        else
                        {
                            MessageBox.Show("TC numarasına ait doktor bulunamadı.");
                            return;
                        }
                    }
                    else
                    {
                        // Kullanıcının kendisi
                        query = "SELECT * FROM rezerves WHERE id_user = @id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", num);

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            rezerveler.Add(new RezerveModel
                            {
                                id = Convert.ToInt32(reader["id"]),
                                name = reader["name"].ToString(),
                                surname = reader["surname"].ToString(),
                                day = reader["day"].ToString(),
                                hour = reader["hour"].ToString(),
                                doctor = reader["doctor"].ToString(),
                                id_user = Convert.ToInt32(reader["id_user"]),
                                rezerve_type = reader["rezerve_type"].ToString()
                            });
                        }
                    }
                    
                }

                listViewRezerveler.ItemsSource = rezerveler;
            }
            else
            {
                MessageBox.Show("Lütfen TC numaranızı girin.");
            }
        }



        private void KabulEtClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var rezerve = button.DataContext as RezerveModel;

            MessageBox.Show($"Kabul edildi: {rezerve.name} {rezerve.surname} (ID: {rezerve.id})");

        }

        private void IptalEtClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var rezerve = button.DataContext as RezerveModel;

            MessageBox.Show($"İptal edildi: {rezerve.name} {rezerve.surname} (ID: {rezerve.id})");

        }
        

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                secilenTip = rb.Content.ToString();
            }
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            Menu main = new Menu();
            main.Show();
            this.Close(); // Menü penceresini kapat
        }
    }
}
