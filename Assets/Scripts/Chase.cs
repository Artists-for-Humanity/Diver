using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public GameObject player;
    public float speed;
    private float distance;
    private bool lunging;
    public float lungeStrength;
    private float lungeLength = 1f;
    private float moveCooldown = 0.3f;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;


    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update() {
        if (lunging) {
            return;
        }
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        if (distance < 5) {
            StartCoroutine(Lunge(direction, angle));
        }

    }
    IEnumerator Lunge(Vector2 direction, float angle) {
        lunging = true;
        rb.velocity = new Vector2(lungeStrength * direction.x, lungeStrength * direction.y);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        yield return new WaitForSeconds(lungeLength);
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(moveCooldown);
        lunging = false;
    }
  
    void Awake(){
        Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(),boxCollider, true);
    }
}
