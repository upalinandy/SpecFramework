Feature: RegisterXpo


@new
Scenario Outline:Go To Register Page
	Given User is at registration page <url>
	When User enters <fn> <ln> <jobtitle> <email> <reemail> <pwd> <repwd>
	And  also enters <accnt> <phno> <add1> <city> <state> <zip> <ind> <weekly> and <cnt>
	

	And user clicks on create account button
	Then User is navigated to Xpo <ThankyouPg>

	@source:DataResources\XpoData.xlsx
	Examples: 
	| url | fn|ln|jobtitle|email|reemail|pwd|repwd|accnt|phno|add1|cnt|city|state|zip|ind|weekly|ThankyouPg|

