using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Automate.WEB.Models
{
    public class DrinkWithImgViewModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Название")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Картинка")]
        public int PictureId { get; set; }

        [Required]
        [Range(0, Int32.MaxValue, ErrorMessage = "Введите целое положительное число")]
        [DisplayName("Количество")]
        public int Number { get; set; }

        [Required]
        [Range(0, Int32.MaxValue, ErrorMessage = "Введите целое положительное число")]
        [DisplayName("Цена")]
        public int Price { get; set; }

        public byte[] Image { get; set; }
    }
}