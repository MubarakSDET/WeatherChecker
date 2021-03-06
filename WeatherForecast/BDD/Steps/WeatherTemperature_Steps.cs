using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using NUnit.Framework;
using WeatherForecast.Libraries;

namespace WeatherForecast.BDD.Steps
{
    [Binding]
    public sealed class WeatherTemperature_Steps
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;
        ApiHelper _apiHelper = new ApiHelper();
        Validations _validations = new Validations();

        public WeatherTemperature_Steps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"As a Weather Enthusiast, I want to know the weather in ""(.*)""")]
        public void GivenAsAWeatherEnthusiastIWantToKnowTheWeatherIn(string CityName)
        {
            ScenarioContext.Current["CityName"] = CityName;
        }

        [When(@"the predicted temperature to be above 20 degrees in the next (.*) days")]
        public void WhenThePredictedTemperatureToBeAboveDegreesInTheNextDays(int Number)
        {
            ScenarioContext.Current["Number"] = Number;
            _validations.NumberofDays = Number;
            _apiHelper.ConstructURL(Number);
        }



        [When(@"I trigger '(.*)' API call to retrieve the weather response")]
        public async Task WhenITriggerAPICallToRetrieveTheWeatherResponse(string apiMethod)
        {
            switch (apiMethod.ToLower())
            {
                case "get":
                    {
                        await _apiHelper.InvokeGetAPI(ScenarioContext.Current["finalUrl"].ToString());
                        break;
                    }
                case "POST":
                    {
                        Console.WriteLine("Please configure post API details");
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Default API details");
                        break;
                    }
            }
        }

        [Then(@"I validate the status code as ""(.*)"" from the response")]
        [Obsolete]
        public void ThenIValidateTheStatusCodeAsFromTheResponse(int responseCode)
        {
            if (ScenarioContext.Current["ResponseCode"].ToString()!= responseCode.ToString())
            {
                var errorMessage = "Unexpected Response Code.Expected: " + responseCode + ", Actual: " + ScenarioContext.Current["ResponseCode"].ToString();
                Assert.Fail(errorMessage);
            }
            else
            {
                var SuccessMessage = "Expected Response Code.Expected: " + responseCode + ", Actual: " + ScenarioContext.Current["ResponseCode"].ToString();

            }
        }

        [Then(@"I check the values ""(.*)"" and (.*) in the response")]
        public void ThenICheckTheValuesAndInTheResponse(string condition, int Degrees)
        {
            _validations.ValidateTemperatures(condition, Degrees);
        }

        [Then(@"I check the values ""(.*)"" and (.*) in the response without date input values")]
        public void ThenICheckTheValuesAndInTheResponseWithoutDateInputValues(string condition, int Degrees)
        {
            _validations.ValidateTemperaturesWithoutDateCalculation(condition, Degrees);
        }


        [Then(@"I logged the Number of days ""(.*)"" in console window")]
        public void ThenILoggedTheNumberOfDaysInConsoleWindow(string Weather)
        {
            _validations.ValidateWeather(Weather);
        }

        [Then(@"I also want to know ""(.*)"" in the same time period in the response without date input values")]
        public void ThenIAlsoWantToKnowInTheSameTimePeriodInTheResponseWithoutDateInputValues(string Weather)
        {
            _validations.ValidateWeatherWithoutDateCalculation(Weather);
        }


        [Then(@"I also want to know ""(.*)"" in the same time period")]
        public void ThenIAlsoWantToKnowInTheSameTimePeriod(string Weather)
        {
            _validations.ConsoleWeather(Weather);
        }

        [Then(@"I logged the retrieved response in console for reference")]
        public void ThenILoggedTheRetrievedResponseInConsoleForReference()
        {
            Console.WriteLine("******************************");
            Console.WriteLine($"Response message: {ScenarioContext.Current["ResponseContent"].ToString()}");
            Console.WriteLine("******************************");
        }




    }
}
