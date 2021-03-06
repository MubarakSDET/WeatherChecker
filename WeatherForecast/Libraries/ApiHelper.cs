using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace WeatherForecast.Libraries
{
    class ApiHelper
    {
        private static readonly HttpClient Client = new HttpClient();  
        List<KeyValuePair<string, string>> _actualResponseList = new List<KeyValuePair<string, string>>();
        public string ResponseContent = null;

        public void ConstructURL(int Number)
        {
            int daysCount = 0;
            string baseUrl = ConfigurationManager.AppSettings["URL"];
            //Per day 8 occurrences, every 3 hours, for example
            //2021 - 03 - 06 00:00:00
            //2021 - 03 - 06 03:00:00
            //2021 - 03 - 06 06:00:00
            //2021 - 03 - 06 09:00:00
            //2021 - 03 - 06 12:00:00
            //2021 - 03 - 06 15:00:00
            //2021 - 03 - 06 18:00:00
            //2021 - 03 - 06 21:00:00
            daysCount = Number * 8;
            //Current date maxmimum count 8.
            daysCount = daysCount + 8;
            ScenarioContext.Current["finalUrl"] = string.Concat(baseUrl, ScenarioContext.Current["CityName"], "&units=metric&cnt=", daysCount, "&appid=", ConfigurationManager.AppSettings["appid"]);
        }

        public async Task<string> InvokeGetAPI(string finalUrl)
        {
            _actualResponseList.Clear();
            Client.DefaultRequestHeaders.Accept.Clear();
            var response = await Client.GetAsync(finalUrl).ConfigureAwait(false);
            ResponseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if(ScenarioContext.Current != null)
            {
                ScenarioContext.Current["ResponseCode"] = (int)response.StatusCode;
            }
            ScenarioContext.Current["ResponseContent"] = ResponseContent;
            return ResponseContent;
        }
    }
}
