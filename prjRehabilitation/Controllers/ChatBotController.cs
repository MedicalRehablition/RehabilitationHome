using Microsoft.AspNetCore.Mvc;
using ChatGPT.Net;
using ChatGPT.Net.DTO;
using System.Text;
namespace prjRehabilitation.Controllers
{
    public class ChatBotController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
         public IActionResult SetMessage(string question)
        {
            return Json(new { message = SubmitMessage(question) });
        }
        async private Task<string> SubmitMessage(string question)
        {
            var chatGpt = new ChatGpt();
            await chatGpt.WaitForReady();
            var chatGptClient = await chatGpt.CreateClient(new ChatGptClientConfig
            {
                SessionToken = "eyJhbGciOiJkaXIiLCJlbmMiOiJBMjU2R0NNIn0..QIn7mb6VxS0oBw9A.jaWvOHS60dUrVWMPM4whuT9rx35WM4mSoNoFBAZh0XyEE58Ci4LPP7NKtXOE7-noEICssv9ePQCoP2p6nnKzLQ77h6AczGZ-gevPFz9FLwT3KsfOwKoHvgZUC5e6siAl3WLcAaLIN9qEo5ixVHaFflBA22EOnA078vOaIQQOjfhurWbBLDgT7s9RCHencKpRpenjDT4XKtuEZbaiB7NaZqE_UZJUOvaXgd9X_D12tjrpaXQAOQqcG1l5d-iCAZWLgoYKOaVBy99abNMAXdEcIhIiuc9gVG1jS1oUQF2v4dlN31yalnYhxWO-slkn7BkhrbBVoTsvqqNOsPxYLLcGH6zNyxq2sv9IHHMygH_2G10fP9TFY0-U_hC2Qt1pAfqLHXdXBtsCfKjCZWmYwt4p0ykoOcHfG8d9-N1mkEAqfgbb1cgaqx1fGimNoK6QLyVlmFpEPJscPgO6C7Wr2COK0ERWPX2nKAAwECkWMo-KSSWI2qYUypalYDTsxYQeVKXSUlQ3oqYcFx_R9eFxei7nivefcDWoL6v0uVVkv9xSrcX_Y1mNWhLOiltduNPmkVhG_NzUFi00EMekeAmiWba6tDcSx6KVi2P0kEZZp3KwodMzpa0oYkUIemSrtgIFvC40d6etLtos_RMPdJA-VBASt-kQDeZBaZpdFj33WR9PI8keU3YKGHDyrBBRBVcoOT9MJq8m7ew_e0elRCE6O5XpXsmifSxbgj4j2fGb5Z9D4b52Pj0emQNEDLKzhy_z0nkqABx4Jr6u35GSmXJqRaHLgpzljVm0xuVIMke1sg_g36gCVje3hATgf3rkBW9nicEJjFldOXYrBbdM3JsKNScaSYYE0qddefupWWU84yB3Epcu--ImgV7eSHB6jdPBQzS25W_WSPPtIgYtbIfNSmrzwfd9opivXO4J3VCChGkPC03nNk4MMnPHnqMQ0Upk1T9DyRIDgf4iFCNMGhOXIA_2056hnZmgr3CAK0cNUXBF0S3JQRk4Ih1vYVVh4wyOKG5uCmkoYEa8KHa09RvH4YtrjsDOwIqWVFBRDOLf38NFi4gZ5q6pPPt4OVwuKUJ78YCU0vJPdIjt6jY_q3NClVofjlFZSLF5TVBye3FLkQ2HO_5CoozdqYzQ0CxMNJRcsu56VldOkIziEvJslPIkZw52MdwG-NUvLfi9OTLiHITBhAreDDcIxiaBjbzCBIKiZ7zk-jUqWEsKY67xT53kTHBbhVkVV__dG-eUCVJrgsxf-ZmKDx8Yu5TIIIbqFzOlcGC0ntUo8sezrK2zZ8UUAjWyylsxAAmb_Jz1elpQORRG96FU-hXzKlMyS1a-VMXMvemr2DJLxnceYYLOfKfuwUHJRUICDclyZrJd20a_Prp45gNvQSdOdyYcLi0pBxxFZNvLt579W-NXKj_ca1cYOFcZQg2jS2VblPPOGFxdgzNaf7E8vtI3qLqCylpncuKRi9LOsAb6gg1Qa8fEc5IhDyXPg4ijnbEZAEofGOEUaA8OEEQzu6_gpCfh2F9wQ_1boe8_008UUQYPQI7QrEZdzSQoTyOSNoAxyd2M9BkiRxz5OdQwEuI_MKDv36gD0uhVBoV4wcbPv5nnrS7Rdt7xGqv9Cqq0FHLVgMjvWpK0V1FDhpRxAfPtnw1jmOPIEXiQLIGfsNLzx0toCq3iS2I1uLYjBWOZe59gzfxuxH_JZ3hRtt3LMMx2l_7CPlh823isowrQyukQ-pPz3PraOypoq76aoQeNSzF-nAQBTrt6y5UeWerKvYPx4Qq4ojNeuSHuIJgUUs1yYoHk6YODKB8k5tkqmoxobiTltfmVa42LMCEvPlDystkQGBC7GqUfMqfQWn_CFqxPhRyWh9e8K1y5YgCENL4WzDds85sGi_3Hn_EPKs8i-8D2ChAKAFQgHnfP8kj5adsusJz4k7hEzhlSleCB1eHpcfxeGszmcI_2630X4T8Z9gckoW6zdHfdU8-52NhEgHW1fdKqIh7h0pwnbihC-xCxdpQIYp_qbMv5QiURvOeRZz79uJyvVu9krlpsnoF0hcQrVzIl8d7Zz2Sed7g0oWq_T9j11YefhY0jDugSpf_U7EJ0m7uaRArXsXc7jnYI-_VYqTImuWKTx751_UNPRTCt-CEh2N-rkN7_PP4neZIGf8t_Fig1V1j3obhRy4yTsHDINb1ICTa2buVTspzguytgA97_TPyiSTb00Gu94vHhZnvCv1FRnfdmHVvJvFjCkFb-YLtYFqhkJyF9qe0RrdKgZ3pFP04Juacmf9RUQcFPG0cHYEG57fHfWT48697a05Mq-Jmq4LRQGq0cI0T7FQQ0lvRqrt24-CXFyNwlRzyve9QpDe8FgdtHI6b2_YClQjXlhQ-sZaEjoxsiGpYKTap0giN6MraqMAdXoFTRbisJSkD6Hbmx1GKCOzgkOaK5T-4RV_q01mmZd9z9zDg8YYs6QY-rY5vVKFmBPA1vDxyCN5f0TYlO_vy6kMMNDc7FBKL8ALZOMZFMm4izTEIOVz0PYze1IWfW95sp-RHi3e9SxXR_MDwLNIRh-l-OGZJKYaMCZWJMzMwjqlxGz7PBxdCdUMA6fLc02Xl6423NtS44XtV5vu7NifwXJEgGg728JDulLVlRUI_845KOP-sTSUJ0w2Ujyif5jbTICb8_8RiR_kvmFjcdnPrgU75OYL52Hwqe1JCpfA.NchKbVkHIwht8xeaEoyh5g"
            });
            string response = await chatGptClient.Ask(question);
                //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            return response;
        }
    }
}
