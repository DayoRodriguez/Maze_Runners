using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TokensIntantiate : MonoBehaviour
{
    Player player;
    MazeInstantiate mazeInstantiate;
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        mazeInstantiate = GameObject.Find("Maze_Instantiate").GetComponent<MazeInstantiate>();
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < player.playerTokens.Length; i++)
        {
            int posIndex = Random.Range(0, mazeInstantiate.ways.Count);
            Vector3 tokenPos = mazeInstantiate.ways[posIndex];
            tokenPos.y += 1;
            Instantiate(player.playerTokens[i].Personaje, tokenPos,player.playerTokens[i].Personaje.transform.rotation,GameObject.Find("Player").transform).name = player.playerTokens[i].name;   
        }
    }
}
