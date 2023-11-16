using TPI_NapolitanoSalinasVazquez_P3.Models;

namespace TPI_NapolitanoSalinasVazquez_P3.Interfaces
{
    public interface IAdminService
    {
        public List<User> GetAdmins();

        //public User GetbyId(int id);
    }
}
