using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Utilities
{
    internal static class StringHelper
    {

        public static string CycleDots(int maxDots = 3)
        {
            return new string('.', (int)(Time.realtimeSinceStartup % maxDots) + 1);
        }
    }
}
