using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIvalue_control : MonoBehaviour
{
    private UI_control uI_Control;

    public Text maxtracknum_string;
    public int maxtracknum_value { get; set; }

    public Text maxnetnum_string;
    public int maxnetnum_value { get; set; }

    public Text maxpinnum_string;
    public int maxpinnum_value { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        uI_Control = GetComponent<UI_control>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #region 设置推荐数值的方法
    /// <summary>
    /// /在布线区域面积发生改变时，传递一个新的最大轨道数推荐值
    /// </summary>
    public void Set_maxtracknum()
    {
        //调用后台程序生成推荐值maxtracknum_value
        maxtracknum_string.text = "推荐最大轨道数量" + maxtracknum_value;
    }

    /// <summary>
    /// /在环境参数设置给定后，设定好最大引脚数量
    /// </summary>
    public void Set_maxpinnum()
    {
        //调用后台程序生成推荐值maxpinnum_value
        maxpinnum_string.text = "推荐最大引脚数量" + maxpinnum_value;
    }

    /// <summary>
    /// /在引脚数量设定好时，设定好最大线网数量（默认以最大引脚数量为参照值设定）
    /// </summary>
    public void Set_maxnetnum()
    {
        //调用后台程序生成推荐值maxnetnum_value
        try
        {
            maxnetnum_value = int.Parse(uI_Control.pins_num.text)/2;
            maxnetnum_string.text = "推荐最大线网数量：" + maxnetnum_value;
        }
        catch
        {
            maxnetnum_string.text = "推荐最大线网数量暂无" ;
        }
    }
    #endregion


}
