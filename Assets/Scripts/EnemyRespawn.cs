using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public PlayerHp Health;
    public Transform ghostPos;
    public Transform ghost2Pos;
    public Transform meaniePos;
    public Transform meanie2Pos;
    public Transform meanie3Pos;
    public GameObject[] enemies;
    private Vector2[] enemyInitialPositions;
    private int enemyCount;
    public int currentEnemyCount;
    
    void Start(){
        Debug.Log(enemies);
        enemyCount = enemies.Length;
        Debug.Log(enemies.Length);
        enemyInitialPositions = new Vector2[enemyCount];
        Debug.Log(enemyInitialPositions);
        for (int i = 0; i < enemyCount; i++){
            enemyInitialPositions[i] = enemies[i].transform.position;
        }
    }

    void FixedUpdate(){
        activeEnemies();
    }
    public void activeEnemies(){
        currentEnemyCount = enemyCount;
        for(int i = 0; i < enemyCount; i++){
            MeanieHp meanieHp = enemies[i].GetComponent<MeanieHp>();
            int health = meanieHp.hp;
            if((health) <= 0){
                currentEnemyCount--;
            }
        }
        if(currentEnemyCount == 0){
            EnemyRespawnsOnDeath();
        }
        currentEnemyCount = 5;
    }
    public void EnemyRespawnsOnDeath()
    {
        for (int i = 0; i < enemyCount; i++){
            Debug.Log("I am being called");
            enemies[i].transform.position = enemyInitialPositions[i];
            enemies[i].GetComponent<MeanieHp>().hp = enemies[i].GetComponent<MeanieHp>().maxHp;
            enemies[i].GetComponent<Collider2D>().enabled = true;
            enemies[i].GetComponent<SpriteRenderer>().enabled = true;
        }
        
    }

}
