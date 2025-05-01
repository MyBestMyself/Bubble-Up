using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.Animations;

public class PufferBehavior : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    CircleCollider2D circCollider;
    SpriteRenderer spriteRenderer;
    bool isInflated;
    float waitTime = 1f;
    Animator animator;
    Animation anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        circCollider = gameObject.GetComponent<CircleCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        anim = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCollider(){
        if(isInflated){
            circCollider.radius = 0.4f;
            isInflated = false;
        }
        else{
            circCollider.radius = 1.295f;
            isInflated = true;
        }
    }

    IEnumerator EndAnimation(){
        UpdateCollider();
        yield return new WaitForSeconds(waitTime);
        animator.SetBool("IsInflated", isInflated);
    }

    IEnumerator StartAnimation(){
        UpdateCollider();
        PlayParticles();
        yield return new WaitForSeconds(waitTime + 1);
        animator.SetBool("IsInflated", isInflated);
    }

    void PlayParticles(){
        particles.Play();
    }
}
