using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Automate.WEB.Models
{
    public class PictureViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
    }
}