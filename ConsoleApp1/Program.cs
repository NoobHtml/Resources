using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// author:Jing WK
    /// updatetime:2019/3/28
    /// description:timer
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString());

            Task.Run(() =>
            {
                //Method1：阻止线程继续执行
                Thread thread = new Thread(new ThreadStart(program.Method1));
                thread.Start();
            });
            //-----------------------------------------
            Task.Run(() =>
            {
                //Method2：System.Timers.Timer类
                System.Timers.Timer t = new System.Timers.Timer(1000);//实例化Timer类，设置时间间隔
                t.Elapsed += new System.Timers.ElapsedEventHandler(program.Method2);//到达时间的时候执行事件
                t.AutoReset = true;//执行一次（false） 持续执行(true)
                t.Enabled = true;//是否执行Timer.Elapsed事件
            });
            //-----------------------------------------
            Task.Run(() =>
            {
                //Method3：使用System.Threading.Timer
                //Timer构造函数参数说明：
                //Callback：一个 TimerCallback 委托，表示要执行的方法。
                //State：一个包含回调方法要使用的信息的对象，或者为空引用（Visual Basic 中为 Nothing）。
                //dueTime：调用 callback 之前延迟的时间量（以毫秒为单位）。指定 Timeout.Infinite 以防止计时器开始计时。指定零(0) 以立即启动计时器。
                //Period：调用 callback 的时间间隔（以毫秒为单位）。指定 Timeout.Infinite 可以禁用定期终止。
                System.Threading.Timer threadTimer = new System.Threading.Timer(new System.Threading.TimerCallback(program.Method3), null, 0, 1000);
            });
            Console.ReadLine();

        }
        void Method1()
        {
            while (true)
            {
                Console.WriteLine($"Method1  {DateTime.Now.ToString()}:{Thread.CurrentThread.ManagedThreadId.ToString()}");
                Thread.CurrentThread.Join(1000);//阻止线程继续执行
            }
        }
        void Method2(object source, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine($"Method2  {DateTime.Now.ToString()}:{Thread.CurrentThread.ManagedThreadId.ToString()}");
        }
        void Method3(Object state)
        {
            Console.WriteLine($"Method3  {DateTime.Now.ToString()}:{Thread.CurrentThread.ManagedThreadId.ToString()}");
        }
    }
}
