using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace CustomArrayPool
{
    class Program
    {
        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

            var n = 5;
            var doubleArrayPool = new CustomArrayPool<double>(n, 1000);
            var tasks = new List<Task>(n);
            var r = new Random();

            tasks.Add(
            Task.Factory.StartNew(() =>
                {
                   
                    var sum = 0d;

                    for (int i = 0; i < 10000; i++)
                    {
                        var arr = doubleArrayPool.Rent();
                        Thread.Sleep(r.Next(100));
                        try
                        {
                            for (int j = 0; j < arr.Length; j++)
                            {
                                sum += arr[j];
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        finally
                        {
                            doubleArrayPool.UnRent(arr);
                        }
                    }
                })
            );

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine("Hello World!");
            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
