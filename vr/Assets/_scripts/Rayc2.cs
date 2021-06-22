using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rayc2 : MonoBehaviour
{
    public GameObject Bullet,bulletSpawn;
    RaycastHit Fi;
    public Camera Cam23;
    AudioSource ThisOne;
    float force = 100000f;
    

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(bulletSpawn.transform.position,bulletSpawn.transform.forward, Color.black);
        if (Physics.Raycast(Cam23.transform.position, Cam23.transform.forward, out Fi))
            Debug.DrawRay(Cam23.transform.position,Fi.transform.forward,Color.black);
        {
            if (Fi.transform.tag == "Destroy")
            {
                Instantiate(Bullet, bulletSpawn.transform.position, Quaternion.identity);
                
                Rigidbody rb = Bullet.GetComponent<Rigidbody>();
             rb.AddForce(bulletSpawn.transform.forward * force*Time.deltaTime,ForceMode.Impulse);
                ThisOne.Play();
                Destroy(Bullet, 1f);
            }
        }
        
    }
}
