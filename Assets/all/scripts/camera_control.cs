using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class camera_control : MonoBehaviour
{
    public GameObject[] tanks;  //坦克的赋值在game_control里面，因为此脚本的start运行时坦克还未生成
    private Vector3 targetCameraPos = Vector3.zero;
  
    public Camera mainCamera;
    private Vector3 currentVelocity = Vector3.zero;
    public float smoothTime;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;       //主相机
    }

    // Update is called once per frame
    void Update()
    {
        CameraPosReset();
        CameraSizeReset();
    }

    void CameraPosReset()
    {
        Vector3 sumPos = Vector3.zero;
        if (tanks.Length > 0)
        {
            for (int i = 0; i < tanks.Length; i++)
            {
                sumPos += tanks[i].transform.position;
            }
            targetCameraPos = sumPos / tanks.Length;
            targetCameraPos.y = mainCamera.transform.position.y;
            mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, targetCameraPos, ref currentVelocity, smoothTime);
        }
    }

    void CameraSizeReset()
    {
        float size = 0;
        foreach(var tank in tanks)
        {
            Vector3 PosDifference = targetCameraPos - tank.transform.position;
            size = Mathf.Max(size, Mathf.Abs(PosDifference.z)); //相机的Size是屏幕高度的一半
            size = Mathf.Max(size, Mathf.Abs(PosDifference.x) / mainCamera.aspect);//mainCamera.aspect : width/height
        }
        size += 4;
        mainCamera.orthographicSize = size;
    }
}
