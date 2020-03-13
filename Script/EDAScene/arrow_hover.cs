using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class arrow_hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text BlocksProperty;    // 详细信息；
    [SerializeField]
    private float timer;           // 计时器；

    private bool isCanTimer;
    public float DelayTime;        // 悬停时间；




    private void Start()
    {
        timer = 0f;
        DelayTime = 0.5f;
        //BlocksProperty = GameObject.Find("BlocksProperties").GetComponent<Text>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        timer = 0f;
        isCanTimer = true;
        BlocksProperty.text = this.gameObject.name + "\n\r" + "Kawano";
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        isCanTimer = false;
        // BlocksProperty.enabled=false;
        HideUIProperty();
    }
    /// <summary>
    ///显示详细信息
    /// </summary>
    private void ShowUIProperty()
    {
        if (!BlocksProperty.IsActive())
        {
            BlocksProperty.enabled = true;
        }



    }
    /// <summary>
    /// 内容归为空
    /// </summary>
    private void HideUIProperty()
    {
        BlocksProperty.text = string.Empty;
        BlocksProperty.enabled = false;

    }
    private void Update()
    {

        DelayTimeShow();


    }
    void DelayTimeShow()
    {
        if (isCanTimer)
        {
            BlocksProperty.rectTransform.position = Input.mousePosition;  // 进入图片后， 提示文字跟随鼠标；
            timer += Time.deltaTime;
            if (timer > DelayTime)
            {
                ShowUIProperty();
                timer = 0f;


            }
        }


    }

}


