using System.Drawing;
using ZXing;
using ZXing.Windows.Compatibility;

namespace prjRehabilitation.ViewModel
{
    public class CCreateqrcode
    {
        public Bitmap createqrcode(string content)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new ZXing.QrCode.QrCodeEncodingOptions
                {
                    //Height = hight,   //圖形的高
                    //Width = width,    //圖形的寬
                    Height = 150,   //固定長寬
                    Width = 150,
                    Margin = 1,
                    CharacterSet = "UTF-8",
                    ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.H,  //容錯等級
                    DisableECI = true,
                }
            };
            var bm = writer.Write(content);  //把文字寫進圖片裡圖
            //Image jpegg = (Image)bm;  //試著把bitmap轉成image
            //string savepath = @"D:\C#\QRcode\";
            //bm.Save(savepath + filename);
            return bm;
        }
    }
}
