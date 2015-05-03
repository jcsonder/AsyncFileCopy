using System;
using System.Collections.Generic;
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
using System.IO;

namespace AsyncFileCopy
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            CopyButton.IsEnabled = false;
            CopyStatusLabel.Content = "copying";

            await CopyFilesAsync();
            
            CopyButton.IsEnabled = true;
            CopyStatusLabel.Content = "idle";
        }

        private async Task CopyFilesAsync()
        {
            const string StartDirectory = @"c:\temp\AsyncFileCopy\start";
            const string EndDirectory = @"c:\temp\AsyncFileCopy\end";

            foreach (string filename in Directory.EnumerateFiles(StartDirectory))
            {
                using (FileStream SourceStream = File.Open(filename, FileMode.Open))
                {
                    using (FileStream DestinationStream = File.Create(EndDirectory + filename.Substring(filename.LastIndexOf('\\'))))
                    {
                        await SourceStream.CopyToAsync(DestinationStream);
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TimeLabel.Content = DateTime.Now.ToString();
        }
    }
}
