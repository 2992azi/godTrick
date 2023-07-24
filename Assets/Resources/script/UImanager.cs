using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UImanager : MonoBehaviour
{
    public Transform belive;
    public Transform point;
    public Transform report;
    public GameObject startpanle;
    public GameObject gamepanle;
    public GameObject overpanle;
    public GameObject report_pre;
    List<GameObject> report_list = new List<GameObject>();
    public int report_num;
    public float timescale = 2;
    void Awake()
    {
        Time.timeScale = 0;
    }
    void Update()
    {
        //Time.timeScale = timescale;
    }
    public void Startgame()
    {
        Time.timeScale = timescale;
        startpanle.SetActive(false);
        gamepanle.SetActive(true);

    }
    public void endgame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void gameover()
    {
        point.parent.SetParent(overpanle.transform);
        overpanle.SetActive(true);
        gamepanle.SetActive(false);
        point.parent.position = overpanle.transform.position;
    }
    public void reset()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
    void Start()
    {
        for (int n = 0; n < report_num; n++)
        {
            report_list.Add(GameObject.Instantiate(report_pre, report));
            report_list[n].GetComponent<RectTransform>().sizeDelta = new Vector2(337.369f, report_list[n].GetComponent<RectTransform>().rect.height);
        }
    }
    int now = 0;
    public GameObject get_report()
    {
        GameObject returnone = report_list[now];
        now++;
        now %= report_num;
        returnone.transform.SetSiblingIndex(0);
        return returnone;
    }

   
}
