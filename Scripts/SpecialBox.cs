using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBox : MonoBehaviour
{
    bool hasBeenVisited;
    [SerializeField] string type;
    EffectsControler effectsControler { get; set; }
    [SerializeField] Effect effect;
    public Player Player1{ get; private set; }
    public bool HasBeenVisited{get{return hasBeenVisited;}}
    // Start is called before the first frame update
    void Start()
    {
        hasBeenVisited = false;
        Player1 = GameObject.Find("Player").GetComponent<Player>();
        effectsControler = GameObject.Find("EffectControler").GetComponent<EffectsControler>();    
       if(type.Equals("Tramp"))
        {
            effect = effectsControler.GetTrampBenefit(0,"Tramp");
        }
       else
        {
            effect = effectsControler.GetTrampBenefit(0,"Benefit");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(HasBeenVisited);
        if(!HasBeenVisited && other.gameObject.CompareTag("Token"))
        {
            if(effect.Condition())
            {
                if(effect.Objetive.Equals("actualToken"))
                {
                    ActivateEffect(Player1.chooseToken);
                }
                else if(effect.Objetive.Equals("ownerTokens"))
                {
                    for (int i = 0; i < Player1.playerTokens.Length; i++)
                    {
                        ActivateEffect(Player1.playerTokens[i]);
                    }
                }                
            }
        }
        hasBeenVisited = true;
    }
    void ActivateEffect(Token token)
    {
        if(effect.AffectField.ToLower().Equals("lp"))
        {
            token.ChangeLp(effect.AmountToAffect, effect.Action);
        }
        if(effect.AffectField.ToLower().Equals("speed"))
        {
            token.ChangeSpeed(effect.AmountToAffect, effect.Action);
        }
    }
}
