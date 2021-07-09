using System.Threading.Tasks;

namespace AduabaNeptune.Services
{
    public interface IImageService
    {
        //upload customer avatar
        //upload product image
        Task<string> UploadCustomerAvatar(string stringImage);

    }
}