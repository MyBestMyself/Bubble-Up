using UnityEngine;

public class CrabBehavior : MonoBehaviour
{
    [SerializeField] float speed = 0.1f;
    Collider2D coll;
    bool flipped;
    float rayX;
    float rayY;
    SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coll = gameObject.GetComponent<Collider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(flipped){
            rayX = coll.bounds.min.x;
            // rayY = coll.bounds.max.y;
            // spriteRenderer.flipX = true;
        }
        else{
            rayX = coll.bounds.max.x;
            // rayY = coll.bounds.min.y;
            // spriteRenderer.flipX = false;
        }
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(rayX, coll.bounds.min.y), -Vector2.up);
        Debug.DrawRay(new Vector2(rayX, coll.bounds.min.y), -Vector2.up * 2, Color.red, 5f);
        if(hit.collider == null){
            Flip();
        }

        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
    }

    void Flip(){
        speed *= -1;
        if(flipped){
            // rayX = coll.bounds.max.x;
            // rayY = coll.bounds.max.y;
            spriteRenderer.flipX = true;
            flipped = false;
        }
        else{
            // rayX = coll.bounds.min.x;
            // rayY = coll.bounds.min.y;
            spriteRenderer.flipX = false;
            flipped = true;
        }
    }
}