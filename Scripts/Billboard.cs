using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField]
    private Transform AimCam = null;
    private void Start()
    {
        AimCam = Camera.main.transform;
    }
    void LateUpdate()
    {
        if (AimCam != null)
        {
            transform.LookAt(transform.position + AimCam.forward);
        }
    }
    public void SetCamera(GameObject camera)
    {
        AimCam = camera.transform;
    }
}
