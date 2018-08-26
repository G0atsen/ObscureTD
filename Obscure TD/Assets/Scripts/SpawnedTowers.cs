using UnityEngine.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;


public class SpawnedTowers : MonoBehaviour, iTowerController {
    [SerializeField] List<Tower> spawnedTowers;
    [SerializeField] Transform towerParent;


    public bool AddTower(Tower tower) {
        if (IsFull())
            return false;
        spawnedTowers.Add(tower);
        return true;
    }

    public bool ContainsTower(Tower tower)
    {
        for (int i = 0; i < spawnedTowers.Count; i++)
        {
            if (spawnedTowers[i] == tower)
            {
                return true;
            }
        }
        return false;
    }

 
    public bool IsFull()
    {
        throw new System.NotImplementedException();
    }

    public bool RemoveTower(Tower tower) {
        if (spawnedTowers.Remove(tower)) {
            Destroy(tower);
            return true;
        }
        return false;
    }

    public int TowerCount(Tower tower)
    {
        int count = 0;
        for (int i = 0; i < spawnedTowers.Count; i++)
        {
            if (spawnedTowers[i] == tower)
            {
                count++;
            }
        }
        return count;
    }
}
