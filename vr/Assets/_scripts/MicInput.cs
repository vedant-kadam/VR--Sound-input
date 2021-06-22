using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicInput : MonoBehaviour
{
    //  public MicVolume instance { get; set; }
    private string NaMeOfTheDevice;//use to store the nam of the device
    public float MicInputVolume;//final output
    public float MicInputVolumeInDecible;//final output in decible
    bool _isMicrophoneActive;//bool to just start or stop recoding
    AudioClip RecordedAuioCip;//the cip which will be recorded
    int samplesRequired = 128;//it is the amout of sample which we will check evry time to know the highest volume,you will understand further what i mean here




    // First we have to get input from the microphone and then analyze it to getthe volume
    //so this first method is use to get the input from the user,Here we are only getting the input from the user i.e through the device that is connected

    void GetnputFromMic()
    {
        //if you are only using the default microphone you can directly start as given below

        // **************NaMeOfTheDevice = Microphone.devices[0];*************************

        //or you can also write it as like this i guesss this wii be fine
        if (NaMeOfTheDevice == null)
        {
            NaMeOfTheDevice = Microphone.devices[0];
        }
        //but if you have Many device then use a for each loop and make it run till the put fromm every microphone is taken 

        //foreach( string device in Microphone.devices)
        //{
        //     RecordedAuioCip = Microphone.Start(NaMeOfTheDevice, true, 999, 44100);

        // }

        ///now you have to start Recoding the clip so
        RecordedAuioCip = Microphone.Start(NaMeOfTheDevice, true, 999, 44100);
        //We use Microphne.Start in which it takes 4 argument 
        // #1 Name of the Microphne from which it is recoded
        //,#2 weather er want to contine recording even after the Length of the recoding is complete i.e the time or the length of recoding
        // #3 Length of recoding
        // #4 Frequency which is standard 

        _isMicrophoneActive = true;

    }

    //We also need a Method to stop the recording of the microphone so
    void StopRecording()
    {
        Microphone.End(NaMeOfTheDevice);
        _isMicrophoneActive = false;
    }

    private void Start()
    {
        _isMicrophoneActive = true;
    }
    void Update()
    {
        MicInputVolume = GetTheVolume();
        MicInputVolumeInDecible = GetTheVolumeInDecible();
    }



    float GetTheVolume()
    {//in this method we will calculate the volume
        float MaxValue = 0;//just a normal storing varialble which will have the end valuee stored in it

        float[] CipData = new float[samplesRequired];//it is the array which will store the clip data from -128th position from the current clip position 
                                                     //and then will help to calculate the highest volume

        int Position_Of_the_Clip_To_StartFrom = Microphone.GetPosition(NaMeOfTheDevice) - (samplesRequired + 1);
        //here we will get the Postion from where we have to check the volume
        //MicroPhone.GetPosition(name of the microphone) will give you the current or the latest postion of the sample or the recodin                                                                                                     
        //and then we will substract 129 so that we can go 129 samples behind and then chheck the volume from that number to the current   sample 

        if (Position_Of_the_Clip_To_StartFrom < 0)
            return 0;//if there is no clip then it will return 0;

        //Now we have to analyze the audio clip i.e we have to get the data from the clip that we are recoding 
        RecordedAuioCip.GetData(CipData, Position_Of_the_Clip_To_StartFrom);
        //the AudioClip.GetData(array where we will store the no of samples ,from where we have to start)
        //its takes 2 argument i.e the size of array and the number from where we have start to get the data and so the size of array should be equal to the differnce 
        //and as we are going 129 samples behind but we are starting to calculate for the next 128 samples
        //****************************what does AudioClip.GetData Return you??*********************************
        // audio clip.GetData return you the amplitude or to say it in another way
        //t gives you PCM audio, like what is stored on an audio CD. It's samples of amplitude over time.*****************



        for (int i = 0; i < samplesRequired; i++) //here i am just comparing and getting the max value in the current clip
        {
            float wavePeak = CipData[i] * CipData[i];
            if (MaxValue < wavePeak) //we multiply here because Maxvalue euals to the highest Normalize value of Power 2,so that we get  a samll no less than 1
            {
                MaxValue = wavePeak;
            }
        }
        print("The Volume is" + MaxValue);
        return MaxValue;
    }
    float GetTheVolumeInDecible()
    {
        float DecibleInput;
        DecibleInput = 20 * Mathf.Log10(Mathf.Abs(MicInputVolume));//just converting it in to decible;
        print("the Input in decible is" + DecibleInput);
        return DecibleInput;
    }
    private void OnEnable()    // Implement OnDisable and OnEnable script functions.
    {                          // These functions will be called when the attached GameObject
        GetnputFromMic();
        _isMicrophoneActive = true;    // is toggled.
    }                                // This example also supports the Editor.  The Update function
                                     // will be called, for example, when the position of the
                                     // GameObject is change}                                    

    private void OnDisable()
    {
        StopRecording();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            if (!_isMicrophoneActive)
            {
                _isMicrophoneActive = true;
                GetnputFromMic();

            }
        }
        if (!focus)
        {
            StopRecording();
            _isMicrophoneActive = false;
        }
    }
}
