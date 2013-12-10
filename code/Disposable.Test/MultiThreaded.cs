using System;
using System.Collections.Generic;

using System.Threading;

namespace Disposable.Test
{
    public static class MultiThreaded
    {
        public static void Setup(int count, Action workerAction)
        {
            var threads = new List<Thread>();

            for (var i = 0; i < count; i++)
            {
                threads.Add(new Thread(() => workerAction()));
            }
           
            threads.ForEach(x => x.Start());
            threads.ForEach(x => x.Join());
        }
    }
}