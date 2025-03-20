using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeInstantiate : MonoBehaviour
{
    [SerializeField] GameObject wall;
    [SerializeField] GameObject way;
    [SerializeField] GameObject way_Benefit;
    [SerializeField] GameObject way_Tramp;
    [SerializeField] GameObject Portal;
    string[,] maze;
    public List<Vector3> ways = new List<Vector3>();
    void Awake()
    {
        maze = Maze.MazeCreate(100, 100);

        Vector3 initalVector = new Vector3(0, 1, 0);

        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                if("Way".Equals(maze[i,j]))
                {
                    Instantiate(way, initalVector ,way.transform.rotation);
                    ways.Add(new Vector3(initalVector.x,initalVector.y,initalVector.z));
                    initalVector.x += way.transform.localScale.x;
                }
                else if("Tramp".Equals(maze[i,j]))
                {
                    Instantiate(way_Tramp, initalVector ,way.transform.rotation);
                    initalVector.x += way_Tramp.transform.localScale.x;
                }
                else if("Benefit".Equals (maze[i,j]))
                {
                    Instantiate(way_Benefit, initalVector ,way_Benefit.transform.rotation);
                    initalVector.x += way_Benefit.transform.localScale.x;
                }
                else if("Portal".Equals(maze[i,j]))
                {
                    initalVector.y += 1;
                    Instantiate(Portal, initalVector ,way.transform.rotation);
                    initalVector.y -= 1;
                    initalVector.x += Portal.transform.localScale.x;
                }
                else
                {
                    initalVector.y += 1;
                    Instantiate(wall, initalVector,wall.transform.rotation);
                    initalVector.y -= 1;
                    initalVector.x += wall.transform.localScale.x;
                }
            }
            initalVector.x = 0;
            initalVector.z += way.transform.localScale.z;
        }  
    }
}
