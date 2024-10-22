using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class ImageService : IImageService
    {
        private readonly string _uploadPath;

        public ImageService(string uploadPath)
        {
            _uploadPath = uploadPath;
        }

        // Upload Image
        public async Task<string> UploadImageAsync(IFormFile image, string folderPath)
        {
            if (image == null || image.Length == 0)
                throw new ArgumentException("File ảnh không hợp lệ.");

            // Tạo đường dẫn thư mục
            string uploadFolder = Path.Combine(_uploadPath, folderPath);
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            // Tạo tên file duy nhất
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            string filePath = Path.Combine(uploadFolder, fileName);

            // Lưu file vào thư mục
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            // Trả về đường dẫn tương đối của ảnh
            return Path.Combine(folderPath, fileName).Replace("\\", "/");
        }

        // Delete Image
        public async Task<bool> DeleteImageAsync(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                throw new ArgumentException("Đường dẫn ảnh không hợp lệ.");

            string filePath = Path.Combine(_uploadPath, imagePath);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }

            return false;
        }
    }
}
