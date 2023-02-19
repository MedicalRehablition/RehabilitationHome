using Microsoft.AspNetCore.Hosting;
using System.Diagnostics.Contracts;
using System.Reflection.Metadata;
using System.Security.Policy;
using TemplateEngine.Docx;

namespace prjRehabilitation.Models.Lin
{
    public class WordCRUD
    {
       public string WordWrite(int id,string path,string source)
        {
            dbClassContext db = new dbClassContext();
            var p = db.PatientInfos.Where(x=>x.Fid== id).FirstOrDefault();
            if (p == null) return "";
            string guid = Guid.NewGuid().ToString();
            var fillContent = new Content();
            //string enPath = source +p.FName.ToString()+ "轉介單" + DateTime.Now.ToString("Y") +(new Random()).Next(999)+ ".docx";
            string enPath = source +guid+ ".docx";
            List<IContentItem> fields = null;

            //FieldContent → 代換一般內容(RTF)
            fields = new List<IContentItem>();
            fields.Add(new FieldContent("@date", DateTime.Now.ToString("D")));
            if (p.FName!=null)
                fields.Add(new FieldContent("@name", p.FName));
            if (p.FBirthday != null)
                fields.Add(new FieldContent("@birthday", p.FBirthday));
            if (p.FIdnum != null)
                fields.Add(new FieldContent("@idnum", p.FIdnum));
            if (p.FAddressResidential != null)
                fields.Add(new FieldContent("@address", p.FAddressResidential));
            if (p.FEdu != null)
                fields.Add(new FieldContent("@edu", p.FEdu));
            if (p.FMarried != null)
                fields.Add(new FieldContent("@marry", p.FMarried));

            var em = db.EmergenceCallers.Where(x => x.FPatientId == p.Fid).FirstOrDefault();
            if (em == null) goto dis;
            if (!string.IsNullOrEmpty(em.FEmergencyName))
                fields.Add(new FieldContent("@em", em.FEmergencyName));
            if (!string.IsNullOrEmpty(em.Frelation))
                fields.Add(new FieldContent("@emrelate",em.Frelation));
            if (!string.IsNullOrEmpty(em.FPhone))
                fields.Add(new FieldContent("@emphone", em.FPhone));
           dis:
            var disease = db.DiseaseDiagnoses.Where(x => x.Fid== p.Fid).FirstOrDefault();
            if(disease == null) goto write;
            if (disease.DiseaseChineseName != null)
                fields.Add(new FieldContent("@disname", disease.DiseaseChineseName));
            if (disease.IdDisease != null)
                fields.Add(new FieldContent("@ICD10", disease.IdDisease));
            write:
            //最後把要代換的所有資料丟到Content內
            fields.ForEach(f => fillContent.Fields.Add(f as FieldContent)); //一般內容丟Fields
            
            //複製 Template 檔案
            System.IO.File.Copy(path,enPath);

            //將Content丟給TemplateProcessor處理，SetRemoveContentControls=true表示要清除標籤內文字，如不要清除則設為false
            using (var outputDocument = new TemplateProcessor(enPath).SetRemoveContentControls(true))
            {
                outputDocument.FillContent(fillContent);
                outputDocument.SaveChanges(); //儲存變更檔案
            }
           return guid;
        }
    }
}
