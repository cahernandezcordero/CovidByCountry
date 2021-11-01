using CountryInfoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class CovidForCountryServices
    {
       
    

        public async Task<ListOfCountryNamesByCodeResponse> GetAllContrys()
        {
            CountryInfoServiceSoapTypeClient service = new CountryInfoServiceSoapTypeClient(CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap12);
            ListOfCountryNamesByCodeResponse result  =  await service.ListOfCountryNamesByCodeAsync();
            return result;
        }
    }
}
 