using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  
using UnityEngine.SceneManagement; 

public class PlayerController : MonoBehaviour
{
    float m_Speed = 10.0f;         
    private Quaternion m_CharacterTargetRot;
    private Quaternion m_CameraTargetRot;
    private Camera m_Camera;

    public float playerHP = 100f; 
    public float timer = 0f;     
    public Text timerText;        
    public int score = 0;         
    public Text scoreText;        
    public float maxTime = 60f;   
    public int maxScore = 100;    
    void Start()
    {
        m_Camera = Camera.main;
        m_CharacterTargetRot = transform.localRotation;
        m_CameraTargetRot = m_Camera.transform.localRotation;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        UpdateTimerText(); 
        UpdateScoreText(); 
    }

    // Update is called once per frame
    void Update()
    {
        float xRot = Input.GetAxis("Mouse Y");
        float yRot = Input.GetAxis("Mouse X");
        m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
        transform.localRotation = m_CharacterTargetRot;

        m_CameraTargetRot *= Quaternion.Euler(xRot, 0f, 0f);
        m_Camera.transform.localRotation = m_CameraTargetRot;

        float vmv = Input.GetAxis("Vertical");
        float hmv = Input.GetAxis("Horizontal");

        Vector2 m_Input = new Vector2(hmv, vmv);
        Vector3 desiredMove = transform.forward * 1 * m_Input.y + transform.right * 1 * m_Input.x;
        transform.position += desiredMove * m_Speed * Time.deltaTime;


        timer += Time.deltaTime;
        UpdateTimerText();


        if (timer >= maxTime || score >= maxScore)
        {
            EndGame();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ENEMY") 
        {
            playerHP -= 10f; 
            if (playerHP <= 0)
            {
                Die(); 
            }
            else
            {
                IncreaseScore(10); 
            }
        }
    }
    void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.Round(timer).ToString() + "s";
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    void Die()
    {

        SceneManager.LoadScene("LoseScene"); 
    }

    void EndGame()
    {
        SceneManager.LoadScene("WinScene");
    }
}
