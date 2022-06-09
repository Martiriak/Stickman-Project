using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stickman
{
    public class EnemySpawner : MonoBehaviour
    {
        private bool m_hasSpawned = false;
        public bool HasSpawned {
            get => m_hasSpawned;
            set =>  m_hasSpawned = value;
        }
        public void SpawnNewEnemy(GameObject enemy)
        {
            Enemy currentEnemy = Instantiate( enemy , transform.position, Quaternion.identity).GetComponent<Enemy>();
            m_hasSpawned = true;
        }

    }
}
