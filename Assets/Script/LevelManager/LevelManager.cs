using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private GameObject wintext;
    [SerializeField] private GameObject losetext;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
    }

    
    public void Restart()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

   
    public void Pause()
    {
        Time.timeScale = 0f; 
    }

    
    public void Resume()
    {
        Time.timeScale = 1f; 
    }

    public void Lose()
    {
        losetext.SetActive(true);
        StartCoroutine(LoseCoroutine());
    }
    public void Win()
    {
        wintext.SetActive(true);
        StartCoroutine(WinCoroutine());
    }
    private IEnumerator LoseCoroutine()
    {
        yield return new WaitForSeconds(3.0f);
        Restart();
    }
    private IEnumerator WinCoroutine()
    {
        yield return new WaitForSeconds(3.0f);
        ReturnToMenu();
    }
}
