using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lakers;
using Nets;
namespace ConsoleApplication1
{

    class User

    {
        public string Age;

        public string Location;
        public string name

        {

            get { return name; }

            set {  name = value; }

        }

        public string Name2 { get; set; }

        public string Location2 { get; set; }

    }
    class Program
    {
        
        static void Main(string[] args)
        {
            int CT = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("In Main before Method1 ThreadID:" + CT);

            Method1();

            int CT2 = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("In Main after Method1 ThreadID:" + CT2);
            Console.Read();
        }
        private static async void Method1()
        {
            int CT6 = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("In Method1 before await ThreadID:" + CT6);

            int count=await Method2();
            Console.WriteLine("Result from  Method2:   Count: "+count);

            int CT7 = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("In Method1 after await ThreadID:" + CT7);
        }
        public static async Task<int> Method2()
        {
            int count = 0;
            int CT3 = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("In Method2 before await Task  ThreadID:" + CT3);
            await Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    //Console.WriteLine(" Method 1");
                    count += 1;
                }

                int CT4 = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine("In Task ThreadID:" + CT4);
            });
            int CT5 = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("In Method2 before await Task  ThreadID:" + CT5);
            return count;
        }



        private static FileSystemWatcher watcher = new FileSystemWatcher();
        static  void oldMain(string[] args)
        {

            Lakers.Cavaliers.shout();

            Nets.Cavaliers.shout();

            Lakers.Cavaliers.shout();

            Nets.Cavaliers.shout();

            //Console.WriteLine($"In Main Before KDAsync,Thread ID:{System.Environment.CurrentManagedThreadId}");
            //KDAsync();
            //Console.WriteLine($"In Main Return from KD Await All Done KDAsync,Thread ID:{System.Environment.CurrentManagedThreadId}");

            //Thread.Sleep(10000);
            //Console.WriteLine($"In Main After All Done KDAsync,Thread ID:{System.Environment.CurrentManagedThreadId}");

            //testNotAwaitTaskRightAway();
            //Console.WriteLine("Back in Main ");
            //Console.Read();

            //start the dir filewatcher
            string dir = $@"C:\Users\Ben Chen\Documents\Ben Good Programming techniques\C#\KD\MonitorDir";
            Directory.CreateDirectory($@"{dir}");
            startFileWatcher(dir);

            //Make the user create a dir by hitting Enter key whenever he likes
            int dirCount = 0;
            while(true)
            {
                Console.ReadKey();
                dirCount++;
                Directory.CreateDirectory($@"{dir}\dir{dirCount}");
                //Thread.Sleep(10000);
            }
            
            
        }

        private static void startFileWatcher(string dir)
        {
            
            watcher.Path = dir;
            watcher.NotifyFilter = NotifyFilters.DirectoryName| NotifyFilters.FileName;
            watcher.Filter = "*.*";
            watcher.IncludeSubdirectories = false;
            watcher.Created += new FileSystemEventHandler(OnCreated);

            watcher.Error += new ErrorEventHandler(OnError);

            watcher.EnableRaisingEvents = true;
        }

        private static void OnCreated(object source, FileSystemEventArgs e)
        {
            //Copies file to another directory.
            Console.WriteLine($"{ e.Name} is created");
        }

        private static void OnError(object source, ErrorEventArgs e)
        {
            // Do whatever
            Console.WriteLine($"Error!{ e.GetException()}");
        }
        private async static void testNotAwaitTaskRightAway()
        {
            //Task<string> LongTask = new Task<string>(() => MyMethod());
            //LongTask.Start();
            Thread.Sleep(6000);
            //string result = await LongTask;
            string result = await Task.Run(() => MyMethod());
            Console.WriteLine("Get await task result: " + result);
        }

        public static string MyMethod()
        {
            Thread.Sleep(1000);
            return "This is it";
        }
        

        public static async Task KI()
        //public static  void  KI()
        {
            
            
            Console.WriteLine($"In KI Before,Thread ID:{System.Environment.CurrentManagedThreadId}");
            Thread.Sleep(10000);
            await Task.Delay(3000);
            
            Console.WriteLine($"In KI After,Thread ID:{System.Environment.CurrentManagedThreadId}");
        }
        public static async void KDAsync()
        {
            Console.WriteLine($"In KD Before,Thread ID:{System.Environment.CurrentManagedThreadId}");
            await KI();
            //await Task.Run(() => KI());
            Console.WriteLine($"In KD Before,Thread ID:{System.Environment.CurrentManagedThreadId}");
        }
    }
}
