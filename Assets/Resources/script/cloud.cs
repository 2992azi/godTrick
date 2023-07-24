using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloud : MonoBehaviour
{
    Vector3 landb,randt;
    float weight,oneblock, tok = 0;
    public float time = 18;
    public int block_num;
    public Transform blocks;
    public Animator selfanima;
    void OnEnable()
    {
        blocks = GameObject.Find("map").transform;
        landb = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        randt = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); 
        weight = randt.x - landb.x;
        oneblock = weight / 6;
        selfanima = GetComponent<Animator>();
        animachange();
        changetarge();
        tik = 0;
    }

    Vector3 targepos, direction;
    void changetarge()
    {
        float Lcloudloss = _weather.neighborL.GetComponent<weather>().Lcloudloss;
        float Rcloudloss = _weather.neighborR.GetComponent<weather>().Rcloudloss;
        int targeblock;
        if (Lcloudloss> Rcloudloss)
        {
            targeblock = block_num-1;
        }
        else if(Lcloudloss < Rcloudloss)
        {
            targeblock = block_num +1;
        }
        else
        {
            targeblock = block_num;
            tik++;
        }
        targeblock = (int)Mathf.Clamp((float)targeblock, 0, 5);
        if (targeblock == 1)
        {
            targeblock--;
        }
        float targeX = Random.Range(landb.x+targeblock*oneblock, landb.x + (targeblock+1) * oneblock);
        targepos = new Vector3(targeX, transform.position.y, -1f);
        direction = (targepos - transform.position).normalized;
    }
    void Update()
    {
        tok += Time.deltaTime;
        float onetime = weight / time * Time.deltaTime ;
        transform.position += onetime * direction;
        if (tok > 1f)
        {
            animachange();
            tok = 0;
        }
        if (Vector3.Distance(transform.position ,targepos)< onetime)
        {

            changetarge();
        }
        if (tik == 5)
        {
            disapear();
        }
    }
    float cloud_;
    void disapear()
    {
        selfanima.SetFloat("cloud", 0);
        transform.parent.GetComponent<cloudpool>().pool_return(gameObject);
    }
    int tik;
    weather _weather;
    void animachange()
    {
        block_num = (int)Mathf.Clamp((transform.position.x - landb.x) / oneblock, 0, 5);
        _weather = blocks.GetChild(block_num).GetComponent<weather>();
        cloud_ = _weather.cloud;
        if (cloud_ < 10|| cloud_ < 100&&block_num == 0)
        {
            tik++;
        }
        selfanima.SetFloat("cloud", cloud_);
        changerain(Mathf.Clamp(_weather.rain,0,10000));
    }
    public ParticleSystem rain;
    public Color snow = new Color(1, 1, 1, 1);
    public Color rainwater = new Color(0, 0, 0.7f, 1);
    void changerain(float rainvalue)
    {
        var emission = rain.emission;
        if (rainvalue < 10)
        {
            emission.rateOverTime = 0;
            return;
        }
        var main = rain.main;
        main.startSpeedMultiplier = 0.5f+rainvalue/50;
        main.startLifetimeMultiplier = Mathf.Min(250 / rainvalue,10);
        emission.rateOverTime = 5+rainvalue/20;
        if (_weather.temp > 268)
        {
            main.startColor=rainwater;
        }
        else
        {
            main.startColor = snow;
        }
    }
    
    public void cloud0()
    {
    }
    public void cloud1()
    {
        var shape = rain.shape;
        rain.transform.localPosition = new Vector3(0, 0, 0);
        shape.scale = new Vector3(3, 1, 1);
    }
    public void cloud2()
    {
        var shape = rain.shape;
        rain.transform.localPosition = new Vector3(0, -0.7f, 0);
        shape.scale = new Vector3(6, 1, 1);
    }
    public void cloud3()
    {
        var shape = rain.shape;
        rain.transform.localPosition = new Vector3(0, -1.5f, 0);
        shape.scale = new Vector3(10, 1, 1);
    }
}
