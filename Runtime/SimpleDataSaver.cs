using System;
using System.Collections.Generic;
using System.Text;

namespace IdkL0.SimpleDataSaver
{
    public static class SimpleDataSaver
    {
        public static bool Logs = false;

        public static void Init()
        {
            DataRegistries.RegisterDefaults();
        }
    }
}
