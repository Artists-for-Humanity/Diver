using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour{

    public int maxHp = 5;
    public int hp;
    void Start(){
        hp = maxHp;
    }
    public void TakeDamage(int damageTaken){
        hp -= damageTaken;
        Debug.Log("Damage Taken");
        if(hp <= 0){
            Destroy(gameObject);
        }
    }
}
