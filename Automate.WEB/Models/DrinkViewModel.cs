﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Automate.WEB.Models
{
    public class DrinkViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PictureId { get; set; }
        public int Number { get; set; }
        public int Price { get; set; }
    }
}