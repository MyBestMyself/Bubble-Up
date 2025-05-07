using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collider){
        Debug.Log("A collision happened");
        if(collider.gameObject.GetComponentInParent<TestPlayerScript>() != null){
            collider.gameObject.GetComponentInParent<TestPlayerScript>().handle_damage();
        }
    }
}
