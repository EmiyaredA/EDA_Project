using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init_network : MonoBehaviour//线网生成脚本
{
    // Start is called before the first frame update
    private float edge = 1.0f;//每个布线区域的边沿距离
    private float track_width = 0.1f;//轨道宽度
    private float track_dis;//轨道间距离
    private float side_len;//布线区域边长
    private float area_thick = 0.05f;//区域面积厚度
    private float area_dis = 3.0f;//上下区域间的距离

    public GameObject Nets_all;
    public GameObject net_cube;//线网cube
    public GameObject area_cube;//轨道cube
    public float speed;//物体绕轴旋转速度
    public GameObject center_point;//线网旋转的中心点
    private GameObject cen_point=null;//生成的中心点

    //public int track_num;//轨道数
    //public int net_num;//线网数
    public float side_length;//区域面积边长

    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void set_nets(int track_num,int net_num)//根据参数生成线网
    {
        Debug.Log("set_nets调用成功");
        Init_net(net_num, track_num, side_length);
        cen_point = Instantiate(center_point, new Vector3(side_length / 2.0f, (net_num - 1) * area_dis / 2.0f, side_length / 2.0f), transform.rotation);
    }
    //netnum表示线网数量,tracknum表示轨道数量,sidelen表示区域面积边长

    public void Init_net(int netnum, int tracknum, float side_len)
    {
        GameObject net_cube_tmp;
        GameObject area_cube_tmp;
        MeshRenderer mr;

        track_dis = (side_len - 2 * edge - tracknum * track_width) / (tracknum - 1);
        float track_len = side_len - 2 * edge;//单个轨道长度

        for(int i = 0;i < netnum;i++)
        {
            net_cube_tmp=Instantiate(net_cube, new Vector3(side_len / 2.0f, i * area_dis, side_len / 2.0f), transform.rotation);
            net_cube_tmp.transform.localScale=new Vector3(side_len, area_thick, side_len);//设置区域面积模块大小
            mr = net_cube_tmp.GetComponent<MeshRenderer>();
            mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            net_cube_tmp.transform.SetParent(Nets_all.transform);

            for (int j = 0;j < tracknum;j++)
            {
                if(i%2==0)
                {
                    area_cube_tmp = Instantiate(area_cube, new Vector3(side_len / 2.0f, i * area_dis, edge + j * track_dis),transform.rotation);
                    area_cube_tmp.transform.localScale=new Vector3(track_len, area_thick + 0.02f, track_width);
                    mr = area_cube_tmp.GetComponent<MeshRenderer>();
                    mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                    area_cube_tmp.transform.SetParent(Nets_all.transform);
                }
                else
                { 
                    area_cube_tmp = Instantiate(area_cube, new Vector3(edge + j * track_dis, i * area_dis, side_len / 2.0f), transform.rotation);
                    area_cube_tmp.transform.localScale=new Vector3(track_width, area_thick + 0.02f, track_len);
                    mr = area_cube_tmp.GetComponent<MeshRenderer>();
                    area_cube_tmp.transform.SetParent(Nets_all.transform);
                }
            }
        }

    }

    void OnGUI()//实现model旋转
    {
        Event Key = Event.current;
        Debug.Log("事件调用成功");

        if(cen_point!=null)
        {
            switch (Key.keyCode)
            {

                case KeyCode.LeftArrow:
                    Nets_all.transform.RotateAround(cen_point.transform.position, Vector3.up, Time.deltaTime * speed);
                    break;

                case KeyCode.RightArrow:
                    Nets_all.transform.RotateAround(cen_point.transform.position, Vector3.up, Time.deltaTime * -speed);
                    break;

                case KeyCode.UpArrow:
                    Nets_all.transform.RotateAround(cen_point.transform.position, Vector3.left, Time.deltaTime * speed);
                    break;

                case KeyCode.DownArrow:
                    Nets_all.transform.RotateAround(cen_point.transform.position, Vector3.left, Time.deltaTime * -speed);
                    break;
            }
        }
        
    }
}
