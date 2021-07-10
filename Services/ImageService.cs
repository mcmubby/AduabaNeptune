using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;

namespace AduabaNeptune.Services
{
    public class ImageService : IImageService
    {
        private string ApiKey { get; set; }
        private string ApiSecret { get; set; }
        private string Cloud { get; set; }
        private Account Account { get; set; }

        public ImageService(IConfiguration configuration)
        {
            this.ApiKey = configuration["Cloudinary:ApiKey"];
            this.ApiSecret = configuration["Cloudinary:ApiSecret"];
            this.Cloud = configuration["Cloudinary:Cloud"];
            this.Account = new Account { ApiKey = this.ApiKey, ApiSecret = this.ApiSecret, Cloud = this.Cloud };
        }

        public async Task<string> UploadCustomerAvatar(string stringImage)
        {
            Cloudinary cloudinary = new Cloudinary(Account);
            cloudinary.Api.Secure = true;

            var prefix = @"data:image/png;base64,";
            var imagePath = prefix + stringImage;

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(@imagePath),
                Folder = "AduabaFresh/avatar"
            };
            var uploadResult = await cloudinary.UploadAsync(@uploadParams);


            return uploadResult.SecureUrl.AbsoluteUri;
        }
    }
}