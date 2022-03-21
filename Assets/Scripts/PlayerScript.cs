using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public TextMeshProUGUI LivesText;
    private int scoreValue = 0;
    public float JumpHeight = 3;
    public int lives = 3;
    public GameObject WinTextObject;
    public GameObject LoseTextObject;
    public AudioClip musicClipOne;
    public AudioClip WinMusic;
    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        LivesText.text = "Lives: " + lives.ToString(); 
        WinTextObject.SetActive(false);
        LoseTextObject.SetActive(false);
        musicSource.clip = musicClipOne;
        musicSource.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float verMovement = Input.GetAxis("Vertical");
        float hozMovement = Input.GetAxis("Horizontal");
        rd2d.AddForce(new Vector2(hozMovement * speed, 0));
    }
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.collider.tag == "Coin") 
        {
            SetCount();
            Destroy(collision.collider.gameObject);
        }

        if (collision.collider.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
            lives--;
            SetLives();
        }
    }
    private void OnCollisionStay2D(Collision2D collision) 
    {
        if (collision.collider.tag == "Ground") 
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, JumpHeight), ForceMode2D.Impulse);
            }
        }
    }

    private void SetCount()
    {
        scoreValue += 1;
        score.text = "Score: " + scoreValue.ToString();

        if (scoreValue == 4)
        {
            transform.position = new Vector2(90.0f, 1.0f);
            lives = 3;
            SetLives();
        }

        if (scoreValue == 8)
        {
            WinTextObject.SetActive(true);
            musicSource.Stop();
            musicSource.clip = WinMusic;
            musicSource.Play();
        }
    }

    private void SetLives()
    {  
        LivesText.text = "Lives: " + lives.ToString();
        if (lives  == 0)
        {
            LoseTextObject.SetActive(true);
            musicSource.Stop();
            Destroy(this);
        }
    }
}