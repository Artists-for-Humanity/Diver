using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHp : MonoBehaviour{
    public Image healthBar;
    public int maxHp;
    public int hp;
    public Transform respawnPoint;
    [Header("iFrames")]
    public float iFrames;
    public int numberOfFlashes;
    public EnemyRespawn enemyRespawn;
    private SpriteRenderer spriteRenderer;
    private bool immune;
    public bool getImmunity(){
        return immune;
    }
    void Start(){
        hp = maxHp;
        spriteRenderer = GetComponent<SpriteRenderer>();
        immune = false;
        Physics2D.IgnoreLayerCollision(3,7,false);
    }
    public void TakeDamage(int damageTaken){
        hp -= damageTaken;
        if(hp<=0){
            healthBar.fillAmount = 10f / 10f;
        } else {
            healthBar.fillAmount = hp / 10f;
        }
        Debug.Log("Damage Taken");
        if(hp <= 0){
            hp = maxHp;
            ScoreScript.scoreVal = 0;
            transform.position = respawnPoint.position;
            enemyRespawn.EnemyRespawnsOnDeath();
            
        }
        StartCoroutine(IFrames());
    }
    private IEnumerator IFrames(){
        Physics2D.IgnoreLayerCollision(3,7,true);
        for(int i = 0; i < numberOfFlashes; i++){
            spriteRenderer.color = new Color(1,0,0,0.2f);
            immune = true;
            yield return new WaitForSeconds(iFrames / (numberOfFlashes * 2));

            spriteRenderer.color = new Color(1,0,0,1f);
            yield return new WaitForSeconds(iFrames / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(3,7,false);
        immune = false;
    }
}
