using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsControler : MonoBehaviour
{
    Effect[] effects;
    public Effect[] Benefit{get; private set;}
    public Effect[] Tramps{get; private set;}
    void Awake()
    {
        effects = Resources.LoadAll<Effect>("SOEffects");
        LoadEffects();
    }
    public Effect GetEffect(int index)
    {
        if(index < 0 || index >= effects.Length)
        {
            int indexNeg = Random.Range(0, effects.Length);
            if(effects[indexNeg] != null) return effects[indexNeg];
        }
        else if(effects[index] != null) return effects[index];

        return new Effect();
    }
    public Effect GetTrampBenefit(int index, string type)
    {
        if(type.Equals("Tramp"))
        {
            if(index < 0 || index >= Tramps.Length)
            {
                int indexNeg = Random.Range(0, Tramps.Length);
                if(Tramps[indexNeg] != null) return Tramps[indexNeg];
            }
            else if(Tramps[index] != null) return Tramps[index];

            return new Effect();
        }
        else
        {
            if(index < 0 || index >= Benefit.Length)
            {
                int indexNeg = Random.Range(0, Benefit.Length);
                if(Benefit[indexNeg] != null) return Benefit[indexNeg];
            }
            else if(Benefit[index] != null) return Benefit[index];

            return new Effect();
        }
    }
    void LoadEffects()
    {
        List<Effect>benefit = new List<Effect>();
        List<Effect>tramps = new List<Effect>();

        for (int i = 0; i < effects.Length; i++)
        {
            if(effects[i].Type.ToLower().Equals("tramp"))
            {
                tramps.Add(effects[i]);
            }
            else
            {
                benefit.Add(effects[i]);
            }
        }
        Tramps = tramps.ToArray();
        Benefit = benefit.ToArray();
    }
}
