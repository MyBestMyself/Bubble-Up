using UnityEngine;
using System.Collections;

public class CrabBehavior : MonoBehaviour
{
    [SerializeField] float speed = 0.1f;
    Collider2D coll;
    bool flipped;
    float rayX;
    float rayY;
    SpriteRenderer spriteRenderer;
    int layerMask;
    bool canMove = true;
    bool canFlip = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coll = gameObject.GetComponent<Collider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        layerMask = ~(1 << 7);  //Found this online, uses all layermasks except the crab layer
    }

    // Update is called once per frame
    void Update()
    {
        if(flipped){
            rayX = coll.bounds.min.x;
        }
        else{
            rayX = coll.bounds.max.x;
        }

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(rayX, coll.bounds.min.y), -Vector2.up, 0.5f, layerMask);
        Debug.DrawRay(new Vector2(rayX, coll.bounds.min.y), -Vector2.up * 0.5f, Color.red, 5f);
        if(hit.collider == null){
            if(canFlip){
                StartCoroutine("WaitForFlip");
            }
        }

        if(canMove){
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
    }

    void Flip(){
        Debug.Log("Flip called");
        speed *= -1;
        if(flipped){
            spriteRenderer.flipX = false;
            flipped = false;
        }
        else{
            spriteRenderer.flipX = true;
            flipped = true;
        }
        canFlip = true;
    }

    IEnumerator WaitForFlip(){
        canMove = false;
        canFlip = false;
        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime * -1.5f, transform.position.y);
        yield return new WaitForSeconds(0.5f);
        Flip();
        canMove = true;
    }
}