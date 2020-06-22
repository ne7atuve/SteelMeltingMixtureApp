using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SteelMeltingMixture.Domain
{
    public class ComponentsElements
    {
        /// <summary>
        /// ID компонента сталеплавильной шихты
        /// </summary>
        [Key]
        public int ID_ComponentElements { get; set; }

        /// <summary>
        /// ID компонента сталеплавильной шихты
        /// </summary>     
        public int ID_Component { get; set; }

        public Components Components { get; set; }

        /// <summary>
        /// ID элемента
        /// </summary>
        public int ID_Element { get; set; }

        public Elements Elements { get; set; }

        /// <summary>
        /// ID варианта расчета
        /// </summary>
        public int ID_Variant { get; set; }

        public Variants Variants { get; set; }

        /// <summary>
        /// Величина
        /// </summary>        
        [Display(Name = "Величина")]
        public double Val { get; set; }

        /// <summary>
        /// Отметить, если учитывать в расчете
        /// </summary>        
        [Display(Name = "Отметить, если учитывать в расчете")]
        public bool IsSolve { get; set; }

        public UserProfile Owner { get; set; }
    }
}