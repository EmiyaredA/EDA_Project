using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class init_circle : MonoBehaviour
{
    //public int sample_num;
    //private float radius;
    private GameObject[] objects;//生成的各类例子

    //public float delta_angle = 20.0f;//物体间相差的角度

    //[HideInInspector]
    //public Vector3 centerPos;    //你围绕那个点 就用谁的角度

    private float radius = 5;     //物理离 centerPos的距离

    private float angle = 0;      //偏移角度  



    void Start()

    {
        
        //CreateMosquitoCoil();

    }

    void Update()
    {
        
    }



    public void CreateCubeAngle30(Vector3 centerPos,float delta_angle)

    {
        objects = GetComponent<init_testmodel>().objects;
        Debug.Log(objects.Length);
        Debug.Log("init" + centerPos);

        //20度生成一个圆
        int i = 0;
        for (angle = 0; i < objects.Length; angle += delta_angle,i++)

        { 


            //先解决你物体的位置的问题

            // x = 原点x + 半径 * 邻边除以斜边的比例,   邻边除以斜边的比例 = cos(弧度) , 弧度 = 角度 *3.14f / 180f;   

            float x = centerPos.x + radius * Mathf.Cos(angle * 3.14f / 180f);

            float y = centerPos.y + radius * Mathf.Sin(angle * 3.14f / 180f);

            

            // 生成一个圆
            objects[i].transform.position = new Vector3(x, y, centerPos.z);

            //GameObject obj1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            //设置物体的位置Vector3三个参数分别代表x,y,z的坐标数  

            //obj1.transform.position = new Vector3(x, centerPos.z, y);

        }

    }



    // 生成螺旋   //原理 += 半径

    public void CreateMosquitoCoil(Vector3 centerPos)

    {

        centerPos = transform.position;

        // 每隔30度就生成一个小方块  

        for (int i = 0; i < 120; angle += 18, radius += 0.2f, i++)

        {

            // 根据原点,角度,半径获取物体的位置.  x = 原点x + 半径 * 邻边除以斜边的比例  

            float x = centerPos.x + radius * Mathf.Cos(angle * 3.14f / 180f);

            float y = centerPos.y + radius * Mathf.Sin(angle * 3.14f / 180f);

            //我们将obj1初始化为一个Cube立方体，当然我们也可以初始化为其他的形状  

            GameObject obj1 = GameObject.CreatePrimitive(PrimitiveType.Cube);

            //设置物体的位置Vector3三个参数分别代表x,y,z的坐标数  

            obj1.transform.position = new Vector3(x, centerPos.z, y);

        }

    }

    // Update is called once per frame
}
