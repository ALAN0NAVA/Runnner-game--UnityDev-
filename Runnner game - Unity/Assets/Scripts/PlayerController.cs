using System.Numerics;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private Animator playerAnim;
    private AudioSource playerAudio;
    public ParticleSystem exploParticles;
    public ParticleSystem dirtParticles;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public TextMeshProUGUI jumpText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI scoreText;
    public MoveLeft moveLeftScript;
    public bool isOnGround = true;
    private float jumpForce = 750f;
    private float gravityModifier = 1.8f;
    public int score;
    public int contSaltos = 0;
    public bool gameOver = false;
    public bool gameActive = false;
    public float gameSpeed = 10.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity = new UnityEngine.Vector3(0, -9.81f, 0);
        Physics.gravity *= gravityModifier;
        dirtParticles.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && gameActive)
        {
            if (contSaltos < 2)
            {
                playerAnim.SetTrigger("Jump_trig");
                dirtParticles.Stop();
                playerAudio.PlayOneShot(jumpSound, 0.8f);
                contSaltos++;
                playerRB.AddForce(UnityEngine.Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            jumpText.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(true);
            gameActive = true;
        }
        if ((Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && gameOver) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (gameActive) playerAnim.SetFloat("Speed_f", 1.0f);
    }
    //function that detects when the player is on the ground
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && gameActive)
        {
            isOnGround = true;
            contSaltos = 0;
            dirtParticles.Play();

        }
        else if (collision.gameObject.CompareTag("Obstacale") && gameActive)
        {
            gameActive = false;
            gameOver = true;
            restartText.gameObject.SetActive(true);
            exploParticles.Play();
            dirtParticles.Stop();

            Debug.Log("GAME OOOVER!!!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            playerAudio.PlayOneShot(crashSound, 0.8f);
        }
    }

    public void IncreaseScore(int scoreToAdd)
    {
        gameSpeed += 1.0f;
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    public float increaseSpeed()
    {
        return gameSpeed;
    }
}
