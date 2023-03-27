using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace AsyncWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        #region Async
        // Async
        private async void HelloWorldButton_Click_AsyncWait(object sender, RoutedEventArgs e)
        {
            // modify UI object in UI thread
            txt.Text = "start counting: ";

            // run a method in another thread
            //await HeavyMethodAsync(txt);
            await HeavyMethodAsync(txt);

            // modify UI object in UI thread
            txt.Text = "done";
        }
        internal async Task HeavyMethodAsync(TextBlock textBox)
        {
            for (int i = 0; i < 10; i++)
            {
                textBox.Dispatcher.Invoke(() =>
                {
                    // UI operation goes inside of Invoke
                    textBox.Text = textBox.Text + " " + i.ToString();
                });

                // CPU-bound or I/O-bound operation goes outside of Invoke
                // await won't block UI thread, unless it's run in a synchronous context
                await Task.Delay(1000);
            }
        }
        #endregion

        #region Async No Wait
        // Async
        private async void HelloWorldButton_Click_AsyncNoWait(object sender, RoutedEventArgs e)
        {
            // modify UI object in UI thread
            txt.Text = "start counting: ";

            // run a method in another thread
            //await HeavyMethodAsync(txt);
            HeavyMethodAsyncNoWait(txt);

            // modify UI object in UI thread
            txt.Text = "done";
        }
        internal async Task HeavyMethodAsyncNoWait(TextBlock textBox)
        {
            for (int i = 0; i < 10; i++)
            {
                textBox.Dispatcher.Invoke(() =>
                {
                    // UI operation goes inside of Invoke
                    textBox.Text = textBox.Text + " " + i.ToString();
                });

                // CPU-bound or I/O-bound operation goes outside of Invoke
                // await won't block UI thread, unless it's run in a synchronous context
                await Task.Delay(1000);
                //Task.Delay(1000);
            }
        }
        #endregion

        #region Sync
        // Sync
        private void HelloWorldButton_Click_Sync(object sender, RoutedEventArgs e)
        {
            // modify UI object in UI thread
            txt.Text = "start counting: ";

            // run a method in another thread
            HeavyMethod(txt);

            // modify UI object in UI thread
            txt.Text = "done";
        }

        // Sync or blocking call
        internal void HeavyMethod(TextBlock textBox)
        {
            for (int i = 0; i < 10; i++)
            {
                textBox.Dispatcher.Invoke(() =>
                {
                    // UI operation goes inside of Invoke
                    textBox.Text = textBox.Text + " " + i.ToString();
                });

                // CPU-bound or I/O-bound operation goes outside of Invoke
                Thread.Sleep(1000);
            }
        }
        #endregion
        private void HelloWorldButton_Click_Restart(object sender, RoutedEventArgs e)
        {
            // modify UI object in UI thread
            txt.Text = "start counting: ";
        }
        

    }
}
