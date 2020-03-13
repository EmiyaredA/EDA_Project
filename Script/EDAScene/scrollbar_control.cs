using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrollbar_control : MonoBehaviour
{

    public GameObject UI_init;
    public GameObject content;
    
    private int netbutton_num;
    [SerializeField]
    private GameObject[] net_buttons;
    //private int chosen_index;
    //private bool[] net_choose;

    private RectTransform content_rect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 生成每个线网对应的按钮
    /// </summary>
    public void init_button(int netnum_value)
    {
        netbutton_num = netnum_value;
        net_buttons = new GameObject[netbutton_num];
        for(int i=0;i<netbutton_num;i++)
        {
            net_buttons[i] = Instantiate(UI_init, transform.position, transform.rotation,content.transform);
            net_buttons[i].GetComponentInChildren<Text>().text = "线网" + (i+1);//名字赋值
            Button temp_button = net_buttons[i].GetComponentInChildren<Button>();
            //temp_button.onClick.AddListener(调用布线算法方法完成相应引脚的布线)
            //temp_button.onClick.AddListener(click_func);
        }
        RectTransform content_rt=content.transform.GetComponent<RectTransform>();

        int extend_val = (netbutton_num - 16) / 4 + (netbutton_num - 16) % 4;
        if(extend_val<0)
        {
            extend_val = 0;
        }
        content_rt.sizeDelta = new Vector2(content_rt.sizeDelta.x,120+extend_val*30);
    }

    
}
