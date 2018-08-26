using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Keith.Towers;

[CreateAssetMenu]
public class Tower : ScriptableObject {

    public string TowerName;
    public TowerStats Damage;
    public TowerStats FireRate;
    public TowerStats Range;
    public TowerStats PowerCooldown;
    public TowerStats ProcChance;
    public LayerMask targetableLayers;

    public enum TargetType
    {
        Closest,
        Lowest,
        Furthest,
        Around,
        Multi,
    }
   
    /*void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, Range.BaseValue);
    }*/

    /*----------------------- UTILITY FUNCTIONS FOR ALL TOWERS -----------------------*/

    public Collider[] Targets(Transform towerTransform, TargetType type, Collider[] inRange, int count = default(int))
    {
        

        switch (type)
        {
            case TargetType.Closest:
                {
                    inRange = inRange.OrderBy(x => Vector3.Distance(towerTransform.position, x.transform.position)).ToArray();
                    return inRange.Take(count).ToArray();
                }
            case TargetType.Lowest:
                {
                    inRange = inRange.OrderBy(x => x.GetComponent<Enemy>().Health).ToArray();
                    return inRange.Take(count).ToArray();
                }
            case TargetType.Furthest:
                {
                    inRange = inRange.OrderBy(x => Vector3.Distance(towerTransform.position, x.transform.position)).ToArray();
                    inRange.Reverse();
                    return inRange.Take(count).ToArray();
                }
            case TargetType.Around:
                {
                    inRange = inRange.OrderBy(x => Vector3.Distance(towerTransform.position, x.transform.position)).ToArray();
                    return inRange;
                }
        }
        return null;
    }
}
