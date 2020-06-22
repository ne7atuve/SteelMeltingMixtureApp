using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SteelMeltingMixture.Domain
{
    [Table("UserProfile")]
    public class UserProfile
    {
        /// <summary>
        /// ID пользователя
        /// </summary>       
        [Key]
        public int ID_User { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        [Required(ErrorMessage = "Вы не ввели имя пользователя")]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        public ICollection<Components> Components { get; set; }

        public ICollection<Elements> Elements { get; set; }

        public ICollection<ComponentsElements> ComponentsElements { get; set; }

        public ICollection<Variants> Variants { get; set; }
    }
}