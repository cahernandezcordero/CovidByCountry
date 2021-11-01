using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Services;
using System.Net;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   
    public class CovidController : Controller
    {
        [HttpGet]
        [Route("{codIso}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetByCountry(string codIso) {
            try
            {
               
                PasisCovid pasisCovids = new PasisCovid();

                RestClient client = new RestClient($"https://covid2019-api.herokuapp.com/v2/country/{codIso}");
                RestRequest request = new RestRequest(Method.GET);
                request.RequestFormat = DataFormat.Json;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                request.AddHeader("cache-control", "no-cache");
                //client.Authenticator = new HttpBasicAuthenticator("", "");

                var response = client.Execute(request);

                Application dat = new Application();
                dat = LibUtil.Funciones.Convertir.JsonStringToObject<Application>(response.Content);

                pasisCovids.location = dat.data.location;
                pasisCovids.codIso = codIso;
                pasisCovids.confirmed = dat.data.confirmed;
                pasisCovids.deaths = dat.data.deaths;
                pasisCovids.recovered = dat.data.recovered;
                pasisCovids.active = dat.data.active;
                pasisCovids.dt = LibUtil.Funciones.Convertir.ToDateTime(dat.dt);

                    // aqui debe llenar la lista de paises y casos con el resultado (response)
        


                return Ok(pasisCovids);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllCountry()
        {
            try
            {

                CovidForCountryServices r = new CovidForCountryServices();
                var te = await r.GetAllContrys();

                List<PasisCovid> pasisCovids = new List<PasisCovid>();
                foreach (var item in te.Body.ListOfCountryNamesByCodeResult)
                {
                    RestClient client = new RestClient($"https://covid2019-api.herokuapp.com/v2/country/{item.sISOCode}");
                    RestRequest request = new RestRequest(Method.GET);
                    request.RequestFormat = DataFormat.Json;

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    request.AddHeader("cache-control", "no-cache");
                    //client.Authenticator = new HttpBasicAuthenticator("", "");

                    var response = client.Execute(request);

                    Application dat = new Application();
                    dat= LibUtil.Funciones.Convertir.JsonStringToObject<Application>(response.Content);


                    pasisCovids.Add(new PasisCovid()
                    {
                        location = dat.data.location,
                        codIso= item.sISOCode,
                        confirmed = dat.data.confirmed,
                        deaths = dat.data.deaths,
                        recovered = dat.data.recovered,
                        active = dat.data.active,
                        dt = LibUtil.Funciones.Convertir.ToDateTime(dat.dt)

                        // aqui debe llenar la lista de paises y casos con el resultado (response)
                    });
                }

                return Ok(pasisCovids);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
