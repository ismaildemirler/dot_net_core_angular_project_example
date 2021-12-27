using DershaneBul.Entities.Concrete;

namespace DershaneBul.Entities.Containers.Response
{
    public  class ResponseRefreshToken: BaseResponse
    {
        public RefreshTokens RefreshToken { get; set; }
    }
}
