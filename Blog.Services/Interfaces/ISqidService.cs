namespace Blog.Services.Interfaces;

public interface ISqidService
{
    string EncryptId(int id);
    int DecryptId(string value);
}
