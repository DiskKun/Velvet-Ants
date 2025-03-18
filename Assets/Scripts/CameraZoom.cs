using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;
    public GameObject player;

    private bool zoomedIn;

    private bool lerp = false;
    public float lerpTime;
    public AnimationCurve curve;
    private float timer = 0;

    private void Update()
    {

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

    public void ZoomIn(Transform target)
    {
        VirtualCamera.Follow = target;
        lerp = true;
        zoomedIn = true;

        //make player destination a little to the (right/left) of zoom target
        player.GetComponent<PlayerMovement>().DialoguePositioning(target);

    }

    public void ZoomOut()
    {
        if (VirtualCamera.Follow != player.transform)
        {
            VirtualCamera.Follow = player.transform;
            lerp = true;
            zoomedIn = false;
        }

    }
    
}
