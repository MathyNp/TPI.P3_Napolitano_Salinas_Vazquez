using TPI_NapolitanoSalinasVazquez_P3.Data;
using TPI_NapolitanoSalinasVazquez_P3.Interfaces;
using TPI_NapolitanoSalinasVazquez_P3.Models;

namespace TPI_NapolitanoSalinasVazquez_P3.Services
{
    public class AdminService : IAdminService
    {
        private readonly TPI_NapolitanoSalinasVazquez_P3Context _context;

        public AdminService(TPI_NapolitanoSalinasVazquez_P3Context context)
        {
            _context = context;
        }

        public List<User> GetAdmins()
        {
            return _context.Users.Where(a => a.UserRol == UserRoleEnum.Admin).ToList();
        }
    }
}
