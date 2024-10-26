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

namespace WPFAwait
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {

        private StringBuilder sb = new StringBuilder();

        public  MainWindow()
        {
            InitializeComponent();
            int CT0 = Thread.CurrentThread.ManagedThreadId;
            sb.Append("In MainWindow ThreadID:" + CT0 + "\n");

            
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            int CT = Thread.CurrentThread.ManagedThreadId;
            sb.Append("In BtnClick before await Method1  ThreadID:" + CT+"\n");

            await Method1();

            int CT2 = Thread.CurrentThread.ManagedThreadId;
            sb.Append("In BtnClick after await Method1  ThreadID:" + CT2 + "\n");
            string log = sb.ToString();
            label.Content = log;
        }


        private async void button2_Click(object sender, RoutedEventArgs e)
        {
            int CT = Thread.CurrentThread.ManagedThreadId;
            sb.Append("In Btn2Click before await Method1  ThreadID:" + CT + "\n");

            await Method1();

            int CT2 = Thread.CurrentThread.ManagedThreadId;
            sb.Append("In Btn2Click after await Method1  ThreadID:" + CT2 + "\n");
            string log = sb.ToString();
            label.Content = log;
        }
        public  async Task<int> Method1()
        {
            int count = 0;
            int CT3 = Thread.CurrentThread.ManagedThreadId;
            sb.Append("In Method1 before await Task.Run  ThreadID:" + CT3 + "\n");
            await Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    //Console.WriteLine(" Method 1");
                    count += 1;
                }

                int CT4 = Thread.CurrentThread.ManagedThreadId;
                sb.Append("In Task.Run  ThreadID:" + CT4 + "\n");
            });
            int CT5 = Thread.CurrentThread.ManagedThreadId;
            sb.Append("In Method1 after await Task.Run  ThreadID:" + CT5 + "\n");

            return count;
        }
    }
}
