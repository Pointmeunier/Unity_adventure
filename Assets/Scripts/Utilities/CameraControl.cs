using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraControl : MonoBehaviour
{
    private CinemachineConfiner2D confiner2D;

    private void Start()
    {
        GetNewCameraBounds();
    }

    private void Awake()
    {
        confiner2D = GetComponent<CinemachineConfiner2D>();
    }

    private void GetNewCameraBounds()
    {
        var obj = GameObject.FindGameObjectWithTag("Bounds");
        var player = GameObject.FindGameObjectWithTag("Player");
        if (obj == null || player == null) {
            return;
        }
        confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();

        var virtualCamera = GetComponent<CinemachineVirtualCamera>();
        if (virtualCamera != null)
        {
            virtualCamera.LookAt = player.transform;
            virtualCamera.Follow = player.transform;
        }

        confiner2D.InvalidateCache();




    }
}
