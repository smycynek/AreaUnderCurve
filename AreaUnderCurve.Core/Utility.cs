using System;
using System.Collections.Generic;
using System.Text;

namespace AreaUnderCurve.Core
{
    /// <summary>
    /// Simple logging class.
    /// </summary>
    public static class Utility
    {
        public static bool LOGGING = true;
        public static void Log(string data)
        {
            System.Diagnostics.Debug.WriteLine(data);
            if (Utility.LOGGING)
                Console.WriteLine(data);
        }
    }
}
