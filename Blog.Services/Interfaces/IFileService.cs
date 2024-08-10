namespace Blog.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadImage(object jsonObject, int width = 50);
    }
}
