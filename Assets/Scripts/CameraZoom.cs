using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;
    public Transform player;
    public Transform zoomTarget;

    private bool zoomedIn;

    private bool lerp = false;
    public float lerpTime;
    public AnimationCurve curve;
    private float timer = 0;

    private void Update()
    {
        //if (Input.GetKeyDown("space"))
        //{
        //    if (!zoomedIn)
        //    {
        //        ZoomIn();    
        //    } else
        //    {
        //        ZoomOut();
        //    }
        //}

        if (lerp)
        {
            if (timer < lerpTime)
            {
                timer += Time.deltaTime;
                float t = timer / lerpTime;
                float interpolation = curve.Evaluate(t);

                if (zoomedIn)
                {
                    VirtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(10, 3, interpolation);
                } else
                {
                    VirtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(3, 10, interpolation);
                }

            } else
            {
                timer = 0;
                lerp = false;
            }
            
        }

    }

    public void ZoomIn()
    {
        VirtualCamera.Follow = zoomTarget;
        lerp = true;
        zoomedIn = true;
        
    }

    public void ZoomOut()
    {
        VirtualCamera.Follow = player;
        lerp = true;
        zoomedIn = false;
    }
    
}
