using System.IO;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] Image picture;
    int currentSlide = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void changeSlide(int amount)
    {
        currentSlide += amount;

        if (currentSlide < 1)
        {
            currentSlide = 1;
        }
        else if (currentSlide > 5)
        {
            SceneManager.LoadScene(sceneName: "level1");
        }
        else
        {
            Sprite newSprite = Resources.Load<Sprite>("Sprites/backgrounds/Intro" + currentSlide);
            if (newSprite != null)
            {
                picture.sprite = newSprite;
            }
        }
    }
}
