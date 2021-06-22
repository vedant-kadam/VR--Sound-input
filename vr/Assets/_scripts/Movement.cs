
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float Speed;
    public Camera MainCam;
     
    private void Update()
    {
        transform.Translate(MainCam.transform.forward * Speed * Time.deltaTime);
    }
}
