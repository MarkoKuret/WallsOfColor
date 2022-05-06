
using UnityEngine;

public class pokretnež : MonoBehaviour {

   // ova skripta kontrolira pokretne prepreke
  

    public MeshRenderer cube;    

    //palete materijala
    public Material[] material;
    public Material[] original;
    public Material[] fresh;
    public Material[] comp;
    public Material[] nature;

    void theme() //funkcija za kontrolu teme (palete materijala)
    {
        switch (PlayerPrefs.GetInt("tema"))
        {
            case 0:
                for (int i = 0; i < 4; i++)
                {
                    material[i] = original[i];
                }
                break;

            case 1:
                for (int i = 0; i < 4; i++)
                {
                    material[i] = fresh[i];
                }
                break;
            case 2:
                for (int i = 0; i < 4; i++)
                {
                    material[i] = comp[i];
                }
                break;
            case 3:
                for (int i = 0; i < 4; i++)
                {
                    material[i] = nature[i];
                }
                break;

        }
    }

    void Start()
    {
        theme();

        int color = Random.Range(0, 4);  // nasumično je odabrana boja prepreke
        switch (color)
        {
            case 0:
                cube.material = material[0];
                cube.tag = "Plava";
                break;
            case 1:
                cube.material = material[1];
                cube.tag = "Žuta";
                break;
            case 2:
                cube.material = material[2];
                cube.tag = "Zelena";
                break;
            case 3:
                cube.material = material[3];
                cube.tag = "Crvena";
                break;
                

        }
    }

    void Update () {
      transform.position = new Vector3(Mathf.PingPong(Time.time * 4, 10) - 5, transform.position.y, transform.position.z);
        // pomiće prepreku lijevo-desno
      
    }


}
