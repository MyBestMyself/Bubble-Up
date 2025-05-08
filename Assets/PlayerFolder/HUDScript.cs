using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
    [SerializeField] TestPlayerScript p;
    Image[] lives;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lives = new Image[3]; 
        lives[0] = transform.GetChild(1).GetComponent<Image>();
        lives[1] = transform.GetChild(2).GetComponent<Image>();
        lives[2] = transform.GetChild(3).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        lives[0].enabled=false;
        lives[1].enabled = false;
        lives[2].enabled = false;
        switch (p.bubble_count)
        {
            case 3:
                lives[2].enabled = true;
                lives[1].enabled = true;
                lives[0].enabled = true;
                break;
            case 2:
                lives[1].enabled = true;
                lives[0].enabled = true;
                break;
            case 1:
                lives[0].enabled = true;
                break;
            default:
                break;
        }
    }
}
