using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterBehaviour : MonoBehaviour
{
    [SerializeField]
    [Header("Forces")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Grounded Information")]
    public Transform groundCheck;
    public LayerMask groundMask;

    [Header("Battle Statistics")]
    public int battleRate;


    //Private
    private Rigidbody2D rb;
    private float Direction;

    //States
    private bool isFacingRight = true;
    private bool isJumping;
    private bool isGrounded;
    private bool isWalking;
    

    //Animator
    public Animator animator;



    //Encounter
    private bool GenerateEncounter = false;
    private bool EncounterAvailable = true;


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //Inputs
        InputHandler();

        // Facing
        if(Direction > 0 && !isFacingRight) {
            Flip();
        }
        else if(Direction < 0 && isFacingRight) {
            Flip();
        }

        //Check for Ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f,groundMask);

        animator.SetBool("isJump", !isGrounded);
    }

    //Physics Handling
    void FixedUpdate()
    {
        //Movement
        Movement();

        //Encounter
        EncounterRNG();
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Props")
        {
            GenerateEncounter = true;
            StartCoroutine(Check());
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.name == "Props")
        {
            StopCoroutine(Check());
            Debug.Log("Left");
            EncounterAvailable = true;
            GenerateEncounter = false;
        }
    }

    private void EncounterRNG()
    {
        if (GenerateEncounter == true && isWalking == true && EncounterAvailable == true)
        {
            //StartCoroutine(Check());

        }
    }


    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f,180.0f,0.0f);
    }

    private void InputHandler()
    {
        Direction = Input.GetAxis("Horizontal");
        if(Direction != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        if(Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
    }

    private void Movement()
    {
        //Move
        rb.velocity = new Vector2(Direction * moveSpeed, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(Direction));

        //Jumping
        if(isJumping && isGrounded)
        {
            rb.AddForce(new Vector2(0.0f, jumpForce));
        }
        isJumping = false;

    }



    IEnumerator Check()
    {
        Debug.Log("Doing Something");
        while(GenerateEncounter)
        {
            for(;;)
            {
                int rng = Random.Range(0,100);
                if(rng <= battleRate)
                {
                    SceneManager.LoadScene("BattleScene");
                    MusicManager.Instance.PlayTrack(TrackID.Battle);
                    GenerateEncounter = false;
                    EncounterAvailable = false;
                }
                Debug.Log(rng);

                if(!GenerateEncounter)
                    break;


                yield return new WaitForSeconds(1.0f);
            }
        }

    }


}

enum StateEnum
{
    idle,
    isGrounded,
    isAirborn,
};


public class playerState
{
    virtual public playerState InputHandler()
    {
        return this;
    }

}

public class isGrounded : playerState
{
    override public playerState InputHandler()
    {
        float Direction = Input.GetAxis("Horizontal");
        if(Direction != 0)
        {

        }
        else
        {

        }
        return this;
    }
}

