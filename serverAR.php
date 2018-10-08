<?php
	// test server side application 
	// extract one part of the json object and send it back to JS for test purposes
	$json = json_decode(file_get_contents("php://input"));
	$response = $json[3]->instructionText;
	echo $response;

?>