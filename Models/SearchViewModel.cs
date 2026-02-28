using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FlightSearchEngine.Models
{
    public class SearchViewModel
    {
        [Required(ErrorMessage = "Source is required")]
        public string Source { get; set; }

        [Required(ErrorMessage = "Destination is required")]
        public string Destination { get; set; }

        [Required(ErrorMessage = "Number of persons is required")]
        [Range(1, 10, ErrorMessage = "Persons must be between 1 and 10")]
        public int NumberOfPersons { get; set; }

        [ValidateNever]   
        public SelectList SourceList { get; set; }

        [ValidateNever]   
        public SelectList DestinationList { get; set; }


    }
}
