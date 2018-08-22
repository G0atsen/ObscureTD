using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObscureTD.Towers;
using Keith.EnemyStats;

public class SlowTower : MonoBehaviour {

    public float fireRate;
    public float damage;
    TowerType type = TowerType.Slow;
    List<GameObject> enemies;

	// Use this for initialization
	void Start () {
        TowerBase tower = new TowerBase(damage, fireRate, type);
		
	}

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<EnemyStats>() != null)
        {
           
        }
    }




    // Update is called once per frame
    void Update () {
		
	}
}
