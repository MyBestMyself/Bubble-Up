using UnityEngine;
using System.Collections;

public class WormBehavior : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    bool canPlay = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // IEnumerator Particles(){
    //     // while(true){
    //         // yield return new WaitForSeconds(2);
    //         particles.SetActive(true);
    //         yield return new WaitForSeconds(3);
    //         particles.SetActive(false);
    //         // yield return new WaitForSeconds(6);
    //     // }
    // }

    public void PlayParticles(){
        // StartCoroutine("Particles");
        if(canPlay){
            particles.Play();
            canPlay = false;
        }
        else{
            canPlay = true;
        }
    }
}
