using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHp : MonoBehaviour{
    public Image healthBar;
    public int maxHp = 5;
    public int hp;
    void Start(){
        hp = maxHp;
    }
    public void TakeDamage(int damageTaken){
        hp -= damageTaken;
        healthBar.fillAmount = hp / 100f;
        Debug.Log("Damage Taken");
        if(hp <= 0){
            Destroy(gameObject);
        }
    }
}
