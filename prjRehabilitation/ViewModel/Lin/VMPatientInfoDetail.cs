using System.ComponentModel;

namespace prjRehabilitation.ViewModel.Lin
{
    public class VMPatientInfoDetail
    {
        public int? fid { get; set; }
        //照片
        [DisplayName("姓名")]
        public string fName { get; set; }
        public string fSex { get; set; }
        [DisplayName("入住日")]
        public string fCheckin { get; set; }
        [DisplayName("住宿到期日")]
        public string fExpireDate { get; set; }
        [DisplayName("身分證字號")]
        public string fIdnum { get; set; }
        [DisplayName("床號")]
        public string fBednum { get; set; }
        [DisplayName("生日")]
        public string fBirthday { get; set; }
        [DisplayName("家用電話")]
        public string fHomeNum { get; set; }
        [DisplayName("手機")]
        public string fPhone { get; set; }
        [DisplayName("教育程度")]
        public string fEdu { get; set; }
        [DisplayName("婚姻")]
        public string fMarried { get; set; }
        [DisplayName("戶籍地址")]
        public string fAddressPermanent { get; set; }
        [DisplayName("現居地址")]
        public string fAddressResidential { get; set; }
        [DisplayName("指定醫院")]
        public string fHos { get; set; }
        [DisplayName("身分別(複選)")]
        public string fIDY { get; set; }
        [DisplayName("請領補助")]
        public string fGrant { get; set; }
        [DisplayName("識別照")]
        public string fPicture { get; set; }
        public List<VMDisease> Diseases{get;set;}
        public bool status { get; set; }
        public IFormFile fphoto { get; set; }


    }
}
