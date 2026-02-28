using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlightSearchEngine.Models
{
    public class SearchViewModel
    {
        public string Source { get; set; }
        public string Destination { get; set; }
        public int NumberOfPersons { get; set; }

        SelectList SourceList { get; set; }
        SelectList DestinationList {get; set; }


    }
}
