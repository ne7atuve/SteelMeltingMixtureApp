using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SteelMeltingMixture.Domain
{
    public class Elements
    {
        /// <summary>
        /// ID элемента
        /// </summary>
        [Key]
        public int ID_Element { get; set; }

        /// <summary>
        /// Наименование химического элемента (обязательное поле)
        /// </summary>
        [Required(ErrorMessage = "Вы не ввели название химического элемента")]
        [Display(Name = "Название компонента")]
        public string NameElement { get; set; }

        /// <summary>
        /// Минимальная величина содержания химического элемента в шихте, %
        /// </summary>        
        [Display(Name = "Минимальная величина содержания химического элемента в шихте, %")]
        public double MinValElement { get; set; }

        /// <summary>
        /// Максимальная величина содержания химического элемента в шихте, %
        /// </summary>        
        [Display(Name = "Максимальная величина содержания химического элемента в шихте, %")]
        public double MaxValElement { get; set; }

        /// <summary>
        /// Отметить, если учитывать в расчете
        /// </summary>        
        [Display(Name = "Отметить, если учитывать в расчете")]
        public bool IsSolve { get; set; }

        public UserProfile Owner { get; set; }
    }
}