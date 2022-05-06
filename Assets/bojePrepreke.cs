using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bojePrepreke : MonoBehaviour {

    // ova skripta je povezana sa preprkama i kontrolira njihovu boju

    // meshRenderer-i za četiri dijela prepreke
    public MeshRenderer CubeP;
    public MeshRenderer CubeŽ;
    public MeshRenderer CubeZ;
    public MeshRenderer CubeC;


    // palete boja (materijala)
    public Material[] material;
    public Material[] original;
    public Material[] fresh;
    public Material[] comp;
    public Material[] nature;

    void theme() // provjerava koja se paleta boja koristi
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
    void Start () {


        theme();

        //namješta svaki dio prepreke u svoju boju
        CubeP.material = material[0];
        CubeŽ.material = material[1];
        CubeZ.material = material[2];
        CubeC.material = material[3];

    }
	
	
	
}
