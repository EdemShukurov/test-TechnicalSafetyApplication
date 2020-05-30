namespace TechnicalSafetyApplication.Models.Repository.Interfaces
{
    public interface IUserRepository
    {
        void GetAllUsers();

        void GetUsersByRole(Role role);
    }
}
