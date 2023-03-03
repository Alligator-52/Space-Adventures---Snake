using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Animator camAnim;

    public void ShakeCamera()
    {
        camAnim.SetTrigger("shake");
    }

    public void ShakeCameraOnFood()
    {
        camAnim.SetTrigger("shakeFood");
    }

    public void ShakeCameraOnBFood()
    {
        camAnim.SetTrigger("shakeBFood");
    }
    
}
