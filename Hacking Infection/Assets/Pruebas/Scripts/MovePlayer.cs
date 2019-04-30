using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    float horizontalMove;
    float VerticallMove;
    [SerializeField, Range(10, 50)]
    float speedFlicker;
    [SerializeField, Range(1.0f, 9.8f)]
    float gravity = 9.8f;
    float fallVelocity;

    [SerializeField, Range(1f, 2f)] //cambiar valores
    float jumpForce;

    public GameObject player;
    Vector3 gravityPlayer;
    CharacterController CCplayer;


    void Start()
    {
        CCplayer = player.GetComponent<CharacterController>();
    }

    void Update()
    {
 
        horizontalMove = Input.GetAxis("Horizontal");
        //VerticalMove = Input.GetAxis("Vertical");
        SetGravity();
        Skills();
        CCplayer.Move(new Vector3(0, fallVelocity, horizontalMove) * speedFlicker * Time.deltaTime);
    }

    void Skills()
    {
        if (CCplayer.isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
        {
            fallVelocity = jumpForce;
        }

    }

    void SetGravity()
    {

        if (CCplayer.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
        }else
        {
            fallVelocity -= gravity * Time.deltaTime;
        }
    }
}
