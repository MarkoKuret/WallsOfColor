
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour {


    /* Ova skripta je GameManager. 
      Glavni zadaci su joj da kontrolira sudbinu igrača nakon što napravi grešku u igri, prozor za pauzu igre*/

    public Text paused;        
    public Text lifeCounter; //tekst koji prikazuje broj života koje korisnik trenutno posjeduje
 

    public Toggle soundToggle;  //prekdač za zvuk (ON/OFF)

    public Button home;   //gumb za povratak na glavni izbornik
    public Button playOn; // gumb za nastavak igre

    public GameObject saveMePanel;  //prozor koji se otvori kada korisnik napravi grešku, a ima jedan život
    public GameObject pausePanel;  // prozor pauze

    bool GameOver = false; 
   
    public Image soundOff;   // slika za prekidač za zvuk
    public Image soundOn;    // slika za prekidač za zvuk
    

    public AudioSource click;  // zvuk pritiska botuna

    // palete (teme) boja
    public Color[] color;
    public Color[] original;
    public Color[] fresh;
    public Color[] comp;
    public Color[] nature;



    void theme() // kontrolira paletu boja koja se koristi
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


    public void buttonHome() // aktivira se na pritisak gumba za povratak na glavni izbornik
    {
         SceneManager.LoadScene(0);
    }

    void Start()
    {
      

        theme();
        lifeCounter.text = PlayerPrefs.GetInt("life", 0).ToString();

        if (PlayerPrefs.GetInt("sound") == 1)
        {
           soundToggle.isOn = true;
        }
        else
        {
           soundToggle.isOn = false;
        }
        
        Time.timeScale = 1;
        
        saveMePanel.SetActive(false);
        pausePanel.SetActive(false);

        int randomColor = Random.Range(0, 4);  // kontrolira boju korisničkog sučelja
       

                soundOn.color = color[randomColor];
                soundOff.color = color[randomColor];
                home.image.color = color[randomColor];
                playOn.image.color = color[randomColor];
                paused.color = color[randomColor];
             
              
    }



    public void Pause() // aktivira se na pritisak gumba za pauzu
    {
        Time.timeScale = 0; // pauzira igru
        click.Play(); // aktivira zvuk pritiska botuna
        pausePanel.SetActive(true); // otvara prozor za pauzu
    }

    public void PlayOn() // aktivira se na pritisak gumba za nastavak igre
    {
        click.Play(); // aktivira zvuk pritiska botuna
        pausePanel.SetActive(false); // zatvara prozor za pauzu
        
        Time.timeScale = 1; // nastavlja igru
    }

    public void Continue()  // aktivira se na pritisak gumba ,za nastavak igre,  na "saveMePanel2 ( na "prozoru za spašavanje" )
    {
        saveMePanel.SetActive(false); // zatvara prozor za spašavanje
        Time.timeScale = 1; // nastavlja igru
        PlayerPrefs.SetInt("life", PlayerPrefs.GetInt("life") - 1); // oduzima jedan život 
        lifeCounter.text = PlayerPrefs.GetInt("life", 0).ToString();
    }

    public void EndGame()  // funkcija se zove iz skripte "PlayerMove"
   {
            if (GameOver == false)  // ako igra i dalje traje
            {
                if (PlayerPrefs.GetInt("life", 0) < 1) // ako korisnik nema život
                {
                    FindObjectOfType<PlayerMove>().speed = 2;  //brzina lopte = 2
                    FindObjectOfType<PlayerMove>().slusaj = false; // korisnik više nemože kontrolirat loptu
                    GameOver = true;  // igra je gotova

               
                    Invoke("Restart", 2); // zove funkciju "Restart" za dvije sekunde
                }
                else // inače (ako korisnik ima život)
                {
                Time.timeScale = 0; // pauzira igru
               
                 saveMePanel.SetActive(true); // otvara prozor za spašavanje ("saveMePanel") gdje ima priliku nastaviti igru (u zamjenu za jedan život)
                
                }
            }
   }

    

    public void directEnd()  // funkcija za direktan kraj se aktivira ako korisnik (lopta) padne s ruba igre
    {
        FindObjectOfType<PlayerMove>().speed = 2; // uspori kretanje lopte
       
        GameOver = true; // igra je gotova

        Invoke("Restart", 2);  // zove funkciju "Restart" za dvije sekunde
    }



    public void SoundToggle() // aktivira se kada korisnik pritisne 0N/OFF prekidč za zvuk
    {
        AudioListener.pause = !AudioListener.pause;
        if (AudioListener.pause == false)
        {
            PlayerPrefs.SetInt("sound", 1);
        }
        else
        {
            PlayerPrefs.SetInt("sound", 0);
        }

    }

    public void Restart() // učita scenu 1 iz početka (restarta igru)
    {
        SceneManager.LoadScene(1);
    }

}
