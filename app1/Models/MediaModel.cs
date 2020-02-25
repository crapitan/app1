using System;
using Xamarin.Forms;

namespace app1.Models
{
    public class MediaModel
    {
        public Guid MediaID { get; set; }

        public string Path { get; set; }

        public DateTime LocalDateTime { get; set; }

        private FileImageSource source = null;
        public FileImageSource Source => source ?? (source = new FileImageSource() { File = Path }); 

    }
}
