using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetScene_Menu : MonoBehaviour//UI界面管理脚本
{
    public Dropdown net;
    public Dropdown track;
    // Start is called before the first frame update
    //public GameObject setbutton;
    //public GameObject cancel_button;
    public GameObject index_panel;
    private Init_network init_Network;

    private int net_value;//线网数
    private int track_value;//轨道数

    void Start()
    {
        init_Network = GetComponent<Init_network>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetButton_active()//参数设置面板出现
    {
        index_panel.SetActive(true);
    }

    public void SetButton_disabled()//参数设置面板消失
    {
        index_panel.SetActive(false);     
    }

    public void Setvalue()//设置线网生成所必要的参数
    {
        net_value = net.value+1;
        track_value = track.value+1;
        init_Network.set_nets(track_value, net_value);
    }
}
