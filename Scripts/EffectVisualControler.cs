using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EffectVisualControler : MonoBehaviour
{
    [SerializeField] Button firstAnswerButton;
    [SerializeField] Button secondAnswerButton;
    [SerializeField] Button thirdAnswerButton;
    [SerializeField] TextMeshProUGUI textQuestion;
    [SerializeField] TextMeshProUGUI textFirstAnswer;
    [SerializeField] TextMeshProUGUI textSecondAnswer;
    [SerializeField] TextMeshProUGUI textThirdAnswer;
    [SerializeField] GameObject effectPanel;
    [SerializeField] GameObject miniMap;
    [SerializeField] GameObject timer;
    public Button FirstAnswerButton{get{return firstAnswerButton;}}
    public Button SecondAnswerButton{get{return secondAnswerButton;}}
    public Button ThirdAnswerButton{get{return thirdAnswerButton;}}
    public TextMeshProUGUI TextQuestion{get{return textQuestion;}}
    public TextMeshProUGUI TextFirstAnswer{get{return textFirstAnswer;}}
    public TextMeshProUGUI TextSecondAnswer{get{return textSecondAnswer;}}
    public TextMeshProUGUI TextThirdAnswer{get{return textThirdAnswer;}}
    public GameObject EffectPanel{get{return effectPanel;}}
    public GameObject MiniMap{get{return miniMap;}}
    public GameObject Timer{ get { return timer; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
