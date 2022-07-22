using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorkerService.Integrations.Apis.B3
{
    public class TokenAnbima : ITokenAnbima
    {
        private readonly string _urlToken;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public TokenAnbima(IConfiguration configuration)
        {
            _urlToken = configuration.GetSection("Anbima:UrlToken").Value;
            _clientId = configuration.GetSection("Anbima:ClientId").Value;
            _clientSecret = configuration.GetSection("Anbima:ClientSecret").Value;
        }

        public async Task<string> GetToken()
        {
            var body = $@"{{""grant_type"": ""client_credentials""}}";
            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Basic {Base64.Encode($"{_clientId}:{_clientSecret}")}" }
            };

            var tokenResult = await Requisicao.Post<TokenResult>(_urlToken, body, headers);
            return tokenResult.Access_Token;
        }
    }
}
