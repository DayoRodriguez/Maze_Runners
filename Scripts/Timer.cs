using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    float timerDuration = 30f;
    float timerRemaining = 0;
    [SerializeField] TextMeshProUGUI timerText;
    public event Action OnTimerEnd;
    // Start is called before the first frame update
    void Start()
    {
        //timerText = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();    
    }

    IEnumerator StartTimer()
    {
        timerRemaining = timerDuration;
        timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
        while(timerRemaining > 0)
        {
            timerRemaining -= Time.deltaTime;
            if(timerText != null)
            {
                timerText.text = Mathf.Ceil(timerRemaining).ToString();
            }
            yield return null;
        }
        timerRemaining = 0;
        OnTimerEnd?.Invoke();
    }
    public void TimerInitialazer()
    {
        StartCoroutine(StartTimer());
    }
}
