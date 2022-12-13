using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class tank_control : MonoBehaviour
{
    public enum TankType
    {
        tank_one=1,
        tank_two=2,                    //注意枚举是逗号
    }
    public TankType tank_type = TankType.tank_one;
    private string horizontalStr,verticalStr,fireStr;
    private float horizontal, vertical;
    public float move_speed,rotate_speed;
    public Rigidbody rigid;
    public GameObject shell;
    public Transform shell_pos;
    public float shell_speed_min, shell_speed_max;
    private float shell_speed_current;
    public float shell_speed_add;
    bool isFire;
    public float PH;
    public UnityEngine.UI.Slider phSlider;
    public ParticleSystem tank_explosion;
    // Start is called before the first frame update
    void Start()
    {
        TankInitialization();
    }

    public void TankInitialization()
    {
        rigid = GetComponent<Rigidbody>();
        horizontalStr = "Horizontal";
        verticalStr = "Vertical";
        fireStr = "Fire";
        horizontalStr = horizontalStr + (int)tank_type;
        verticalStr = verticalStr + (int)tank_type;
        fireStr = fireStr + (int)tank_type;
        phSlider.maxValue = PH;
        phSlider.value = PH;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis(horizontalStr);
        vertical = Input.GetAxis(verticalStr);
        rigid.MovePosition(transform.position + transform.forward * vertical * move_speed * Time.deltaTime);
        if (vertical<0)
        {
            horizontal = -horizontal;          //倒车时候的转向是相反的
        }
        transform.Rotate(Vector3.up, horizontal*rotate_speed*Time.deltaTime);
       if(Input.GetButtonDown(fireStr))
        {
            isFire = true;
            shell_speed_current = shell_speed_min;
        }
       if(Input.GetButton(fireStr))
        {
            shell_speed_current += shell_speed_add * Time.deltaTime;
            if(shell_speed_current>=shell_speed_max)
            {
                shell_speed_current = shell_speed_max;
                if(isFire)
                {
                    openFire(shell_speed_current);
                    isFire = false;
                }
            }
        }
        if(Input.GetButtonUp(fireStr))
        {
            if(isFire)
            {
                openFire(shell_speed_current);
                isFire = false;
            }
        }
    
    }

    void openFire(float shell_speed)
    {
        GameObject shell_fire = Instantiate(shell, shell_pos.position, shell_pos.rotation);
        Rigidbody shell_fire_rigid = shell_fire.GetComponent<Rigidbody>();
        if(shell_fire_rigid!=null)
        {
            shell_fire_rigid.velocity = shell_pos.forward * shell_speed;
        }
    }

    public void ShellDamage(float damage)
    {
        PH -= damage;
        phSlider.value = PH;
        if(PH<=0)
        {
            tank_explosion.gameObject.transform.parent = null;
            tank_explosion.Play();
            Destroy(tank_explosion, tank_explosion.main.duration);
            Destroy(this.gameObject);
        }
    }
}
