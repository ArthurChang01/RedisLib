Feature: MultiSendersLaterReceiverFirst
	In order to make sure system is stable
	As a log system
	I want to make sure in different multi-sender/receiver initiate timing everything is be alright

@MultiSendersLaterReceiverFirst
Scenario: A receiver initiate before multi-sender
	Given A receiver has been initiated
	When Initiate multi-sender
	Then Multi-sender can save data into redis
	Then A receiver can fetch data which are saved by senders
