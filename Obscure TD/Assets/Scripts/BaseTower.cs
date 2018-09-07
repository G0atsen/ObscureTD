using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Keith.EnemyStats;

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
        [SerializeField] protected LayerMask TargetLayer;
        // Element * this would be a EnemyStats?
        protected GameObject NearestEnemy;
        protected SphereCollider sphereCollider;

        private void Start()
        {
            sphereCollider = GetComponent<SphereCollider>();
            sphereCollider.isTrigger = true;
            Range = sphereCollider.radius;
        }

        // Constructor 
        /*public TowerBase(float damage, float fireRate, TowerType type)
        {
            BaseDamage = damage;
            BaseFireRate = fireRate;
            TypeOfTower = type;
        }*/

        // get neareset enemy and return its transform.position, assuming this is
        // called  wrt a colliderB
        protected virtual GameObject GetClosetEnemy(Collider[] enemies)
        {
            if (enemies != null)
            {
                Vector3 currentPosition = transform.position;
                // temp nearest Transform
                GameObject nearestEnemy = null;
                float minDistance = Mathf.Infinity;
                foreach (Collider enemy in enemies)
                {
                    Vector3 directionToTarget = enemy.transform.position - currentPosition;
                    float sqrDistToTarget = directionToTarget.sqrMagnitude;
                    if (sqrDistToTarget < minDistance)
                    {
                        minDistance = sqrDistToTarget;
                        nearestEnemy = enemy.gameObject;                       
                    }
                }
                Debug.Log("Returning Nearest enemy" + " " + nearestEnemy);
                return nearestEnemy;
            }
            else
            {
                Debug.Log("Null now");
                return null;
            }
        }

        protected virtual void AOE(Collider[] enemies)
        {
            foreach (Collider enemy in enemies)
            {
                // aoe
                /* Currently we would need to get the related EnemyScript
                 * tell it that it would take its health and minus the damage from it
                 * requires the enemy script to start being done imo
                 * but each enemy.gameobject.getcomponent<enemyAI>.health thing would need to be told
                 */
            }
        }

    

        // Currently this creates and passes the new array to the function every time theres a new enemy
        // not sure about performance at this current time
        protected void OnTriggerEnter(Collider other)
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, Range, TargetLayer);
            NearestEnemy = GetClosetEnemy(enemies);
        }

    }
}
