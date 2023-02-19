using Microsoft.AspNetCore.Mvc;
using prjRehabilitation.Models.Lin;
namespace prjRehabilitation.Controllers
{

    public class DownloadController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DownloadController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();

        }
        public IActionResult WriteFile(int id)
        {
            int ran = new Random().Next(9999);
            string fileNamexx = _webHostEnvironment.WebRootPath + "\\word\\慢性精神病患社區復健轉介醫囑單及申請書.docx";

            string outcome = (new WordCRUD()).WordWrite(id,fileNamexx, _webHostEnvironment.WebRootPath + "\\word\\");
            //Dictionary<string, string> dct = new Dictionary<string, string>()
            //{
            //      { "name", "大光頭" },
            //};
            //using (DocX document = DocX.Load("C:\\Users\\s3100\\Desktop\\MAIN\\a\\RehabilitationHome\\prjRehabilitation\\wwwroot\\word\\2.doc"))
            //{
            //    foreach (string key in dct.Keys)
            //    {
            //        document.ReplaceText("[$" + key + "$]", dct[key]);
            //    }

            //document.SaveAs(fileNamexx);
            //}
            return Json(new {path = outcome});


        }



    }
}
