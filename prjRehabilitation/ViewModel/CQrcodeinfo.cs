namespace prjRehabilitation.ViewModel
{
    public class CQrcodeinfo
    {
        public string? Person { get; set; }
        public string? Time { get; set; }
        public string? word { get; set; }  //判斷員工還是訪客，存e或c專用

        public string? Morningafternoon { get; set; }
        public string morningafternoon() //寫一個自動判斷'上班時間'跟'下班時間'的建構子，只能讀。   
        {
            string result = "";
            DateTime nine = DateTime.Parse("09:00:00");
            DateTime seventeen = DateTime.Parse("17:00:00");
            DateTime dayend = DateTime.Parse("23:59:59");
            DateTime now = DateTime.Now;

            if (DateTime.Compare(now, nine) == 1 && DateTime.Compare(now, seventeen) == -1)
            {
                result = "上班時間:";
            }
            else if (DateTime.Compare(now, seventeen) == 1 && DateTime.Compare(now, dayend) == -1)
            {
                result = "下班時間:";
            }
            else
            {
                result = "現在非上班時間";
            }
            return result;
        }
    }
}
