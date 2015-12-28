Feature: I can view Alice and Bob's timelines
	In order to be part of the Chatbot community
	As a Chatbot subscriber
	I want to read other users' timelines

Scenario: View Alice's timeline
	Given Alice has posted to Chatbot
	When I view Alice's timeline
	Then I should see Alice's message
