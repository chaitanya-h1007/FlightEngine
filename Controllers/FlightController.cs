using FlightSearchEngine.Data;
using FlightSearchEngine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlightSearchEngine.Controllers
{
    public class FlightController : Controller
    {
        private readonly DatabaseHelper _databaseHelper;

        public FlightController(IConfiguration configuration)
        {
            _databaseHelper = new DatabaseHelper(configuration);
        }

        // GET: Index
        public async Task<IActionResult> Index()
        {
            var model = new SearchViewModel();

            var sources = await _databaseHelper.GetSourcesAsync();
            var destinations = await _databaseHelper.GetDestinationsAsync();

            model.SourceList = new SelectList(sources);
            model.DestinationList = new SelectList(destinations);

            return View(model);
        }

        // POST: Search Flights Only
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchFlights(SearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.SourceList = new SelectList(await _databaseHelper.GetSourcesAsync());
                model.DestinationList = new SelectList(await _databaseHelper.GetDestinationsAsync());
                return View("Index", model);
            }

            var results = await _databaseHelper
                .SearchFlightsAsync(model.Source, model.Destination, model.NumberOfPersons);

            return View("Results", results.Cast<object>());
        }

        // POST: Search Flights + Hotels
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchFlightsWithHotels(SearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.SourceList = new SelectList(await _databaseHelper.GetSourcesAsync());
                model.DestinationList = new SelectList(await _databaseHelper.GetDestinationsAsync());
                return View("Index", model);
            }

            var results = await _databaseHelper
                .SearchFlightsWithHotelsAsync(model.Source, model.Destination, model.NumberOfPersons);

            return View("Results", results.Cast<object>());
        }
    }
}