using UnityEngine.UI;
using UnityEngine;

public class coin : MonoBehaviour {

    // ova skripta kontrolira ponašanje kovanica (dijamanata)

  
    public ParticleSystem PS; // ćestice koje se aktiviraju kada korisnik skupi dijamant
    public Renderer r;  // renderer dijamanta
  

    private void Update()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time * 3, 10) - 5, transform.position.y, transform.position.z);
        // namješta poziciju dijamanta, tako da se on kreće lijevo-desno
    }


    void OnTriggerEnter(Collider collider) // funkcija koja se aktivira kada lopta dođe u kontakt s dijamantom
     {
        PS.Play();
        r.enabled = false;
        
        PlayerPrefs.SetInt("balance", PlayerPrefs.GetInt("balance") + 1);
        FindObjectOfType<ScoreText>().updateBalance();
        PlayerPrefs.SetInt("Total Coins", PlayerPrefs.GetInt("Total Coins") + 1);
     }
}
