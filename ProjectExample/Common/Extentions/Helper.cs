namespace ProjectExample.Common.Extentions
{
    public class Helper
    {
        public static async Task<string> UploadFilesAsync(IFormFile file)
        {

            string relativePath = $"Media/{DateTime.UtcNow.Ticks + file.FileName}";

            string absolutePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);

            using (Stream stream = new FileStream(absolutePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return relativePath;
        }
    }
}
