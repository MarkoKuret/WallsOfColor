using System.Collections;
using UnityEngine;

public class follow : MonoBehaviour {

    /* Zbog optimizacije, lopta zapravo ne ide po beskonačnoj cesti, nego je cesta mala i pomiće se svako 2 sekunde. 
      ova skripta kontrolira poziciju ceste*/

    public Transform player;  // pozicija lopte
     public Transform ground;  // pozicija ceste

    IEnumerator GroundFollow()  // svako 2 sekunde pozicija ceste na osi Z se pomakne na istu poziciju (na osi Z) kao lopta
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            transform.position = new Vector3(0, 0, player.position.z);
        }    
    }

    void Start()
    {
        StartCoroutine(GroundFollow());
    }
}
