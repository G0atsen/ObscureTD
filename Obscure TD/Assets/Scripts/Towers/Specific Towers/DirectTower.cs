using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the regular projectile launching tower, we could add a crit chance using the procChance and powerCooldown
/// I've not yet implemented any kind of attributes with regards to 'type' or w/e we use, we can do that later using the cool stats system (thx russian)
/// </summary>
public class DirectTower : InGameTower {

    void Attack() {
        Collider[] enemiesInRange = Physics.OverlapSphere(this.transform.position, range.Value,targetableLayers);
        foreach (Collider c in Targets(this.transform, targettype, enemiesInRange, 1)) {
            if (c.gameObject.GetComponent<Enemy>() as Enemy) {
                c.gameObject.GetComponent<Enemy>().Health.AddModifier(new Keith.EnemyStats.StatModifier(-damage.Value,Keith.EnemyStats.StatModType.Flat));
                c.gameObject.GetComponent<Enemy>().updateHealth();
            }
        }
    }
}
