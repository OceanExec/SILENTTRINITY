﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace Kaliya.Utils
{
    public static class Retry
    {
        public static T Do<T>(Func<T> action, TimeSpan retryInterval,
            int maxAttempts = 3)
        {
            var exceptions = new List<Exception>();

            for (var attempts = 0; attempts < maxAttempts; attempts++)
            {
                try
                {
                    if (attempts > 0)
                    {
                        Thread.Sleep(retryInterval);
                    }
#if DEBUG
                    Console.WriteLine($"[-] Attempt #{attempts + 1}");
#endif
                    return action();
                }
                catch (Exception ex)
                {
#if DEBUG
                    Console.WriteLine("\t[!] {0}", ex.Message);
#endif
                    exceptions.Add(ex);
                }
            }

            throw new AggregateException(exceptions);
        }
    }
}