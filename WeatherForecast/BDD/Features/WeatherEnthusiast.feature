Feature: WeatherEnthusiast

#Requirements
#As a Weather Enthusiast, I would like to know the number of days in Sydney
#where the temperature is predicated to be above 20 degrees in the next 5 days, (from the current days date), or
#
#I would also like to know how many days it is predicted to be sunny in the same time period.

@IncludedDynamicDateValidation_ExcludedToday'sResults
	Scenario Outline: TemperatureAndWeatherChecker
	Given As a Weather Enthusiast, I want to know the weather in "<CityName>"
	When the predicted temperature to be above 20 degrees in the next <Number> days
	And I trigger 'GET' API call to retrieve the weather response
	Then I validate the status code as "<StatusCode>" from the response
	Then I check the values "<Condition>" and <Degrees> in the response
	Then I also want to know "<Weather>" in the same time period
	And I logged the Number of days "<Weather>" in console window
	And I logged the retrieved response in console for reference

	Examples: 
	| CityName  | Number | StatusCode | Condition | Degrees | Weather          |
	| Sydney    | 5      | 200        | above     | 20      | sunny            |
	| Adelaide  | 5      | 200        | below     | 15      | broken clouds    |
	| Brisbane  | 5      | 200        | above     | 20      | scattered clouds |
	| London    | 5      | 200        | above     | 20      | overcast clouds  |
	| Melbourne | 5      | 200        | above     | 20      | overcast clouds  |


@WithoutDateInput_IncludedToday'sResults
	Scenario Outline: TemperatureAndWeatIherCheckerWithoutDateValidation
	Given As a Weather Enthusiast, I want to know the weather in "<CityName>"
	When the predicted temperature to be above 20 degrees in the next <Number> days
	And I trigger 'GET' API call to retrieve the weather response
	Then I validate the status code as "<StatusCode>" from the response
	Then I check the values "<Condition>" and <Degrees> in the response without date input values
	Then I also want to know "<Weather>" in the same time period in the response without date input values
	And I logged the Number of days "<Weather>" in console window
	And I logged the retrieved response in console for reference

	Examples: 
	| CityName  | Number | StatusCode | Condition | Degrees | Weather          |
	| Sydney    | 5      | 200        | above     | 20      | sunny            |
	| Adelaide  | 5      | 200        | below     | 15      | broken clouds    |
	| Brisbane  | 5      | 200        | above     | 20      | scattered clouds |
	| London    | 5      | 200        | above     | 20      | overcast clouds  |
	| Melbourne | 5      | 200        | above     | 20      | overcast clouds  |
