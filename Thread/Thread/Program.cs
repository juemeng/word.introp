using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Thread
{
    class Program
    {
        #region thread
        //static void Main(string[] args)
        //{
        //    System.Threading.Thread t1 = new System.Threading.Thread(new ThreadStart(TestMethod));
        //    System.Threading.Thread t2 = new System.Threading.Thread(new ParameterizedThreadStart(TestMethod));
        //    t1.IsBackground = true;
        //    t2.IsBackground = true;
        //    t1.Start();
        //    t2.Start("hello");
        //    Console.ReadKey();
        //}

        //public static void TestMethod()
        //{
        //    Console.WriteLine("不带参数的线程函数");
        //}

        //public static void TestMethod(object data)
        //{
        //    string datastr = data as string;
        //    Console.WriteLine("带参数的线程函数，参数为：{0}", datastr);
        //}
        #endregion

        #region thread pool
        //        static void Main(string[] args)
        //        {
        //            //将工作项加入到线程池队列中，这里可以传递一个线程参数
        //            ThreadPool.QueueUserWorkItem(TestMethod, "Hello");
        //            Console.ReadKey();
        //        }
        //
        //        public static void TestMethod(object data)
        //        {
        //            string datastr = data as string;
        //            Console.WriteLine(datastr);
        //        }
        #endregion

        #region APM

        //static void Main(string[] args)
        //{
        //    Action<string> action = TestMethod;
        //    var result = action.BeginInvoke("Jo", null, null);
        //    action.EndInvoke(result);

        //    Func<string, string> func = TestMethod1;
        //    var result1 = func.BeginInvoke("World", null, null);
        //    var text = func.EndInvoke(result1);

        //    Console.WriteLine(text);
        //    Console.ReadKey();
        //}

        //public static void TestMethod(string data)
        //{ 
        //    System.Threading.Thread.Sleep(3000);
        //    Console.WriteLine(data);
        //}

        //public static string TestMethod1(string data)
        //{
        //    System.Threading.Thread.Sleep(3000);
        //    return "Hello " + data;
        //}

        #endregion

        #region EAP
        //        static void Main(string[] args)
        //        {
        //            Func<string, byte[]> func = DownloadFile;
        //            Console.WriteLine("start");
        //            func.BeginInvoke("Hello",new AsyncCallback(DownloadFileComplete),func);
        //
        //            Console.WriteLine("console -- end");
        //            Console.ReadKey();
        //        }
        //
        //        public static byte[] DownloadFile(string url)
        //        {
        //            System.Threading.Thread.Sleep(6000);
        //            return new byte[5000];
        //        }
        //
        //        public static void DownloadFileComplete(IAsyncResult result)
        //        {
        //            var func = result.AsyncState as Func<string, byte[]>;
        //            if (func == null) return;
        //            var file = func.EndInvoke(result);
        //            Console.WriteLine("complete");
        //            Console.WriteLine(file.Length);
        //        }
        #endregion

        #region .NET EAP

        //        private static void Main(string[] args)
        //        {
        //            Console.WriteLine("start");
        //            var webClient = new WebClient();
        //            webClient.DownloadStringCompleted += WebClient_DownloadStringCompleted;
        //            webClient.DownloadStringAsync(new Uri("http://baidu.com/1.txt"));
        //
        //            Console.WriteLine("console -- end");
        //            Console.ReadKey();
        //        }
        //
        //        private static void WebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        //        {
        //            Console.WriteLine(e.Result);
        //        }

        #endregion

        #region TPL Parallel
        //        private static void Main(string[] args)
        //        {
        //            var parallelDemo = new ParallelDemo();
        //            //parallelDemo.ParallelInvokeMethod(); //Parallel.Invoke
        //            //parallelDemo.ParallelForMethod(); //Parallel.For
        //            //parallelDemo.ParallelForMethod1(); //Parallel.For slow
        //            parallelDemo.ParallelForeachMethod(); //Parallel.Foreach
        //            
        //            Console.ReadKey();
        //        }
        #endregion

        #region TPL Parallel
        //private static void Main(string[] args)
        //{
        //    var demo = new PEnumerableDemo();
        //    //demo.ListWithParallel();
        //    //demo.ConcurrentBagWithPallel();
        //    //demo.ConcurrentBagWithPallelDetails();
        //    //demo.TestPLinq();
        //    demo.TestGroupBy();
        //    Console.ReadKey();
        //}
        #endregion

        #region TPL Task
        static void Main(string[] args)
        {
            #region how to create task
            //var task1 = new Task(() =>
            //{
            //    Console.WriteLine("Hello,task");
            //});
            //task1.Start();

            //var task2 = Task.Factory.StartNew(() =>
            //{
            //    Console.WriteLine("Hello,task started by task factory");
            //});

            //Console.Read();
            #endregion

            #region life cycle
            //            var task1 = new Task(() =>
            //            {
            //                Console.WriteLine("Begin");
            //                System.Threading.Thread.Sleep(2000);
            //                Console.WriteLine("Finish");
            //            });
            //            Console.WriteLine("Before start:" + task1.Status);
            //            task1.Start();
            //            Console.WriteLine("After start:" + task1.Status);
            //            task1.Wait();
            //            Console.WriteLine("After Finish:" + task1.Status);
            //
            //            Console.Read();
            #endregion

            #region WaitAll
            //            var task1 = new Task(() =>
            //            {
            //                Console.WriteLine("Task 1 Begin");
            //                System.Threading.Thread.Sleep(2000);
            //                Console.WriteLine("Task 1 Finish");
            //            });
            //            var task2 = new Task(() =>
            //            {
            //                Console.WriteLine("Task 2 Begin");
            //                System.Threading.Thread.Sleep(3000);
            //                Console.WriteLine("Task 2 Finish");
            //            });
            //
            //            task1.Start();
            //            task2.Start();
            //            Task.WaitAll(task1, task2);
            //            Console.WriteLine("All task finished!");
            //
            //            Console.Read();
            #endregion

            #region ContinueWith
            //            var task1 = new Task(() =>
            //            {
            //                Console.WriteLine("Task 1 Begin");
            //                System.Threading.Thread.Sleep(2000);
            //                Console.WriteLine("Task 1 Finish");
            //            });
            //            var task2 = new Task(() =>
            //            {
            //                Console.WriteLine("Task 2 Begin");
            //                System.Threading.Thread.Sleep(3000);
            //                Console.WriteLine("Task 2 Finish");
            //            });
            //
            //
            //            task1.Start();
            //            task2.Start();
            //            var result = task1.ContinueWith<string>(task =>
            //            {
            //                Console.WriteLine("task1 finished!");
            //                return "This is task result!";
            //            });
            //
            //            Console.WriteLine(result.Result);
            //
            //
            //            Console.Read();
            #endregion

            #region Cancel
            //var tokenSource = new CancellationTokenSource();
            //var token = tokenSource.Token;
            //var task = Task.Factory.StartNew(() =>
            //{
            //    for (var i = 0; i < 1000; i++)
            //    {
            //        System.Threading.Thread.Sleep(1000);
            //        if (token.IsCancellationRequested)
            //        {
            //            Console.WriteLine("Abort mission success!");
            //            return;
            //        }
            //    }
            //}, token);
            //token.Register(() =>
            //{
            //    Console.WriteLine("Canceled");
            //});
            //Console.WriteLine("Press enter to cancel task...");
            //Console.ReadKey();
            //tokenSource.Cancel();

            //Console.ReadKey();
            #endregion

            #region create task in task --- parallel
            //            var pTask = Task.Factory.StartNew(() =>
            //            {
            //                var cTask = Task.Factory.StartNew(() =>
            //                {
            //                    System.Threading.Thread.Sleep(2000);
            //                    Console.WriteLine("Childen task finished!");
            //                });
            //                Console.WriteLine("Parent task finished!");
            //            });
            //            pTask.Wait();
            //            Console.WriteLine("Flag");
            //            Console.Read();
            #endregion

            #region create sub task in task
            //var pTask = Task.Factory.StartNew(() =>
            //{
            //    var cTask = Task.Factory.StartNew(() =>
            //    {
            //        System.Threading.Thread.Sleep(2000);
            //        Console.WriteLine("Childen task finished!");
            //    }, TaskCreationOptions.AttachedToParent);
            //    Console.WriteLine("Parent task finished!");
            //});
            //pTask.Wait();
            //Console.WriteLine("Flag");
            //Console.Read();
            #endregion

            #region flow
            //Task.Factory.StartNew(() =>
            //{
            //    var t1 = Task.Factory.StartNew<int>(() =>
            //    {
            //        Console.WriteLine("Task 1 running...");
            //        return 1;
            //    });
            //    t1.Wait(); //等待任务一完成
            //    var t3 = Task.Factory.StartNew<int>(() =>
            //    {
            //        Console.WriteLine("Task 3 running...");
            //        return t1.Result + 3;
            //    });
            //    var t4 = Task.Factory.StartNew<int>(() =>
            //    {
            //        Console.WriteLine("Task 2 running...");
            //        return t1.Result + 2;
            //    }).ContinueWith<int>(task =>
            //    {
            //        Console.WriteLine("Task 4 running...");
            //        return task.Result + 4;
            //    });
            //    Task.WaitAll(t3, t4);  //等待任务三和任务四完成
            //    var result = Task.Factory.StartNew(() =>
            //    {
            //        Console.WriteLine("Task Finished! The result is {0}", t3.Result + t4.Result);
            //    });
            //});
            //Console.Read();
            #endregion

            #region handle exception
            //            try
            //            {
            //                var pTask = Task.Factory.StartNew(() =>
            //                {
            //                    var cTask = Task.Factory.StartNew(() =>
            //                    {
            //                        System.Threading.Thread.Sleep(2000);
            //                        throw new Exception("cTask Error!");
            //                        Console.WriteLine("Childen task finished!");
            //                    });
            //                    throw new Exception("pTask Error!");
            //                    Console.WriteLine("Parent task finished!");
            //                });
            //
            //                pTask.Wait();
            //            }
            //            catch (AggregateException ex)
            //            {
            //                foreach (Exception inner in ex.InnerExceptions)
            //                {
            //                    Console.WriteLine(inner.Message);
            //                }
            //            }
            //            Console.WriteLine("Flag");
            //            Console.Read();
            #endregion

            #region Async/Await
            var asyncDemo = new AsyncAwaitDemo();
            //asyncDemo.DidAsync();
            //asyncDemo.GetCustomerNameAsync();
            var age = asyncDemo.GetCustomerAgeAsync().Result;
            Console.WriteLine(age);
            Console.Read();

            #endregion

            //SpinLock lock1 = new SpinLock();
        }
        #endregion


    }


    public class ParallelDemo
    {
        private readonly Stopwatch stopWatch = new Stopwatch();

        public void Run1()
        {
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("Task 1 is cost 2 sec");
        }

        public void Run2()
        {
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("Task 2 is cost 3 sec");
        }

        public void ParallelInvokeMethod()
        {
            stopWatch.Start();
            Parallel.Invoke(Run1, Run2);
            stopWatch.Stop();
            Console.WriteLine("Parallel run " + stopWatch.ElapsedMilliseconds + " ms.");

            stopWatch.Restart();
            Run1();
            Run2();
            stopWatch.Stop();
            Console.WriteLine("Normal run " + stopWatch.ElapsedMilliseconds + " ms.");
        }

        public void ParallelForMethod()
        {
            stopWatch.Start();
            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < 60000; j++)
                {
                    int sum = 0;
                    sum += i;
                }
            }
            stopWatch.Stop();
            Console.WriteLine("NormalFor run " + stopWatch.ElapsedMilliseconds + " ms.");

            stopWatch.Reset();
            stopWatch.Start();
            Parallel.For(0, 10000, item =>
            {
                for (int j = 0; j < 60000; j++)
                {
                    int sum = 0;
                    sum += item;
                }
            });
            stopWatch.Stop();
            Console.WriteLine("ParallelFor run " + stopWatch.ElapsedMilliseconds + " ms.");
        }

        public void ParallelForMethod1()
        {
            var obj = new Object();
            long num = 0;
            ConcurrentBag<long> bag = new ConcurrentBag<long>();

            stopWatch.Start();
            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < 60000; j++)
                {
                    //int sum = 0;
                    //sum += item;
                    num++;
                }
            }
            stopWatch.Stop();
            Console.WriteLine("NormalFor run " + stopWatch.ElapsedMilliseconds + " ms.");

            stopWatch.Reset();
            stopWatch.Start();
            Parallel.For(0, 10000, item =>
            {
                for (int j = 0; j < 60000; j++)
                {
                    //int sum = 0;
                    //sum += item;
                    lock (obj)
                    {
                        num++;
                    }
                }
            });
            stopWatch.Stop();
            Console.WriteLine("ParallelFor run " + stopWatch.ElapsedMilliseconds + " ms.");

        }

        public void ParallelForeachMethod()
        {
            stopWatch.Start();
            for (int i = 0; i < 10000; i++)
            {
                if (i == 5000)
                {
                    break;
                }
                for (int j = 0; j < 60000; j++)
                {
                    int sum = 0;
                    sum += i;
                }
            }
            stopWatch.Stop();
            Console.WriteLine("NormalFor run " + stopWatch.ElapsedMilliseconds + " ms.");

            stopWatch.Reset();
            stopWatch.Start();
            Parallel.ForEach(Enumerable.Range(0,10000), (item,state) =>
            {
                if (item == 5000)
                {
                    state.Break();
                }
                for (int j = 0; j < 60000; j++)
                {
                    int sum = 0;
                    sum += item;
                }
            });
            stopWatch.Stop();
            Console.WriteLine("ParallelFor run " + stopWatch.ElapsedMilliseconds + " ms.");
        }
    }

    public class PEnumerableDemo
    {
        public void ListWithParallel()
        {
            var list = new List<int>();
            Parallel.For(0, 10000, item =>
            {
                list.Add(item);
            });
            Console.WriteLine("List's count is {0}", list.Count());
        }

        public void ConcurrentBagWithPallel()
        {
            ConcurrentBag<int> list = new ConcurrentBag<int>();
            Parallel.For(0, 10000, item =>
            {
                list.Add(item);
            });
            Console.WriteLine("ConcurrentBag's count is {0}", list.Count());
        }

        public void ConcurrentBagWithPallelDetails()
        {
            ConcurrentBag<int> list = new ConcurrentBag<int>();
            Parallel.For(0, 10000, item =>
            {
                list.Add(item);
            });
            Console.WriteLine("ConcurrentBag's count is {0}", list.Count());
            int n = 0;
            foreach (int i in list)
            {
                if (n > 10)
                    break;
                n++;
                Console.WriteLine("Item[{0}] = {1}", n, i);
            }
            Console.WriteLine("ConcurrentBag's max item is {0}", list.Max());
        }

        public void TestPLinq()
        {
            Stopwatch sw = new Stopwatch();
            List<Custom> customs = new List<Custom>();
            for (int i = 0; i < 2000000; i++)
            {
                customs.Add(new Custom() { Name = "Jack", Age = 21, Address = "NewYork" });
                customs.Add(new Custom() { Name = "Jime", Age = 26, Address = "China" });
                customs.Add(new Custom() { Name = "Tina", Age = 29, Address = "ShangHai" });
                customs.Add(new Custom() { Name = "Luo", Age = 30, Address = "Beijing" });
                customs.Add(new Custom() { Name = "Wang", Age = 60, Address = "Guangdong" });
                customs.Add(new Custom() { Name = "Feng", Age = 25, Address = "YunNan" });
            }

            sw.Start();
            var result = customs.Where<Custom>(c => c.Age > 26).ToList();
            sw.Stop();
            Console.WriteLine("Linq time is {0}.", sw.ElapsedMilliseconds);

            sw.Restart();
            sw.Start();
            var result2 = customs.AsParallel().Where<Custom>(c => c.Age > 26).ToList();
            sw.Stop();
            Console.WriteLine("Parallel Linq time is {0}.", sw.ElapsedMilliseconds);
        }

        public void TestGroupBy()
        {
            Stopwatch stopWatch = new Stopwatch();
            List<Custom> customs = new List<Custom>();
            for (int i = 0; i < 2000000; i++)
            {
                customs.Add(new Custom() { Name = "Jack", Age = 21, Address = "NewYork" });
                customs.Add(new Custom() { Name = "Jime", Age = 26, Address = "China" });
                customs.Add(new Custom() { Name = "Tina", Age = 29, Address = "ShangHai" });
                customs.Add(new Custom() { Name = "Luo", Age = 30, Address = "Beijing" });
                customs.Add(new Custom() { Name = "Wang", Age = 60, Address = "Guangdong" });
                customs.Add(new Custom() { Name = "Feng", Age = 25, Address = "YunNan" });
            }

            stopWatch.Restart();
            var groupByAge = customs.GroupBy(item => item.Age).ToList();
            foreach (var item in groupByAge)
            {
                Console.WriteLine("Age={0},count = {1}", item.Key, item.Count());
            }
            stopWatch.Stop();

            Console.WriteLine("Linq group by time is: " + stopWatch.ElapsedMilliseconds);


            stopWatch.Restart();
            var lookupList = customs.ToLookup(i => i.Age);
            foreach (var item in lookupList)
            {
                Console.WriteLine("LookUP:Age={0},count = {1}", item.Key, item.Count());
            }
            stopWatch.Stop();
            Console.WriteLine("LookUp group by time is: " + stopWatch.ElapsedMilliseconds);
        }
    }


    public class Custom
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }

    public class AsyncAwaitDemo
    {
        public async void DidAsync()
        {
            await DoSomeThing();
            Console.WriteLine("Task end");
        }

        public async void GetCustomerNameAsync()
        {
            var name = await GetCustomerName();
            Console.WriteLine("Task end --- "+ name);
        }

        public async Task<int> GetCustomerAgeAsync()
        {
            Console.WriteLine("Start task");
            await Task.Delay(5000);
            return 18;
        }

        private Task DoSomeThing()
        {
            return Task.Run(() =>
            {
                Console.WriteLine("Start task");
                System.Threading.Thread.Sleep(10000);
            });
        }

        private Task<string> GetCustomerName()
        {
            Console.WriteLine("Start task");
            System.Threading.Thread.Sleep(10000);
            return Task.FromResult("Jo L Zou");
        }

        
    }
}
