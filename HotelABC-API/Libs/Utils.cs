using Microsoft.AspNetCore.Hosting;


namespace HotelABC_API.Libs
{
    public class Utils
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public Utils(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }
        public void DeleteImageFromFolder(string image_name)
        {
            string[] segments = image_name.Split('/');

            string folderPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images");
            string filePath = Path.Combine(folderPath, segments[segments.Length - 1]);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
