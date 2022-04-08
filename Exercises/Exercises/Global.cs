using System;
using System.Collections.Generic;
using System.Text;

namespace Exercises
{
    public static class Global
    {
        public static Models.Setting Setting { get; set; }

        static Global()
        {
            Setting = Other.ResourceManager.LoadAppConfiguration();
        }
    }
}
