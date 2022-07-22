using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkerService.Core.Contracts.IntegrationsServices;
using WorkerService.Core.Dtos;

namespace WorkerService.Integrations.Apis.B3
{
    public class ApiAnbimaFacade : IApiAnbimaFacade
    {
        private readonly ITokenAnbima _tokenAnbima;

        private readonly string _urlApi;
        private readonly string _clientId;

        public ApiAnbimaFacade(ITokenAnbima tokenAnbima, IConfiguration configuration)
        {
            _tokenAnbima = tokenAnbima;

            _urlApi = configuration.GetSection("Anbima:UrlApi").Value;
            _clientId = configuration.GetSection("Anbima:ClientId").Value;
        }

        public async Task<ResultList<MercadoSecundarioTPF>> GetMercadoSecundarioTPF()
        {
            try
            { 
                var url = $"{_urlApi}/feed/precos-indices/v1/titulos-publicos/mercado-secundario-TPF";
                var data = await Requisicao.Get<List<MercadoSecundarioTPF>>(url, await GetHeaders());

                return ResultList<MercadoSecundarioTPF>.CreateSuccess(data);
            }
            catch (Exception ex)
            {
                return ResultList<MercadoSecundarioTPF>.CreateError(ex.Message);
            }
        }

        private async Task<Dictionary<string, string>> GetHeaders()
        {
            var accessToken = await _tokenAnbima.GetToken();

            return new Dictionary<string, string>
                {
                    { "client_id", _clientId },
                    { "access_token",  accessToken }
                };
        }
    }
}
