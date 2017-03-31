using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Automate.WEB.Models
{
    public class CoinViewModel
    {
        public int Id { get; set; }

        [Required]
        [Range(0, Int32.MaxValue, ErrorMessage = "Введите целое положительное число")]
        [DisplayName("Номинал")]
        public int Nominal { get; set; }

        [Required]
        [Range(0, Int32.MaxValue, ErrorMessage = "Введите целое положительное число")]
        [DisplayName("Количество")]
        public int Number { get; set; }

        [Required]
        [DisplayName("Блокировка")]
        public bool Blocked { get; set; }
    }
}