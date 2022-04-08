using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Drawing;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Exercises.Other
{
    public static class ResourceManager
    {
        public static Stream GetResourceStream(string name)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resource = assembly.GetManifestResourceNames().Where(x => x.EndsWith(name)).Single();
            return assembly.GetManifestResourceStream(resource);
        }

        public static void SaveAppConfiguration(Models.Setting setting)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Setting.bin");
            Stream s = File.Create(path);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(s, setting);
            s.Close();
        }
        public static Models.Setting LoadAppConfiguration()
        {
            Models.Setting ret = new Models.Setting();
            try
            {
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Setting.bin");
                if (!File.Exists(path)) return Models.Setting.GetDefault();
                Stream s = File.OpenRead(path);
                BinaryFormatter formatter = new BinaryFormatter();
                ret = (Models.Setting)formatter.Deserialize(s);
                s.Close();
            }catch(SerializationException ex)
            {

            }
            return ret;
        }
    }
}
