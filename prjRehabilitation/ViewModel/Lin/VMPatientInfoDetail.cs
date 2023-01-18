using prjRehabilitation.Models;
using System.ComponentModel;

namespace prjRehabilitation.ViewModel.Lin
{
    public class VMPatientInfoDetail
    {
        public VMPatientInfoDetail()
        {
            _patientInfo= new PatientInfo();
            DiseaseList = new List<VMDisease>();
        }
        public PatientInfo _patientInfo { get; set; }
        //基本資料
        public int? fid
        {
            get { return _patientInfo.Fid; }
            set { _patientInfo.Fid = (int)value; }
        }
        [DisplayName("照片位址")]
        public string? fPicture
        {
            get { return _patientInfo.FPicture; }
            set { _patientInfo.FPicture = value; }
        }
        //照片
        [DisplayName("姓名")]
        public string? fName
        {
            get { return _patientInfo.FName; }
            set { _patientInfo.FName = value; }
        }
        public string? fSex
        {
            get { return _patientInfo.FSex; }
            set { _patientInfo.FSex = value; }
        }
        [DisplayName("入住日")]
        public string? fCheckin
        {
            get { return _patientInfo.FCheckin; }
            set { _patientInfo.FCheckin = value; }
        }
        [DisplayName("住宿到期日")]
        public string? fExpireDate
        {
            get { return _patientInfo.FExpireDate; }
            set { _patientInfo.FExpireDate = value; }
        }
        [DisplayName("身分證字號")]
        public string? fIdnum
        {
            get { return _patientInfo.FIdnum; }
            set { _patientInfo.FIdnum = value; }
        }
        [DisplayName("床號")]
        public string? fBednum
        {
            get { return _patientInfo.FBednum; }
            set { _patientInfo.FBednum = value; }
        }
        [DisplayName("生日")]
        public string? fBirthday
        {
            get { return _patientInfo.FBirthday; }
            set { _patientInfo.FBirthday = value; }
        }
        [DisplayName("家用電話")]
        public string? fHomeNum
        {
            get { return _patientInfo.FHomeNum; }
            set { _patientInfo.FHomeNum = value; }
        }
        [DisplayName("手機")]
        public string? fPhone
        {
            get { return _patientInfo.FPhone; }
            set { _patientInfo.FPhone = value; }
        }
        [DisplayName("教育程度")]
        public string? fEdu
        {
            get { return _patientInfo.FEdu; }
            set { _patientInfo.FEdu = value; }
        }
        [DisplayName("籍貫")]
        public string? fCountry
        {
            get { return _patientInfo.FCountry; }
            set { _patientInfo.FCountry = value; }
        }
        [DisplayName("婚姻")]
        public string? fMarried
        {
            get { return _patientInfo.FMarried; }
            set { _patientInfo.FMarried = value; }
        }
        [DisplayName("戶籍地址")]
        public string? fAddressPermanent
        {
            get { return _patientInfo.FAddressPermanent; }
            set { _patientInfo.FAddressPermanent = value; }
        }
        [DisplayName("現居地址")]
        public string? fAddressResidential
        {
            get { return _patientInfo.FAddressResidential; }
            set { _patientInfo.FAddressResidential = value; }
        }
        [DisplayName("指定醫院")]
        public string? fHos
        {
            get { return _patientInfo.FHos; }
            set { _patientInfo.FHos = value; }
        }
        [DisplayName("身分別(複選)")]
        public string? fIDY
        {
            get { return _patientInfo.FIdy; }
            set { _patientInfo.FIdy = value; }
        }
        [DisplayName("請領補助")]
        public string? fGrant
        {
            get { return _patientInfo.FGrant; }
            set { _patientInfo.FGrant = value; }
        }
        [DisplayName("補助類型")]

        public bool status { get; set; }
        public IFormFile fphoto { get; set; }

        public bool fidy_健保 { get; set; }
        public bool fidy_福保 { get; set; }
        public bool fidy_重大傷病 { get; set; }
        public bool fidy_身心障礙 { get; set; }
        public bool fidy_低收 { get; set; }
        public string? fGrant_無 { get; set; }
        public string? fGrantType { get; set; }

        //緊急聯絡人
        public EmergenceCaller emerCaller1 { get; set; }
        public EmergenceCaller emerCaller2 { get; set; }
        //疾病資料
        public List<VMDisease> DiseaseList { get; set; }

        public VMDisease disease1 { get; set; }
        public VMDisease disease2 { get; set; }
        public VMDisease disease3 { get; set; }
        public VMDisease disease4 { get; set; }
        public VMDisease disease5 { get; set; }

    }
}
