using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Keith.EnemyStats;

public class Enemy : MonoBehaviour {
    public EnemyStats Damage;
    public EnemyStats Speed;
    public EnemyStats Range;
    public EnemyStats Health;

    EnemyStats MaxHealth;
    public GameObject healthBarPrefab;
    public RectTransform healthPanelRect;
    
    // Use this for initialization
    void Start () {
        generateHealthBar();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void generateHealthBar() {
        GameObject healthBar = Instantiate(healthBarPrefab) as GameObject;
        healthBar.GetComponent<HealthBarController>().SetHealthBarData(this.transform, healthPanelRect);
        healthBar.transform.SetParent(healthPanelRect, false);
    }
}
