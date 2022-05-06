using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CC : MonoBehaviour
{
    // ova skripta kontrolira CC (ColorChanger) prepreke, to jest prepreke koje mijenjaju boju lopti nakon što lopta pređe preko njih 

    // palete korištenih materjala
    public Material[] material;
    public Material[] original;
    public Material[] fresh;
    public Material[] comp;
    public Material[] nature;

    int index; // ovisi o boji lopte
    public MeshRenderer cubeCC;

    Coroutine co;

    int color;

    string PlayerColor;

    void theme() // kontrolira paletu materijala koji se trenutno koriste
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



    IEnumerator updateColor() // namiješta boju CC prepreke tako da nikada ne može biti ista kao i trenutna boja lopte
    {
        while (true)
        {
            index = FindObjectOfType<PlayerMove>().index;
            if (color == index)
            {
                if (color == 3)
                {
                    color = 2;
                }
                else
                {
                    color = color + 1;
                }
            }
            cubeCC.material = material[color];
           
            switch (color)
            {
                case 0:
                    
                    cubeCC.tag = "PlavaCC";
                    
                    break;
                case 1:
                   
                
                    cubeCC.tag = "ŽutaCC";
                    break;
                case 2:
                    
                    cubeCC.tag = "ZelenaCC";
                    break;
                case 3:
                    
                    cubeCC.tag = "CrvenaCC";
                    break;
            }
            yield return new WaitForSeconds(2);
        }
    }

void Start()
    {
        theme();
        color = Random.Range(0, 4);
       co = StartCoroutine(updateColor());
        



       

         
           

    }
     
    void OnTriggerEnter(Collider other)
    {
        StopCoroutine(co);
        FindObjectOfType<PlayerMove>().ColorChanger(color);
    }
}
