using UnityEngine.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnedTowers : MonoBehaviour, iTowerController {
    [SerializeField] List<Tower> spawnedTowers;
    [SerializeField] Transform towerParent;


    public bool AddTower(Tower tower) {
        if (IsFull())
            return false;
        Tower asd = Instantiate(tower);
        asd.name = UnityEngine.Random.Range(0f,2f).ToString();
        spawnedTowers.Add(asd);
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
        return false;
    }

    public bool RemoveTower(Tower tower) {
        if (spawnedTowers.Remove(tower)) {
            Destroy(tower);
            return true;
        }
        return false;
    }

    public Tower RemoveTower(string towerID)
    {
        for (int i = 0; i < spawnedTowers.Count; i++)
        {
            Tower tower = spawnedTowers[i];
            if (tower != null && tower.ID == towerID)
            {
                spawnedTowers[i] = null;
                return tower;
            }
        }
        return null;
    }


    public int TowerCount(string towerID)
    {
        int count = 0;
        for (int i = 0; i < spawnedTowers.Count; i++)
        {
            if (spawnedTowers[i].ID == towerID)
            {
                count++;
            }
        }
        return count;
    }


}
