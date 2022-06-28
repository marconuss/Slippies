using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLSpawner : MonoBehaviour
{
    public GameObject spawneable;
    public float queueTime;

    private List<GameObject> poolList;

    private float time = 0;

    public int poolSize;
    public float leftSpawnEdge;
    public float rightSpawnEdge;

    private void Start()
    {
        poolList = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < poolSize; i++)
        {
            tmp = Instantiate(spawneable, GetNewSpawnPosition(), Quaternion.identity, transform);
            tmp.SetActive(false);
            poolList.Add(tmp);
        }
    }

    private void Update()
    {
        if (!SLPauseManager.instance.IsPaused)
        {
            if (time > queueTime / SLGameManager.instance.gameSpeed)
            {

                GameObject obj = GetPooledObject();
                if (obj != null)
                {
                    obj.transform.position = GetNewSpawnPosition();
                    obj.SetActive(true);
                }

                time = 0;
            }

            time += Time.deltaTime;
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (!poolList[i].activeInHierarchy)
            {
                return poolList[i];
            }
        }
        return null;
    }

    private Vector3 GetNewSpawnPosition()
    {
        return new Vector3(Random.Range(leftSpawnEdge, rightSpawnEdge), transform.position.y, 0.0f);
    }
}
