using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudpool : MonoBehaviour
{
    List<GameObject> readyPrefab;
    public GameObject prefab;
    public int prefabNum = 6;
    public float instan_time;
    Vector3 landb,randt;
    void Start()
    {
        landb = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        randt = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        readyPrefab = new List<GameObject>();
        for (int num = 0; num < prefabNum; num++)
        {
            pool_get(new Vector3(Random.Range(landb.x, randt.x), Random.Range(landb.y, randt.y), -1f));
            pool_get(new Vector3(Random.Range(randt.x - 10, randt.x), Random.Range(landb.y, randt.y), -1f));
        }
        instan_time = Random.Range(0f, 6f);
    }
    float tik = 0;
    void Update()
    {
        tik += Time.deltaTime;
        if (tik > instan_time)
        {
            instan_time = Random.Range(5f, 10f);
            tik = 0;

           
            pool_get(new Vector3(Random.Range(landb.x, randt.x), Random.Range(landb.y, randt.y), -1f));
            pool_get(new Vector3(Random.Range(randt.x-10, randt.x), Random.Range(landb.y, randt.y), -1f));
        }
    }
    public void pool_return(GameObject returnE)
    {
        returnE.SetActive(false);
        readyPrefab.Add(returnE);
    }

    public void pool_get(Vector3 start)
    {
        GameObject newE;
        if (readyPrefab.Count == 0)
        {
            pool_return(GameObject.Instantiate(prefab, transform));
        }
        newE = readyPrefab[0];
        readyPrefab.RemoveAt(0);
        newE.transform.position = start;
        newE.SetActive(true);
    }
}
