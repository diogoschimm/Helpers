using System.Threading.Tasks;

namespace WorkerService.Integrations.Apis.B3
{
    public interface ITokenAnbima
    {
        Task<string> GetToken();
    }
}
