using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SteelMeltingMixture.Domain
{
    public class Variants
    {
        /// <summary>
        /// ID варианта расчета
        /// </summary>
        [Key]
        public int ID_Variant { get; set; }

        /// <summary>
        /// Название варианта расчета (обязательное поле)
        /// </summary>
        [Required(ErrorMessage = "Вы не ввели название варианта расчета")]
        [Display(Name = "Название варианта")]
        public string NameVariant { get; set; }

        /// <summary>
        /// Дата расчета
        /// </summary>     
        [Required(ErrorMessage = "Вы не заполнили дату выполнения расчета")]
        [Display(Name = "Дата выполнения расчета")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateVariant { get; set; }

        /// <summary>
        /// Комментарий к варианту расчета
        /// </summary>        
        [Display(Name = "Комментарий к варианту расчета")]
        public string Description { get; set; }
       

        public UserProfile Owner { get; set; }
    }
}