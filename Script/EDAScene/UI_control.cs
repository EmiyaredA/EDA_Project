using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_control : MonoBehaviour
{
    #region UI动画相关参数
    //警告模块
    private int warning_value;
    public Animator warning_anim;

    //进度条模块
    private int progress_value;
    public Animator progress_anim;

    //学习模块界面
    

    //测试模块界面
    private int envi_value;
    private int test1_value;
    private int test_1_value;
    public Animator environment_anim;
    public Animator test_panel1_anim;
    public Animator test_panel_1_anim;

    //布线模块界面
    public int netnum;//生成线网数量
    public Dropdown netnum_show;

    public Animator tools_anim;
    private int tools_value;
    public Animator index_anim;
    private int index_exist;
    public RectTransform index;
    public Animator netinit_anim;
    private int netinit_enter;

    //主菜单界面
    public Animator main1_anim;
    public Animator main2_anim;
    private int main1_out;
    private int main2_exist;
    private int scene_index;

    //问题界面
    public Animator problem_anim;
    private int problem_para;

    //相机
    public Animator camera_anim;
    //播放动画的HASH;
    private int anim_speed;//暂时废弃
    #endregion

    #region UI参数界面传递


    //------------------------相关按钮--------------------------
    public Button envi_set_btn;
    public Button cir_set_btn;
    public Button netinit_btn;

    //----------------------环境参数传递------------------------
    public InputField net_floors;
    public Dropdown net_areas;
    public InputField max_tracksum;
    public int net_floors_num { get; set; }
    public int maxtrack_num { get; set; }
    public GameObject cameraset;
    private bool cameraset_isok = false;//布线区域相机是否已经激活
    public GameObject maincamera;

    //-----------------------问题生成---------------------------
    public Text order_num_text;//题目序号
    public Toggle[] ques_toggles = new Toggle[5];//存放问题的选项
    private bool[,] answers_index;//用于存储填入的结果 
    public bool[] answer_is_have;//该答案是否已经有保存的数据
    private int answers_length=10;//题目数量
    public int curr_index;//当前的题目
    public Button end_btn;//结束测试的button
    public GameObject[] ques_objects;//存放生成的线网模型


    /// <summary>
    /// 生成线网的规格大小scale
    /// </summary>
    public float net_scale { get; set; }
    //-----------------------电路相关参数-----------------------
    public InputField pins_num;
    public InputField net_num;

    //-----------------------布线参数设置-----------------------    
    //线网生成的位置获取参数
    public Transform base_up_pos;
    public Transform base_down_pos;
    #endregion

    
    #region 脚本交互相关
    private rate_progress rate_pro;
    private UIvalue_control uivalue_Control;
    private PanelControl panelControl;
    private scrollbar_control scrollbar_Control;
    #endregion
    [SerializeField]
    private int currscene_index;//当前场景索引
    //0为主菜单，1为测试模块，2为布线模块

    private void Awake()
    {
        cameraset.SetActive(false);
        curr_index = -1;//未进入答题模式时数值为-1
        net_scale = 0.1f;
        init_answer_vector();
        #region 脚本获取
        rate_pro = GetComponent<rate_progress>();
        panelControl = GetComponent<PanelControl>();
        scrollbar_Control = GetComponent<scrollbar_control>();
        #endregion
        #region UI动画参数ID转化
        warning_value = Animator.StringToHash("is_warning");
        scene_index = Animator.StringToHash("scene_index");
        index_exist = Animator.StringToHash("index_set");
        main1_out = Animator.StringToHash("main1_act");
        main2_exist = Animator.StringToHash("main2_exist");
        envi_value = Animator.StringToHash("testpanel1_exist");
        anim_speed = Animator.StringToHash("anim_speed");
        netinit_enter = Animator.StringToHash("netinit_enter");
        test1_value = Animator.StringToHash("testindex_isenter");
        tools_value = Animator.StringToHash("tools_exist");
        progress_value = Animator.StringToHash("pro_panel_enter");
        problem_para = Animator.StringToHash("problem_par");
        test_1_value = Animator.StringToHash("test_panel_1_enter");
        #endregion
    }
    // Start is called before the first frame update
    void Start()
    {
        currscene_index = 0;
        

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #region UI动画控制
    // -------------------colunmn_panel-----------------------

    public void return_to_main()
    {
        if(currscene_index==1)
        {
            test_to_main();
        }
        else if(currscene_index==2)
        {
            Set_to_main();
        }
        else if(currscene_index==3)
        {
            learn_to_main();
        }
    }


    //  -----------------camera_panel-------------------------

    public void main_to_test()//主菜单转至测试模块
    {
        //camera_anim.SetFloat(anim_speed, 1);
        main2panel_quit();
        camera_anim.SetInteger(scene_index, 1);
        currscene_index = 1;
        Invoke("test_panel_1_enter", 3.0f);
    }

    public void test_to_main()//测试模块回到主菜单
    {
        maincamera.GetComponent<Animator>().applyRootMotion = false;
        camera_anim.SetInteger(scene_index, -1);
        //camera_anim.Play(maintotest);
        currscene_index = 0;
        Invoke("test_panel_1_exit", 3.0f);
        //test_panel1_quit();
        Invoke("main1panel_enter", 4);
    }

    public void main_to_sets()//主菜单转至布线模块
    {
        main2panel_quit();
        camera_anim.SetInteger(scene_index, 2);
        currscene_index = 2;
        index.localScale=new Vector3(1, 1, index.localScale.z);
        //if(cameraset_isok)
        //{
        //    cameraset.SetActive(true);
        //}
        //maincamera.GetComponent<Animator>().applyRootMotion = true;
    }

    public void Set_to_main()//布线模块转至主菜单
    {
        //cameraset.SetActive(false);//重新另相机的操作被沉默
        camera_anim.SetInteger(scene_index, -2);
        //camera_anim.speed = -1;
        //camera_anim.Play(maintoset);
        currscene_index = 0;
        index.localScale=new Vector3(0, 0, index.localScale.z);
        indexpanel_deactive();
        Invoke("main1panel_enter", 3);
    }

    public void main_to_learn()
    {
        main2panel_quit();
        camera_anim.SetInteger(scene_index, 3);
        currscene_index = 3;
        
    }

    public void learn_to_main()
    {
        currscene_index = 0;
        camera_anim.SetInteger(scene_index, -3);
        Invoke("main1panel_enter", 4);
    }

    //  -----------------主菜单panel-------------------

    public void main1panel_out()
    {
        main1_anim.SetBool(main1_out, true);
        Invoke("main2panel_enter", 1.0f);
    }

    public void main1panel_enter()
    {
        main1_anim.SetBool(main1_out, false);
    }

    public void main2panel_enter()
    {
        main2_anim.SetBool(main2_exist, true);
    }

    public void main2panel_quit()
    {
        main2_anim.SetBool(main2_exist, false);
    }

    //  -----------------学习panel------------------
    public void learn_panel_active()
    {

    }

    public void learn_panel_deactive()
    {

    }

    //  -----------------布线panel------------------
    public void set_netnum()//设置线网数量
    {

    }

    public void tools_exist()
    {
        tools_anim.SetBool("tools_exist", true);
    }

    public void tools_exit()
    {
        tools_anim.SetBool("tools_exist", false);
    }

    public void indexpanel_active()
    {
        index_anim.SetBool(index_exist, true);
    }

    public  void indexpanel_deactive()
    {
        index_anim.SetBool(index_exist, false);
    }

    public void netinit_active()
    {
        netinit_anim.SetBool(netinit_enter, true);
    }

    public void netinit_deactive()
    {
        netinit_anim.SetBool(netinit_enter, false);
    }

    //   ----------------测试panel-------------------

    public void test_panel_1_enter()
    {
        test_panel_1_anim.SetBool(test_1_value, true);
    }

    public void test_panel_1_exit()
    {
        test_panel_1_anim.SetBool(test_1_value, false);
    }

    public void environment_panel1_exist()
    {
        environment_anim.SetBool(envi_value, true);
    }

    public void environment_panel1_quit()
    {
        environment_anim.SetBool(envi_value, false);
    }

    public void test_panel_enter()
    {
        test_panel1_anim.SetBool(test1_value, true);
    }

    public void test_panel_exit()
    {
        test_panel1_anim.SetBool(test1_value, false);
    }
    
    //--------------------进度条panel---------------------

    public void progress_panel_enter()
    {
        progress_anim.SetBool(progress_value, true);
    }

    public void progress_panel_exit()
    {
        progress_anim.SetBool(progress_value, false);
    }

    //-----------------------提醒panel-----------------------

    public void warning_enter()
    {
        warning_anim.SetBool(warning_value, true);
    }

    public void warning_exit()
    {
        warning_anim.SetBool(warning_value, false);
    }

    //------------------------问题panel-------------------------
    public void problem_panel_enter()
    {
        problem_anim.SetBool(problem_para, true);
    }

    public void problem_panel_exit()
    {
        problem_anim.SetBool(problem_para, false);
    }

    #endregion

    #region UI读入的参数传递 方法
    //-----------------------设置环境参数-----------------------------
    
    /// <summary>
    /// 传递输入好的环境参数到后台
    /// </summary>
    public void environment_index_send()
    {
        Text netfloors_text = net_floors.gameObject.transform.Find("Text").GetComponent<Text>();
        Debug.Log("输入的布线层数为" + netfloors_text.text);
        
        Text maxtrack_text = max_tracksum.gameObject.transform.Find("Text").GetComponent<Text>();
        Debug.Log("输入的轨道数目为" + maxtrack_text.text);

        try
        {
            net_floors_num = int.Parse(netfloors_text.text);
            maxtrack_num = int.Parse(maxtrack_text.text);
            environment_panel1_quit();
            envi_set_btn.interactable = false;
            cir_set_btn.interactable = true;
            rate_pro.active_progress_image(0);
            cameraset.SetActive(true);
            cameraset_isok = true;
        }
        catch
        {
            warning_enter();
        }
        
    }


    //------------------------设置电路数据------------------------------

    /// <summary>
    /// 返回一个基础的线网生成坐标值
    /// </summary>
    /// <returns></returns>
    public Vector3 init_baespos()
    {
        return (base_up_pos.position + base_down_pos.position) / 2;
    }

    /// <summary>
    /// 将输入的电路数据（如引脚数量或线网数量等）传送到后台
    /// </summary>
    public void Circuit_data_send()
    {
        Text pinsnum_text = pins_num.gameObject.transform.Find("Text").GetComponent<Text>();
        Debug.Log("输入的引脚数量为" + pinsnum_text.text);
        Text netnum_text = net_num.gameObject.transform.Find("Text").GetComponent<Text>();
        Debug.Log("输入的线网数量为" + netnum_text.text);

        try
        {
            int pins_num = int.Parse(pinsnum_text.text);
            int nets_num = int.Parse(netnum_text.text);
            UI_Message.MaxPoint = pins_num;
            test_panel_exit();
            cir_set_btn.interactable = false;
            netinit_btn.interactable = true;
            scrollbar_Control.init_button(nets_num);
            rate_pro.active_progress_image(1);
        }
        catch
        {
            warning_enter();
        }
    }

    //--------------------------问题界面设置----------------------------------


    /// <summary>
    /// 跳转至上/下一个问题
    /// </summary>
    /// <param name="plus"></param>
    public void change_to_another_ques(int plus)
    {
        curr_index = (curr_index + plus + answers_length) % answers_length;
        order_num_text.text = "题目"+(curr_index+1).ToString();
        if (curr_index == answers_length - 1)
        {
            end_btn.gameObject.SetActive(true);
        }
        else
        {
            end_btn.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 生成用于放置答案的容器
    /// </summary>
    public void init_answer_vector()
    {
        //获取题目数量
        answer_is_have = new bool[answers_length];
        for(int i=0;i<answer_is_have.Length;i++)
        {
            answer_is_have[i] = false;
        }
        answers_index = new bool[answers_length,5];
        curr_index = 0;
        order_num_text.text = "题目" + (curr_index + 1).ToString();
        //order_num_text.text = "题目";
    }

    /// <summary>
    /// 获取相应的答案数据并生成
    /// </summary>
    public void Get_Answers_toggle_init()
    {
        if(answer_is_have[curr_index]==true)
        {
            for(int i=0;i<ques_toggles.Length;i++)
            {
                ques_toggles[i].isOn = answers_index[curr_index, i];
            }
        }
        
    }

    /// <summary>w
    /// 清空所有toggle选项
    /// </summary>
    public void clear_all_toggle()
    {
        for(int i=0;i<ques_toggles.Length;i++)
        {
            ques_toggles[i].isOn = false;
        }
    }

    /// <summary>
    /// 保存答案的数据
    /// </summary>
    public void save_curr_answer()
    {
        for (int i = 0; i < ques_toggles.Length; i++)
        {
            answers_index[curr_index,i] = ques_toggles[i].isOn;
        }
        answer_is_have[curr_index] = true;
    }
    #endregion
}
