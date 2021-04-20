using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;

    public TMP_Text TutorialText;
    public bool tutorial = false;

    AudioSource Speaker;
    public AudioClip ForwardSound;
    public AudioClip BackwardSound;

    // Start is called before the first frame update
    void Start()
    {
        Speaker = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setPanel(int p)
    {
        Speaker.clip = ForwardSound;
        Speaker.PlayOneShot(Speaker.clip);

        switch (p)
        {
            case 1:
                p1.SetActive(true);
                p2.SetActive(false);
                p3.SetActive(false);
                break;
            case 2:
                p1.SetActive(false);
                p2.SetActive(true);
                p3.SetActive(false);
                break;
            case 3:
                p1.SetActive(false);
                p2.SetActive(false);
                p3.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ExitToMenu()
    {
        try
        {
            Speaker.clip = BackwardSound;
            Speaker.PlayOneShot(Speaker.clip);

            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        catch
        {
            Application.Quit();
        }
    }

    public void ToggleTutorial()
    {
        try
        {
            Speaker.clip = ForwardSound;
            Speaker.PlayOneShot(Speaker.clip);

            tutorial = !tutorial;

            if(tutorial)
            {
                TutorialText.text = "ON";
            }
            else
            {
                TutorialText.text = "OFF";
            }
        }
        catch
        {
            Debug.Log("No Audio");
        }
    }
}
