using System.Collections.Generic;
using System.Linq;
using FerryLegacy.domain;
using FerryLegacy.DAO;

namespace FerryLegacy
{
    public class JourneyBookingService
    {
        private ITimeTables _timeTables;
        private IBookings _bookings;
        private readonly FerryAvailabilityService _ferryService;

        public JourneyBookingService(ITimeTables timeTables, IBookings bookings, FerryAvailabilityService ferryService)
        {
            _timeTables = timeTables;
            _bookings = bookings;
            _ferryService = ferryService;
        }

        public bool CanBook(int journeyId, int passengers)
        {
            var timetables = _timeTables.GetAll();
            var allEntries = timetables.SelectMany(x => x.Entries).OrderBy(x => x.Time).ToList();
            foreach (var timetable in allEntries)
            {
                var ferry = _ferryService.NextFerryAvailableFrom(timetable.OriginId, timetable.Time);

                if (timetable.Id == journeyId)
                {
                    var bookings = _bookings.GetAll().Where(x => x.JourneyId == journeyId);
                    var seatsLeft = ferry.Passengers - bookings.Sum(x => x.Passengers);
                    return seatsLeft >= passengers;
                }
            }
            return false;
        }

        public void Book(Booking booking)
        {
            _bookings.Create(booking);
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return _bookings.GetAll();
        }
    }
}