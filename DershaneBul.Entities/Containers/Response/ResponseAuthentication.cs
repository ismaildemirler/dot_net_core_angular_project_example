
namespace DershaneBul.Entities.Containers.Response
{
    public class ResponseAuthentication : BaseResponse
    {
        public ResponseAuthentication()
        {
            Token = null;
            RefreshToken = null;
        }

        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
