using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows;
using SilverlightAsyncRestWcf.Common;

namespace SilverlightAsyncRestWcf
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void LoadDataButton_Click(object sender, RoutedEventArgs e)
        {
            var request = new WebClient();
            var uri = new Uri("http://localhost:5349/services/cars");
            var jsonString = await request.DownloadStringTaskAsync(uri);

            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(jsonString.ToCharArray())))
            {
                var serializer = new DataContractJsonSerializer(typeof (Car[]));
                var cars = (Car[]) serializer.ReadObject(stream);
                CarsListBox.ItemsSource = cars;
            }
        }
    }
}