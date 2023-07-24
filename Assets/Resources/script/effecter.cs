using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class effecter : MonoBehaviour
{
    // public GameObject manager;
    // float basictem;
    // float tik = 0;
    // float eventtime = 0.5f;
    // Slider belive;
    float use;
    void Update()
    {
        // use = Mathf.Max(0.1f, belive.value * 0.0043f);
        // tik += Time.deltaTime;
        // if (tik > eventtime && enter)
        // {
        //     basictem = btk * manager.GetComponent<flow>().basictem;
        //     if (Input.GetMouseButton(0))
        //     {
        //         manager.GetComponent<flow>().tempflow(transform.parent, basictem, 1);
        //         belive.value -= use;
        //     }
        //     else if (Input.GetMouseButton(1))
        //     {
        //         manager.GetComponent<flow>().tempflow(transform.parent, -basictem, 1);
        //         belive.value -= use;
        //     }
        //     tik = 0;
        // }
        if (Input.GetMouseButtonUp(0))
        {
            // enter = false;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0f);
        }
    }
    
    void Start()
    {
        // manager = GameObject.Find("manager");

        // belive = manager.transform.GetComponent<UImanager>().belive.GetComponent<Slider>();
    }
    bool enter;
    void OnMouseEnter()
    {

    }
    void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            GetComponent<SpriteRenderer>().color = new Color(0.5f, 1, 0.8f, 0.5f);
            // enter = true;
        }
    }
    void OnMouseExit()
    {
        // enter = false;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0f);
    }
}
