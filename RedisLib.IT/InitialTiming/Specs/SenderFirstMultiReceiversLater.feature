Feature: SenderFirstMultiReceiversLater
	In order to make sure system is stable
	As a log system
	I want to make sure in different sender/multi-receiver initiate timing everything is be alright

@SenderFirstMultiReceiversLater
Scenario: A sender initiate before multi-receiver
	Given A sender has been initiated
	And This sender is going to send data
	When Initiate multi-receiver
	Then A sender can save data into redis
	Then Multi-receiver can fetch data which are saved by sender
	Then Every receiver gets different node id 
