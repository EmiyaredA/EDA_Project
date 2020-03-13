using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSet : MonoBehaviour//修改
{
    //更新
    public GameObject center_obj;
    public GameObject MainScence;
    public GameObject CameraPos1;
    public Camera MainCamera;
    public Camera SetCamera;
    public GameObject CameraPos2;
    private Vector3 center=new Vector3(0,0,0);
    public void SeletModeCamera()
    {
        //MainCamera.enabled = true;
        SetCamera.enabled = false;
    }
    public void ChangeButtonCamera()
    {
        //MainCamera.enabled = false;
        SetCamera.enabled = true;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            foreach (var item in PanelControl.vector3s)
            {
                center += item;
            }
            center /= PanelControl.vector3s.Count;
            //center.z -= 5;
            Debug.Log(center);
            center_obj.transform.position = center;
        }
        if (Input.GetMouseButton(2))
        {
            MainScence.transform.RotateAround(center, MainCamera.transform.up * 360, Input.GetAxis("Mouse X"));
            MainScence.transform.RotateAround(center, -MainCamera.transform.right * 360, Input.GetAxis("Mouse Y"));
        }
        if (Input.GetMouseButtonUp(2))
        {
            center = Vector3.zero;
        }
    }
}
