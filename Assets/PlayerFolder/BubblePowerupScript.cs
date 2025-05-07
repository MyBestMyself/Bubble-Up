using UnityEngine;

public class BubblePowerupScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collide");
        if (other.gameObject.GetComponentInParent<TestPlayerScript>() != null)
        {
            other.gameObject.GetComponentInParent<TestPlayerScript>().add_bubble();
            Destroy(gameObject);
            Destroy(this);
        }
    }
}
