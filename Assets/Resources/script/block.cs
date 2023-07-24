using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class block : MonoBehaviour
{
    public List<happen> events = new List<happen>();
    public TextAsset csvtext;
    [System.Serializable]
    public struct premise
    {
        public Season season;
        public float temperature;
        public float cloud;
        public float rain;
        public float water;
        public bool enable;
    }
    public enum Season
    {
        allyear, spring, summer, fall, winter
    }
    [System.Serializable]
    public struct result
    {
        public int point;
        public int belive;
        public List<int> enable;
        public List<int> disable;
        public string anime;
    }
    [System.Serializable]
    public struct happen
    {
        public int ID;
        public premise pre;
        public string report;
        public result re;
    }
    Transform season_comp, _belive, _point, _repoter;
    void Start()
    {
        season_comp = GameObject.Find("manager").transform;
        _belive = season_comp.GetComponent<UImanager>().belive;
        _point = season_comp.GetComponent<UImanager>().point;
        string[] lines = csvtext.text.Split("\n");
        for (int i = 2; i < lines.Length; i++)
        {
            if (lines[i] == "") continue;
            string[] data = lines[i].Split(",");
            happen _happen = new happen();
            _happen.ID = int.Parse(data[0]);
            _happen.pre.season = (Season)int.Parse(data[2]);
            _happen.pre.temperature = float.Parse(data[3]);
            _happen.pre.cloud = float.Parse(data[4]);
            _happen.pre.rain = float.Parse(data[5]);
            _happen.pre.water = float.Parse(data[6]);
            _happen.pre.enable = bool.Parse(data[7]);
            _happen.re.point = int.Parse(data[8]);
            _happen.re.belive = int.Parse(data[9]);
            if (data[10] != "")
            {
                List<int> _list = new List<int>();

                string[] _list_s = data[10].Split("、");
                for (int n = 0; n < _list_s.Length; n++)
                {
                    _list.Add(int.Parse(_list_s[n]));
                }
                _happen.re.enable = _list;
            }

            else
            {
                _happen.re.enable = new List<int>();
            }
            if (data[11] != "")
            {
                List<int> _list = new List<int>();
                string[] _list_s = data[11].Split("、");
                for (int n = 0; n < _list_s.Length; n++)
                {
                    _list.Add(int.Parse(_list_s[n]));
                }
                _happen.re.disable = _list;
            }
            else
            {
                _happen.re.disable = new List<int>();
            }
            _happen.re.anime = data[12];
            if (data[13] == "\r")
            {
                data[13] = "";
            }
            _happen.report = data[13];
            switch (_happen.pre.season)
            {

                case Season.spring:
                    spring.Add(_happen);
                    break;
                case Season.summer:
                    summer.Add(_happen);
                    break;
                case Season.fall:
                    fall.Add(_happen);
                    break;
                case Season.winter:
                    winter.Add(_happen);
                    break;
                default:
                    year.Add(_happen);
                    break;
            }
            canhappen.Add(_happen.ID, _happen.pre.enable);
        }

        // foreach (happen _ha in events)
        // {
        //     switch (_ha.pre.season)
        //     {
        //         case Season.spring:
        //             spring.Add(_ha);
        //             happenint[0].Add(nforall);
        //             break;
        //         case Season.summer:
        //             summer.Add(_ha);
        //             happenint[1].Add(nforall);
        //             break;
        //         case Season.fall:
        //             fall.Add(_ha);
        //             happenint[2].Add(nforall);
        //             break;
        //         case Season.winter:
        //             winter.Add(_ha);
        //             happenint[3].Add(nforall);
        //             break;
        //         default:
        //             year.Add(_ha);
        //             happenint[4].Add(nforall);
        //             break;

        //     }
        // if (_ha.pre.season == Season.spring)
        // {
        //     summer.Add(_ha);
        //     happenint[0].Add(nforall);
        // }
        // else if (_ha.pre.season == Season.summer)
        // {
        //     summer.Add(_ha);
        //     happenint[1].Add(nforall);
        // }
        // else if (_ha.pre.season == Season.fall)
        // {
        //     winter.Add(_ha);
        //     happenint[2].Add(nforall);
        // }
        // else if (_ha.pre.season == Season.winter)
        // {
        //     winter.Add(_ha);
        //     happenint[3].Add(nforall);
        // }
        // else
        // {
        //     year.Add(_ha);
        //     happenint[4].Add(nforall);
        // }
        // canhappen.Add(_ha.pre.enable);
        // nforall++;
        // }
        // seasonHappen = new List<List<happen>>() { spring, summer, fall, winter, year };
    }
    private void resethappen()
    {
        seasonHappen = new List<List<happen>>() { year, spring, summer, fall, winter };
    }


    // List<List<int>> happenint = new List<List<int>>();
    Dictionary<int, bool> canhappen = new Dictionary<int, bool>();
    // int nforall = 0;
    List<happen> spring = new List<happen>();
    List<happen> summer = new List<happen>();
    List<happen> fall = new List<happen>();
    List<happen> winter = new List<happen>();
    List<happen> year = new List<happen>();
    private List<List<happen>> seasonHappen;

    private void dojudge(int m)
    {

        for (int n = 0; n < seasonHappen[m].Count; n++)
        {

            happen child = seasonHappen[m][n];
            if (canhappen[child.ID] && judge(child.pre.temperature, child.pre.cloud, child.pre.rain, child.pre.water) && (child.report == "" || nohappen))
            {

                { Debug.Log(transform.name + child.ID + child.report); }
                canhappen[child.ID] = false;
                Result(child.report, child.re.point, child.re.belive, child.re.enable, child.re.disable, child.re.anime);
                if (child.report != "")
                {
                    nohappen = false;
                }
            }
        }
    }
    private bool nohappen = true;
    private int lastseason = 4;
    public void suject()
    {

        int seasonn = (int)season_comp.GetComponent<season>()._season + 1;
        if (lastseason == 4 && seasonn == 1)
        {
            resethappen();
        }
        lastseason = seasonn;
        nohappen = true;
        dojudge(seasonn);
        dojudge(0);


        // if (seasonn == 0)
        // {

        //     for (int n = 0; n < spring.Count; n++)
        //     {

        //         happen child = spring[n];
        //         if (!canhappen[happenint[0][n]] && judge(child.pre.temperature, child.pre.cloud, child.pre.rain, child.pre.water) && (child.���� == "" || nohappen))
        //         {
        //             canhappen[happenint[0][n]] = true;
        //             Result(child.����, child.���?.����, child.���?.belive, child.���?.���ú�, child.���?.���û�, child
        //         if (child.���� != "")
        //             {
        //                 nohappen = false;
        //             }
        //         }
        //     }
        // }
        // else if (seasonn == 1)
        // {

        //     for (int n = 0; n < summer.Count; n++)
        //     {

        //         happen child = summer[n];
        //         if (!canhappen[happenint[1][n]] && judge(child.ǰ��.�¶�, child.ǰ��.��, child.ǰ��.��, child.ǰ��.pre_water) && (child.���� == "" || nohappen))
        //         {
        //             canhappen[happenint[1][n]] = true;
        //             Result(child.����, child.���?.����, child.���?.belive, child.���?.���ú�, child.���?.���û�, child.���?.����);
        //             if (child.���� != "")
        //             {
        //                 nohappen = false;
        //             }
        //         }
        //     }
        // }
        // else if (seasonn == 2)
        // {

        //     for (int n = 0; n < fall.Count; n++)
        //     {

        //         happen child = fall[n];
        //         if (!canhappen[happenint[2][n]] && judge(child.ǰ��.�¶�, child.ǰ��.��, child.ǰ��.��, child.ǰ��.pre_water) && (child.���� == "" || nohappen))
        //         {
        //             canhappen[happenint[2][n]] = true;
        //             Result(child.����, child.���?.����, child.���?.belive, child.���?.���ú�, child.���?.���û�, child
        //                 if (child.���� != "")
        //             {
        //                 nohappen = false;
        //             }
        //         }
        //     }
        // }
        // else if (seasonn == 3)
        // {

        //     for (int n = 0; n < winter.Count; n++)
        //     {

        //         happen child = winter[n];
        //         if (!canhappen[happenint[3][n]] && judge(child.ǰ��.�¶�, child.ǰ��.��, child.ǰ��.��, child.ǰ��.pre_water) && (child.���� == "" || nohappen))
        //         {
        //             canhappen[happenint[3][n]] = true;
        //             Result(child.����, child.���?.����, child.���?.belive, child.���?.���ú�, child.���?.���û�, child
        //         if (child.���� != "")
        //             {
        //                 nohappen = false;
        //             }
        //         }
        //     }
        // }

        // for (int n = 0; n < year.Count; n++)
        // {
        //     happen child = year[n];
        //     if (!canhappen[happenint[4][n]] && judge(child.ǰ��.�¶�, child.ǰ��.��, child.ǰ��.��, child.ǰ��.pre_water) && (child.���� == "" || nohappen))
        //     {
        //         canhappen[happenint[4][n]] = true;
        //         Result(child.����, child.���?.����, child.���?.belive, child.���?.���ú�, child.���?.���û�, child.���?.����);
        //         if (child.���� != "")
        //         {
        //             nohappen = false;
        //         }
        //     }
        // }
    }
    public Animator animetor;
    void Result(string say, float point, float belive, List<int> behind1, List<int> behind2, string anime)
    {
        _belive.GetComponent<Slider>().value += belive;
        _point.GetComponent<point>()._point += (int)point;
        if (say != "")
        {
            _repoter = season_comp.GetComponent<UImanager>().get_report().transform;
            _repoter.GetComponent<Text>().text = say;
        }
        for (int n = 0; n < behind1.Count; n++)
        {
            canhappen[behind1[n]] = true;
        }
        for (int n = 0; n < behind2.Count; n++)
        {
            canhappen[behind2[n]] = false;
        }
        if (anime != "")
        {
            animetor.SetTrigger(anime);
        }
    }

    bool judge(float tem, float cloud, float rain, float water)
    {
        bool _tem = compare(tem, GetComponent<weather>().temp);
        bool _cloud = compare(cloud, GetComponent<weather>().cloud);
        bool _rain = compare(rain, GetComponent<weather>().rain);
        bool _water = compare(water, GetComponent<weather>().water);
        if (_tem && _cloud && _rain && _water)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool compare(float value, float tocompare)
    {
        if (value >= 0 && value <= tocompare)
        {
            return true;
        }
        else if (value < 0 && -value > tocompare)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
