using System;
using System.IO;
using System.Threading.Tasks;

namespace app1.Services
{
    public interface IPhotoPickerService
    {
        Task<Stream> GetImageStreamAsync(); 
    }
}
