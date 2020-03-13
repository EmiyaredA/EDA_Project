using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rate_progress : MonoBehaviour
{
    public Sprite act_progress;

    public Image[] pro_images = new Image[5];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 激活对应步骤的Image
    /// </summary>
    public void active_progress_image(int i)
    {
        pro_images[i].sprite =act_progress;
    }
}
