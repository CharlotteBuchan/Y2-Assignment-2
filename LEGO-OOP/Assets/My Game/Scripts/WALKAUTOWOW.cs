using UnityEngine;
using System.Collections;

[System.Serializable]
public enum SIDE
{
    Left,
    Mid,
    Right
};

public class WALKAUTOWOW : MonoBehaviour
{
    [Header("Movement")]
    public SIDE m_Side = SIDE.Mid;
    public float movementSpeed;
    public float xOffset;
    public float xOffsetSpeed = 10;
    public float newPosX = 0;

    public bool isJumping, isSliding;
    bool swipeLeft, swipeRight, swipeUp, swipeDown, swipeDownStop;
    public float x, y, z;
    public float playerScore;

    CharacterController cCtrl;
    Rigidbody rb;
    Animator anim;

    [Header("Strafe")]
    public float strafeRate = .4f;
    [SerializeField] private float nextTimeToStrafe;

    [Header("Jumping")]
    public float jumpForce = 7;
    public float jumpRate = .8f;

    [Header("Sliding")]
    public float slideRate = .8f;
    [SerializeField] private float nextTimeToSlide;
    //default settings
    public Vector3 defCentre;
    public float defHeight;
    //slide settings
    public Vector3 slideCentre;
    public float slideHeight;

    [Header("SFX")]

    public AudioSource jump;
    public AudioSource swipe;

    public GameObject START;

    void Start()
    {
        //transform.position = new Vector3 (-41,41,1204);

        cCtrl = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        newPosX = -41;
    }

    void Update()
    {
        swipeLeft = Input.GetKeyDown(KeyCode.A);
        swipeRight = Input.GetKeyDown(KeyCode.D);
        swipeUp = Input.GetKeyDown(KeyCode.W);
        swipeDown = Input.GetKeyDown(KeyCode.S);

        // Move smoothly towards newPosX using Lerp
        float moveSpeed = Mathf.Lerp(transform.position.x, newPosX, Time.deltaTime * xOffsetSpeed);
        Vector3 moveVector = new Vector3(moveSpeed - transform.position.x, y * Time.deltaTime, movementSpeed * Time.deltaTime);

        cCtrl.Move(moveVector);

        SwipeMechanics();
        Jump();
        Sliding();
    }

    void SwipeMechanics()
    {
        if (swipeLeft && !swipeRight && Time.time >= nextTimeToStrafe)
        {
            if (m_Side == SIDE.Mid)
            {
                newPosX = -41 - xOffset;  // Smoothly move left from the middle
                m_Side = SIDE.Left;
                Debug.Log("SIDE LEFT: Current Position: " + transform.position + " newPosX: " + newPosX);
            }
            else if (m_Side == SIDE.Right)
            {
                newPosX = -41;  // Move back to middle (Mid)
                m_Side = SIDE.Mid;
                Debug.Log("SIDE MID: Current Position: " + transform.position + " newPosX: " + newPosX);
            }

            nextTimeToStrafe = Time.time + strafeRate;
        }
        else if (swipeRight && !swipeLeft && Time.time >= nextTimeToStrafe)
        {
            if (m_Side == SIDE.Mid)
            {
                newPosX = -41 + xOffset;  // Smoothly move right from the middle
                m_Side = SIDE.Right;
                Debug.Log("SIDE RIGHT: Current Position: " + transform.position + " newPosX: " + newPosX);
            }
            else if (m_Side == SIDE.Left)
            {
                newPosX = -41;  // Move back to middle (Mid)
                m_Side = SIDE.Mid;
                Debug.Log("SIDE MID: Current Position: " + transform.position + " newPosX: " + newPosX);
            }

            nextTimeToStrafe = Time.time + strafeRate;
        }
    }

void Jump()
    {
        if (cCtrl.isGrounded && !isSliding)
        {
            if (swipeUp)
            {
                //jump.Play();
                y = jumpForce;
                anim.SetTrigger("Jump");
                isJumping = true;
            }

        }
        else
        {
            y -= jumpForce * 2 * Time.deltaTime;
            isJumping = false;
        }
    }

    void Sliding()
    {
        if (swipeDown && !isJumping && Time.time >= nextTimeToSlide)
        {
            StartCoroutine(PlayerSlide());
            anim.SetTrigger("Sliding");
            nextTimeToSlide = Time.time + slideRate;
        }
    }

    IEnumerator PlayerSlide()
    {
        cCtrl.center = slideCentre;
        cCtrl.height = slideHeight;
        isSliding = true;

        yield return new WaitForSeconds(0.8f);

        cCtrl.center = defCentre;
        cCtrl.height = defHeight;
        isSliding = false;
    }
}