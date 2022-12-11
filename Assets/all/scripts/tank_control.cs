using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tank_control : MonoBehaviour
{
    public enum TankType
    {
        tank_one=1,
        tank_two=2,                    //ע��ö���Ƕ���
    }
    public TankType tank_type = TankType.tank_one;
    private string horizontalStr,verticalStr,fireStr;
    private float horizontal, vertical;
    public float move_speed,rotate_speed;
    public Rigidbody rigid;
    public GameObject shell;
    public Transform shell_pos;
    public float shell_speed;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        horizontalStr = "Horizontal";
        verticalStr = "Vertical";
        fireStr = "Fire";
        horizontalStr = horizontalStr + (int)tank_type;
        verticalStr = verticalStr + (int)tank_type;
        fireStr = fireStr + (int)tank_type;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis(horizontalStr);
        vertical = Input.GetAxis(verticalStr);
        rigid.MovePosition(transform.position + transform.forward * vertical * move_speed * Time.deltaTime);
        if (vertical<0)
        {
            horizontal = -horizontal;          //����ʱ���ת�����෴��
        }
        transform.Rotate(Vector3.up, horizontal*rotate_speed*Time.deltaTime);
       if(Input.GetButtonDown(fireStr))
        {
            openFire();
        }
    
    }

    void openFire()
    {
        GameObject shell_fire = Instantiate(shell, shell_pos.position, shell_pos.rotation);
        Rigidbody shell_fire_rigid = shell_fire.GetComponent<Rigidbody>();
        if(shell_fire_rigid!=null)
        {
            shell_fire_rigid.velocity = shell_pos.forward * shell_speed;
        }
    }
}