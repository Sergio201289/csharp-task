using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Homework_16
{
    class Program
    {
        static Random random = new Random();
        
        static async void Method(object o)
        {
            var token = (CancellationToken)o;
            int i = random.Next(1000, 20000);
            int id = (int)Task.CurrentId;
            string str = $"{new string('+', i / 1000), 20} Выполнила задача №{id}";
            while (!token.IsCancellationRequested) 
                {  
                    Console.WriteLine(str);
                    await Task.Delay(i);
                }
            if (token.IsCancellationRequested) { Console.WriteLine($"Задача {id} завершилась!"); }
        }
        static void Main(string[] args)
        {
            var cancelTokenSource = new CancellationTokenSource();
            Task[] tasks = new Task[10];
            for(int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Factory.StartNew(Method, cancelTokenSource.Token);
            }
            
            Thread.Sleep(50000);
            cancelTokenSource.Cancel();
            Console.ReadKey();
        }
    }
}