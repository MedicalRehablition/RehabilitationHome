using prjRehabilitation.Models;
using System.ComponentModel;

namespace prjRehabilitation.ViewModel
{
    public class CConsultationViewModel
    {
        public List<int> Typeconsult { get; set; }
        public List<int?> Typeevaluate { get; set; }

        private CounsultTypeRecord _CounsultTypeRecord;
        private Consultation _consult;
        public CConsultationViewModel()
        {
            _consult = new Consultation();
            _CounsultTypeRecord = new CounsultTypeRecord();
        }
        public CounsultTypeRecord CounsultTypeRecord
        {
            get { return _CounsultTypeRecord; }
            set { _CounsultTypeRecord = value; }
        }
        public int Fid
        {
            get { return _CounsultTypeRecord.Fid; }
            set { _CounsultTypeRecord.Fid = value; }
        }
        public int TypeNameId
        {
            get { return _CounsultTypeRecord.TypeNameId; }
            set { _CounsultTypeRecord.TypeNameId = value; }
        }
        public int CTRFConsultId
        {
            get { return _CounsultTypeRecord.FConsultId; }
            set { _CounsultTypeRecord.FConsultId = value; }
        }

        public Consultation Consult
        {
            get { return _consult; }
            set { _consult = value; }
        }
        public int FConsultId
        {
            get { return _consult.FConsultId; }
            set { _consult.FConsultId = value; }
        }
        public int? PatinetId
        {
            get { return _consult.PatinetId; }
            set { _consult.PatinetId = value; }
        }
        [DisplayName("日期")]
        public string? Date
        {
            get { return _consult.Date; }
            set { _consult.Date = value; }
        }
        [DisplayName("概述")]
        public string? Summary
        {
            get { return _consult.Summary; }
            set { _consult.Summary = value; }
        }
        [DisplayName("評估")]
        public string? Assessment
        {
            get { return _consult.Assessment; }
            set { _consult.Assessment = value; }
        }
        [DisplayName("結果")]
        public string? Result
        {
            get { return _consult.Result; }
            set { _consult.Result = value; }
        }

    }
}
