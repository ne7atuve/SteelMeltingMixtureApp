using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SteelMeltingMixture.Domain
{
    public class Components
    {
        /// <summary>
        /// ID компонента сталеплавильной шихты
        /// </summary>
        [Key]
        public int ID_Component { get; set; }

        /// <summary>
        /// Наименование компонента сталеплавильной шихты (обязательное поле)
        /// </summary>
        [Required(ErrorMessage = "Вы не ввели название компонента")]
        [Display(Name = "Название компонента")]
        public string NameComponent { get; set; }

        /// <summary>
        /// Комментарии
        /// </summary>        
        [Display(Name = "Комментарии")]
        public string Description { get; set; }

        /// <summary>
        /// Стоимость компонента шихты, у.е.
        /// </summary>        
        [Display(Name = "Стоимость компонента шихты, у.е.")]
        public double Cost { get; set; }

        /// <summary>
        /// Минимальная величина содержания компонента в шихте, %
        /// </summary>        
        [Display(Name = "Минимальная величина содержания компонента в шихте, %")]
        public double MinValComponent { get; set; }

        /// <summary>
        /// Максимальная величина содержания компонента в шихте, %
        /// </summary>        
        [Display(Name = "Максимальная величина содержания компонента в шихте, %")]
        public double MaxValComponent { get; set; }

        /// <summary>
        /// Отметить, если учитывать в расчете
        /// </summary>        
        [Display(Name = "Отметить, если учитывать в расчете")]
        public bool IsSolve { get; set; }

        public UserProfile Owner { get; set; }
    }
}