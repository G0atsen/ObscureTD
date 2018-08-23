using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObscureTD.Towers
{
    public enum TowerType
    {
        Slow = 975,
        DPS = 950,
        DOT = 925,
        AOE = 900,
    }

    public class BaseTower : MonoBehaviour
    {
        [SerializeField] protected float BaseDamage;
        [SerializeField] protected float BaseFireRate;
        [SerializeField] protected float Range;
        [SerializeField] protected TowerType TypeOfTower;
        // Element * this would be a EnemyStats?
        protected GameObject NearestEnemy;

        // Constructor 
        /*public TowerBase(float damage, float fireRate, TowerType type)
        {
            BaseDamage = damage;
            BaseFireRate = fireRate;
            TypeOfTower = type;
        }*/

        // get neareset enemy and return its transform.position, assuming this is
        // called  wrt a collider
        protected GameObject GetClosetEnemy(Collider[] enemies)
        {     
            Vector3 currentPosition = transform.position;
            // temp nearest Transform
            GameObject nearestEnemy = null;
            float minDistance = Mathf.Infinity;
            foreach (Collider enemy in enemies)
            {
                Vector3 directionToTarget = enemy.transform.position - currentPosition;
                float sqrDistToTarget = directionToTarget.sqrMagnitude;
                if (sqrDistToTarget > minDistance)
                {
                    minDistance = sqrDistToTarget;
                    nearestEnemy = enemy.GetComponent<GameObject>();
                }
                
            }
            return nearestEnemy;
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "enemy")
            {
                Collider[] enemies = Physics.OverlapSphere(transform.position, Range);
                NearestEnemy = GetClosetEnemy(enemies);
            }
        }

    }
}
