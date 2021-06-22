
using UnityEngine;


public class VolumeForInput : MonoBehaviour
{
    
    bool starts=true;
    private string Device;
    public AudioClip Curr;
    public float totalVolume,totalDecibal;
    float tm=0,tc=0;
    public float Maxvalue=1f;
//#if ANDROID

//#endif
    private void Awake()
    {
        startRecodin();
        
        
    }
    void startRecodin()
    {
        if(Device == null)
        {
            Device = Microphone.devices[0];
        }
        Curr = Microphone.Start(Device, true,999, 44100);

    }
    void stopRecording()
    {
        Microphone.End(Device);
    }
    
    float calculateVolume()
    {
        float val=0f;
        int number = 128;
        float[] audioWave = new float[number];//it is the array which will store the clip data from -10th position from the current 
                                              //clip position 
                                              //and then will help to calculate the highest volume
        int temp1 = Microphone.GetPosition(Device) - (number+1);//here we will get the Postion from where we have to check the volume
        //MicroPhone.GetPosition(name of the microphone) will give you the current or the latest postion of the sample or the recodin                                                                                                     
        //and then we will substract 11 so that we can go 11 samples behind and then chheck the volume from that number+1 to the current   sample 

        Curr.GetData(audioWave, temp1);
        //the AudioClip.GetData(array where we will store the no of samples ,from where we have to start)
        //its takes 2 argument i.e the size of array and the number from where we have start to get the data and so the size of
        //array should be equal to the differnce 
        //and as we are going 11 samples behind but we are starting to calculate for the next 10 samples
        //****************************what does AudioClip.GetData Return you??*********************************
        // audio clip.GetData return you the amplitude or to say it in another way
        //it gives you PCM audio, like what is stored on an audio CD. It's samples of amplitude over time.*****************

        for (int i = 0; i <number; i++)
        {
            float temp12 = audioWave[i] * audioWave[i];
            //we multiply here because Maxvalue euals to the highest Normalize value of Power 2,so that we get  a samll no less than 1
            if (temp12>val)
            {
                val = temp12;
            }
        }
     //   Debug.Log(val);
        
        return val;

    }
    private void Start()
    {
        startRecodin();
        starts = true;
        
    }
    private void Update()
    {
        totalVolume = calculateVolume();
      //  print("The volume is  " + totalVolume);
     //   totalDecibal= 20 * Mathf.Log10(Mathf.Abs(totalVolume));
       totalDecibal= Mathf.Sqrt(Mathf.Sqrt(totalVolume));//we just doo the sq root to get a large volume..i cant say its in decimal 
        //but what gets the job done is good enough for me
       print("The volume is  " + totalDecibal);
        if(totalDecibal>Maxvalue)
        {
            //FindObjectOfType<SpeechToTextControls>().GetTHeWords();
            FindObjectOfType<Raycast_shoot>().Shoot();
            totalDecibal/=2f;
           
           
        }
        
     //   db = -1 * FindObjectOfType<VolumeForInput>().totalDecibal;
     if(Time.time-tm>11)
        {
            tm = Time.time;
            stopRecording();
            if(totalDecibal<Maxvalue)
            {
                Destroy(Curr);
            }
           
            startRecodin();
        }
    }

    




}
