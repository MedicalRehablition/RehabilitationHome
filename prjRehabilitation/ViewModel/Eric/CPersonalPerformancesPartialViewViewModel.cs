using prjRehabilitation.Models;

namespace prjRehabilitation.ViewModel.Eric
{
    public class CPersonalPerformancesPartialViewViewModel
    {
        public TPersonalPerformance[] tpp { get; set; }
        public string[]? ResidentName { get; set; }
      

        public List<string> comboxEmotions {
            get   {   return new List<string>  { "未參與","高亢/興奮","愉快/平穩","淡漠/平板","焦慮/緊張","憤怒/煩躁"};} 
        }
        public List<string> comboxParticipatePersistence { 
            get { return new List<string> { "未參與", "全程參與", "部分參與", "偶有參與", "幾乎不參與" }; } 
        }
        public List<string> comboxCooperate { 
            get { return new List<string> { "未參與", "主動配合", "被動配合", "偶有配合", "拒絕配合" }; } 
        }
        public List<string> comboxHumanInteraction { 
            get { return new List<string> { "未參與", "主動與人互動", "能與交談", "偶有簡單回應", "答非所問/無邏輯", "意念飛躍" }; } 
        }
        public List<string> comboxAttentionRes { 
            get { return new List<string> { "未參與", "全程專心", "偶有分心", "專注度差", "渙散嗜睡", "症狀干擾" }; } 
        }
        public List<string> comboxParticipatePerformance { 
            get { return new List<string> { "未參與", "可獨自完成", "少量協助", "部分完成", "大量協助" }; } 
        }
    }
}
