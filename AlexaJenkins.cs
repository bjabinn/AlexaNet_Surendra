using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET;
using Alexa.NET.Response;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;

namespace AlexaAzureTest0_0_1
{
    public static class AlexaJenkins
    {
        [FunctionName("Alexa")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequest req, ILogger log)
        {
            string json = await req.ReadAsStringAsync();
            var skillRequest = JsonConvert.DeserializeObject<SkillRequest>(json);

            var requestType = skillRequest.GetRequestType();

            SkillResponse response = null;

            switch (requestType)
            {
                case Type _ when requestType == typeof(LaunchRequest):
                    response = ResponseBuilder.Tell("Surendra a su servicio señor");
                    response.Response.ShouldEndSession = false;
                    return new OkObjectResult(response);

                case Type _ when requestType == typeof(IntentRequest):
                    var intentRequest = (IntentRequest)skillRequest.Request;
                    string aplicacion = intentRequest.Intent.Slots["aplicacion"].Value;
                    string entorno = intentRequest.Intent.Slots["entorno"].Value;

                    string mensaje = string.Empty;
                    response = ResponseBuilder.Tell(mensaje);
                    response.Response.ShouldEndSession = false;
                    return new OkObjectResult(response);

                case Type _ when requestType == typeof(Error):
                    response = ResponseBuilder.Tell("Ha habido un error");
                    response.Response.ShouldEndSession = false;
                    return new OkObjectResult(response);

                default:
                    response = ResponseBuilder.Tell("Lo siento compañero, no he entendido su petición");
                    response.Response.ShouldEndSession = false;
                    return new OkObjectResult(response);
            }
        }
    }
}
