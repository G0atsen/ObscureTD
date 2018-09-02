using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Keith.Towers;

public class InGameTower : MonoBehaviour {

    public Tower tower;
    public GameObject manager;
    public Combiner temp;
    TowerStats damage;
    TowerStats range;
    TowerStats fireRate;
    TowerStats cooldown;
    TowerStats procChance;
    LayerMask targetableLayers;
    Tower.TargetType targettype;
    
    private void Start()
    {
        this.transform.position = new Vector3(0f, 0f, UnityEngine.Random.Range(0f, 10f));
        manager.GetComponent<SpawnedTowers>().AddTower(tower);
        tower.TargetTower = this.gameObject;
        print(tower.TargetTower.transform.position);
        temp.Craft(manager.GetComponent<SpawnedTowers>());
        this.name = tower.name;
        damage = tower.Damage;
        range = tower.Range;
        fireRate = tower.FireRate;
        cooldown = tower.PowerCooldown;
        procChance = tower.ProcChance;
        targetableLayers = tower.targetableLayers;
        targettype = tower.targettype;
    }

    /*void OnDrawGizmosSelected()
 {
     Gizmos.color = Color.black;
     Gizmos.DrawWireSphere(transform.position, Range.BaseValue);
 }*/

    /*----------------------- UTILITY FUNCTIONS FOR ALL TOWERS -----------------------*/

    public Collider[] Targets(Transform towerTransform, Tower.TargetType type, Collider[] inRange, int count = default(int))
    {
        switch (type)
        {
            case Tower.TargetType.Closest:
                {
                    inRange = inRange.OrderBy(x => Vector3.Distance(towerTransform.position, x.transform.position)).ToArray();
                    return inRange.Take(count).ToArray();
                }
            case Tower.TargetType.Lowest:
                {
                    inRange = inRange.OrderBy(x => x.GetComponent<Enemy>().Health).ToArray();
                    return inRange.Take(count).ToArray();
                }
            case Tower.TargetType.Furthest:
                {
                    inRange = inRange.OrderBy(x => Vector3.Distance(towerTransform.position, x.transform.position)).ToArray();
                    inRange.Reverse();
                    return inRange.Take(count).ToArray();
                }
            case Tower.TargetType.Around:
                {
                    inRange = inRange.OrderBy(x => Vector3.Distance(towerTransform.position, x.transform.position)).ToArray();
                    return inRange;
                }
        }
        return null;
    }

}
