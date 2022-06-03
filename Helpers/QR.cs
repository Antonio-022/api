using API_SAADS_CORE_61.DTOs;
using IronBarCode;
using System.Drawing;
using Thought.vCards;


namespace API_SAADS_CORE_61.Helpers
{
    public static class QR
    {
        public  static string GenerarQRWithUploadImage(string pTexto,byte[] imagenByte)
        {
            Image ImageCovert;
            using (var ms = new MemoryStream(imagenByte))
            {
                ImageCovert= Image.FromStream(ms);
            }

            var qrCodeImageBmp = QRCodeWriter.CreateQrCodeWithLogoImage(pTexto, ImageCovert, 500);
            qrCodeImageBmp.ChangeBarCodeColor(Color.Black);
            var QR = qrCodeImageBmp.Image;
            ImageConverter converter = new();
            byte[] imgByte = (byte[])converter.ConvertTo(QR, typeof(byte[]));
            string Imgb64 = Convert.ToBase64String(imgByte);
            return Imgb64;
        }
        public static string GenerarQRContact(string pTexto, byte[] imagenByte)
        {
            Image ImageCovert;
            using (var ms = new MemoryStream(imagenByte))
            {
                ImageCovert = Image.FromStream(ms);
            }

            vCard vCard = new vCard();
            vCard.GivenName = "antonio";
            vCard.FamilyName = "Famila Arauz";
            vCard.Organization = "UPDS";
            vCard.Phones.Add(new vCardPhone("(591) 75059121", vCardPhoneTypes.Home));

            vCardStandardWriter writer = new vCardStandardWriter();
            StringWriter stringWriter = new StringWriter();
            writer.Write(vCard, stringWriter);


            var qrCodeImageBmp = QRCodeWriter.CreateQrCodeWithLogoImage(stringWriter.ToString(), ImageCovert, 500);
            qrCodeImageBmp.ChangeBarCodeColor(Color.Black);
            var QR = qrCodeImageBmp.Image;
            ImageConverter converter = new();
            byte[] imgByte = (byte[])converter.ConvertTo(QR, typeof(byte[]));
            string Imgb64 = Convert.ToBase64String(imgByte);
            return Imgb64;
        }


    }

}
