using UnityEngine;
using System.Collections;

public class CrabBehavior : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    Collider2D coll;
    bool flipped;
    float rayX;
    SpriteRenderer spriteRenderer;
    int layerMask;
    bool canMove = true;
    bool canFlip = true;
    [SerializeField] ParticleSystem particles;
    [SerializeField] ParticleSystem particlesFlipped;
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
                Flip();
                StartCoroutine("WaitForFlip");
                canFlip = true;
                
                // if(canMove){
                    // Debug.Log("Particles play???");
                    // if(flipped){
                    //     particlesFlipped.Play();
                    // }
                    // else{
                    //     particles.Play();
                    // }
                // }
            }
        }

        if(particlesFlipped.isStopped && canMove && flipped){
            particlesFlipped.Play();
        }
        else if(particles.isStopped && canMove && !flipped){
            particles.Play();
        }

        if(canMove){
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
    }

    void Flip(){
        speed *= -1;
        
        if(flipped){
            spriteRenderer.flipX = false;
            flipped = false;
        }
        else{
            spriteRenderer.flipX = true;
            flipped = true;
        }
    }

    IEnumerator WaitForFlip(){
        canMove = false;
        canFlip = false;
        // Debug.Log("particles stopped???");
        particles.Stop();
        particlesFlipped.Stop();
        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime * 1.5f, transform.position.y);
        yield return new WaitForSeconds(3f);
        canMove = true;
    }
}