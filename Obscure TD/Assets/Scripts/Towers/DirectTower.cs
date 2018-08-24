using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the regular projectile launching tower, we could add a crit chance using the procChance and powerCooldown
/// I've not yet implemented any kind of attributes with regards to 'type' or w/e we use, we can do that later using the cool stats system (thx russian)
/// </summary>
public class DirectTower : Tower {
    
	// Use this for initialization
	void Start () {

	}
    /// <summary>
    /// This should cause the enemy to take damage? It's mostly good because it means that we can just run a health check on the enemies isDirty to update their health bar
    /// as well as apply % damage etc. I'm not entirely sure how best we can apply a DOT or delayed damage.
    /// We can also use this to rather instantiate projectiles or sumn, which would be noice, and then the projectile does the damage.
    /// Lastly, we can also pass in the damage type (we'd have to change the StatModifier constructor) but it's an easy procedure. Rather we don't do that now.
    /// </summary>
    protected override void Attack() {
        Collider[] enemiesInRange = Physics.OverlapSphere(this.transform.position, this.Range.Value,targetableLayers);
        foreach (Collider c in Targets(this.transform, TargetType.Closest, enemiesInRange, 1)) {
            if (c.gameObject.GetComponent<Enemy>() as Enemy) {
                c.gameObject.GetComponent<Enemy>().Health.AddModifier(new Keith.EnemyStats.StatModifier(-this.Damage.Value,Keith.EnemyStats.StatModType.Flat));             }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
