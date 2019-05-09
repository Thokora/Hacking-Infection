using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ForPlayer : MonoBehaviour
{

    public int IsGameOver = 0;
    private GameObject IsCamera;
    private MoveCamera sIsCamera;
    private float CamSpeed;
    private float SaveHeight;
    public Image EffectsToGO;
    public GameObject Fire;

    public AudioClip ToGameOver;

    [Header("Fawer Changes")]

    public static bool DieFlicker;
    public static bool Inmunidad;

    public GameObject Player;

    [SerializeField, Range(0.0f, 0.1f)]
    public float suctionForce;

    [SerializeField, Range(0.0f, 1f)]
    public float AntiSuctionValue;

    public GameObject antiSuctionButton;
    Animator animPropulsores;

    Rigidbody PlayerRB;
    bool suction;

    public GameObject flickerShield;

    int myScore = 0;
    public Text ScoreText;

    int counterObjects;
    public Text ItemText;

    public static bool BulletImpactEnemy;


    void Start()
    {
        DieFlicker = false;

        IsCamera = GameObject.Find("Main Camera");
        sIsCamera = IsCamera.GetComponent<MoveCamera>();
        CamSpeed = sIsCamera.SpeedOfPlayer;
        SaveHeight = sIsCamera.HeightOfCam;

        PlayerRB = Player.GetComponent<Rigidbody>();
        animPropulsores = antiSuctionButton.GetComponent<Animator>();
        animPropulsores.SetBool("Propulsores", false);
        flickerShield.SetActive(false);
        myScore = 0;

        StartCoroutine(FlickerIsDead());
    }
    
    IEnumerator FlickerIsDead()
    {
        Debug.Log("Aun no es true");
        yield return new WaitUntil(() => DieFlicker == true);
        Debug.Log("Ya es true");

        CamSpeed *= 0.7f;
        IsGameOver = 1;

        StartCoroutine("ProcessOfGO");
    }

//#if UNITY_STANDALONE

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) //Escudo
        {
            ButtonShield();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) //Propulsores
        {
            ButtonSuction();
        }
        if (BulletImpactEnemy == true)
        {
            Debug.Log("entro la bala");
            Score();
        }
    }

//#endif
    void Score()
    {
        myScore++;
        ScoreText.text = "Score: " + myScore;
        BulletImpactEnemy = false;
    }

    IEnumerator ResetHigh()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        if (suction == false)
        {
            float resettingHigh = Player.transform.position.y;
            if (Player.transform.position.y < 2.0f)
            {
                resettingHigh += 0.1f;
                Player.transform.position = new Vector3(Player.transform.position.x, resettingHigh, Player.transform.position.z);

                StartCoroutine(ResetHigh());
            }
            else if (Player.transform.position.y > 2.0f)
            {
                Player.transform.position = new Vector3(Player.transform.position.x, 2.0f, Player.transform.position.z);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collectible")
        {
            counterObjects++;
            ItemText.text = "Items: " + counterObjects;

            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "BulletFromEnemy")
        {

            if (Inmunidad == false)
            {
                Debug.Log("Mato a flicker, pero se debe cambiar por bajar puntos"); //cambiar por bajar puntos de vida
                DieFlicker = true;
            }

        }

    }


    void OnTriggerStay(Collider other)
    {
        if (IsGameOver == 0)
        {
            if (other.gameObject.tag == "GOisDeep")
            {
                Fire.SetActive(true);
                CamSpeed *= 1.3f;
                

                animPropulsores.SetBool("Propulsores", true);

                if (Player.transform.position.y < -0.5)
                {
                    DieFlicker = true;
                }
                else
                {
                    suction = true;
                    StartCoroutine(SuctionBehaviour());
                }
            }
            if (other.gameObject.tag == "GOisiObjects" || other.gameObject.tag == "CrystallsRed") 
            {
                DieFlicker = true;
            }

            if (other.gameObject.tag == "Ennemy") //esto es lo que hace que muera..si toca algo cone este tag
            {
                if (Inmunidad == false)
                {
                    DieFlicker = true;
                }
            }
        }
        else
        {
            CamSpeed *= 0.7f;

            StartCoroutine("ProcessOfGO");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsGameOver == 0)
        {
            if (other.gameObject.tag == "GOisDeep")
            {

                //anim
                animPropulsores.SetBool("Propulsores", false);

                if (Player.transform.position.y > -0.5)
                {
                    suction = false;
                    Player.transform.position = new Vector3(Player.transform.position.x, 2.0f, Player.transform.position.z);
                    StartCoroutine(ResetHigh());
                }
                else
                {
                    DieFlicker = true;
                }

            }
        }
    }


    IEnumerator SuctionBehaviour()
    {
        if (suction)
        {
            yield return new WaitForSecondsRealtime(1f);
            Player.transform.position = new Vector3(Player.transform.position.x, (Player.transform.position.y - suctionForce), Player.transform.position.z);
        }
        else
        {
            Player.transform.position = new Vector3(Player.transform.position.x, 2.0f, Player.transform.position.z);
        }

    }

    public void ButtonSuction()
    {
        float AntiSuction = Player.transform.position.y;

        if (Player.transform.position.y > -0.5 && Player.transform.position.y < 2.0f)
        {
            AntiSuction += AntiSuctionValue;
            Player.transform.position = new Vector3(Player.transform.position.x, AntiSuction, Player.transform.position.z);
        }
    }

    public void ButtonShield()
    {
        flickerShield.SetActive(true);
    }

    IEnumerator ProcessOfGO()
    {
        sIsCamera.enabled = false;
        gameObject.GetComponent<AudioSource>().PlayOneShot(ToGameOver);
        EffectsToGO.enabled = true;
        int Point = 0;
        //Transform gIsCamera = IsCamera.transform;
        while (Point == 0)
        {
            if (CamSpeed > 0.05f || CamSpeed < -0.05f)
            {
                if (EffectsToGO.color.a < 1)
                {
                    EffectsToGO.color = new Color(1, 1, 1, EffectsToGO.color.a + 0.2f);
                }
                CamSpeed = CamSpeed * 0.98f;
                //gIsCamera.Translate(0,0,CamSpeed);
                //gIsCamera.position = new Vector3 (gIsCamera.position.x,SaveHeight,gIsCamera.position.z);
            }
            else
            {
                Point = 1;
                GameObject.Find("Reset Panel").GetComponent<Reset>().StartCoroutine("ProcessToReset");
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
