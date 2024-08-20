using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHp : MonoBehaviour{
    public Image healthBar;
    public int maxHp;
    public int hp;
    // public float iFrames = 1f;
    // public float timer = 0f;
    // public bool giveIFrames = false;
    void Start(){
        hp = maxHp;
    }
    public void TakeDamage(int damageTaken){
        // if (giveIFrames){
            
        //     return;
        // }
        hp -= damageTaken;
        healthBar.fillAmount = hp / 10f;
        // giveIFrames = true;
        Debug.Log("Damage Taken");
        if(hp <= 0){
            Destroy(gameObject);
        }
    }
    // public void IFrames(){
    //     timer += Time.deltaTime;
    //     if (timer <)
    // }
}
