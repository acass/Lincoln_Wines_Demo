﻿using UnityEngine;
using TMPro;
using UnityEngine.UI;
using KKSpeech;

public class RecordingCanvas : MonoBehaviour {

	public Button startRecordingButton;
    public TMP_Text resultText;
    public parseMyJson mparseMyJson;

	void Start() {

		if (SpeechRecognizer.ExistsOnDevice()) {
			SpeechRecognizerListener listener = GameObject.FindObjectOfType<SpeechRecognizerListener>();
			listener.onAuthorizationStatusFetched.AddListener(OnAuthorizationStatusFetched);
			listener.onAvailabilityChanged.AddListener(OnAvailabilityChange);
			listener.onErrorDuringRecording.AddListener(OnError);
			listener.onErrorOnStartRecording.AddListener(OnError);
			listener.onFinalResults.AddListener(OnFinalResult);
			listener.onPartialResults.AddListener(OnPartialResult);
			startRecordingButton.enabled = false;
			SpeechRecognizer.RequestAccess();
		} else {
			//resultText.text = "Sorry, but this device doesn't support speech recognition";
			startRecordingButton.enabled = false;
		}
	}

	public void OnFinalResult(string result) {
		
        //resultText.text = result;
        mparseMyJson.talkToMe(result);
	}

	public void OnPartialResult(string result) {
		//resultText.text = result;
	}

	public void OnAvailabilityChange(bool available) {
		startRecordingButton.enabled = available;
		if (!available) {
			//resultText.text = "Speech Recognition not available";
		} else {
			//resultText.text = "Say something :-)";
		}
	}

	public void OnAuthorizationStatusFetched(AuthorizationStatus status) {
		switch (status) {
		case AuthorizationStatus.Authorized:
			startRecordingButton.enabled = true;
			break;
		default:
			startRecordingButton.enabled = false;
			//resultText.text = "Cannot use Speech Recognition, authorization status is " + status;
			break;
		}
	}

	public void OnError(string error) {
		Debug.LogError(error);
        resultText.text = "Press\n to Ask your\n Question";
		//resultText.text = "Something went wrong... Try again! \n [" + error + "]";
		//startRecordingButton.GetComponentInChildren<Text>().text = "Start\n Recording";
	}

	public void OnStartRecordingPressed() {
		if (SpeechRecognizer.IsRecording()) {
			SpeechRecognizer.StopIfRecording();
            resultText.text = "Press\n to Ask your\n Question";
		} else {
			SpeechRecognizer.StartRecording(true);
            resultText.text = "";
		}
	}
}
