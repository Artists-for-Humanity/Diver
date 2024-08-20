using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeanieHp : MonoBehaviour{
    // Start is called before the first frame update
    public int maxHp; 
    public int hp;
    void Start(){
        hp = maxHp;
    }
    public void TakeDamage(int damageTaken){
        hp -= damageTaken;

        if(hp <= 0){
            Destroy(gameObject);
        }
    }
}
