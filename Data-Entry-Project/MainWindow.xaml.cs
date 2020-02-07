using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;

namespace Data_Entry_Project
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Airplane> airplanes = new List<Airplane>();

        public MainWindow()
        {
            InitializeComponent();
            LoadAirplanesGrid();
        }
        
        private void AddRecord_Click(object sender, RoutedEventArgs e)
        {
            Airplane airplane = new Airplane()
            {
                AirplaneModel = airPlaneName.Text,
                AirplaneNumber = airPlaneNumber.Text,
                AirplaneSpec = airPlaneType.Text,
                AirplaneImage = (ImageBox.Source as BitmapImage).ToByteArray()
                
            };
            SQLiteDataAccess.SaveAirplanes(airplane);

            airPlaneName.Text = "";
            airPlaneNumber.Text = "";
            LoadAirplanesGrid();
        }
       
        private void LoadAirplanesGrid(List<Airplane> filterItems = null)
        {
            LoadAirplanes.ItemsSource = filterItems ?? SQLiteDataAccess.LoadAirplanes();
        }

        private void LoadAirplanes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadAirplanes.ItemsSource = SQLiteDataAccess.LoadAirplanes();
        }

        private void AirPlaneFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            var dbData = SQLiteDataAccess.LoadAirplanes();
            var texbox = (TextBox)sender;

            if (!string.IsNullOrEmpty(texbox.Text))
            {
                dbData = dbData.Where(p => p.AirplaneModel.ToLower().Contains(texbox.Text.ToLower())).ToList();
                LoadAirplanesGrid(dbData);
            }
            else
            {
                LoadAirplanesGrid();
            }
        }

        private void Add_photo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPG files (*.jpg, *.jpeg, *.jpe)|*.jpg; *.jpeg; *.jpe;|PNG file (*.png)|*.png|BMP file(*.bmp)|*.bmp";
            bool? result = ofd.ShowDialog();
            if (result == true)
            {
                var airplaneImage = new BitmapImage(new Uri(ofd.FileName));
                ImageBox.Source = airplaneImage;
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ItemDetails window = new ItemDetails((Airplane)LoadAirplanes.SelectedItem);
            window.Show();
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
           var data = new List<string>
           {
               "Myśliwski", "Pasażerski", "Szybowiec", "Odrzutowy"
           };
            var comboBox = sender as ComboBox;
            comboBox.ItemsSource = data;
        }

        private void DataGridRow_MouseOneClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                var airplane = (Airplane)LoadAirplanes.SelectedItem;

                using (var ms = new MemoryStream(airplane.AirplaneImage))
                {
                    ImageBox.Source = Extensions.GetImageStream(System.Drawing.Image.FromStream(ms));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd: "+ Environment.NewLine + ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }


}
