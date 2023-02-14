using prjRehabilitation.Models;

namespace prjRehabilitation.ViewModel.Eric
{
    public class CCalendarTotalNeedViewModel
    {
        public List<Consultation> ResidentReVisitDay { get; set; }
        public List<PatientInfo> ResidentExpireDate { get; set; }
        public List<TCalendar> GetTodayNextAndFrontOneMonth { get; set; }

        public string getFrontSession { get; set; }
        public string getBackSession { get; set; }
    }
}
