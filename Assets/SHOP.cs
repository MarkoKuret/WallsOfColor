using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SHOP : MonoBehaviour {

    // ova skripta kontrolira trgovinu u igrici, u trgovini se mogu kupiti životi i palete boja (teme)

    public Toggle CANDY;
    public Toggle NATURE;
    public Toggle FRESH;
    public Toggle ORIGINAL;

    public AudioSource fail;
    public AudioSource spend;

    int balance; // količina novaca koju korisnik posjeduje

    public Text Balance; // tekst koji prikazuje trenutnu količinu novaca koju korisnik posjeduje
    public Text lifeCount;  //  tekst koji prikazuje trenutnu količinu života koju korisnik posjeduje


    public GameObject[] cost; // slika cijene predmeta
   

    public void BACK() // funkcija povezana s gumbom za povratak na glavni izbornik
    {
        
        SceneManager.LoadScene(0);
    }

    void Start()
    {
       
        Balance.text = PlayerPrefs.GetInt("balance", 0).ToString(); // ažurira tekst
        lifeCount.text = PlayerPrefs.GetInt("life", 0).ToString();  // ažurira tekst

        switch (PlayerPrefs.GetInt("tema")) // provjerava koja paleta boja se trenutno koristi, i ovisno o tome
        {                                   // odlučuje koji su prekidači upaljeni (samo jedan prekidač može biti upaljen u isto vrijeme)
            case 0:
                ORIGINAL.isOn = true;
                break;

            case 1:
                FRESH.isOn = true;
                break;
            case 2:
                CANDY.isOn = true;
                break;
            case 3:
                NATURE.isOn = true;
                break;
        }

        // sljedeče funkcije određuju ako je korisnik kupio određenu paletu boja, da se ne prikazuje slika s cijenom te palete
        if (PlayerPrefs.GetInt("fresh") == 1) 
        {
            cost[0].SetActive(false);
        }

        if (PlayerPrefs.GetInt("candy") == 1)
        {
            cost[1].SetActive(false);
        }

        if (PlayerPrefs.GetInt("nature") == 1)
        {
            cost[2].SetActive(false);
        }
    

    }

   
    public void OriginalTema()  // određuje što ako korisnik pritisne prekidač za odabir palete ORIGINAL, nju korisnik posjeduje od početka
    {
        PlayerPrefs.SetInt("tema", 0);
    }

    public void FreshTema()// određuje što ako korisnik pritisne prekidač za odabir palete FRESH
    {
        if (PlayerPrefs.GetInt("fresh") == 1)  // ako je korisnik već kupio ovu paletu
        {
            PlayerPrefs.SetInt("tema", 1);
         
        }
        else  // ako paleta još nije kupljena
        {
            if ((PlayerPrefs.GetInt("balance", 0) >= 30))
            {
                PlayerPrefs.SetInt("balance", PlayerPrefs.GetInt("balance") - 30);
                spend.Play();
                Balance.text = PlayerPrefs.GetInt("balance", 0).ToString();
                PlayerPrefs.SetInt("tema", 1);
                PlayerPrefs.SetInt("fresh", 1);
                cost[0].SetActive(false);
            }
            else // ako korisnik nema dovoljno novaca za kupiti paletu
            {
                fail.Play();
            }
        }

    }
    
    public void CandyTema() // određuje što ako korisnik pritisne prekidač za odabir palete CANDY
    {

        if (PlayerPrefs.GetInt("candy") == 1) // ako je korisnik već kupio ovu paletu
        {
            PlayerPrefs.SetInt("tema", 2);
         
        }
        else  // ako paleta još nije kupljena
        {
            if ((PlayerPrefs.GetInt("balance", 0) >= 30))
            {
                PlayerPrefs.SetInt("balance", PlayerPrefs.GetInt("balance") - 30);
                spend.Play();
                Balance.text = PlayerPrefs.GetInt("balance", 0).ToString();
                PlayerPrefs.SetInt("tema", 2);
                PlayerPrefs.SetInt("candy", 1);
                cost[1].SetActive(false);
            }
            else  // ako korisnik nema dovoljno novaca za kupiti paletu
            {
                fail.Play();
            }
        }

    }

    public void NatureTema()  // određuje što ako korisnik pritisne prekidač za odabir palete NATURE
    {

        if (PlayerPrefs.GetInt("nature") == 1)  // ako je korisnik već kupio ovu paletu
        {
            PlayerPrefs.SetInt("tema", 3);
          
        }
        else // ako paleta još nije kupljena
        {
            if ((PlayerPrefs.GetInt("balance", 0) >= 30))
            {
                PlayerPrefs.SetInt("balance", PlayerPrefs.GetInt("balance") - 30);
                spend.Play();
                Balance.text = PlayerPrefs.GetInt("balance", 0).ToString();
                PlayerPrefs.SetInt("tema", 3);
                PlayerPrefs.SetInt("nature", 1);
                cost[2].SetActive(false);
            }
            else // ako korisnik nema dovoljno novaca za kupiti paletu
            {
                fail.Play();
            }
        }

    }

    public void buyLife() // ova funkcija se aktivira kada korisnik klikne gumb za kupovinu životas
    {
        if (PlayerPrefs.GetInt("balance", 0) >= 15) // ako korisnik ima 15 ili više kovanica
        {
            if (PlayerPrefs.GetInt("life", 0) < 1) // ako nema ni jedan život
            {
                spend.Play();
                PlayerPrefs.SetInt("balance", PlayerPrefs.GetInt("balance") - 15);
                
                PlayerPrefs.SetInt("life", PlayerPrefs.GetInt("life") + 1);
                Balance.text = PlayerPrefs.GetInt("balance", 0).ToString();
                lifeCount.text = PlayerPrefs.GetInt("life", 0).ToString();
            }
            else  // ako korisnik nema dovoljno novaca za kupiti život, ili već ima jedan život
            {
                fail.Play(); 
            }
        }
    }
}
