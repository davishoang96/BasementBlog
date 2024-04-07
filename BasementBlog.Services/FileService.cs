using BasementBlog.Services.Interfaces;
using Newtonsoft.Json;

namespace BasementBlog.Services;

public class FileService : IFileService
{
    public Task<string> UploadImage(object jsonObject, int width = 90)
    {
        return Task.Run(() =>
        {
            var file = JsonConvert.DeserializeObject<Utilities.File>(jsonObject.ToString());
            if (file is not null)
            {
                Guid guid = Guid.NewGuid();
                file.Name = guid.ToString();
                var dict = Path.Combine(Environment.CurrentDirectory, "Images");
                var filePath = Path.Combine(dict, $"{file.Name}.jpg");
                System.IO.File.WriteAllBytes(filePath, file.Data);

                return $"![Image-{DateTime.Now.ToShortDateString()}](/Images/{file.Name}.jpg){{ width={width}% }}"; ;
            }

            return string.Empty;
        });
    }
}
