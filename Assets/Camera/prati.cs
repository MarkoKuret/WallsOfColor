using System.Collections;
using UnityEngine;

public class prati : MonoBehaviour {


    /* ova skripta kontrolira kameru, glavna funkcija joj je da kamera preti igrača (loptu),
   i da mijenja boju pozadine kamere */

    public Transform player;     // pozicja lopte
    public float offsetY;  // offset kamere od lopte na osi Y
    private float offsetZ = 5;  // offset kamere od lopte na osi Z
    float t;
    float duration = 3f;
    public Camera cam;   // glavna i jedina kamera

    Color colors; 
    Color colors1;

    // palete boja
    public Color[] color;
    public Color[] original;
    public Color[] fresh;
    public Color[] comp;
    public Color[] nature;


    private void Start()
    {
        theme();
        StartCoroutine(boja()); 
        
        if (Camera.main.aspect >= 0.5625) // kontrolira offset kamere na osi Y, ovisno o omjeru ekrana
            offsetY = 30;
        else if (Camera.main.aspect >= 0.5)
            offsetY = 33;
        else
            offsetY = 30;
        cam.clearFlags = CameraClearFlags.SolidColor;
    }

    void theme() //kontrolira paletu boja koja se koristi
    {
        switch (PlayerPrefs.GetInt("tema"))
        {
            case 0:
                for (int i = 0; i < 4; i++)
                {
                    color[i] = original[i];
                }
                break;

            case 1:
                for (int i = 0; i < 4; i++)
                {
                    color[i] = fresh[i];
                }
                break;
            case 2:
                for (int i = 0; i < 4; i++)
                {
                    color[i] = comp[i];
                }
                break;
            case 3:
                for (int i = 0; i < 4; i++)
                {
                    color[i] = nature[i];
                }
                break;

        }
    }

    IEnumerator boja()  // kontrolira boju1 i boju2, koje utječu na boju pozadine 
    {
        while (true)
        {

            colors = color[0];
            colors1 = color[1]; 
            yield return new WaitForSeconds(6);
            colors = color[2];
            colors1 = color[1];
            yield return new WaitForSeconds(6);
            colors = color[2];
            colors1 = color[3];
            yield return new WaitForSeconds(6);
            colors = color[0];
            colors1 = color[3];
            yield return new WaitForSeconds(6);

        }
    }
    void Update () {

       
        transform.position =new Vector3(transform.position.x, offsetY, player.position.z - offsetZ); // kontrolira poziciju kamere
        t = Mathf.PingPong(Time.time, duration) / duration;
        cam.backgroundColor = Color.Lerp(colors, colors1, t); // polako mijenja boju pozadine kamere
       
	}

}
