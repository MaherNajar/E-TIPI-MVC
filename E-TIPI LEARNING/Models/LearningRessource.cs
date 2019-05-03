using E_TIPI_LEARNING.CustomValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_TIPI_LEARNING.Models
{
    public class LearningRessource
    {
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        [Display(Name = "Start date")]
        [DataType(DataType.Date)]
        [BeforeEndDate(EndDatePropertyName = "EndDate")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End date")]
        [DataType(DataType.Date)]
        [AfterStartDate(StartDatePropertyName = "StartDate")]
        public DateTime? EndDate { get; set; }
    }
}
