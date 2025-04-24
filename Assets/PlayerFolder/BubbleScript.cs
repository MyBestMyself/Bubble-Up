using UnityEngine;

public class BubbleScript : MonoBehaviour
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
        if (collision.transform.GetComponent<CrabBehavior>() != null||
            collision.transform.GetComponent<WormBehavior>() != null||
            collision.transform.GetComponent<PufferBehavior>() != null)
        {
            transform.GetComponentInParent<PlayerScript>().handle_damage();
        }
        if (collision.gameObject.tag.Equals("bubble"))
        {
            transform.GetComponentInParent<PlayerScript>().add_bubble();
        }
    }
}
