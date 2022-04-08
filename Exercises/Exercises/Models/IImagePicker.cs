using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Exercises.Models
{
    public interface IImagePicker
    {
         Task<Stream> GetImageStream();
    }
}
