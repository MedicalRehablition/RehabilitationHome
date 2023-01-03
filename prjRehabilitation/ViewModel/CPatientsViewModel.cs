using prjRehabilitation.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace prjRehabilitation.ViewModel
{
    public class CPatientsViewModel
    {
        private PatientInfo _patient;
        public CPatientsViewModel()
        {
            _patient = new PatientInfo();
            Consultations = new HashSet<Consultation>();
            DiseaseDiagnoses = new HashSet<DiseaseDiagnosis>();
            EmergenceCallers = new HashSet<EmergenceCaller>();
            PatientGetMedDates = new HashSet<PatientGetMedDate>();
            TPersonalPerformances = new HashSet<TPersonalPerformance>();
            功能評估s = new HashSet<功能評估>();
        }
        public PatientInfo Patient
        {
            get { return _patient; }
            set { _patient= value; }
        }
        public int Fid
        {
            get { return _patient.Fid; }
            set { _patient.Fid=value; }
        }
        [DisplayName("姓名")]
        public string? FName
        {
            get { return _patient.FName; }
            set { _patient.FName = value; }
        }
        public string? FSex
        {
            get { return _patient.FSex; }
            set { _patient.FSex = value; }
        }
        public string? FCheckin
        {
            get { return _patient.FCheckin; }
            set { _patient.FCheckin = value; }
        }
        public string? FIdnum
        {
            get { return _patient.FIdnum; }
            set { _patient.FIdnum = value; }
        }
        public string? FBednum
        {
            get { return _patient.FBednum; }
            set { _patient.FBednum = value; }
        }
        public string? FBirthday
        {
            get { return _patient.FBirthday; }
            set { _patient.FBirthday = value; }
        }
        public string? FHomeNum
        {
            get { return _patient.FHomeNum; }
            set { _patient.FHomeNum = value; }
        }
        public string? FPhone
        {
            get { return _patient.FPhone; }
            set { _patient.FPhone = value; }
        }
        public string? FEdu
        {
            get { return _patient.FEdu; }
            set { _patient.FEdu = value; }
        }
        public string? FMarried
        {
            get { return _patient.FMarried; }
            set { _patient.FMarried = value; }
        }
        public string? FAddress
        {
            get { return _patient.FAddressPermanent; }
            set { _patient.FAddressPermanent = value; }
        }
        public string? FHos
        {
            get { return _patient.FHos; }
            set { _patient.FHos = value; }
        }
        public string? FIdy
        {
            get { return _patient.FIdy; }
            set { _patient.FIdy = value; }
        }
        public string? FGrant
        {
            get { return _patient.FGrant; }
            set { _patient.FGrant = value; }
        }
        public byte[]? FPicture 
        { 
            get { return _patient.FPicture; }
            set { _patient.FPicture = value; } 
        }
        public bool? Status
        {
            get { return _patient.Status; }
            set { _patient.Status = value; }
        }
        public IFormFile photo { get; set; }
        public virtual ICollection<Consultation> Consultations { get; set; }
        public virtual ICollection<DiseaseDiagnosis> DiseaseDiagnoses { get; set; }
        public virtual ICollection<EmergenceCaller> EmergenceCallers { get; set; }
        public virtual ICollection<PatientGetMedDate> PatientGetMedDates { get; set; }
        public virtual ICollection<TPersonalPerformance> TPersonalPerformances { get; set; }
        public virtual ICollection<功能評估> 功能評估s { get; set; }
    }
}
