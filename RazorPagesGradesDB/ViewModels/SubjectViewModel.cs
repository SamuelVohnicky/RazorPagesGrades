using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesGrades.ViewModels
{
    public class SubjectViewModel
    {
            /// <summary>
            /// Zkratka předmětu - MAT
            /// </summary>
            [Key]
            [Required]
            [StringLength(3, ErrorMessage = "Název tvoří právě 3 znaky", MinimumLength = 3)]
            [Display(Name = "Zkratka předmětu")]
            public string Acronym { get; set; }

            /// <summary>
            /// Celý název předmětu - Matematika
            /// </summary>
            [Display(Name = "Název předmětu")]
            public string Name { get; set; }
    }
}
