using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Data_Entry_Project
{
    /// <summary>
    /// Interaction logic for ItemDetails.xaml
    /// </summary>
    public partial class ItemDetails : Window
    {
        private Airplane _airplane;
        public ItemDetails(Airplane airplane)
        {
            InitializeComponent();
            _airplane = airplane;
            GetAirplane();
        }

        private void GetAirplane()
        {
            var airplane = SQLiteDataAccess.LoadAirplanes().Where(p => p.Id == _airplane.Id).FirstOrDefault();
            using (var ms = new MemoryStream(airplane.AirplaneImage))
            {
                AirplaneImage.Source = Extensions.GetImageStream(System.Drawing.Image.FromStream(ms));
            }

            AirplaneName.Content = airplane.AirplaneName;
            AirplaneNumber.Content = airplane.AirplaneNumber;
            AirplaneType.Content = airplane.AirplaneSpec;
        }
    }
}
