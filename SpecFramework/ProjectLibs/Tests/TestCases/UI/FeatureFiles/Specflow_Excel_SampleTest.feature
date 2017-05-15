Feature: Specflow_Excel_SampleTest

@excel
Scenario Outline:Go To Login Page
	Given User is at homepage <url>
	When User clicks on Signin
	Then User is navigated to Uber <signinpage>

	@source:DataResources\TestData.xlsx
	Examples: 
	| url                         | signinpage|

@table
Scenario Outline: tabletest
	Given User is at homepage <url>
	When User clicks on Login
	Then User is navigated to Airbnb <loginpage>

	Examples: 
	| url                       | loginpage                  |
	| https://www.airbnb.co.in/ |Vacation Rentals, Homes, Experiences & Places - Airbnb  |

      





