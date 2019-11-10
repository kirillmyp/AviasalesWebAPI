using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AviasalesWebAPI.Configs;
using AviasalesWebAPI.ModelAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AviasalesWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AviaController : ControllerBase
    {
        public readonly AppsettingOption appsettingOption;
        public AviaController(IOptionsMonitor<AppsettingOption> _appsettingOption)
        {
            appsettingOption = _appsettingOption?.CurrentValue;
        }

        [HttpGet("Matrix")]
        public async Task<IEnumerable<ticketsV2>> GetMounthMatrixAsync()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            var httpClient = new HttpClient(handler);

            var fullUrl = string.Empty;
            httpClient.DefaultRequestHeaders.Add("X-Access-Token", appsettingOption.Token);
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");

            string CityFrom = "LED";
            string CityTo = "HKT";
            fullUrl = "http://api.travelpayouts.com/v2/prices/month-matrix";
            fullUrl += "?";
            fullUrl += @$"currency=rus&show_to_affiliates=true&origin={CityFrom}&destination={CityTo}&month=2019-11-10&one_way=false&limit=10";
            fullUrl += "&sorting=price";

            var request = await httpClient.GetAsync(fullUrl);
            try
            {
                string json = request.Content.ReadAsStringAsync().Result;
                var listTikets = JsonConvert.DeserializeObject<ResponseV2>(json);// result
                var result = listTikets.data.Where((t => t.number_of_changes < 2)).OrderBy((t) => t.value).ToList();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error");
            }
            return null;
        }

        [HttpGet("Mounth")]
        public async Task<IEnumerable<ticketsV2>> GetMounthAsync()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            var httpClient = new HttpClient(handler);

            var fullUrl = string.Empty;
            httpClient.DefaultRequestHeaders.Add("X-Access-Token", appsettingOption.Token);
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");

            string CityFrom = "BAK";
            string CityTo = "LED";
            fullUrl = "http://api.travelpayouts.com/v2/prices/latest";
            fullUrl += "?";
            fullUrl += @$"currency=rus&period_type=month&beginning_of_period=2020-02-01&limit=100&show_to_affiliates=false&sorting=price&one_way=false&trip_class=0&origin={CityFrom}&destination={CityTo}";
            fullUrl += "&sorting=price";

            var request = await httpClient.GetAsync(fullUrl);
            try
            {
                string json = request.Content.ReadAsStringAsync().Result;
                var listTikets = JsonConvert.DeserializeObject<ResponseV2>(json);
                var result = listTikets.data.Where((t => t.number_of_changes < 2)).OrderBy((t) => t.value).ToList();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error");
            }
            return null;
        }

        [HttpGet("X")]
        public async Task<IEnumerable<ticketsV2>> GetAnyoneAsync()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            var httpClient = new HttpClient(handler);

            var fullUrl = string.Empty;
            httpClient.DefaultRequestHeaders.Add("X-Access-Token", appsettingOption.Token);
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");

            string CityFrom = "LED";
            string CityTo = "AMS";// "AMS";
            fullUrl = "http://api.travelpayouts.com/v2/prices/nearest-places-matrix";
            fullUrl += "?";
            fullUrl += @$"currency=rus&limit=100&show_to_affiliates=false&sorting=price&trip_class=0&origin={CityFrom}&destination={CityTo}&depart_date=2019-12-20&return_date=2019-12-20&distance=1";
            fullUrl += "&sorting=price";

            var request = await httpClient.GetAsync(fullUrl);
            try
            {
                string json = request.Content.ReadAsStringAsync().Result;
                var listTikets = JsonConvert.DeserializeObject<ResponseV2>(json);
                var result = listTikets.data.Where((t => t.number_of_changes < 1)).OrderBy((t) => t.value).ToList();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error");
            }
            return null;
        }

        [HttpGet("X2")]
        public async Task<IEnumerable<ticketsV2>> GetAnyone2Async()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            var httpClient = new HttpClient(handler);

            var fullUrl = string.Empty;
            httpClient.DefaultRequestHeaders.Add("X-Access-Token", appsettingOption.Token);
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");

            string CityFrom = "LED";
            string CityTo = "AMS";// "AMS";
            fullUrl = "http://api.travelpayouts.com/v2/prices/week-matrix";
            fullUrl += "?";
            fullUrl += @$"currency=rus&show_to_affiliates=false&origin={CityFrom}&&depart_date=2020-01-01&return_date=2020-01-10";//destination={CityTo}   &distance=1
            fullUrl += "&sorting=price";

            var request = await httpClient.GetAsync(fullUrl);
            try
            {
                string json = request.Content.ReadAsStringAsync().Result;
                var listTikets = JsonConvert.DeserializeObject<ResponseV2>(json);
                var result = listTikets.data.Where((t => t.number_of_changes < 1)).OrderBy((t) => t.value).ToList();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error");
            }
            return null;
        }
    }
}