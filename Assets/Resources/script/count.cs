using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class count : MonoBehaviour
{
    public float oneturn = 1,totalw;
    float tick = 0,tok = 0f;
    int num = 0;
    Slider belive;
    void Start()
    {
        belive = GameObject.Find("manager").transform.GetComponent<UImanager>().belive.GetComponent<Slider>();
    }
    void FixedUpdate()
    {
        
        if(tok > oneturn)
        {
            tok = 0;
            transform.GetChild(num).GetComponent<block>().suject();
            num++;
            num = num % transform.childCount;
            if (num == 0)
            {
              belive .value -= 0.5f;
            }
        }
        if(tick > oneturn)
        {
            tick = 0;
            transform.GetChild(num).GetComponent<weather>().Count();
            transform.GetChild(num).GetComponent<weather>().showtemp();
            seawater();
        }
        tick += Time.deltaTime;
        tok += Time.deltaTime*1.1f;
        totalw = total();
    }
    void seawater()
    {
        float sealevel = 0;
        for (int n = 3; n < 6; n++)
        {
            sealevel += transform.GetChild(n).GetComponent<weather>().water;
        }
        for (int n = 3; n < 6; n++)
        {
            weather wea2cha = transform.GetChild(n).GetComponent<weather>();
            wea2cha.water = sealevel / 3+wea2cha.underwater;
            wea2cha.underwater = 0;
        }
    }
    float total()
    {
        float ret=0;
        foreach(Transform block in transform)
        {
            ret += block.GetComponent<weather>().cloud;
            ret += block.GetComponent<weather>().rain;
            ret += block.GetComponent<weather>().water;
        }
        return ret;
    }
}
