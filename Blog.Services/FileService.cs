using Blog.Services.Interfaces;
using Newtonsoft.Json;

namespace Blog.Services;

public class FileService : IFileService
{
    public Task<string> UploadImage(object jsonObject, int width = 50)
    {
        return Task.Run(() =>
        {
            var root = Path.GetFullPath("wwwroot");
            var dict = Path.Combine(root, "Images");

            if (!Path.Exists(dict))
            {
                Directory.CreateDirectory(dict);
            }

            var file = JsonConvert.DeserializeObject<Utilities.File>(jsonObject.ToString());
            if (file is not null)
            {
                Guid guid = Guid.NewGuid();
                file.Name = guid.ToString();
                var filePath = Path.Combine(dict, $"{file.Name}.jpg");
                File.WriteAllBytes(filePath, file.Data);

                return $"![Image-{DateTime.Now.ToShortDateString()}](/Images/{file.Name}.jpg){{ width={width}% }}"; ;
            }

            return string.Empty;
        });
    }
}
