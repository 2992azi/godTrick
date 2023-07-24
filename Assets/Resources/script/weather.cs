using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weather : MonoBehaviour
{
    public float temp, cloud, rain,water,underwater;
    public Transform neighborL,neighborR;
    public float temp_nei = 0.125f, temp_cloud = 2, temp_rain=-0.01f,sun=20;
    void Start()
    {
        
        map = transform.GetChild(0).GetComponent<SpriteRenderer>();
        neiL = neighborL.GetComponent<weather>();
        neiR = neighborR.GetComponent<weather>();
        showtemp();
    }
    public void Count()
    {
        temp += tempChange();
        temp = Mathf.Max(0, temp);
        rain = rainChange();
        cloud += cloudChange();
        water += waterChange();
        underwaterChange();
    }
    weather neiL, neiR;
    float neiL_temp = 0, neiR_temp = 0;
    float tempChange()
    {
        
        neiL_temp = neiL.temp- temp;
        neiR_temp = neiR.temp- temp;
        neiL.temp -= neiL_temp * temp_nei;
        neiL.temp = Mathf.Max(0, neiL.temp);
        neiR.temp -= neiR_temp * temp_nei;
        neiR.temp = Mathf.Max(0, neiR.temp);
        return (neiL_temp + neiR_temp) * temp_nei + sun / (sun + cloud * temp_cloud) + rain * temp_rain;
    }
    SpriteRenderer map;
    public void showtemp()
    {
        float tempH = Mathf.Clamp(temp, 260, 320);
        float tempS = Mathf.Clamp(temp, 320, 580);
        float tempV = Mathf.Clamp(temp, 0, 260);
        map.color = Color.HSVToRGB((320 - tempH) / 90, 1,Mathf.Min((580 - tempS) / 260, tempV /260));
    }
    public float normal_temp = 298,cloud_water=0.005f,cloud_move = 400f;
    float cloudplus,cloudloss;
    public float Lcloudloss,Rcloudloss;
    float cloudChange()
    {
        cloudloss = 0;
        Lcloudloss = cloud * Mathf.Clamp01(Mathf.Log(Mathf.Max(cloud - neiL.cloud,0) + 1, cloud_move) - 0.5f);
        neiL.cloud += Lcloudloss;
        Rcloudloss = (cloud-Lcloudloss) * Mathf.Clamp01(Mathf.Log(Mathf.Max(cloud - neiR.cloud,0) + 1, cloud_move) - 0.5f);
        neiR.cloud += Rcloudloss;
        cloudloss = Lcloudloss + Rcloudloss;
        if (temp - normal_temp > 0)
        {
            cloudplus = (temp - normal_temp) * water * cloud_water;
            water -= cloudplus;
        }
        else
        {
            cloudplus = 0;
        }
        return cloudplus-cloudloss;
    }
    public float mid_rain = 50,rain_cloud = 0.9f;
    float rainplus;
    float rainChange()
    {
        rain_last = rain;
        if (Random.Range(0f, 1f) > (mid_rain / (mid_rain + water + rain_last)))
        {
            return 0;
        }
        else
        {
            rainplus = cloud * rain_cloud;
            cloud -= rainplus;
            return rainplus;
        }
    }
    public float water_faz = 250f,water_hot = 700f;
    float waterplus, waterloss, rain_last;
    float waterChange()
    {
        waterplus = rain_last;
        if (neighborR == transform)
        {
            waterloss = 0;
        }
        else
        {
            float water_move = Mathf.Clamp01(Mathf.Log(Mathf.Max(temp - water_faz + 1, 1), water_hot - water_faz + 1));
            waterloss = water * water_move;
        }
        neiR.water += waterloss;
        
        return waterplus - waterloss;
    }
    public float under_move = 0.2f;
    void underwaterChange()
    {
        if (water < underwater*0.1f)
        {
            water *= 3;
            underwater -= water * 2;
        }
        else if (underwater < water * 0.5f)
        {
            underwater += water * 0.2f;
            water *= 0.8f;
        }
        neiR.underwater += underwater * under_move;
        underwater *= 1 - under_move;
    }
}
