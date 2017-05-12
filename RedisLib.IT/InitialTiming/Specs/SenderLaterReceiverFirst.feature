Feature: SenderLaterReceiverFirst
	In order to make sure system is stable
	As a log system
	I want to make sure in different sender/receiver initiate timing everything is be alright

@SenderLaterReceiverFirst
Scenario: A eceiver initiate before a sender
	Given A receiver has been initiated
	And A receiver is waiting for trigger
	When I initiate a sender
	Then A sender can save data into redis
	Then A receiver can fetch data which is saved by sender
