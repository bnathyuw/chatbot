Feature: Following
	In order to keep up to date on my friends' lives
	As a Chatbot user
	I want to have a convenient way of seeing all the posts by my friends
	
Scenario: Charlie can subscribe to Alice’s timeline, and view an aggregated list of all subscriptions
	Given Alice and Charlie have posted to Chatbot
	And Charlie has followed Alice
	When Charlie views his wall
	Then he should see Alice and Charlie's messages

Scenario: Charlie can subscribe to Alice’s and Bob’s timelines, and view an aggregated list of all subscriptions
	Given Alice, Bob and Charlie have posted to Chatbot
	And Charlie has followed Alice
	And Charlie has followed Bob
	When Charlie views his wall a little later
	Then he should see Alice, Bob and Charlie's messages
