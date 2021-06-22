using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(VoiceController))]
public class VoiceTest : MonoBehaviour {

    public Text uiText;
    string DataLog;
    VoiceController voiceController;

    public void GetSpeech() {
        voiceController.GetSpeech();
    }

    void Start() {
        voiceController = GetComponent<VoiceController>();
    }

    void OnEnable() {
        VoiceController.resultRecieved += OnVoiceResult;
    }

    void OnDisable() {
        VoiceController.resultRecieved -= OnVoiceResult;
    }

    void OnVoiceResult(string text) {
        uiText.text = text;
        DataLog = text;
        Debug.Log(DataLog);
    }
}
