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

    public class TowerBase : MonoBehaviour
    {
        public readonly float BaseDamage;
        public float BaseFireRate;
        private TowerType TypeOfTower;
        // Element * this would be a EnemyStats?

        // Constructor 
        public TowerBase(float damage, float fireRate, TowerType type)
        {
            this.BaseDamage = damage;
            this.BaseFireRate = fireRate;
            this.TypeOfTower = type;
        }

        // get neareset enemy and return its transform.position, assuming this is
        // called  wrt a collider
        private GameObject GetClosetEnemy(GameObject[] enemies)
        {     
            Vector3 currentPosition = this.transform.position;
            // temp nearest Transform
            GameObject nearestEnemy = null;
            float minDistance = Mathf.Infinity;
            foreach (GameObject enemy in enemies)
            {
                Vector3 directionToTarget = enemy.transform.position - currentPosition;
                float sqrDistToTarget = directionToTarget.sqrMagnitude;
                if (sqrDistToTarget > minDistance)
                {
                    minDistance = sqrDistToTarget;
                    nearestEnemy = enemy;
                }
                return nearestEnemy;
            }
            return null;
        }

    }
}
