using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour {


    // Ova skripta kontrolira stvaranje prepreka

    public float lenghtBetweenObstacles = 25; // razmak između prepreka

    Vector3 lastPositionObstacle;  // pozicija posljednje prepreke


    float PosZspecial;       /* pozicija "specijalne" prepreke na osi Z, specijalne prepreke su prepreke 
                             koje se miču i prepreke koje mijenjaju boju lopti kada ona prođe kroz tu prepreku */
    float PosZ;   // pozicija kovanica na osi Z

    int[] dif = new int[] { 50, 75, 100 }; // moguće udaljenosti između dvije kovanice ili dvije posebne prepreke

    int randomDif;  
    int randomDifSpecial;
    
    public GameObject[] obstacles; // prepeke

    public GameObject coins;   // kovanice

    public GameObject[] obstaclesSpecial;  // posebne prepreke

    public bool Score10;   // istinit ako je rezultata 10 ili veći

    [SerializeField]
    Transform firstObstacle;   // prva prepreka 

    [SerializeField]
    Transform firstCoin;   // prva kovanica






    void SpawnObstacles()  // funkcija koja stvara prepreke
    {

        int RandomObstacle = Random.Range(0, 12);  // nasumična prepreka

        GameObject _object2 = Instantiate(obstacles[RandomObstacle] as GameObject);
        _object2.transform.position = lastPositionObstacle + new Vector3(0f, 0f, lenghtBetweenObstacles);
        lastPositionObstacle = _object2.transform.position;
    }

    void SpawnCoins()  // funkcija koja stvara kovanice
    {
        float PosX = Random.Range(-5, 5);  // pozicija kovanice na osi x
        int index = Random.Range(0, dif.Length);  // nasumičan broj između brojeva: 50, 75 i 100 
        randomDif = dif[index]; 
        GameObject _coin = Instantiate(coins as GameObject);
        _coin.transform.position = new Vector3(PosX, 1, PosZ + randomDif);
        PosZ = _coin.transform.position.z;

    }

    void SpawnSpecailObstacle()   // funkcja koja stvara posebne prepreke
    {
        
        if (Score10)  // posebne prepreke se mogu stvarati samo ako je trenutni rezultat 10 ili veći
        {
          
            int indexS = Random.Range(0, dif.Length);  // nasumičan broj između brojeva: 50, 75 i 100 
            int RandomObstacleMove = Random.Range(0, 2);     // odlucuje koji od dva tipa posebnih prepreka će stvoriti
            randomDifSpecial = dif[indexS];
            GameObject _obstacleMove = Instantiate(obstaclesSpecial[RandomObstacleMove] as GameObject);
            _obstacleMove.transform.position = new Vector3(0, 1, PosZspecial + randomDifSpecial);
            PosZspecial = _obstacleMove.transform.position.z;
        }
    }

    
    
        
    public void score10()  // ovu funkciju se zove iz skripte "ScoreText" ako je trenutni rezultat 10 ili veći
    {
        Score10 = true;
    }

    IEnumerator spawn()  // zove funkcije za stvaranje prepreka i kovanica svako dvije sekunde
    {
        while (true)
        {
            
            SpawnSpecailObstacle();
            SpawnObstacles();
            SpawnCoins();
            yield return new WaitForSeconds(2);

        }
    }


    void Start ()
    {
      

        lastPositionObstacle = firstObstacle.transform.position;
        PosZspecial = 260;
        PosZ = firstCoin.position.z;
        Score10 = false;
        StartCoroutine(spawn()); 

    }

}
