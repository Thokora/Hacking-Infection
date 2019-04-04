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
    public GameObject Player;

    [SerializeField, Range(0.0f, 0.1f)]
    public float suctionForce;

    public GameObject antiSuctionButton;

    Rigidbody PlayerRB;
    bool suction;



    void Start()
    {
        IsCamera = GameObject.Find("Main Camera");
        sIsCamera = IsCamera.GetComponent<MoveCamera>();
        CamSpeed = sIsCamera.SpeedOfPlayer;
        SaveHeight = sIsCamera.HeightOfCam;

        PlayerRB = Player.GetComponent<Rigidbody>();
        antiSuctionButton.SetActive(false);
        //StartCoroutine(FlickerIsDead());
    }
    /*
    IEnumerator FlickerIsDead()
    {
        Debug.Log("Aun no es true");
        yield return new WaitUntil(() => MoveBulletShooter.deathFlicker == true);
        Debug.Log("Ya es true");

        CamSpeed *= 0.7f;

        StartCoroutine("ProcessOfGO");
    }*/

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

    void OnTriggerStay(Collider other)
    {
        if (IsGameOver == 0)
        {
            if (other.gameObject.tag == "GOisDeep")
            {
                Fire.SetActive(true);
                CamSpeed *= 1.3f;

                antiSuctionButton.SetActive(true);

                if (Player.transform.position.y < -0.5)
                {
                    IsGameOver = 1;
                    StartCoroutine("ProcessOfGO");
                }
                else
                {
                    suction = true;
                    StartCoroutine(SuctionBehaviour());
                }
            }
            if (other.gameObject.tag == "GOisiObjects" || other.gameObject.tag == "CrystallsRed" || other.gameObject.tag == "Ennemy")
            {
                CamSpeed *= 0.7f;
                IsGameOver = 1;

                StartCoroutine("ProcessOfGO");
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

                antiSuctionButton.SetActive(false);

                if (Player.transform.position.y > -0.5)
                {
                    suction = false;
                    Player.transform.position = new Vector3(Player.transform.position.x, 2.0f, Player.transform.position.z);
                    StartCoroutine(ResetHigh());
                }
                else
                {
                    IsGameOver = 1;
                    StartCoroutine("ProcessOfGO");
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
            AntiSuction += 0.1f;
            Player.transform.position = new Vector3(Player.transform.position.x, AntiSuction, Player.transform.position.z);
        }
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
