Feature: GoogleApi
	To test the latitude and longgitude for location

@api
Scenario Outline: Verify Latitude and Longitude 
#SFLOW-619 Opened on: 2017-06-26T03:25:28.267-0500
#Last Execution Failed on: 26-06-2017, 13:54
	Given Google api that takes address and returns latitude and longitude
	When The client Gets response by <address>
	Then The < Latitude> and <Longitude> returned should be as expected

	@source:DataResources\APIData.xlsx
	Examples: 
	| address                                      | Latitude   | Longitude |


