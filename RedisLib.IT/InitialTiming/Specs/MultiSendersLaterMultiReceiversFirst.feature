Feature: MultiSendersLaterMultiReceiversFirst
	In order to make sure system is stable
	As a log system
	I want to make sure in different multi-sender/multi-receiver initiate timing everything is be alright

@MultiSendersLaterMultiReceiversFirst
Scenario: Multi-receiver initiate before multi-sender
	Given Multi-receiver have been initiated
	And This multi-receiver are waiting for tirgger
	When I initiate multi-sender
	Then Multi-sender can save data into redis
	Then Multi-receiver can fetch data which are saved by senders
	Then Every receiver get different node id 
