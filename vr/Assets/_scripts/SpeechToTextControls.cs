using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(VoiceController))]
public class SpeechToTextControls : MonoBehaviour
{
    string dataLoging;
    VoiceController VoCo;
    public GameObject MicInputController;
    public Text OpText;
    private void Start()
    {
        VoCo = GetComponent<VoiceController>();
    }
    public void GetTHeWords()
    {
        VoCo.GetSpeech();
    }
    void OnEnable()
    {
        VoiceController.resultRecieved +=OnGettingResult;
    }

    void OnDisable()
    {
        VoiceController.resultRecieved -= OnGettingResult;
    }
  public  void OnGettingResult(string text)
    {
        dataLoging = text;
        Debug.Log(dataLoging);
        MicInputController.GetComponent<Raycast_shoot>().GetCommand = dataLoging;
        OpText.text = dataLoging;
    }
   
}
