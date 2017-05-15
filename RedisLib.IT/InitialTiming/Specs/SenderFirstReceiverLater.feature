Feature: SenderFirstReceiversLater
	In order to make sure system is stable
	As a log system
	I want to make sure in different sender/receiver initiate timing everything is be alright

@SenderFirstReceiversLater
Scenario: A sender initiate before a receiver
	Given A sender has been initiated
	And This sender is going to send data
	When Initiate a receiver
	Then A sender can save data into redis
	Then A receiver can fetch data which are saved by sender
