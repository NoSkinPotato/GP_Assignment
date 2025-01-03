using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject levelSelect;


    private void Start()
    {
        int x = PlayerPrefs.GetInt("Menu");

        if (x == 1)
        {
            mainMenu.SetActive(false);
            levelSelect.SetActive(true);
            PlayerPrefs.SetInt("Menu", 0);
        }
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(true);
    }

    public void backtoMenu()
    {
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
    }


    public void StartLevel1(int x)
    {
        SceneManager.LoadScene(x);
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
