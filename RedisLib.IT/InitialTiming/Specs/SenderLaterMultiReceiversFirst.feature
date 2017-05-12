Feature: SenderLaterMultiReceiversFirst
	In order to make sure system is stable
	As a log system
	I want to make sure in different sender/multi-receiver initiate timing everything is be alright

@SenderLaterMultiReceiversFirst
Scenario: Multi-receiver initiate before a sender
	Given Multi-receiver have been initiated
	And Multi-receiver are waiting for trigger
	When I initiate a sender
	Then A sender can save data into redis
	Then First one receiver can fetch data which are saved by sender
	Then Every receiver get different node id 
