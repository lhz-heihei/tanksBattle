using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class camera_control : MonoBehaviour
{
    public GameObject[] tanks;  //̹�˵ĸ�ֵ��game_control���棬��Ϊ�˽ű���start����ʱ̹�˻�δ����
    private Vector3 targetCameraPos = Vector3.zero;
  
    public Camera mainCamera;
    private Vector3 currentVelocity = Vector3.zero;
    public float smoothTime;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;       //�����
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
            size = Mathf.Max(size, Mathf.Abs(PosDifference.z)); //�����Size����Ļ�߶ȵ�һ��
            size = Mathf.Max(size, Mathf.Abs(PosDifference.x) / mainCamera.aspect);//mainCamera.aspect : width/height
        }
        size += 4;
        mainCamera.orthographicSize = size;
    }
}
