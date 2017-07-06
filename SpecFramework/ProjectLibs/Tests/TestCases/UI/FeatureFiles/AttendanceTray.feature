Feature: MarkLMSAttendanceWithAttendanceTray

@cefapp
Scenario Outline: Verify the attendance gets marked as expected when user is in office for the day(office,office)
	Given Tray App opens up and user logs in with his wipfli credenditals
	When  the user marks the attendance as first half <firstHalfOption> and second half <secondHalfOption>
	Then  his attendance should get marked/saved as <firstHalfExpected> and <secondHalfExpected> 

Examples: 
|firstHalfOption  |secondHalfOption  |firstHalfExpected             |secondHalfExpected             |
|firstHalfInOffice|secondHalfInOffice|attendanceForUser.FirstHalf==1|attendanceForUser.SecondHalf==1|
|				  |                  |attendanceForUser.FirstHalf==1|attendanceForUser.SecondHalf==1|
|firstHalfInOffice|                  |attendanceForUser.FirstHalf==1|attendanceForUser.SecondHalf==1|
|                 |secondHalfInOffice|attendanceForUser.FirstHalf==1|attendanceForUser.SecondHalf==1|


Scenario Outline: Verify the attendance gets marked as expected when user selects any of 'WFH/InOffice/Official Travel' options for the first half and 2nd half
	Given Tray App opens up and user logs in with his wipfli credenditals
	When  User marks the attendance as first half < firstHalfOption > and second half < secondHalfOption > and adds the comments <comments> and recipients <recipients>
	Then  his attendance should get marked/saved as <firstHalfExpected> and <secondHalfExpected> 

Examples: 
| firstHalfOption    | secondHalfOption      | comments         | recipients                            | firstHalfExpected            | secondHalfExpected            |
| firstHalfInOffice  | secondHalfWfh         | Not Well         |Uday Jampala <ujampala@spiderlogic.com>|attendanceForUser.FirstHalf==1|attendanceForUser.SecondHalf==2|
| firstHalfWfh       | secondHalfInOffice    | Courier expected |Uday Jampala <ujampala@spiderlogic.com>|attendanceForUser.FirstHalf==2|attendanceForUser.SecondHalf==1|
| firstHalfWfh       | secondHalfWfh         | baby sitting     |Uday Jampala <ujampala@spiderlogic.com>|attendanceForUser.FirstHalf==2|attendanceForUser.SecondHalf==2|
| firstHalfOfficial  | secondHalfInOffice    | Banglore Travel  |Uday Jampala <ujampala@spiderlogic.com>|attendanceForUser.FirstHalf==3|attendanceForUser.SecondHalf==1|
| firstHalfInOffice  | secondHalfOfficial    | Flying to Blore  |Uday Jampala <ujampala@spiderlogic.com>|attendanceForUser.FirstHalf==1|attendanceForUser.SecondHalf==3|
| firstHalfOfficial  | secondHalfOfficial    | US Travel        |Uday Jampala <ujampala@spiderlogic.com>|attendanceForUser.FirstHalf==3|attendanceForUser.SecondHalf==3|
| firstHalfOfficial  | secondHalfWfh         | Out of station   |Uday Jampala <ujampala@spiderlogic.com>|attendanceForUser.FirstHalf==3|attendanceForUser.SecondHalf==2|
| firstHalfWfh       | secondHalfOfficial    | Attending Conf   |Uday Jampala <ujampala@spiderlogic.com>|attendanceForUser.FirstHalf==2|attendanceForUser.SecondHalf==3|


Scenario Outline: Verify the attendance gets marked as expected when user is on leave for a day or half
	Given the Tray App opens up and user logs in with his wipfli credenditals to mark leave
	When  User marks the attendance as first half < firstHalfOption > and second half < secondHalfOption > and adds the comments <comments> and recipients <recipients>
	Then  users attendance should get marked/saved as < firstHalfExpected > and < secondHalfExpected > and leave balance should be calculated based on leave count <leaveCount>

Examples: 
| firstHalfOption | secondHalfOption | comments | recipients                              | firstHalfExpected              | secondHalfExpected              | leaveCount |
| firstHalfLeave  | secondHalfLeave  | Day Off  | Uday Jampala <ujampala@spiderlogic.com> | attendanceForUser.FirstHalf==4 | attendanceForUser.SecondHalf==4 | 1          |

Scenario Outline: Verify cancel leave
	Given try
	When  cancel leave
	Then  gets canceled

Examples: 
|firstHalfOption  |secondHalfOption  |firstHalfExpected             |secondHalfExpected             |
|firstHalfInOffice|secondHalfInOffice|attendanceForUser.FirstHalf==1|attendanceForUser.SecondHalf==1|

