Feature: Reading
	In order to be part of the Chatbot community
	As a Chatbot subscriber
	I want to read other users' timelines

Scenario Outline: I can view Alice and Bob's timelines
	Given <user> has posted to Chatbot
	When I view <user>'s timeline
	Then I should see <user>'s messages

Scenarios:
	| user  |
	| Alice |
	| Bob   |
