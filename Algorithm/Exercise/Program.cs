﻿using System;
using System.ComponentModel.Design;
using System.Numerics;

namespace Exercise
{
    class Program
    {


        static void Main(string[] args)
        {
            Graph graph = new Graph();

            graph.BFS(0);

        }

        class Graph
        {
            int[,] adj = new int[6, 6]
{
            { 0, 1, 0, 1, 0, 0 },
            { 0, 0, 1, 1, 0, 0 },
            { 0, 1, 0, 0, 0, 0 },
            { 1, 1, 0, 0, 1, 0 },
            { 0, 0, 0, 1, 0, 1 },
            { 0, 0, 0, 0, 1, 0 }
};

            List<int>[] adj2 = new List<int>[]
            {
            new List<int> { 1, 3 },
            new List<int> { 0, 2, 3 },
            new List<int> { 1 },
            new List<int> { 0, 1, 4 },
            new List<int> { 3, 5 },
            new List<int> { 4 },
            };

            bool[] visited;

            private void DFS_Array(int now)
            {
                Console.WriteLine(now);
                visited[now] = true;

                for (int next = 0; next < adj.GetLength(0); next++)
                {
                    if (adj[now, next] == 0)
                        continue;
                    else
                    {
                        if (visited[next])
                            continue;

                        DFS_Array(next);
                    }
                }
            }

            private void DFS_List(int now)
            {
                Console.WriteLine(now);
                visited[now] = true;

                for (int index = 0; index < adj2[now].Count; index++)
                {
                    if (visited[adj2[now][index]] == true)
                        continue;

                    DFS_List(adj2[now][index]);
                }

            }

            public void DFS_SearchAll()
            {
                visited = new bool[adj.GetLength(0)];

                for (int index = 0; index < adj.GetLength(0); index++)
                {
                    if (visited[0] == false)
                        DFS_Array(0);
                }
            }

            public void BFS(int start)
            {
                Queue<int> vertexQueue = new Queue<int>();
                bool[] found = new bool[adj.GetLength(0)];
                int[] distance = new int[adj.GetLength(0)];

                vertexQueue.Enqueue(start);
                found[start] = true;
                distance[start] = 0;

                int current = 0;

                while (vertexQueue.Count > 0)
                {
                    current = vertexQueue.Dequeue();

                    Console.WriteLine($"{current} :: {distance[current]}");

                    for (int next = 0; next < adj.GetLength(0); next++)
                    {
                        if (adj[current, next] == 0)
                            continue;

                        if (found[next] == true)
                            continue;

                        vertexQueue.Enqueue(next);
                        found[next] = true;
                        distance[next] = distance[current] + 1;
                    }
                }
            }
        }
    }
}
