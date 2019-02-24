using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveCamera : MonoBehaviour {
	[Header("Main")]
	public float SpeedOfPlayer;
	public GameObject i_Player;
	public float MaxDistBS;
	public float PositionOfFinish;
	public float MaxAngleOfRotate;
	public float SpeedOfRotate;

	[Header("Other")]
	public GameObject Step;
	public float HeightOfCam;
	public float DistCamZ;

	private float HighOfStep;
	private float SaveSpeed;
	private int CurrentStep = 1;
	private Vector3 i_PlayerSaved;
	private float DistanceBSteps;

	private int stayrotate = 0;
	private float currentAngle = 0;

	[Header("For Scripts")]
	public bool Finished = false;
	private Transform tI_Player;
	public RectTransform AddThis;
	public Text AddThisText;

    // Joystick  //
    // Joystick  //

    public LeftJoystick leftJoystick; // the game object containing the LeftJoystick script
    public RightJoystick rightJoystick; // the game object containing the RightJoystick script


    private Vector3 leftJoystickInput; // holds the input of the Left Joystick
    private Vector3 rightJoystickInput; // hold the input of the Right Joystick

    // Joystick  //
    // Joystick  //

    public void Start(){
		SaveSpeed = SpeedOfPlayer;
		HighOfStep = Step.transform.position.y;
		tI_Player = i_Player.transform;
	}

	public void RotateRight(){
		stayrotate = 2;
	}
	public void RotateLeft(){
		stayrotate = -2;
	}
	public void RotateEnd(){
		stayrotate = 0;
	}


	void FixedUpdate (){
		
		if (stayrotate == 0) {
			if (currentAngle > 0.1f) {
				stayrotate = 1;
			}
			if (currentAngle < -0.1f) {
				stayrotate = -1;
			}
		}

		if (stayrotate == 1) {
			if (currentAngle > 0.1f) {
				tI_Player.Rotate (0, -SpeedOfRotate, 0);
				currentAngle -= SpeedOfRotate;
			} else {
				currentAngle = 0f;
				stayrotate = 0;
			}
		}

		if (stayrotate == -1) {
			if (currentAngle < -0.1f) {
				tI_Player.Rotate (0, SpeedOfRotate, 0);
				currentAngle += SpeedOfRotate;
			} else {
				currentAngle = 0f;
				stayrotate = 0;
			}
		}

		if (stayrotate == 2) {
			if (currentAngle < MaxAngleOfRotate) {
				tI_Player.Rotate (0,SpeedOfRotate,0);
				currentAngle += SpeedOfRotate;
			}
		}

		if (stayrotate == -2) {
			if (currentAngle > -MaxAngleOfRotate) {
				tI_Player.Rotate (0,-SpeedOfRotate,0);
				currentAngle -= SpeedOfRotate;
			}
		}
		gameObject.transform.eulerAngles = new Vector3 (gameObject.transform.eulerAngles.x,gameObject.transform.eulerAngles.y,-currentAngle/15);


		if (!Finished) {
			if (tI_Player.position.z + MaxDistBS > PositionOfFinish) {
				Finished = true;
			}
		} else {
			if (SpeedOfPlayer != 0) {
				if (SpeedOfPlayer > SaveSpeed * 0.5f) {
					SpeedOfPlayer = SpeedOfPlayer * 0.99f;
				} else if (SpeedOfPlayer > SaveSpeed * 0.05f) {
					SpeedOfPlayer = SpeedOfPlayer * 0.98f;
				} else {
					SpeedOfPlayer = 0;
					GameObject.Find ("Finish Panel").GetComponent<Finish> ().StartCoroutine ("ProcessToFinish");
				}
			}
		}



		gameObject.transform.position = new Vector3 (tI_Player.position.x, HeightOfCam, tI_Player.position.z - DistCamZ);	
	



		tI_Player.Translate (0, 0, SpeedOfPlayer);




		DistanceBSteps += Mathf.Abs (tI_Player.position.z - i_PlayerSaved.z);
		DistanceBSteps += Mathf.Abs (tI_Player.position.x - i_PlayerSaved.x);
		if (DistanceBSteps > MaxDistBS) {
			DistanceBSteps = 0;
			if (i_Player.GetComponent<ForPlayer> ().IsGameOver == 0) {
				SetSteps ();
			}
		}
		i_PlayerSaved = tI_Player.position;


        // Joystick  //

        JoystickMovement();
 
        // Joystick  //
    }


    void SetSteps(){
		GameObject NewStep = Instantiate (Step);
		NewStep.SetActive (true);
		NewStep.transform.parent = tI_Player;
		NewStep.transform.position = new Vector3 (0,HighOfStep,0);
		NewStep.transform.localPosition = new Vector3 (0,NewStep.transform.localPosition.y,0);
		NewStep.transform.localEulerAngles = new Vector3 (90,0,0);
		NewStep.transform.localScale = new Vector3 (NewStep.transform.localScale.x * CurrentStep,NewStep.transform.localScale.y,NewStep.transform.localScale.z);
		NewStep.transform.parent = tI_Player.parent.transform;
		NewStep.GetComponent<StepsSet> ().StartCoroutine ("StartCount");
		if (CurrentStep == 1) {
			CurrentStep = -1;
		} else {
			CurrentStep = 1;
		}
	}

    // Joystick  //
    // Joystick  //

    void JoystickMovement()
    {
        // get input from both joysticks
        leftJoystickInput = leftJoystick.GetInputDirection();
        rightJoystickInput = rightJoystick.GetInputDirection();

        float xMovementLeftJoystick = leftJoystickInput.x; // The horizontal movement from joystick 01
        float zMovementLeftJoystick = leftJoystickInput.y; // The vertical movement from joystick 01	

        float xMovementRightJoystick = rightJoystickInput.x; // The horizontal movement from joystick 02
        float zMovementRightJoystick = rightJoystickInput.y; // The vertical movement from joystick 02


        // if there is only input from the left joystick
        if (leftJoystickInput != Vector3.zero && rightJoystickInput == Vector3.zero)
        {

            if (xMovementLeftJoystick > 0.1f)
            {
                RotateRight();
            }
            else if (xMovementLeftJoystick < -0.1f)
            {
                RotateLeft();
            }
            else
            {
                RotateEnd();
            }
            if (xMovementLeftJoystick == 0)
            {
                RotateEnd();
            }

        }

    }
}
