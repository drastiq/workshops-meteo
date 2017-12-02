using System.Threading.Tasks;
using Meteo.Core.Security;

namespace Meteo.Core.Services
{
    public interface IUserService
    {
        Task SignUpAsync(string email, string password, string role);
        Task<JsonWebToken> SignInAsync(string email, string password);
    }
}