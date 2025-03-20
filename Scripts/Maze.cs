using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Maze 
{
    public static string[,] MazeCreate(int m, int n)
    {
        string[,] maze = new string[m,n];
        List<(int x, int y)> ways = new List<(int x, int y)> ();

        MazeGenerate(maze,ways,0,0);
        while (!IsAccesible(maze))
        {
            MazeGenerate(maze,ways,0,0);
        }
        DistributeTrampBenefitBox(maze,ways,20,"Tramp");
        DistributeTrampBenefitBox(maze,ways,10,"Benefit");
        DistributePortal(maze,ways,5);
        
        return maze;
    }
    static void MazeGenerate(string[,] maze, List<(int x, int y)> ways, int x, int y)
    {
        int[] dx = {2,0,-2,0};
        int[] dy = {0,2,0,-2};

        //Alternar los valores de los array de direccion
        for (int i = 0; i < 4; i++)
        {
            int pos = UnityEngine.Random.Range(0,4);
            (dx[i],dx[pos]) = (dx[pos],dx[i]);
            (dy[i],dy[pos]) = (dy[pos],dy[i]);
        }

        //Generar Los Caminos Del Laberinto
        for (int i = 0; i < 4; i++)
        {
            int nx = dx[i] + x;
            int ny = dy[i] + y;

            if(nx >= 0 && ny >= 0 && nx < maze.GetLength(0) && ny < maze.GetLength(1) && maze[nx,ny] == null)
            {
                maze[nx,ny] = "Way";
                maze[x + dx[i]/2, y + dy[i]/2] = "Way";
                ways.Add((x + dx[i]/2, y + dy[i]/2));
                ways.Add((nx, ny));

                MazeGenerate(maze,ways,nx,ny);
            }
        }
    }
    static bool IsAccesible(string[,] maze)
    {
        int m = maze.GetLength(0);
        int n = maze.GetLength(1);

        bool[,] visited = new bool[m,n];
        Queue<(int x, int y)> mazeWays = new Queue<(int x,int y)>();

        for (int i = 0; i < m; i++)
        {
            //We look for a accesible box
            for (int j = 0; j < n; j++)
            {
                if(maze[i,j] != null)
                {
                    mazeWays.Enqueue((i,j));
                    visited[i,j] = true;
                    break;
                }
            }
            //When we found a accesible box we get out the loop
            if(mazeWays.Count > 0) break;
        }

        int[][] directions = new int[][]
        {
            new int[] { 1, 0},
            new int[] { 0, 1},
            new int[] { -1, 0},
            new int[] { 0, -1}
        };

        //BFS to mark accesible box
        while(mazeWays.Count > 0)
        {
            (int x, int y) = mazeWays.Dequeue();

            foreach(var dir in directions)
            {
                int newX = x + dir[0];
                int newY = y + dir[1];

                if(newX >= 0 && newY >= 0 && newX < m && newY < n && !visited[newX,newY])
                {
                    mazeWays.Enqueue((newX, newY));
                    visited[newX,newY] = true;
                }
            }
        }

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if(maze[i,j] != null && !visited[i,j]) return false; //We found a no accesible box
            }
        }
        return true;
    }

    static void DistributeTrampBenefitBox(string[,] maze, List<(int x, int y)> ways, int porcentege, string type)
    {
        int total = porcentege / 100 * ways.Count;

        int attemps = 100;

        while(attemps-- > 0)
        {
            int index = UnityEngine.Random.Range(0,ways.Count);
            (int x, int y) = ways[index];

            if("Way".Equals(maze[x,y]) && IsSeparate(maze, x, y, 1, type))
            {
                maze[x, y] = type == "Tramp" ? "Tramp" : "Benefit";
            }
        }
    }
    static void DistributePortal(string[,] maze, List<(int x, int y)> ways, int porcentege)
    {
        int count = porcentege * 100 / ways.Count;

        int attemps = 50;

        while(attemps-- > 0)
        {
            int index = UnityEngine.Random.Range(0, ways.Count);
            (int x, int y) = ways[index];

            if("Way".Equals(maze[x,y]))
            {
                if(!ThereIsInArea(maze, x, y, "Portal", 10))
                {
                    maze[x,y] = "Portal";
                }
            }
        }
    }
    //Metoth to distribute the special box
    static bool IsSeparate(string[,] maze, int x, int y, int separation ,string type)
    {
        int[] dx = new int[]{1,0,-1,0,1,1,-1,-1};
        int[] dy = new int[]{0,1,0,-1,-1,1,1,-1};

        for (int i = 0; i < dx.Length; i++)
        {
            int newX = x + dx[i] * separation;
            int newY = y + dy[i] * separation;

            if(newX >= 0 && newY >= 0 && newX < maze.GetLength(0) && newY < maze.GetLength(1) && type.Equals(maze[newX,newY])) 
            {
                return false;
            }
        }
        return true;
    }
    static bool ThereIsInArea(string[,] maze, int x, int y, string type, int area)
    {
        for (int i = x + 1; i < x + area/2; i++)
        {
            for (int j = y + 1; j < y + area/2; j++)
            {
                if(i >= 0 && j >= 0 && i < maze.GetLength(0) && j < maze.GetLength(1))
                {
                    if(type.Equals(maze[i,j])) return true;
                }
            }
        }
        for (int i = x + area/2; x < i ; i--)
        {
            for (int j = y - area/2; j < y; j++)
            {
                if(i >= 0 && j >= 0 && i < maze.GetLength(0) && j < maze.GetLength(1))
                {
                    if(type.Equals(maze[i,j])) return true;
                }
            }
        }
        for (int i = x - area/2; i < x; i++)
        {
            for (int j = y - area/2; j < y; j++)
            {
                if(i >= 0 && j >= 0 && i < maze.GetLength(0) && j < maze.GetLength(1))
                {
                    if(type.Equals(maze[i,j])) return true;
                }
            }
        }
        for (int i = x - area/2; i < x; i++)
        {
            for (int j = y + 1; j < y + area/2; j++)
            {
                if(i >= 0 && j >= 0 && i < maze.GetLength(0) && j < maze.GetLength(1))
                {
                    if(type.Equals(maze[i,j])) return true;
                }
            }
        }                
        return false;
    }
}
