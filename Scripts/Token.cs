using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Token", menuName = "ScriptableObject/Token", order = 1)]

public class Token : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] string skill;
    [SerializeField] string skillDescription;
    [SerializeField] string tokenHistory;
    [SerializeField] int lp;
    [SerializeField] int speed;
    [SerializeField] int colderTime;
    [SerializeField] string universe;  
    [SerializeField] GameObject personaje;
    public string Name { get{ return name; }}
    public string Skill{ get{return skill; }}
    public string SkillDescription{ get{return skillDescription; }}
    public string TokenHistory{ get{return tokenHistory;} }
    public int LP{ get{return lp;} }
    public int Speed{ get{return speed;} }
    public int ColderTime{ get{return colderTime;}}
    public string Universe{ get{return universe;}}
    public GameObject Personaje{ get{return personaje;}}
    public void ActiveSkill(string skillName)
    {

    }
    public void Move(Rigidbody rbPersonaje,Vector3 force)
    {
        rbPersonaje.AddForce(force,ForceMode.Impulse);
    }
}
