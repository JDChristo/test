using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public AudioClip[] music;

    public PlayerController player;
    public GameObject pauseButton;
    public GameObject pause;
    public GameObject win;
    public GameObject lose;

    void Start()
    {
        Time.timeScale = 1f;
        int i = Random.Range(0, 3);
        GetComponent<AudioSource>().clip = music[i];
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isWin)
        {
            win.SetActive(true);
            pauseButton.SetActive(false);
            Time.timeScale = 0f;
        }
        if (player.isLost)
        {
            lose.SetActive(true);
            pauseButton.SetActive(false);
            Time.timeScale = 0f;
        }
    }

    public void OnResume()
    {
        Time.timeScale = 1f;
        pause.SetActive(false);
        pauseButton.SetActive(true);
    }
    public void OnPause()
    {
        Time.timeScale = 0f;
        pause.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void OnNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnBack()
    {
        SceneManager.LoadScene(1);
    }
    public void OnRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
