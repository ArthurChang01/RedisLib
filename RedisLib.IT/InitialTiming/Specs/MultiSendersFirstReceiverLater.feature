Feature: MultiSendersFirstReceiverLater
	In order to make sure system is stable
	As a log system
	I want to make sure in different multi-sender/receiver initiate timing everything is be alright

@MultiSendersFirstReceiverLater
Scenario: Multi-sender initiate before a receiver
	Given A multi-sender has been initiated
	And this multi-sender are going to send data
	When I initiate a receiver
	Then Multi-sender can save data into redis
	Then A receiver can fetch data which are saved by senders
