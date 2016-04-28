using System;
using System.Runtime.InteropServices;

namespace AkkaMvvm.Utilities
{
    public class Guard
    {
        public static void NotNull<T>(T parameter, [Optional] string argumentName)
        {
            if (parameter == null)
            {
                var argumentText = argumentName == null
                    ? ""
                    : argumentName + " ";

                throw new ArgumentNullException($"Argument {argumentText }must not be null)");
            }
        }
    }
}