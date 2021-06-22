
using UnityEngine;
using System;

using UnityEngine.Android;
using UnityEngine.UI;

public class Raycast_shoot : MonoBehaviour
{
    float vol;
    public ParticleSystem MusseelFlash,deathPar;
    public Camera Cam;
    public GameObject Mussels1;
    //public Animation Recoil1;
    const string LANG_CODE = "en-US";
   public string GetCommand;
  //  public Text text1;
    //public Animation Reciol2;
    private void Start()
    {
       
    }
    
    private void Update()
    {
        vol = FindObjectOfType<VolumeForInput>().totalDecibal;
      
         if ( Input.GetButton("Fire1") || GetCommand == "shoot"||GetCommand=="should")
            {

           // SpeechToText.instance.StartRecording();
            Shoot();
            //SpeechToText.instance.StartRecording();

        }
        else
        {
           // SpeechToText.instance.StopRecording();
        }


       
    }
    void doshoootingWithvolume()
    {
         vol = FindObjectOfType<VolumeForInput>().totalDecibal;
        if (vol >= 0.22f|| Input.GetButton("Fire1")||GetCommand=="shoot")
        {
             //SpeechToText.instance.StartRecording();
            Shoot();
              //SpeechToText.instance.StartRecording();
        }
         else
        {
            // SpeechToText.instance.StopRecording();
        }
    }

    public  void Shoot()
    {
       
        RaycastHit Pos;
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out Pos)) 
        {
         //   Instantiate(MusseelFlash, Mussels1.transform.position, Quaternion.identity);
            Debug.Log(Pos.transform.name);
            if(Pos.transform.tag=="Destroy")
            {
                //Recoil1.Play();
                // Recoil2.Play();
             //  Reciol2.Play();
                 Instantiate(deathPar, Pos.transform.position, Quaternion.identity);
                Destroy(Pos.transform.gameObject);
            }
            
        }
        
    }
}
