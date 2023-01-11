using prjRehabilitation.Models;
using System.ComponentModel;

namespace prjRehabilitation.ViewModel
{
    public class CAdminViewModel
    {
        private Admin _admin;
        public CAdminViewModel()
        {
            _admin = new Admin();
        }
        public Admin admin
        {
            get { return _admin; }
            set { _admin = value; }
        }
        public int Fid
        {
            get { return _admin.Fid; }
            set { _admin.Fid = value; }
        }
        [DisplayName("姓名")]

        public string? FName
        {
            get { return _admin.FName; }
            set { _admin.FName = value; }
        }
        [DisplayName("信箱")]

        public string? FEmail
        {
            get { return _admin.FEmail; }
            set { _admin.FEmail = value; }
        }
        [DisplayName("密碼")]

        public string? FPassword
        {
            get { return _admin.FPassword; }
            set { _admin.FPassword = value; }
        }
        [DisplayName("職級")]

        public string? FRank
        {
            get { return _admin.FRank; }
            set { _admin.FRank = value; }
        }
        [DisplayName("照片")]
        public string? Fphoto
        {
            get { return _admin.Fphoto; }
            set { _admin.Fphoto = value; }
        }
        public string? FSex {
            get { return _admin.FSex; }
            set { _admin.FSex = value; }
        }
        public string? FBirth {
            get { return _admin.FBirth; }
            set { _admin.FBirth = value; }
        }

        public IFormFile photo { get; set; }
    }
}
