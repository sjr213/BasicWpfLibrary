using System;
using System.Threading.Tasks;

namespace BasicWpfLibrary;

public static class TaskHelpers
{
    // https://stackoverflow.com/questions/63245986/c-sharp-waiting-for-all-async-tasks-to-complete-when-calling-from-a-non-async-me
    public static async void FireAndForgetSafeAsync(this Task task, Action<Exception>? handleErrorAction = null)
    {
        try
        {
            await task.ConfigureAwait(true);
        }
        catch (Exception ex)
        {
            handleErrorAction?.Invoke(ex);
        }
    }
}

