using prjRehabilitation.Models;

namespace prjRehabilitation.ViewModel.Eric
{
    public class CGroupActivityEditViewModel
    {
        

        public CGroupActivityViewModel cgavm { get; set; }
        public List<string> GroupActivityType
        {
            get
            {
                return new List<string>
                {
                    "體能",
                    "烹飪",
                    "護理衛教",
                    "家事清潔",
                    "財務管理",
                    "休閒娛樂",
                    "社區適應討論會",
                    "住民自治會議"
                };
            }
        }

        public TGroupActivityPicAndFile tgapaf { get; set; }

        public List<TScheduleDetail> ScheduleS { get; set; }
        public List<TGroupActivityClassTheme> ClassThemeS { get; set; }
        public List<TPersonalPerformance> PersonalPerformanceS { get; set; }
    }
}
