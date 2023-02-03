using prjRehabilitation.Models;

namespace prjRehabilitation.ViewModel
{
    
    public class CEvaluateViewModle
    {
        private 功能評估 _功能評估;
        private 功能評估個表 _功能評估個表;
        public CEvaluateViewModle()
        {
            _功能評估 = new 功能評估();
            _功能評估個表 = new 功能評估個表();
        }
        public 功能評估 Evaluate 
        {
            get { return _功能評估; }
            set { _功能評估 = value; }
        }

        public 功能評估個表 Evaluate2
        {
            get { return _功能評估個表; }
            set { _功能評估個表 = value; }
        }

        public int F功能評估Id
        {
            get { return _功能評估.F功能評估Id; } 
            set { _功能評估.F功能評估Id = value; }
        }
        public int Fid { 
            get { return _功能評估.Fid; } 
            set { _功能評估.Fid = value; } 
        }
        public string? F日期 
        {
            get { return _功能評估.F日期; }
            set { _功能評估.F日期 = value; }
        }
        public decimal? F身高 
        {
            get { return _功能評估.F身高; }
            set { _功能評估.F身高 = value; }
        }
        public decimal? F體重 
        {
            get { return _功能評估.F體重; }
            set { _功能評估.F體重 = value; }
        }
        public bool? Deleted 
        {
            get { return _功能評估.Deleted; }
            set { _功能評估.Deleted = value; }
        }
        public int Id評估表 
        {
            get { return _功能評估個表.Id評估表; }
            set { _功能評估個表.Id評估表 = value; }
        }
        public int F功能評估Id參考
        {
            get { return _功能評估個表.F功能評估Id; }
            set { _功能評估個表.F功能評估Id = value; }
        }
        public int? F評估項目 
        {
            get { return _功能評估個表.F評估項目; }
            set { _功能評估個表.F評估項目 = value; }
        }
        public string? F現狀評估 
        {
            get { return _功能評估個表.F現狀評估; }
            set { _功能評估個表.F現狀評估 = value; }
        }
        public string? F問題 
        {
            get { return _功能評估個表.F問題; }
            set { _功能評估個表.F問題 = value; }
        }
        public string? F復健目標
        {
            get { return _功能評估個表.F復健目標; }
            set { _功能評估個表.F復健目標 = value; }
        }
        public string? F復健計畫 
        {
            get { return _功能評估個表.F復健計畫; }
            set { _功能評估個表.F復健計畫 = value; }
        }
        public bool? Deleted個表 
        {
            get { return _功能評估個表.Deleted; }
            set { _功能評估個表.Deleted = value; }
        }
    }
}
