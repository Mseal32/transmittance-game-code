using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    //reference to camera and char controller
    public CharacterController controller;
    public Transform cam;

    //variable used to determine speed
    public float speed;

    //variables and references handling gravity and jumping
    Vector3 velocity;
    public float gravity;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    public bool isGrounded;
    public float jumpHeight;

    //variables handling sprinting
    public float sprintSpeed;
    private bool isSprinting;

    //variables handling particle systems
    public Transform particlesHere;
    public ParticleSystem playerSpawnParticles;

    private void Awake() {
        //ensures that the "Player" isnt destroyed when a scene is loaded, and makes sure there is only 1 "Player"
        if( instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        
    }
    //sets values of all variables on game start
    void Start() {
        speed = 8.1f;
        gravity = -16f;
        groundDistance = .4f;
        jumpHeight = 3f;
        sprintSpeed = 1f;
        isSprinting = false;
    }

    
    void Update()
    {
        //checks when player is "grounded" 
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // ensures player doesnt get hung up on the ground by pushing them into ground
        if (isGrounded && velocity.y <0) {
            velocity.y = -2f;
        }

        //stores raw input into variables
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //defines direction player is facing using input variables
        Vector3 move = transform.right * x + transform.forward * z;
        
        //uses all movement variables to move the player
        controller.Move(move * speed * sprintSpeed * Time.deltaTime);

        //allows jumping when grounded
        if (Input.GetButtonDown("Jump") && isGrounded && PlayerPickUp.Instance.objHeld == null) {
            FindObjectOfType<AudioManager>().Play("Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //"pushes" player downwards in accordance to gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //calls Sprint method every frame
        Sprint();
    }

        //simple method for sprinting
        void Sprint () {
        
        bool shiftKeyDown = Input.GetKeyDown("left shift") || Input.GetKeyDown("right shift");
        bool shiftKeyUp = Input.GetKeyUp("left shift") || Input.GetKeyUp("right shift");

        if (shiftKeyDown && !isSprinting) {
            isSprinting = true;
            sprintSpeed = 1.5f;
        } else if (shiftKeyUp) {
            isSprinting = false;
            sprintSpeed = 1f;
        }
    }

    //when loaded, play particles and a sound
    private void OnEnable() {
     Instantiate(playerSpawnParticles, particlesHere.position, Quaternion.identity);
     AudioManager.instance.Play("PlayerSpawn");   
    }
 
    
}