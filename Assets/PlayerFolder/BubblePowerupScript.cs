using UnityEngine;

public class BubblePowerupScript : MonoBehaviour
{
    float t=0f;
    byte state = 0;
    private void Update()
    {
        t += Time.deltaTime;
        if (t >= 0.225f)
        {
            t = 0f;
            state++;
            switch (state)
            {
                case 1:
                    transform.position = new Vector3(transform.position.x, transform.position.y+.1f,transform.position.z);
                    break;
                case 2:
                    transform.position = new Vector3(transform.position.x-.1f, transform.position.y + .1f, transform.position.z);
                    break;
                case 3:
                    transform.position = new Vector3(transform.position.x + .1f, transform.position.y - .1f, transform.position.z);
                    break;
                case 4:
                    transform.position = new Vector3(transform.position.x, transform.position.y - .1f, transform.position.z);
                    state = 0;
                    break;
                default:
                    break;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collide");
        if (other.gameObject.GetComponentInParent<TestPlayerScript>() != null)
        {
            Destroy(gameObject);
            other.gameObject.GetComponentInParent<TestPlayerScript>().add_bubble();
            Destroy(this);
        }
    }
}
