namespace App.Admin.Utils
{
    public class FileHelper
    {
        //public static async Task<string> FileLoaderAsync(IFormFile formFile)
        //{
        //    string fileName = "";

        //    fileName = formFile.FileName;

        //    string directory = Directory.GetCurrentDirectory() + "/wwwroot/Images/" + fileName;

        //    using var stream = new FileStream(directory, FileMode.Create);
        //    await formFile.CopyToAsync(stream);
        //    return fileName;
        //}

        public static bool FileRemover(string fileName, string filePath = "/wwwroot/Images/")
        {
            string directory = Directory.GetCurrentDirectory() + filePath + fileName;
            if (File.Exists(directory))
            {
                File.Delete(directory);
                return true;
            }
            return false;
        }

        public static async Task<string> FileLoaderAsync(IFormFile ImageUrl, string filePath = "/wwwroot/Images/")
        {
            if (ImageUrl == null || ImageUrl.Length <= 0)
                return null;

            string extension = Path.GetExtension(ImageUrl.FileName);
            string fileName = Guid.NewGuid().ToString() + extension;
            string directory = Directory.GetCurrentDirectory() + filePath;
            string fullPath = Path.Combine(directory, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await ImageUrl.CopyToAsync(stream);
            }

            return $"/images/{fileName}";
        }

    }

}
