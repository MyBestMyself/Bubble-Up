using UnityEngine;

public class BubblePowerupScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        /*Debug.Log(collision);
        if (collision.gameObject.GetComponent<BubbleScript>()!=null|| 
            collision.gameObject.GetComponentInParent<PlayerScript>()!=null)
        {
            collision.gameObject.GetComponentInParent<PlayerScript>().add_bubble();
            Destroy(gameObject);
            Destroy(this);
        }*/
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
