using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class init_testmodel : MonoBehaviour
{
    public int testsamples;//当前展示的题数
    public float distance;//不同model之间的距离
                          // Start is called before the first frame update
    public GameObject[] objects;
    public Transform object_parent;
    private Vector3 centerPos;
    private float delta_angle;

    public Transform centerpoint;

    public init_circle init_Circle;//获取生成圈的脚本

    void Start()
    {
        init_Circle = GetComponent<init_circle>();
        centerPos = centerpoint.position;
        object_parent.position = centerPos;
        Debug.Log("test"+centerPos);
        init_models();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init_models()
    {
        GameObject tempobject=GameObject.CreatePrimitive(PrimitiveType.Cube);
        //float view_dis = 2.0f;

        objects = new GameObject[testsamples];
        for(int i=0;i<testsamples;i++)
        {
            //objects[i] = Instantiate(tempobject, new Vector3(distance * i, 0, view_dis), transform.rotation);
            objects[i] = Instantiate(tempobject, new Vector3(0, 0, 0), transform.rotation);
            objects[i].transform.SetParent(object_parent);
        }
        float delta_angle = 360.0f / (testsamples * 1.0f);
        init_Circle.CreateCubeAngle30(centerPos,delta_angle);
        Destroy(tempobject);
    }
}
