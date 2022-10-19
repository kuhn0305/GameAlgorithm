using System;
using System.CodeDom.Compiler;
using System.ComponentModel.Design;
using System.Net.Mime;
using System.Numerics;

namespace Exercise
{
    class Program
    {

        static void Main(string[] args)
        {
            PriorityQueue q = new PriorityQueue();
            q.Push(20);
            q.Push(10);
            q.Push(30);
            q.Push(90);
            q.Push(40);

            while(q.Count() >0)
            {
                Console.WriteLine(q.Pop());
            }
        }

        class PriorityQueue
        {
            List<int> _heap = new List<int>();

            public void Push(int data)
            {
                // 힙의 맨 끝에 새로운 데이터를 삽입한다.
                _heap.Add(data);

                // 힙의 맨 마지막에서 시작한다.
                int now = _heap.Count - 1;

                // now가 힙의 index내에 있다면 계속 시도함
                while(now > 0)
                {
                    int next = (now - 1) / 2;

                    // 부모노드의 값이 나보다 크면 break
                    if (_heap[next] > _heap[now])
                        break;

                    // 내가 부모노드보다 값이 크거나 같으면 교체한다.
                    int temp = _heap[next];
                    _heap[next] = _heap[now];
                    _heap[now] = temp;

                    // 검사 위치를 이동한다.
                    now = next;
                }
            }

            public int Pop()
            {
                // 가장 큰 값 Root (index : 0) 을 반환한다.
                int ret = _heap[0];

                int lastIndex = _heap.Count - 1;
                // 마지막 데이터를 루트로 이동한다
                _heap[0] = _heap[lastIndex];
                _heap.RemoveAt(lastIndex);
                lastIndex--;

                int now = 0;

                while (true)
                {
                    int left = (now * 2) + 1;
                    int right = (now * 2) + 2;
                    int next = now;

                    if (left <= _heap.Count - 1 && _heap[left] > _heap[next])
                        next = left;
                    if (right <= _heap.Count - 1 && _heap[right] > _heap[next])
                        next = right;

                    // 왼쪽 & 오른쪽 모두 현재값보다 작으면 종료
                    if (next == now)
                        break;

                    int temp = _heap[now];
                    _heap[now] = _heap[next];
                    _heap[next] = temp;

                    now = next;

                }


                return ret;
            }

            public int Count()
            {
                int count = _heap.Count;

                return count;
            }
        }

        #region Tree
        class TreeNode<T>
        {
            public T Data { get; set; }
            public List<TreeNode<T>> Children { get; set; } = new List<TreeNode<T>>();
        }

        static TreeNode<string> MakeTree()
        {
            TreeNode<string> root = new TreeNode<string> { Data = "R1 개발실" };
            {
                {
                    TreeNode<string> node = new TreeNode<string> { Data = "디자인팀" };
                    node.Children.Add(new TreeNode<string> { Data = "전투" });
                    node.Children.Add(new TreeNode<string> { Data = "경제" });
                    node.Children.Add(new TreeNode<string> { Data = "스토리" });
                    root.Children.Add(node);
                }
                {
                    TreeNode<string> node = new TreeNode<string> { Data = "프로그래밍" };
                    node.Children.Add(new TreeNode<string> { Data = "서버" });
                    node.Children.Add(new TreeNode<string> { Data = "클라" });
                    node.Children.Add(new TreeNode<string> { Data = "엔진" });
                    root.Children.Add(node);
                }
                {
                    TreeNode<string> node = new TreeNode<string> { Data = "아트팀" };
                    node.Children.Add(new TreeNode<string> { Data = "배경" });
                    node.Children.Add(new TreeNode<string> { Data = "캐릭터" });
                    root.Children.Add(node);
                }
            }

            return root;
        }

        static void PrintTree(TreeNode<string> root)
        {
            Console.WriteLine(root.Data);

            foreach (TreeNode<string> child in root.Children)
                PrintTree(child);
        }

        static int GetHeight(TreeNode<string> root)
        {
            int height = 0;

            foreach (TreeNode<string> child in root.Children)
            {
                int newHeight = GetHeight(child) + 1;

                // Math.Max 코드로 표현 가능
                //if(height < newHeight)
                //height = newHeight;

                height = Math.Max(height, newHeight);
            }

            return height;
        }

        #endregion

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

            int[,] weightAdj = new int[6, 6]
{
                { -1, 15, -1, 35, -1, -1 },
                { 15, -1, 05, 10, -1, -1 },
                { -1, 05, -1, -1, -1, -1 },
                { 35, 10, -1, -1, 05, -1 },
                { -1, -1, -1, 05, 0, 05 },
                { -1, -1, -1, -1, 05, -1 }
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

            public void Dijikstra(int start)
            {
                bool[] visited = new bool[weightAdj.GetLength(0)];
                int[] distance = new int[weightAdj.GetLength(0)];
                int[] parent = new int[weightAdj.GetLength(0)];

                Array.Fill(distance, Int32.MaxValue);

                distance[start] = 0;
                parent[start] = start;

                while(true)
                {
                    // 제일 좋은 후보를 찾는다 (가장 가깝게 있는)

                    // 가장 유력한 후보의 거리와 번호를 저장한다.
                    int closest = Int32.MaxValue;
                    int now = -1;

                    for(int i = 0; i < weightAdj.GetLength(0); i++)
                    {
                        // 이미 방문한 정점은 스킵
                        if (visited[i])
                            continue;

                        // 아직 발견(예약)된 적이 없거나, 기존 후보보다 멀리 있으면 스킵
                        if (distance[i] == Int32.MaxValue || distance[i] >= closest)
                            continue;

                        // 여태껏 발견한 가장 좋은 후보라는 의미, 정보를 갱신한다.
                        closest = distance[i];
                        now = i;
                    }

                    // 다음 후보가 없다 -> 종료
                    if (now == -1)
                        return;

                    // 제일 좋은 후보를 찾았으니, 방문한다.
                    visited[now] = true;

                    // 방문한 정점과 인접한 정점들을 조사해서,
                    // 상황에 따라 발견한 최단거리를 갱신한다.
                    for(int next = 0; next < weightAdj.GetLength(0); next++)
                    {
                        // 연결되지 않은 정점 스킵
                        if (weightAdj[now, next] == -1)
                            continue;

                        // 이미 방문한 정점은 스킵
                        if (visited[next])
                            continue;

                        // 새로 조사된 정점의 최단거리를 계산한다.
                        int nextDist = distance[now] + weightAdj[now, next];

                        // 만약 기존에 발견한 최단거리가 새로 조사된 최단거리보다 크며면, 정보를 갱신한다.
                        if(nextDist < distance[next])
                        {
                            distance[next] = nextDist;
                            parent[next] = now;
                        }
                    
                    }
                }
            }



        }
    }
}
