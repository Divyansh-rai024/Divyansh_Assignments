namespace InventoryManagementWebApi.Services
{
    public interface IAuthService
    {
        string Register(User user);
        string Login(LoginModel model);
    }
}
