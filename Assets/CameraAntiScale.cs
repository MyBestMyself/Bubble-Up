using UnityEngine;

public class CameraAntiScale : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = GetComponentInParent<Transform>().localScale;
        transform.localScale = new Vector3(1/v.x,1/v.y,1);
    }
}
