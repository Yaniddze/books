using System.Text;

namespace BooksApi.Options
{
    public class JwtOptions
    {
        public string Secret { get; set; }
        public byte[] SecretInBytes => Encoding.ASCII.GetBytes(Secret);
    }
}