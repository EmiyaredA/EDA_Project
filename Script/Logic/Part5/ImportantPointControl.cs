using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.Part5;
public class ImportantPointControl : MonoBehaviour
{
    [HideInInspector]
    public List<ImportantPoint_number> importantPoint_Numbers = new List<ImportantPoint_number>();
    public GameObject ImportantPoint;
    public GameObject ImportantPointContain;
    readonly private List<GameObject> Points = new List<GameObject>();
    public void CreatPointButton()
    {
        if (PanelControl.panelList.Count != 0)
        {
            List<GameObject> gameObjects = PanelControl.panelList[0].GetComponent<SinglePanel>().Lis;
            int count = new int();
            for (int t = 0; t < gameObjects.Count; t++)
            {
                Vector3 temp1 = gameObjects[t].GetComponent<LineRenderer>().GetPosition(0);
                Vector3 temp2 = gameObjects[t].GetComponent<LineRenderer>().GetPosition(1);
                Vector3 temp3 = temp2 - temp1;

                Debug.Log(temp1);
                Debug.Log(temp2);
                //GameObject Po=Instantiate(ImportantPoint, temp2 + 0.04f * Vector3.up, Quaternion.AngleAxis(-90, Vector3.right), ImportantPointContain.transform);
                //Po.transform.localScale = PanelControl.floor_scale * new Vector3(Po.transform.localScale.x, Po.transform.localScale.y, Po.transform.localScale.z);
                Debug.Log(temp3);
                //count = (int)(temp3.x);
                //更新
                //Debug.Log((count * 1.0) / PanelControl.floor_scale);
                count = (int)(temp3.x / PanelControl.floor_scale);

                //count = (int)(1.7 * 5);
                Debug.Log(count);
                for (int i = 0; i <= count; i++)
                {
                    //更新
                    GameObject Point = Instantiate(ImportantPoint, temp1 + PanelControl.floor_scale * i * new Vector3(1, 0, 0) + 0.04f * Vector3.up, Quaternion.AngleAxis(-90, Vector3.right), ImportantPointContain.transform);
                    #region 新增
                    Point.transform.localScale = PanelControl.floor_scale * new Vector3(Point.transform.localScale.x, Point.transform.localScale.y, Point.transform.localScale.z); 
                    #endregion
                    Point.GetComponent<ImportantPoint_number>().Floor = 1;
                    Point.GetComponent<ImportantPoint_number>().Number = t * (count + 1) + i + 1;
                    Points.Add(Point);
                    importantPoint_Numbers.Add(Point.GetComponent<ImportantPoint_number>());
                }
            }
            UI_Message.MaxPoint = (count + 1) * PanelControl.panelList[0].GetComponent<SinglePanel>().Pathway_count;
        }
    }



    public void ExitButton1()
    {
        importantPoint_Numbers.Clear();
        foreach (var item in Points)
        {
            Destroy(item);
        }
        Points.Clear();
    }


}
