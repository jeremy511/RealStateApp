

namespace RealState.Core.Application.Dtos.Account
{
    //variable que pide un para un Registro y valida
    public class RegisterRequest
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cedula { get; set; } //Cedula
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string TUser { get; set; } //Tuser
        public string? Fotog { get; set; }



    }
}
