using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Keith.EnemyStats;

namespace ObscureTD.Towers
{
    [SerializeField]
    public class SlowTower : BaseTower
    {


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                Collider[] nearbyEnemies = Physics.OverlapSphere(transform.position, Range, layer);
                NearestEnemy = GetClosetEnemy(nearbyEnemies);
                Debug.Log("T pressed");
            }
        }

    }
}