Feature: MultiSendersFirstReceiverLater
	In order to make sure system is stable
	As a log system
	I initial multi sender first and then I initial a receiver later

@MultiSendersFirstReceiverLater
Scenario: Multi-sender initiate before a receiver
	Given A multi-sender has been initiated
	And this multi-sender are going to send data
	When I initiate a receiver
	Then Multi-sender can save data into redis
	Then A receiver can fetch data which are saved by senders
