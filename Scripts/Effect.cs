using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using System;

[CreateAssetMenu(fileName = "New Effect", menuName = "ScriptableObject/Effect",order = 2)]
public class Effect : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] string objetive;
    [SerializeField] string type;
    [SerializeField] string description;
    [SerializeField] string action;
    [SerializeField] string affectField;
    [SerializeField] string question;
    [SerializeField] string playerAnswer;
    [SerializeField] string correctAnswer;
    [SerializeField] string firstAnswer;
    [SerializeField] string secondAnswer;
    [SerializeField] string thirdAnswer;
    [SerializeField] int duration;
    [SerializeField] int amountToAffect;
    bool conditionMet = true;
    [SerializeField] EffectVisualControler effectVisualControler;
    public string Name{ get{return name;}}
    public string Objetive{ get{return objetive;}}
    public string Type{ get{return type;}}
    public string Description{ get{return description;}}
    public string Action{ get{return action;}}
    public string AffectField{ get{return affectField;}}
    public int Duration{ get{return duration;}}
    public int AmountToAffect{ get{return amountToAffect;}}
    public bool Condition()
    {
        conditionMet = true;
        playerAnswer = string.Empty;
        Utils.EnableCursor();
        effectVisualControler = GameObject.Find("EffectVisualControler").GetComponent<EffectVisualControler>();
        
        effectVisualControler.TextQuestion.text = question;
        effectVisualControler.TextFirstAnswer.text = firstAnswer;
        effectVisualControler.TextSecondAnswer.text = secondAnswer;
        effectVisualControler.TextThirdAnswer.text = thirdAnswer;

        effectVisualControler.EffectPanel.SetActive(true);
        effectVisualControler.MiniMap.SetActive(false);

        Timer timerComponent = effectVisualControler.Timer.GetComponent<Timer>();
        timerComponent.TimerInitialazer();
        timerComponent.OnTimerEnd += () => conditionMet = false;

        effectVisualControler.FirstAnswerButton.onClick.RemoveAllListeners();
        effectVisualControler.FirstAnswerButton.onClick.AddListener(() => SetPlayerAnswer(effectVisualControler.FirstAnswerButton));
        
        effectVisualControler.SecondAnswerButton.onClick.RemoveAllListeners();
        effectVisualControler.SecondAnswerButton.onClick.AddListener(() => SetPlayerAnswer(effectVisualControler.SecondAnswerButton));
        
        effectVisualControler.ThirdAnswerButton.onClick.RemoveAllListeners();
        effectVisualControler.ThirdAnswerButton.onClick.AddListener(() => SetPlayerAnswer(effectVisualControler.ThirdAnswerButton));
        
        bool answer = EvaluatePlayerAnswer();

        effectVisualControler.StartCoroutine(WaitForPlayerAnswer(timerComponent));

        return conditionMet && answer;
    }
    void SetPlayerAnswer(Button btn)
    {
        GameObject answer = btn.transform.GetChild(0).gameObject;
        playerAnswer = answer.GetComponent<TextMeshProUGUI>().text;
    }
    bool EvaluatePlayerAnswer()
    {
        if(playerAnswer.ToLower().Equals(correctAnswer.ToLower()))
        {
            return true;
        }
        return false;   
    }
    IEnumerator WaitForPlayerAnswer(Timer timer)
    {
        //Esperar a que el jugador seleccione una respuesta o se acabe el tiempo
        while(conditionMet && string.IsNullOrEmpty(playerAnswer))
        {
            yield return null;
        }
        
        //Una vez rota la condicion
        effectVisualControler.MiniMap.SetActive(true);
        effectVisualControler.EffectPanel.SetActive(false);
        Utils.DisableCursor();
    }
}
