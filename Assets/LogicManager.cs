using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{
    public static LogicManager Instance { get; private set; }

    public TextMeshProUGUI wheatCurrText;
    public TextMeshProUGUI wheatTargetText;
    public TextMeshProUGUI tomatoCurrText;
    public TextMeshProUGUI tomatoTargetText;

    public GameObject screenLayer;

    public int wheat, tomato;

    public int targetWheat, targetTomato;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (wheat >= targetWheat && tomato >= targetTomato) {

            screenLayer.SetActive(true);
            Time.timeScale = 0;
        
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  
    }

    public void BackToMenu()
    {
        PlayerPrefs.SetInt("Menu", 1);
        SceneManager.LoadScene(0);
    }


    private void Start()
    {
        UpdateCount();
    }

    public void UpdateCount()
    {
        wheatCurrText.text = wheat.ToString();
        wheatTargetText.text = targetWheat.ToString();
        tomatoCurrText.text = tomato.ToString();    
        tomatoTargetText.text = targetTomato.ToString();
    }



    

}
