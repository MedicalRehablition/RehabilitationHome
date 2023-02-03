using prjRehabilitation.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace prjRehabilitation.ViewModel.Eric
{
	public class CCalendarViewModel
	{
		private TCalendar _calendar;

		public CCalendarViewModel() { 
			_calendar= new TCalendar();
		}

        public TCalendar calendar
        {
            get { return _calendar; }
            set { _calendar = value; }
        }

        public int FId { get { return _calendar.FId; } set { _calendar.FId = value; } }
        public bool? FDeleteBool { get { return _calendar.FDeleteBool; } set { _calendar.FDeleteBool = value; } }

        [DisplayName("設定日期")]
		[Required(ErrorMessage = "必填")]
		public string? date { get { return _calendar.FDate; } set {_calendar.FDate=value; } }
		[DisplayName("行事曆提示字")]
		[Required(ErrorMessage = "必填")]
		public string? eventName { get { return _calendar.FEventName; } set { _calendar.FEventName = value; } }

        public string? className { get { return _calendar.FClassName; } set { _calendar.FClassName = value; } }

        public string? dateColor { get { return _calendar.FDateColor; } set { _calendar.FDateColor = value; } }
        [DisplayName("行事曆抬頭")]
		[Required(ErrorMessage = "必填")]
		public string? title { get { return _calendar.FTitle; } set { _calendar.FTitle = value; } }
        [DisplayName("行事曆內容")]
		[Required(ErrorMessage = "必填")]
		public string? content { get { return _calendar.FContent; } set { _calendar.FContent = value; } }
        [DisplayName("填表人")]
		[Required(ErrorMessage = "必填")]
		public string? fRecorder { get { return _calendar.FRecorder; } set { _calendar.FRecorder = value; } }
        [DisplayName("填表日期")]
		[Required(ErrorMessage = "必填")]
		public string? fRecorderDate { get { return _calendar.FRecorderDate; } set { _calendar.FRecorderDate = value; } }
    }
}
