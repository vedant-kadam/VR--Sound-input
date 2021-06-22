using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public ParticleSystem DeathParticals;
    public GameObject Enemy, posi;
    float db;
    public GameObject Recoil;

    private void Update()
    {
       //  db = MicInput2.MicLoudnessinDecibels;
      //  db = FindObjectOfType<VolumeForInput>().totalDecibal;
    }
    public void Die()
    {
        db = FindObjectOfType<VolumeForInput>().totalDecibal;

        if (db >= 0.22f )
        {
            Recoil.GetComponent<Animation>().Play();

            Vector3 TEMPe;
            TEMPe = posi.transform.position;
            Instantiate(DeathParticals, TEMPe, Quaternion.identity);
            Destroy(Enemy);
        }
    }
    public void die1()
    {
        Vector3 TEMPe;
        TEMPe = posi.transform.position;
        Instantiate(DeathParticals, TEMPe, Quaternion.identity);
        Destroy(Enemy);
    }

}
