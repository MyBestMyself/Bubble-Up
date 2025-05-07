using UnityEngine;

public class WaterLevelTransition : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Player")){
            GameManager.instance.NextLevel();
        }
    }
}
