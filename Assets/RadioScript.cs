using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioScript : MonoBehaviour
{

    public GameObject input;
    public float distance;
    public AudioSource audioS;

    float seconds = 0;

    public void SetInput(bool x)
    {
        if (input == null) return;

        input.SetActive(x);
        RectTransform trans = input.GetComponent<RectTransform>();
        trans.position = Camera.main.WorldToScreenPoint((Vector2)transform.position + Vector2.down * distance);

    }

    public void SetMusic()
    {
        Debug.Log("Music");
        if (!audioS.isPlaying)
        {
            Debug.Log("Play Music");
            audioS.Play();
            audioS.time = seconds;
        }
        else
        {
            Debug.Log("Stop Music");
            seconds = audioS.time;
            audioS.Pause();
        }
    }
}
