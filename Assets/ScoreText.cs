
using UnityEngine;
using UnityEngine.UI;


public class ScoreText : MonoBehaviour {

    // ova skripta kontrolira rezultate i prati najbolji rezultat
    public Text score;  // tekst trenutnog rezultata
    public Text highScore;  // tekst najboljeg rezultata
    public Text bankBalance;  // tekst količine novaca koju korisnik trenutno posjeduje
    int t;    //prati boju lopte i mijenja se ovisno o njoj 

    

    public int Score = 0;  // trenutni rezultat

    

   
    // palete boja
    public Color[] color;
    public Color[] original;
    public Color[] fresh;
    public Color[] comp;
    public Color[] nature;

    void theme()  // kontrolira koja paleta boja se koristi)
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

    void Start()
    {

        theme();  
        updateBalance();  
        highScore.text = PlayerPrefs.GetInt("High Score", 0).ToString();  // zapisuje najbolji rezultat
    }


    public void updateBalance()  // ažurira tekst bankBalance
    {
        bankBalance.text = PlayerPrefs.GetInt("balance").ToString();
    }



    public void updateScore()  // kontrolra rezultat i tekst rezultata ( i boju tog teksta)
    {
        Score = Score + 1;  //ažurira trenutni rezultat (povečava za 1 bod)
        score.text = Score.ToString(); // ažurira tekst trenutnog rezultata
        t = FindObjectOfType<PlayerMove>().index; // postavlja int t, ovisno o boji lopte
        score.color = color[t];  // postavlja boju teksta da bude ista kao i boja lopte

        if (Score > PlayerPrefs.GetInt("High Score", 0))  // ako je trenutni rezultat veći od najboljeg rezultata,
        {                                               
            PlayerPrefs.SetInt("High Score", Score);  // ažuria najbolji rezultat da bude jednak trenutnog rezultata
            highScore.text = score.text; // ažuria tekst najboljeg rezultata

        }

        if (Score == 10) // ako je trenutni rezultat veći od 10
        {
            FindObjectOfType<Spawn>().score10(); // aktivira funkciju u skripti "Spawn"
        }
    }

    //sljedeće 4 funkcije se zovu iz skripte "PlayerMove" i kontroliraju boju teksta trenutnog rezultata
    public void Plava() 
    {
        score.color = color[0];

    }
    

    public void Crvena()
    {
        score.color = color[3];

    }

    public void Žuta()
    {
        score.color = color[1];


    }

    public void Zelena()
    {
        score.color = color[2];

    }


    

}
