using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pointer : MonoBehaviour
{
    public GameObject effecthot, effectice;
    RaycastHit hit;
    private Slider belive;
    private flow _flow;
    private float basictem, btk = 1, tik, taktime = 0.5f;
    void Start()
    {
        effecthot.GetComponent<ParticleSystem>().Stop();
        effectice.GetComponent<ParticleSystem>().Stop();
        toucheffect = effecthot.GetComponent<ParticleSystem>();
        Input.multiTouchEnabled = false;
        _flow = transform.GetComponent<flow>();
        belive = transform.GetComponent<UImanager>().belive.GetComponent<Slider>();
    }
    private Transform lastblock;
    void Update()
    {
        tik += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            toucheffect.Play();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            toucheffect.Stop();
        }
        else if (Input.GetMouseButton(0))
        {
            effecthot.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            effectice.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
   
            Ray point = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(point, out hit, 20, LayerMask.GetMask("block")) && (hit.transform != lastblock || tik > taktime))
            {
                tik = 0;
                lastblock = hit.transform;
                basictem = btk * _flow.basictem;
                _flow.tempflow(hit.transform.parent, basictem, 1);
                belive.value -= Mathf.Max(0.1f, belive.value * 0.0043f);
            }
        }


        // if (Input.GetMouseButtonDown(0))
        // {
        //     effecthot.GetComponent<ParticleSystem>().Play();
        // }
        // else if (Input.GetMouseButtonDown(1))
        // {
        //     effectice.GetComponent<ParticleSystem>().Play();
        // }
        // else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        // {
        //     effecthot.GetComponent<ParticleSystem>().Stop();
        //     effectice.GetComponent<ParticleSystem>().Stop();
        // }

    }
    private ParticleSystem toucheffect;
    public void switcheffect()
    {
        toucheffect.Stop();
        if (toucheffect == effecthot.GetComponent<ParticleSystem>())
        {

            toucheffect = effectice.GetComponent<ParticleSystem>();
        }
        else
        {

            toucheffect = effecthot.GetComponent<ParticleSystem>();
        }
        if (!sw)
        {
            switchbutton.color = ice;
        }
        else
        {
            switchbutton.color = hot;
        }
        sw = !sw;
        btk *= -1;
    }
    public Image switchbutton;
    public Color hot, ice;
    private bool sw;

}
