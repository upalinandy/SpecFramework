Feature: ExecuteDBQuery

@api
Scenario: Connect to DB and execute select statement to print details 
	Given User connects to the DB
	When Select query is executed 
	Then Result is returned and displayed 

	
