using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShotCentre : MonoBehaviour {
	[Header("Main")]
	public LayerMask ShotInputLayer; // Layer if objects
	public LayerMask ShotInputLayerAdd; // Layer if objects
	public int MaxDistanceOfShot;
	public GameObject ExplosionInTouch;
	public GameObject CubeInTouch;
	public Transform ContainerForCreated;
	[Header("Additional")]
	public Image LineFon;
	public Image LineBack;

	private GameObject FutureExplosion;
	private GameObject FutureCube;
	private bool AccessToTouch = true; // Can I Shot?
	private RaycastHit hit; // Create RayToSpace
	private Vector3 DotOfShot; // Point where we must create explosion

    [Header("Fawer Changes")]
    [SerializeField, Range(0, 20)]
    float m_launchIntensity;

    [SerializeField]
    GameObject m_projectile;

    [SerializeField]
    GameObject Player;

    public static bool esperar; //LINEA AGREGADA POR FAWER


    public void Touch(){
		if (AccessToTouch) {
			Ray ray = GetComponent<Camera> ().ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, MaxDistanceOfShot, ShotInputLayer)) {
				DotOfShot = hit.point;

                StartCoroutine("CreateAll");
			}
		}
    }

//#if UNITY_STANDALONE
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.C)) //Disparar
        {
            ButtonDisparar();
        }

    }
    //#endif

    public void ButtonDisparar()
    {
        StartCoroutine("CreateAll");
    }

    public void Fire() //Funcion agregada por Fawer
    {
        GameObject newProjectile = Instantiate(m_projectile, Player.transform.position, Player.transform.rotation) as GameObject;

        if (newProjectile.GetComponent<Rigidbody2D>())
            newProjectile.GetComponent<Rigidbody2D>().AddForce(Player.transform.forward * m_launchIntensity, ForceMode2D.Impulse);
        else if (newProjectile.GetComponent<Rigidbody>())
            newProjectile.GetComponent<Rigidbody>().AddForce(Player.transform.forward * m_launchIntensity, ForceMode.Impulse);
    }

    IEnumerator CreateAll () {
        Fire();
        Debug.Log("entra");
        yield return new WaitUntil(() => esperar == true); //LINEA AGREGADA POR FAWER
        esperar = false;


        AccessToTouch = false;
		FutureExplosion = Instantiate (ExplosionInTouch);
		FutureExplosion.transform.position = DotOfShot;
		FutureExplosion.transform.SetParent (ContainerForCreated);
		FutureExplosion.SetActive (true);

		LineFon.enabled = true;
		LineBack.enabled = true;
		LineFon.color = new Color (0,0,0,0.4f);
		LineBack.color = new Color (0,0,0,0.8f);
		LineBack.fillAmount = 0;
		int Point = 0;
		while (Point == 0) {
			if (LineBack.fillAmount < 1) {
				LineBack.fillAmount += 0.02f;
			} else 
            {
				if (LineBack.color.a > 0) {
					LineBack.color = new Color (0, 0, 0, LineBack.color.a - 0.1f);
					LineFon.color = new Color (0, 0, 0, LineFon.color.a - 0.05f);
				} else {
					LineFon.enabled = false;
					LineBack.enabled = false;
					Point = 1;
				}
			}
			yield return new WaitForFixedUpdate ();
		}
		AccessToTouch = true;
	}
}
