using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TokensIntantiate : MonoBehaviour
{
    Player player;
    MazeInstantiate mazeInstantiate;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        mazeInstantiate = GameObject.Find("Maze_Instantiate").GetComponent<MazeInstantiate>();

        for (int i = 0; i < player.tokens.Length; i++)
        {
            int posIndex = Random.Range(0, mazeInstantiate.ways.Count);
            Vector3 tokenPos = mazeInstantiate.ways[posIndex];
            tokenPos.y += 1;
            Instantiate(player.tokens[i].Personaje, tokenPos,player.tokens[i].Personaje.transform.rotation,GameObject.Find("Player").transform).name = player.tokens[i].name;   
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
