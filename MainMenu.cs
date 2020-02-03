using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    bool go;
    void Start()
    {
        Time.timeScale = 1f;
        go = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(go)
        {
            GetComponent<Animator>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<Rigidbody2D>().velocity = -transform.up * 2f;
            Invoke("LoadNext", 2f);
        }
    }

    public void LoadNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void onStart()
    {
        go = true;
    }

    public void Tut()
    {
        SceneManager.LoadScene(8);
    }
    public void onExit()
    {
        Application.Quit();
    }
}
