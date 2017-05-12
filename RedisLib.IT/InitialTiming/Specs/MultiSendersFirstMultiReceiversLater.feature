﻿Feature: MultiSendersFirstMultiReceiversLater
	In order to make sure system is stable
	As a log system
	I want to make sure in different multi-sender/multi-receiver initiate timing everything is be alright

@MultiSendersFirstMultiReceiversLater
Scenario: Multi-sender initiate before multi-receiver
	Given Multi-sender have been initiated
	And This multi-sender are going to send data
	When I initiate multi-receiver
	Then Multi-sender can save data into redis
	Then Multi-receiver can fetch data which are saved by senders
	Then Every receiver get different node id 
