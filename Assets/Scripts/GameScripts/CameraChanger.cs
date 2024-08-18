using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    public float max =100;
    public float min = 30;
    private void Update()
    {
        float mw = Input.GetAxis("Mouse ScrollWheel");
        if (mw < 0.1)
        {
              if(GetComponent<Camera>().fieldOfView <= max)
                GetComponent<Camera>().fieldOfView += 1;


        }
        if (mw > -0.1)
        {
            if (GetComponent<Camera>().fieldOfView >= min)
                GetComponent<Camera>().fieldOfView -= 1;
        }
    }
}
