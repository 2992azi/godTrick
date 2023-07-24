using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class season : MonoBehaviour
{
    public Animator map2;
    public AudioSource _as;
    public enum Season
    {
        spring, summer, fall, winter
    }
    public Season _season = Season.spring;
    public float season_time = 10;
    float time;
    public Transform manager,blocks;
    void FixedUpdate()
    {
        
        time += Time.deltaTime;
        if (time >= season_time)
        {
            time = 0;
            _season = (Season)(((int)_season+1)%4);
            map2.SetInteger("season", (int)_season);
            seasonchange = true;
            swith = true;
            giveflow = true;
            
        }
        if (giveflow && time >= season_time / 2)
        {
            giveflow = false;
            foreach (Transform block in blocks)
            {
                float temp = (tempdic[_season] - block.GetComponent<weather>().temp) / season_time;
                manager.GetComponent<flow>().tempflow(block,temp , season_time);

            }
        }
        if (seasonchange)
        {
            musicplay();
        }
    }
    bool seasonchange, giveflow = true;
    bool swith;
    public List<AudioClip> audios;
    void musicplay()
    {
        if (swith)
        {
            _as.volume -= 0.3f * Time.deltaTime;
        }
        else
        {
            _as.volume += 0.3f * Time.deltaTime;
        }
        if(_as.volume >= 0.5)
        {
            seasonchange = false;
        }
        else if (_as.volume == 0)
        {
            swith = false;
            _as.clip = audios[(int)_season];
            _as.Play();
        }
    }
    Dictionary<Season, float> tempdic = new Dictionary<Season, float>();
    void Start()
    {
        _as = GetComponent<AudioSource>();
        tempdic.Add(Season.spring, 305);
        tempdic.Add(Season.summer, 288);
        tempdic.Add(Season.fall, 263);
        tempdic.Add(Season.winter, 290);
        foreach (Transform block in blocks)
        {
            float temp = (tempdic[_season] - block.GetComponent<weather>().temp) / season_time;
            manager.GetComponent<flow>().tempflow(block, temp, season_time / 2);

        }
    }
}
