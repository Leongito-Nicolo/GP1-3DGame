using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;                 // reference to the CharacterController component

    private Vector3 moveDirection;                          // vector to store the movement direction
    private float gravity = 9.81f;                          // gravity applied to the player

    [SerializeField] private float moveSpeed = 5f;          // speed of player movement
    [SerializeField] private float jumpForce = 5f;          // force of player jump
    [SerializeField] private float sprintSpeed;             // speed to apply when sprinting
    public bool hasPickedWeapon = false;                    // to check if the player has a weapon

    [SerializeField] private GameManager gameManager;       // instance of game manager

    [SerializeField] private GameObject playerHand;         // where to move the gun
    [SerializeField] private GameObject playerCamera;       // to set the gun as child and follow rotation

    public int playerScore = 0;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");     // get horizontal (A/D) input
        float yInput = Input.GetAxis("Vertical");       // get vertical (W/S) input

        Vector3 move = transform.right * xInput + transform.forward * yInput;       // convert the input into a movement

        moveDirection.x = move.x * moveSpeed;       // set x direction
        moveDirection.z = move.z * moveSpeed;       // set y direction

        // if the player is on ground
        if (controller.isGrounded)
        {
            moveDirection.y = -0.5f;        // apply a force to keep the player on the ground

            // check for jump input
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;        // apply a force for the jump
            }

            // check for sprint input
            if (Input.GetButton("Sprint"))
            {
                moveDirection.x *= sprintSpeed;     // increase movement speed
                moveDirection.z *= sprintSpeed;
            }
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;        // apply gravity to make the player fall
        }

        controller.Move(moveDirection * Time.deltaTime);        // move the player with Move
    }

    private void OnTriggerEnter(Collider other)
    {
        // check contact with power ups
        if (other.gameObject.tag == "Power UP")
        {
            StartCoroutine(gameManager.Invincibility());     // start the invincibility coroutine
            Destroy(other.gameObject);      // destroy the object the player collided with
        }

        // check contact with a gun
        if (other.gameObject.tag == "Gun" && !hasPickedWeapon)
        {
            hasPickedWeapon = true;     // player has a gun
            other.gameObject.transform.SetParent(playerCamera.transform);     // set the parent to the player hand
            other.gameObject.transform.position = playerHand.transform.position;        // set the position to the player hand
            other.gameObject.transform.rotation = playerHand.transform.rotation;        // set the rotation to the player hand
        }

        if(other.gameObject.tag == "End")
        {
            gameManager.hasPlayerWon = true;
        }
    }
}
