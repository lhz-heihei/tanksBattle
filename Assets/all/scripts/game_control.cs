using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_control : MonoBehaviour
{
    public Transform tankPos1,tankPos2;
    public GameObject tankPrefab;
    public Color tankOneColor, tankTwoColor;
    public camera_control cameraControl;
    
    // Start is called before the first frame update
   

    void Start()
    {
        TankInstantiate();
        cameraControl.tanks=GameObject.FindGameObjectsWithTag("Player");
        audio_manager.audioManager.bgm_play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TankInstantiate()
    {
        GameObject tank1 = Instantiate(tankPrefab, tankPos1.position, tankPos1.rotation);
        GameObject tank2 = Instantiate(tankPrefab, tankPos2.position, tankPos2.rotation);
        tank1.GetComponent<tank_control>().tank_type = tank_control.TankType.tank_one;
        tank2.GetComponent<tank_control>().tank_type = tank_control.TankType.tank_two;
        MeshRenderer[] renders1 = tank1.GetComponentsInChildren<MeshRenderer>();
        MeshRenderer[] renders2 = tank2.GetComponentsInChildren<MeshRenderer>();
        for(int i=0;i<renders1.Length;i++)
        {
            renders1[i].material.color = tankOneColor;
        }
        for(int i=0;i<renders2.Length;i++)
        {
            renders2[i].material.color = tankTwoColor;
        }
        
    }
}
