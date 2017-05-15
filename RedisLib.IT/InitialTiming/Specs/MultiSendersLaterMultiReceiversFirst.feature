Feature: MultiSendersLaterMultiReceiversFirst
	In order to make sure system is stable
	As a log system
	I initial multi receiver first

@mytag
Scenario: Multi-receiver initiate before multi-sender
	Given Multi-receiver have been initiated
	When Initiate multi-sender
	Then Multi-sender can save data into redis
	Then Multi-receiver can fetch data which are saved by senders
	Then Every receiver gets different node id 
