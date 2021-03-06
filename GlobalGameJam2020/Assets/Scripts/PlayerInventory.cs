﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private GameObject stairPrefab;

    [SerializeField] private int maxStair;

    private Queue<GameObject> stairQueue;

    public event UnityAction<int> OnStairChange;

    private void Start()
    {
        stairQueue = new Queue<GameObject>();

        CreateObjects();
    }

    public void SpawnStair(Vector3 position, int direction)
    {
        if (stairQueue.Count == 0) // No stair to spawn
            return;

        GameObject stairObj = stairQueue.Dequeue();

        Stair stair = stairObj.GetComponent<Stair>();
        stair.SetOrientation(Stair.Orientation.Horizontal);

        Vector3 scale = stair.transform.localScale; 
        scale.x = stair.transform.localScale.x * direction;
        stair.transform.localScale = scale;
        stairObj.transform.position = position;
        stairObj.SetActive(true);

        OnStairChange?.Invoke(stairQueue.Count);
    }

    public void StoreStair(GameObject stair)
    {
        stair.SetActive(false);
        stairQueue.Enqueue(stair);

        OnStairChange?.Invoke(stairQueue.Count);
    }

    private void CreateObjects()
    {
        for (int i = 0; i < maxStair;i ++)
        {
            GameObject obj = Instantiate(stairPrefab);
            obj.SetActive(false);

            stairQueue.Enqueue(obj);
        }
    }


}
