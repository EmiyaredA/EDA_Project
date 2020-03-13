using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse_control : MonoBehaviour
{
    private Vector3 mouse_move_velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //获取鼠标移动
        mouse_move_velocity.y = -Input.GetAxis("Mouse X");
        mouse_move_velocity.x = Input.GetAxis("Mouse Y");
        //rote camera 
        Vector3 target_angle = transform.rotation.eulerAngles + mouse_move_velocity;
        //限定角度
        //调整周期，限定角度
        if (target_angle.x > 180) target_angle.x -= 360;
        if (target_angle.y > 180) target_angle.y -= 360;
        //target_angle = new Vector3(Mathf.Clamp(target_angle.x, X_min, X_max), Mathf.Clamp(target_angle.y, Y_min, Y_max), 0);
        //转化四元角度
        transform.rotation = Quaternion.Euler(target_angle);
    }
}
