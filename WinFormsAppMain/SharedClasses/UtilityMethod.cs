using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppMain.SharedClasses
{
    public static class UtilityMethod
    {
        public static async Task<bool> RetryAsync(Func<Task> action, int maxAttempts, TimeSpan delay)
        {
            for (int attempts = 0; attempts < maxAttempts; attempts++)
            {
                try
                {
                    await action();
                    return true; // Success
                }
                catch (Exception)
                {
                    if (attempts < maxAttempts - 1)
                    {
                        await Task.Delay(delay);
                    }
                }
            }
            return false; // Failed after retries
        }


    }
}
