using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stickman.Managers.Speed;
namespace Stickman
{
    public class EnemySpawnerGroup : MonoBehaviour
    {
        private enum SpawnerSchemas { ALL , RANDOM , BLOCKONE};
        [SerializeField]
        private GameObject [] m_enemy, m_spawners;
        [SerializeField]
        private SpawnerSchemas m_spawnerSchema;

        //private SpeedManager m_speedMng; 

        private void Awake()
        {
         //   m_speedMng = GameObject.Find("Managers").GetComponent<SpeedManager>();
        }
        void Start()
        {
           switch (m_spawnerSchema) {
               case SpawnerSchemas.ALL:
                   AllSpawnMode();
                   break;
               case SpawnerSchemas.RANDOM :
                   RandomSpawnMode();
                   break;
                case SpawnerSchemas.BLOCKONE:
                    BlockoneSpawnMode();
                    break;
           }
        }

        private void RandomSpawnMode(){
            float nEnemiesToSpawn = m_spawners.Length * 70 / 100;
            for(int i= 0 ; i< nEnemiesToSpawn ; i++){
                int chosenEnemyIndex = PickRandomEnemy();
                EnemySpawner chosenSpawner = m_spawners[PickRandomSpawner()].GetComponent<EnemySpawner>();
                chosenSpawner.SpawnNewEnemy(m_enemy[chosenEnemyIndex]);
            }
        }

        private void AllSpawnMode(){
            for(int i= 0 ; i< m_spawners.Length ; i++){
                int chosenEnemyIndex = PickRandomEnemy();
                EnemySpawner chosenSpawner = m_spawners[i].GetComponent<EnemySpawner>();
                chosenSpawner.SpawnNewEnemy(m_enemy[chosenEnemyIndex]);
            }
        }
        private void BlockoneSpawnMode(){
            int spawnerOff = Random.Range(0 , m_spawners.Length);
            for(int i= 0 ; i< m_spawners.Length ; i++){
                int chosenEnemyIndex = PickRandomEnemy();
                Debug.Log(spawnerOff);
                if(i != spawnerOff){
                    Debug.Log(i);
                    EnemySpawner chosenSpawner = m_spawners[i].GetComponent<EnemySpawner>();
                    chosenSpawner.SpawnNewEnemy(m_enemy[chosenEnemyIndex]);
                }
            }
        }
        
        private int PickRandomEnemy()
        {
            if (m_enemy.Length == 1) 
                return 0;
            int currentEnemy = Random.Range(0 , m_enemy.Length-1);
            return currentEnemy;
        }
        private int PickRandomSpawner()
        {
            if (m_spawners.Length == 1) 
                return 0;
            //Trovo uno Spawner non che non ha ancora spawnato
            int currentSpawner = Random.Range(0 , m_spawners.Length-1);
            while(m_spawners[currentSpawner].GetComponent<EnemySpawner>().HasSpawned)
                currentSpawner = Random.Range(0 , m_spawners.Length-1);

            return currentSpawner;
        }

    }
}
