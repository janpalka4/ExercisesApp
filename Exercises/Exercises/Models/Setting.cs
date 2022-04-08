using System;
using System.Collections.Generic;
using System.Text;

namespace Exercises.Models
{
    [Serializable]
    public class Setting
    { 
        public bool SilentApp { get; set; }

        public static Setting GetDefault()
        {
            return new Setting() { SilentApp = false };
        }
    }
}
