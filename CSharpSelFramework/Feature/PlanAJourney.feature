@POC
Feature: POC on Plan my journey
POC on Plan a Journey widget in TFL website

@smoke
Scenario: Verify that a valid journey can be planned using the widget.
	Given I navigate to the tfl plan a journey page
	When I enter From location as "Deptford Bridge DLR Station"
	And I enter To location as "Bexleyheath Rail Station"
	And I click on Plan my Jouney button
	Then I should see the Journey results as below
		| Key  | Value                       |
		| From | Deptford Bridge DLR Station |
		| To   | Bexleyheath Rail Station    |
	And I should see the following default options in cycling section
		| Cycling               |
		| Least walking         |
		| Fewest changes        |
		| Full step free access |
		| Nearby taxi ranks     |
	And I should see the default section public transport and bus only



@smoke
Scenario: Verify that the widget is unable to provide results when an invalid journey is planned.
	Given I navigate to the tfl plan a journey page
	When I enter invalid From location as "1234" and To location as "5678"
	And I click on Plan my Jouney button
	Then I should see the Journey results as below
		| Key  | Value |
		| From | 1234  |
		| To   | 5678  |
	And I should see the error "Journey planner could not find any results to your search. Please try again"

@smoke
Scenario: Verify that the widget is unable to plan a journey if no locations are entered into the widget.
	Given I navigate to the tfl plan a journey page
	When I directly click on Plan my Jouney button with no locations entered
	Then I should see the below error in
		| Key  | Value                       |
		| From | The From field is required. |
		| To   | The To field is required.   |
@smoke
Scenario: On the journey results page,verify that a journey can be amended by using the 'Edit Journey' button.
	Given I navigate to the tfl plan a journey page
	When I enter invalid From location as "London Bridge" and To location as "London Bridge"
	And I click on Plan my Jouney button
	Then I should see the Journey results as below
		| Key  | Value         |
		| From | London Bridge |
		| To   | London Bridge |
	When I click on Edit Journey hyperlink
	And I modify the To location as "Woolwich Arsenal"
	And I click on Update Journey button
	Then I should see the Journey results as below
		| Key  | Value            |
		| From | London Bridge    |
		| To   | Woolwich Arsenal |

@smoke
Scenario Outline: Verify that Recents tab on the widget displays a list of recently planned journeys.
	Given I navigate to the tfl plan a journey page
	When I enter From location as "<From>" and select the value using "<FromId>" dropdown
	And I enter To location as "<To>" and select the value usig "<ToId>" dropdown
	And I click on Plan my Jouney button
	Then I should see the default section public transport
	When I navigate back to plan a journey page
	And I select the values from recent search
	And I click on Plan my Jouney button
	Then I should see the default section public transport
	When I navigate back to plan a journey page
	Then I should see the Recent tab updated as '<Recent Tab>'
Examples:
	| From          | To            | FromId    | ToId        | Recent Tab                                                                 |
	| london bridge | picc          | HUBLBG    | 940GZZLUPCC | Piccadilly Circus Underground Station to London Bridge                     |
	| London        | Milton        | 910GLIVST | 910GMKNSCEN | Milton Keynes Central Rail Station to London Liverpool Street Rail Station |
	| Canary Wharf  | Cannon Street | HUBCAW    | HUBCST      | Cannon Street to Canary Wharf                                              |





