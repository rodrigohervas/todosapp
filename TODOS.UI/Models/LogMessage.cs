using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODOS.UI.Models
{
    public static class LogMessage
    {
        public static string generateMessage(Exception ex)
        {
            return Environment.NewLine +
                   $"||======== LOG START ========||" +
                   Environment.NewLine +
                   $"An error happened in: " +
                   Environment.NewLine +
                   $"Class [{ex.TargetSite.ReflectedType.FullName}] | " +
                   Environment.NewLine +
                   $"Method: [{ex.TargetSite.Name}] | " +
                   Environment.NewLine +
                   $"Message: [{ex.Message}] | " +
                   Environment.NewLine +
                   $"Stacktrace: [{ex.StackTrace}]" + 
                   Environment.NewLine + 
                   $"Exception Type: [{ex.GetType().FullName}]." + 
                   Environment.NewLine +
                   $"||======== LOG END ==========||";
        }
    }
}
