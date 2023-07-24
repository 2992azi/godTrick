using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class flow : MonoBehaviour
{
    
    float tick;
    List<Transform> targeG = new List<Transform>();
    List<float> temp_changeG = new List<float>();
    List<float> change_timeG = new List<float>();
    public float basictem = 1;
    public void tempflow(Transform targe,float temp_change, float change_time)
    {
        targeG.Add(targe);
        temp_changeG.Add(temp_change);
        change_timeG.Add(change_time);
    }
    void Update()
    {
        basicchange(GetComponent<UImanager>().belive.GetComponent<Slider>().value);
    }
    void FixedUpdate()
    {
        tick += Time.deltaTime;
            if (tick > 1f)
            {
                tick = 0;
                for(int num=0; num < targeG.Count; num++)
                {
                targeG[num].GetComponent<weather>().temp += temp_changeG[num];
                targeG[num].GetComponent<weather>().temp = Mathf.Max(0, targeG[num].GetComponent<weather>().temp);

                    change_timeG[num]--;
                    if (change_timeG[num] == 0)
                    {
                        targeG.RemoveAt(num);
                        temp_changeG.RemoveAt(num);
                        change_timeG.RemoveAt(num);
                    }
                }
            }
    }
    public void basicchange(float add)
    {
        basictem = Mathf.Max(0.5f,Mathf.Pow(1.05f,(add-50)));
        if (add == 0)
        {
            Time.timeScale = 0;
            GetComponent<UImanager>().gameover();
        }
    }
}
