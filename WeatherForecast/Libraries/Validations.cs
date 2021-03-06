using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TechTalk.SpecFlow;

namespace WeatherForecast.Libraries
{
    public class Validations
    {
        public static int NumberOfDaysAbove20Degrees = 0;
        public static int NumberOfDaysBelow20Degrees = 0;
        public static int NumberOfDaysWeather = 0;
        public int NumberofDays = 0;

        
        public void ValidateTemperatures(string condition, int Degrees)
        {
            JObject jobject = JObject.Parse((string)ScenarioContext.Current["ResponseContent"]);
            IEnumerable<JToken> Temperatures = jobject.SelectTokens("$..dt_txt");

            //Excluded today's date
            for (int DateInitial = 1; DateInitial < NumberofDays + 1; DateInitial++)
            {
                //To search the next today's date
                string SearchDay = DateTime.UtcNow.AddDays(DateInitial).ToString("yyyy-MM-dd");

                foreach (JToken item in Temperatures)
                {
                    string value = item.ToObject<string>();
                    if (value.Contains(SearchDay))
                    {
                        JToken parentValue = item.Parent.Parent;
                        JToken temperatureActual = parentValue.SelectToken("$..main.temp");
                        int Convertedvalue = temperatureActual.ToObject<int>();

                        if (condition.Equals("above"))
                        {
                            if (Convertedvalue > Degrees)
                            {
                                NumberOfDaysAbove20Degrees = NumberOfDaysAbove20Degrees + 1;
                            }
                        }
                        else
                        {
                            if (Convertedvalue < Degrees)
                            {
                                NumberOfDaysBelow20Degrees = NumberOfDaysBelow20Degrees + 1;
                            }
                        }

                    }
                }

            }

            if (condition.Equals("above"))
            {
                Console.WriteLine("Number of above " + Degrees + " degree days: " + NumberOfDaysAbove20Degrees.ToString());
            }
            else
            {
                Console.WriteLine("Number of above " + Degrees + " degree days: " + NumberOfDaysBelow20Degrees.ToString());

            }
        }
    
    
        public void ValidateTemperaturesWithoutDateCalculation(string condition, int Degrees)
        {
            JObject jobject = JObject.Parse((string)ScenarioContext.Current["ResponseContent"]);
            IEnumerable<JToken> Temperatures = jobject.SelectTokens("$..main.temp");
            
            foreach (JToken item in Temperatures)
            {
                int value = item.ToObject<int>();
                if (condition.Equals("above"))
                {
                    if (value > Degrees)
                    {
                        NumberOfDaysAbove20Degrees = NumberOfDaysAbove20Degrees + 1;
                    }
                }
                else
                {
                    if (value < Degrees)
                    {
                        NumberOfDaysBelow20Degrees = NumberOfDaysBelow20Degrees + 1;
                    }
                }
            }

            if (condition.Equals("above"))
            {
                Console.WriteLine("Number of above " + Degrees + " degree days: " + NumberOfDaysAbove20Degrees.ToString());
            }
            else
            {
                Console.WriteLine("Number of above " + Degrees + " degree days:" + NumberOfDaysBelow20Degrees.ToString());

            }
        }
        public void ValidateWeather (string weather)
        {
            JObject jobject = JObject.Parse((string)ScenarioContext.Current["ResponseContent"]);
            IEnumerable<JToken> TotalWeather = jobject.SelectTokens("$..dt_txt");

            //Excluded today's date
            for (int DateInitial = 1; DateInitial < NumberofDays + 1; DateInitial++)
            {
                //To search the next today's date
                string SearchDay = DateTime.UtcNow.AddDays(DateInitial).ToString("yyyy-MM-dd");

                foreach (JToken item in TotalWeather)
                {
                    string value = item.ToObject<string>();
                    if (value.Contains(SearchDay))
                    {
                        JToken parentValue = item.Parent.Parent;
                        JToken WeatherActual = parentValue.SelectToken("$..description");
                        string WeatherActualvalue = WeatherActual.ToObject<string>();

                        if (!WeatherActualvalue.Equals(""))
                        {
                            if (WeatherActualvalue.ToString().Contains(weather))
                            {
                                NumberOfDaysWeather = NumberOfDaysWeather + 1;
                            }
                        }

                    }
                }

            }
        }

        public void ValidateWeatherWithoutDateCalculation(string weather)
        {
            JObject jobject = JObject.Parse((string)ScenarioContext.Current["ResponseContent"]);
            IEnumerable<JToken> WeatherDescription = jobject.SelectTokens("$..description");

            foreach (JToken item in WeatherDescription)
            {
                if (!item.ToString().Equals(""))
                {
                    if (item.ToString().Contains(weather))
                    {
                        NumberOfDaysWeather = NumberOfDaysWeather + 1;
                    }
                }
            }
        }

        public void ConsoleWeather (string weather)
       {
            Console.WriteLine("Number of '" + weather + "' days: " + NumberOfDaysWeather.ToString());

       }

   

    }
}
