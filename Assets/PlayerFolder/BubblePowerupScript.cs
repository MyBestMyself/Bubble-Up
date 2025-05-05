using UnityEngine;

public class BubblePowerupScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BubblePowerupScript>() != null)
        {
            transform.GetComponentInParent<TestPlayerScript>().add_bubble();
            Destroy(gameObject);
            Destroy(this);
        }
    }
}
