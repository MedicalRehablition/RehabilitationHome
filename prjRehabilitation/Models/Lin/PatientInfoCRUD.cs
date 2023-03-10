using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using prjRehabilitation.ViewModel.Lin;
using System.Drawing.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace prjRehabilitation.Models.Lin
{
    public class PatientInfoCRUD
    {
        dbClassContext db = new dbClassContext();
        public bool c_Delete(int fid)
        {
            var p = db.PatientInfos.Where(x => x.Fid == fid).FirstOrDefault();
            p.Status = false;
            db.SaveChanges();
            return true;
        }
        public void c_recover(int fid)
        {
            var p = db.PatientInfos.Where(x => x.Fid == fid).FirstOrDefault();
            p.Status = true;
            db.SaveChanges();
        }
        public string c_edit(VMPatientInfoDetail vm)
        {

            //基本資料
            var p = db.PatientInfos.FirstOrDefault(x => x.Fid == vm.fid);
            p.FName = vm.fName;
            p.FSex = vm.fSex;
            p.FCheckin = vm.fCheckin;
            p.FHos = vm.fHos;
            p.FEdu= vm.fEdu;
            p.FAddressPermanent = vm.fAddressPermanent;
            p.FAddressResidential = vm.fAddressResidential;
            p.FGrant = vm.fGrant;
            p.FExpireDate = vm.fExpireDate;
            p.FIdnum =  vm.fIdnum;
            p.FBednum = vm.fBednum;
            p.FBirthday = vm.fBirthday;
            p.FHomeNum = vm.fHomeNum;
            p.FPhone = vm.fPhone;
            p.FMarried = vm.fMarried;
            p.FCountry = vm.fCountry;
            p.FIdy = vm.fIDY;
            p.FPicture = vm.fPicture;
            //照片
            if (vm.fphoto != null)
            {
                byte[] FileBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    vm.fphoto.CopyTo(ms);
                    FileBytes = ms.GetBuffer();
                    p.FPhotoFile = FileBytes;
                }
            }
            db.SaveChanges();

            //疾病
            var list = new List<VMDisease>();
            list.Add(vm.disease1);
            list.Add(vm.disease2);
            list.Add(vm.disease3);
            list.Add(vm.disease4);
            list.Add(vm.disease5);
            var dis = db.DiseaseDiagnoses.Where(x => x.Fid == p.Fid);
            foreach (var c in dis.ToList())
                db.DiseaseDiagnoses.Remove(c);
            db.SaveChanges();
            foreach (VMDisease c in list)
            {
                if (c == null) { continue; }
                db.DiseaseDiagnoses.Add(new DiseaseDiagnosis
                {
                    Fid = p.Fid,
                    DiseaseChineseName = c.DiseaseChineseName,
                    IdDisease = c.ID_Disease,
                });
            }
            db.SaveChanges();
            //緊急聯絡人
            var emgc = db.EmergenceCallers.Where(x => x.FPatientId == p.Fid);
            foreach (var c in emgc.ToList())
                db.EmergenceCallers.Remove(c);
            db.SaveChanges();

            db.EmergenceCallers.Add(new EmergenceCaller
            {
                FPatientId = p.Fid,
                FEmergencyName = vm.emerCaller1.FEmergencyName,
                FPhone = vm.emerCaller1.FPhone,
                Frelation = vm.emerCaller1.Frelation,
            });
            db.EmergenceCallers.Add(new EmergenceCaller
            {
                FPatientId = p.Fid,
                FEmergencyName = vm.emerCaller2.FEmergencyName,
                FPhone = vm.emerCaller2.FPhone,
                Frelation = vm.emerCaller2.Frelation,
            });
            db.SaveChanges();
            return "修改成功";
        }
        public string c_PatientInfo(VMPatientInfoDetail vm)
        {
            if (string.IsNullOrEmpty(vm.fName)) return "錯誤";
            var q = db.PatientInfos.FirstOrDefault(x => x.FIdnum == vm.fIdnum);
            if (q != null) { return "失敗: 該住民已被註冊"; }

            if (!c_BasicInfo(vm))
            {
                return "失敗:匯入基本資料時發生錯誤";
            }

            if (!c_EmergencyCaller(vm))
            {
                return "失敗: 匯入緊急聯絡人時發生錯誤";
            }

            if (!c_DiseaseInfo(vm))
            {
                return "失敗: 匯入疾病資料時發生錯誤";
            }
            return "成功";
        }
        //TODO: 還沒加try&catch
        private bool c_DiseaseInfo(VMPatientInfoDetail vm)
        {
            //if (vm.DiseaseList == null) return true;
            var p = db.PatientInfos.FirstOrDefault(x => x.FIdnum == vm.fIdnum);
            var list = new List<VMDisease>
            {
                vm.disease1,
                vm.disease2,
                vm.disease3,
                vm.disease4,
                vm.disease5
            };

            foreach (VMDisease c in list)
            {
                if (c == null) { continue; }
                db.DiseaseDiagnoses.Add(new DiseaseDiagnosis
                {
                    Fid = p.Fid,
                    DiseaseChineseName = c.DiseaseChineseName,
                    IdDisease = c.ID_Disease,
                });
            }
            db.SaveChanges();
            return true;
        }

        private bool c_EmergencyCaller(VMPatientInfoDetail vm)
        {
            var p = db.PatientInfos.FirstOrDefault(x => x.FIdnum == vm.fIdnum);
            db.EmergenceCallers.Add(new EmergenceCaller
            {
                FPatientId = p.Fid,
                FEmergencyName = vm.emerCaller1.FEmergencyName,
                FPhone = vm.emerCaller1.FPhone,
                Frelation = vm.emerCaller1.Frelation,
            });
            db.EmergenceCallers.Add(new EmergenceCaller
            {
                FPatientId = p.Fid,
                FEmergencyName = vm.emerCaller2.FEmergencyName,
                FPhone = vm.emerCaller2.FPhone,
                Frelation = vm.emerCaller2.Frelation,
            });
            db.SaveChanges();
            return true;
        }
        private bool checkMD5(string input,string password)
        {
            //input=網頁使用者填的 & password=資料庫儲存經MD5處理的密碼
            using (var cryptoMD5 = System.Security.Cryptography.MD5.Create())
            {
                //將輸入的字串編碼成 UTF8 位元組陣列
                input += "putSomeSalt"; //加鹽，避免駭客知道加密方法後回推密碼
                var bytes = Encoding.UTF8.GetBytes(input);

                //取得雜湊值位元組陣列
                var hash = cryptoMD5.ComputeHash(bytes);

                //取得 MD5
                var md5 = BitConverter.ToString(hash)
                  .Replace("-", String.Empty)
                  .ToUpper();

                //雜湊化密碼相同即回傳正確
                if(md5==password) return true;
                return false;
            }
        }
        private string getMD5(string input)
        {
            //將密碼雜湊化後回傳
            using (var cryptoMD5 = System.Security.Cryptography.MD5.Create())
            {
                //將輸入的字串編碼成 UTF8 位元組陣列
                input += "putSomeSalt"; //加鹽，避免駭客知道加密方法後回推密碼
                var bytes = Encoding.UTF8.GetBytes(input);

                //取得雜湊值位元組陣列
                var hash = cryptoMD5.ComputeHash(bytes);

                //取得 MD5
                var md5 = BitConverter.ToString(hash)
                  .Replace("-", String.Empty)
                  .ToUpper();

                //回傳密碼
                return md5;
            }
        }
        private bool c_BasicInfo(VMPatientInfoDetail vm)
        {
            byte[]? FileBytes = null;
            if (vm.fphoto != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    vm.fphoto.CopyTo(ms);
                    FileBytes = ms.GetBuffer();
                }
            }
            db.PatientInfos.Add(new PatientInfo
            {
                FName = vm.fName,
                FSex = vm.fSex,
                FCheckin = vm.fCheckin,
                FAddressPermanent = vm.fAddressPermanent,
                FAddressResidential = vm.fAddressResidential,
                FExpireDate = vm.fExpireDate,
                FIdnum = vm.fIdnum,
                FBednum = vm.fBednum,
                FBirthday = vm.fBirthday,
                FHomeNum = vm.fHomeNum,
                FPhone = vm.fPhone,
                FEdu = vm.fEdu,
                FMarried = vm.fMarried,
                FHos = vm.fHos,
                FCountry = vm.fCountry,
                FGrant = vm.fGrant,
                FIdy = vm.fIDY,
                FPhotoFile = FileBytes,
            });

            db.SaveChanges();


            return true;
        }

        public  object getPatientsList()
        {
            dbClassContext db = new dbClassContext();
            IEnumerable<PatientInfo> data = db.PatientInfos.Where(x => x.Status != false).Take(100);
            List<VMPatientList> List = new List<VMPatientList>();
            foreach (var c in data.ToList())
            {
                VMPatientList p = new VMPatientList();
                p.fid = (int)c.Fid;
                p.fName = c.FName;
                p.fPhone = c.FPhone;
                p.fidnum = c.FIdnum;
                if (c.FPhotoFile != null)
                {
                    p.fphoto = c.FPhotoFile;
                }
                List.Add(p);
            }
            return List;
        }

        public object getList()
        {
            dbClassContext db = new dbClassContext();
            IEnumerable<PatientInfo> data = db.PatientInfos.Where(x => x.Status != false).Take(100);
            List<VMPatientList> List = new List<VMPatientList>();
            foreach (var c in data.ToList())
            {
                VMPatientList p = new VMPatientList();
                p.fid = (int)c.Fid;
                p.fName = c.FName;
                p.fPhone = c.FPhone;
                p.fidnum = c.FIdnum;
                if (c.FPhotoFile != null)
                {
                    p.fphoto = c.FPhotoFile;
                }
                List.Add(p);
            }
            return List;
        }

        internal void EditAndProcess(VMPatientInfoDetail vm, string? disease)
        {


        }

        internal void c_Recover(int id)
        {
            var p = db.PatientInfos.Where(x => x.Fid == id).FirstOrDefault();
            p.Status = true;
            db.SaveChanges();
        }
    }
}
