Feature: Following
	In order to keep up to date on my friend's lives
	As a Chatbot user
	I want to have a convenient way of seeing all the posts by my friends
	
Scenario: Charlie can subscribe to Alice’s timeline, and view an aggregated list of all subscriptions
	Given Alice and Charlie have posted to Chatbot
	And Charlie has followed Alice
	When Charlie views his wall
	Then he should see Alice and Charlie's messages
