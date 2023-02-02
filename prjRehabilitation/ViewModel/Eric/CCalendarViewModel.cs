using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace prjRehabilitation.ViewModel.Eric
{
	public class CCalendarViewModel
	{
		[DisplayName("設定日期")]
		[Required(ErrorMessage = "必填")]
		public string? date { get; set; }
		[DisplayName("行事曆提示字")]
		[Required(ErrorMessage = "必填")]
		public string? eventName { get; set; }
	
		public string? className { get; set; }
	
		public string? dateColor { get; set; }
		[DisplayName("行事曆抬頭")]
		[Required(ErrorMessage = "必填")]
		public string? title { get; set; }
		[DisplayName("行事曆內容")]
		[Required(ErrorMessage = "必填")]
		public string? content { get; set; }
		[DisplayName("填表人")]
		[Required(ErrorMessage = "必填")]
		public string? fRecorder { get; set; }
		[DisplayName("填表日期")]
		[Required(ErrorMessage = "必填")]
		public string? fRecorderDate { get; set; }
	}
}
