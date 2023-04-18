
namespace RealState.Core.Application.Dtos.Account
{
    //Respues al request de registro
    public class RegisterResponse
    {
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
