Feature: RegisterXpo_New


@excel
Scenario Outline:Go To Registeration Page
	Given User is on registration page <url>
	When User entered <fn> <ln> <jobtitle> <email> <reemail> <pwd> <repwd>
	And  also entered <accnt> <phno> <add1> <city> <state> <zip> <ind> <weekly> and <cnt>
	

	And user clicks on create account button
	Then User is navigated to Xpo <ThankyouPg>

	@source:DataResources\XpoData.xlsx
	Examples: 
	| url | fn|ln|jobtitle|email|reemail|pwd|repwd|accnt|phno|add1|cnt|city|state|zip|ind|weekly|ThankyouPg|

