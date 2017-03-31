using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.DAL
{
    /// <summary>
    /// Class for get byte array 
    /// from img file
    /// </summary>
    public static class Image
    {
        public static byte[] GetImg(string pathImg)
        {
            byte[] bData = File.ReadAllBytes(pathImg);

            return bData;
        }
    }
}
