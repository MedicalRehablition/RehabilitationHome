using prjRehabilitation.Models;
using System.ComponentModel;

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
        [DisplayName("功能評估Id")]
        public int F功能評估Id
        {
            get { return _功能評估.F功能評估Id; } 
            set { _功能評估.F功能評估Id = value; }
        }
        public int Fid { 
            get { return _功能評估.Fid; } 
            set { _功能評估.Fid = value; } 
        }
        [DisplayName("日期")]
        public string? F日期 
        {
            get { return _功能評估.F日期; }
            set { _功能評估.F日期 = value; }
        }
        [DisplayName("身高")]
        public decimal? F身高 
        {
            get { return _功能評估.F身高; }
            set { _功能評估.F身高 = value; }
        }
        [DisplayName("體重")]
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
        [DisplayName("評估項目")]
        public int? F評估項目 
        {
            get { return _功能評估個表.F評估項目; }
            set { _功能評估個表.F評估項目 = value; }
        }
        [DisplayName("現狀評估")]
        public string? F現狀評估 
        {
            get { return _功能評估個表.F現狀評估; }
            set { _功能評估個表.F現狀評估 = value; }
        }
        [DisplayName("問題")]
        public string? F問題 
        {
            get { return _功能評估個表.F問題; }
            set { _功能評估個表.F問題 = value; }
        }
        [DisplayName("復健目標")]
        public string? F復健目標
        {
            get { return _功能評估個表.F復健目標; }
            set { _功能評估個表.F復健目標 = value; }
        }
        [DisplayName("復健計畫")]
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
