using GooglePlayGames;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
// ova skripta kontrolira glavni izbornik
    public AudioSource click;

    public GameObject statsPanel; //prozor za statistiku
    public GameObject howToPanel;  // prozor s uputama za igranje

    // sve boje korištene
    public Color[] color;
    public Color[] original;
    public Color[] fresh;
    public Color[] comp;
    public Color[] nature;

    public GameObject playCircle; // krug koji se vrti u sredini glavnog izbornika

    public Button[] button; //svi botuni na glavnom izborniku
    public Text[] text;

    // sljedeće slike su za kocke na vrhu ekrana u glavnom izborniku i za djelove kruga koji čine krug u sredini ("playCircle")
    public RawImage kocka1;
    public RawImage kocka2;
    public RawImage kocka3;
    public RawImage kocka4;

    public RawImage circle1;
    public RawImage circle2;
    public RawImage circle3;
    public RawImage circle4;

    void Update()
    {
     
        playCircle.transform.Rotate(0f, 0f, -5); // rotira krug u sredini glavnog izbornika
    }


    void Stats() // funkcija koja namješta tekst koji prikazuje statistiku kada se otvori prozor statistike (stats)
    {
        text[1].text = PlayerPrefs.GetInt("High Score").ToString();
        text[2].text = PlayerPrefs.GetInt("Total Coins").ToString();
        text[3].text = PlayerPrefs.GetInt("Games Played").ToString();
    }

    void theme() // funkcija koja kontrolira koje palete boja će se koristiti
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

    private void Start()

        
    {
        statsPanel.SetActive(false);
        howToPanel.SetActive(false);

        theme();
        Stats();


        kocka1.color = color[3];
        kocka2.color = color[1];
        kocka3.color = color[0];
        kocka4.color = color[2];
        circle1.color = color[0];
        circle2.color = color[1];
        circle3.color = color[2];
        circle4.color = color[3];





        if (PlayerPrefs.GetInt("sound") == 1) //ako je gumb za zvuk uključen
        {
           AudioListener.pause = false;
        }
        else 
        {
           AudioListener.pause = true;
        }
       
        PlayGamesPlatform.Activate();
       
        text[0].text = PlayerPrefs.GetInt("High Score").ToString();
        int randomColor = Random.Range(0, 4); // odlučuje jednu nasumičnu boju od četiri boje iz odabrane palete
        for (int i = 0; i < 5; i++) // namješta boju slika korištenih na glavnom izborniku
        {
            button[i].image.color = color[randomColor];
            text[i].color = color[randomColor];
        }
        for (int i = 0; i < 6; i++) // namješta tekst na glavnom izborniku i njegovim prozorima
        {
            
            text[i].color = color[randomColor];
        }

    }

    public void spajanje() // funkcija koja kontrolira spajanje na google play usluge
    {
        Social.localUser.Authenticate((bool success) => {
           if (success)
            {
                Social.ShowLeaderboardUI();
                Social.ReportScore(PlayerPrefs.GetInt("High Score"), WOCGPS.leaderboard_highscore, (bool uspijeh) =>
                {

                });

            }
           else
            {

            }
        });
    }

    public void playGame() // aktivira se kada je glavni botun pritisnut i započinje igru
    {
      
        SceneManager.LoadScene(1);
        
    }

    public void stats() // aktivira se  kada je botun za statistiku pritisnut
    {
        statsPanel.SetActive(true); // otvara prozor za statistiku
        click.Play(); // aktivira zvuk dodira
    }

    public void back() // aktivira se  kada je botun za izlaz s otvorenog panela pritisnut
    {
        statsPanel.SetActive(false);
        howToPanel.SetActive(false);
        click.Play();  // aktivira zvuk dodira
    }

    public void buttonSHOP() // aktivira se  kada je botun za trgovinu
    {
      
        SceneManager.LoadScene(2); // otvara trgovinu
        
    }

    public void buttonHowTo() // aktivira se  kada je botun za upute za igru
    {
        click.Play();  // aktivira zvuk dodira
        howToPanel.SetActive(true);  // otvara prozor s uputama z aigru
    }
}
