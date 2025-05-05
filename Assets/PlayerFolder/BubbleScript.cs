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
    }
        private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BubblePowerupScript>() != null)
        {
            transform.GetComponentInParent<TestPlayerScript>().add_bubble();
        }
    }
}
