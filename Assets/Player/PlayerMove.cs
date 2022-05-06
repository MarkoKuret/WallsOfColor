
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

   /*Ova skripta kontrolora ponašanje same loptice.
     Kontrolira kretanje, umiranje i boju loptice.
     */
   
    public Rigidbody rb;                       
    public MeshRenderer mr;

    //audio koji se aktivira kada igrač: prođe prepreku, skupi novac ili udari u krivi dio prepreke.
    public AudioSource prolaz;
    public AudioSource CoinPickUp;
    public AudioSource fail;
   

    float Timer; //koristi se za mjerenje vremena
    public float speed = 10; // početna brzina loptice
    float ubrzanje = 0.1f;  // iznos za koji loptica ubrza u jednoj sekundi
    float topSpeed = 25f; // maksimalna moguća brzina loptice

    public bool slusaj;   // kada je istinit igrica omogućava korisniku promjenu smjera loptice ("sluša" naredbe) 
    bool startUbrzanje;  //  kada je istinit ubrzanje kretanja lopte započinje
   
  
    public int index;  // index označava trenutnu boju lopte npr. index = 0 je za plavu boju 

    public string currentColor; // također označava trenutnu boju lopte (korišten je i string zbog lakšeg snalaženja)

    //postoji više skupina materijala jer postoji više mogućih tema (paleta boja i materijala)
    public Material[] material;
    public Material[] original;
    public Material[] fresh;
    public Material[] comp;
    public Material[] nature;

    private Vector3 dir; // vektor3 korišten za kretnju loptice



    void OnTriggerEnter(Collider col) // kontrolira što se dogodi nakon sudara lopte i nekog drugog predmeta
    {
        if (col.tag != currentColor && col.tag != currentColor + "CC") // ako sudar nije s predmetom iste boje kao i lopta ili CC
                                                                      //CC je kratica za color changer, to jest prepreku koja mijenja boju lopte
        {
            if (col.tag != "Coin") // ako predmet s kojim se sudarimo nije ni kovanica
            {
                if (Time.time - Timer > 0.2f) // kontrolira da vremenska razlika između dva sudara nije veća od 0.2 sekunde
                {                             // to eliminira mogučnost "lažnog" dvostrukog kontakta 
                    FindObjectOfType<GameManager>().EndGame(); /* funkcija EndGame odlucuje o sudbini igraca ("jeli igra gotova za njega?")
                                                                   ta funkcija se nalazi u skripti "GameManager"*/

                    Timer = Time.time;
                    fail.Play();
                }
            }
            else if (col.tag == "Coin") // ako se "sudarimo" s kovanicom 
            {
                CoinPickUp.Play(); 
            }
            
        }
        else
        {
            FindObjectOfType<ScoreText>().updateScore(); //  aktivira funkciju u skripti "ScoreText" koja ažurira rezultat
            prolaz.Play();
        }
    }

    

    public void ColorChanger(int ColorChange) // kontrolira boju igrača (lopte) 
    {

        index = ColorChange;
        mr.material = material[ColorChange];
        switch (ColorChange)
        {
            case 0:
                currentColor = "Plava";
               
                FindObjectOfType<ScoreText>().Plava(); //zove funkciju iz skripte "ScoreText" koja kontrolira boju teksta rezultata
                break;
            case 1:
                currentColor = "Žuta";
             
                FindObjectOfType<ScoreText>().Žuta();  //zove funkciju iz skripte "ScoreText" koja kontrolira boju teksta rezultata
                break;
            case 2:
                currentColor = "Zelena";
               
                FindObjectOfType<ScoreText>().Zelena();  //zove funkciju iz skripte "ScoreText" koja kontrolira boju teksta rezultata
                break;
            case 3:
                currentColor = "Crvena";
               
                FindObjectOfType<ScoreText>().Crvena();  //zove funkciju iz skripte "ScoreText" koja kontrolira boju teksta rezultata
                break;
        }

    }

    void theme() // kontrolira paletu materijala koja se koristi
  
    {
        switch (PlayerPrefs.GetInt("tema")) // int tema ovisi o tome koju je paletu (temu) korisnik odabrao u trgovini (fiktivna trgovina u igri)
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
        theme(); // zove funkciju koja kontrolira palete materijala
        index = Random.Range(0, 4); // index je nasumično odabran 
        ColorChanger(index);        // zove funkciju koja kontrolira boju lopte
        Application.targetFrameRate = 60;  // postavvlja zadani FPS (broj slićica u sekundi)
        speed = 10;  // brzina lopte na početku igre
        dir = Vector3.zero;  // smjer na početku igre (lopta se ne krece)
       
        Timer = Time.time;  // resetira tajmer
        PlayerPrefs.SetInt("Games Played", PlayerPrefs.GetInt("Games Played") + 1); //povecava broj odigranih igara za jedan, i sprema taj podatak
        slusaj = true;  // korisnik može kontrolirati loptu
        startUbrzanje = false;  //ubrzanje još nije počelo

    }

    
    

    void Update()
    {
        
        if (slusaj) // samo ako je "slusaj" istinito korisnik može kontrolirati loptu
        {
            if (Input.GetMouseButtonDown(0)) // kontrlira što se dogodi nakon što korisnik dodirne ekran
            {
                if (dir == Vector3.forward) // ako je smjer kretnje lopte desno, lopta nakon dodira ide lijevo (i obrnuto).
                {                           // forward je zapravo desno, jer je lopta zarotirana 45 stupnjeva u desno
                    dir = Vector3.left;
                }

                else
                {
                    dir = Vector3.forward;
                }
                startUbrzanje = true; // započinje ubrzavajne lopte
                
            }
        }
        float amoutToMove = speed * Time.deltaTime; /* određuje koliko će se lopta pomaknuti (množi se s Time.deltaTime da kolicina pomaka 
                                                       ne bi ovisila o trenutnom FPS-u) */
        if (startUbrzanje)  // kontrolira brzinu lopte                         
        {
            speed += Time.deltaTime * ubrzanje; // svake sekunde lopta ubrza za 0.1
        }

        if  (speed > topSpeed) // funkija koja limitira brzinu lopte
        {
            speed = topSpeed;
        }

        transform.Translate(dir * amoutToMove); // pomiče loptu u zadanom smjeru za zadanu količinu

        if (rb.position.x <= -7.5 || rb.position.x >= 7.5) // kontrolira događaje su slučaju da lopta pređe rub ceste
        {
            slusaj = false; // oduzima kontrolu korisniku
            FindObjectOfType<GameManager>().directEnd();

        }


    }
}


 

    
    

