using IdentityModel.Client;
using System.Threading.Tasks;

namespace Majority.RemittanceProvider.API.Services
{
    public interface IIdentityServerService
    {
        Task<TokenResponse> GetToken(string apiScope);
    }
}
