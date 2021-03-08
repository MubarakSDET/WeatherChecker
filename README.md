# WeatherChecker

Dynamic weather checker using OpenWeather API.

Developed with "scenario outline" features to get the predicted weather information using the openweather API with dynamic user inputs.

Setup: Simply download, build and execute the results, if required, can change the appid in app.config file.

Language: C# IDE: Visual Studio Enterprise 2019 Framework: BDD (Specflow) with Nunit

RequiredReferences: nunit.framework Newtonsoft.Json System.Configuration TechTalk.Specflow TechTalk.Specflow.Nunit.SpecflowPlugin

Dynamic Input: User can provides whatever CityName, Weather and other details in the examples section. And can check whole world weathers.

Files Details:

App.config Configured URL and Appid values. If anyone wants to update the values can simply update in app.config itself.

WeatherEnthusiat.feature - Feature File (10 scenarios) WeatherTemperature_Steps.cs - Steps file

ApiHelper.cs Dynamic User construction and API GET method ingestion.

Validation.cs Used linq query validation. All validations methods and logging methods.

Feature additional information:

Parametrized the below values in "Examples" in dynamic way to retreive different types of Cities weather information. Cityname (Diffeent types of City Name) Number of days (Predicted days), StatusCode (Check the response) Condition (below 20 degrees or above 20 degrees) Degrees (10, 15, 20, 30 degrees) Weather (Differnt types of weather)

########################

SenarioDescription:

Name: TempertatureAndWeatherChecker Info: DEveloped the date based validation without today's results

Name: TempertatureAndWeatherCheckerWithoutDateValidation Info: DEveloped without date based validation and includes today's results

TotalScenarios: 10 Execution Time (Total): 5.6 Seconds

#########################

Logged the results in console window.

Added the appropriate comments for reference.
