using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGenerator : MonoBehaviour
{
    public GameObject[] boxPrefabs;
    private BoxCollider area;
    public int count = 100;
    private List<GameObject> boxes = new List<GameObject>();


	void Start ()
	{
	    area = GetComponent<BoxCollider>();
	    for (int i = 0; i < count; i++)
	    {
	        Spawn();
	    }
	    area.enabled = false;
	}

    private void Spawn()
    {
        int selection = Random.Range(0, boxPrefabs.Length);

        GameObject selectedPrefab = boxPrefabs[selection];

        Vector3 spawnPos = GetRandomPos();

        GameObject instance = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);

        boxes.Add(instance);
    }

    private Vector3 GetRandomPos()
    {
        Vector3 basePos = transform.position;
        Vector3 size = area.size;

        float posX = basePos.x + Random.Range(-size.x * 0.5f, size.x * 0.5f);
        float posY = basePos.y + Random.Range(-size.y * 0.5f, size.y * 0.5f);
        float posZ = basePos.z + Random.Range(-size.z * 0.5f, size.z * 0.5f);
        
        Vector3 spawnPos = new Vector3(posX, posY, posZ);
        return spawnPos;
    }

    public void Reset()
    {
        for (int i = 0; i < boxes.Count; i++)
        {
            boxes[i].transform.position = GetRandomPos();
            boxes[i].SetActive(true);
        }
    }
}
