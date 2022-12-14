using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Algorithm
{
    #region List 직접구현
    class MyList<T>
    {
        const int DEFAULT_SIZE = 1;

        T[] _data = new T[DEFAULT_SIZE];

        public int count = 0;   // 실제로 사용중인 데이터 개수
        public int Capacity { get { return _data.Length; } }    // 예약된 데이터 개수

        // O(1) 예외 케이스 : 이사 비용은 무시한다.
        public void Add(T item)
        {
            if (count >= Capacity)
            {
                T[] newArray = new T[count * 2];

                for (int index = 0; index < count; index++)
                    newArray[index] = _data[index];

                _data = newArray;
            }

            _data[count] = item;
            count++;
        }

        // Best case : 1번만 반복
        // Worst case : N번 반복
        // O(N)
        public void RemoveAt(int index)
        {
            for (int i = index; i < count - 1; i++)
                _data[i] = _data[i + 1];

            _data[count - 1] = default(T);

            count--;
        }

        // O(1)
        public T this[int index]
        {
            get { return _data[index]; }
            set { _data[index] = value; }
        }

    }
    #endregion

    #region LinkedList 직접구현
    class MyLinkedListNode<T>
    {
        public T Data;
        public MyLinkedListNode<T> Next;
        public MyLinkedListNode<T> Prev;
    }

    class MyLinkedList<T>
    {
        public MyLinkedListNode<T> head;
        public MyLinkedListNode<T> tail;
        public int count = 0;

        // O(1)
        public MyLinkedListNode<T> AddLast(T data)
        {
            MyLinkedListNode<T> newRoom = new MyLinkedListNode<T>();

            newRoom.Data = data;

            if (head == null)
                head = newRoom;

            if (tail != null)
            {
                tail.Next = newRoom;
                newRoom.Prev = tail;
            }

            tail = newRoom;
            count++;

            return newRoom;
        }

        // O(1)
        public void Remove(MyLinkedListNode<T> room)
        {
            if (head == room)
                head = head.Next;

            if (tail == room)
                tail = tail.Prev;

            if (room.Prev != null)
                room.Prev.Next = room.Next;

            if (room.Next != null)
                room.Next.Prev = room.Prev;

            count--;
        }
    }
    #endregion


    internal class Board
    {
        const char CIRCLE = '\u25cf';
        public TileType[,] Tile { get; private set; }
        public int Size { get; private set; }

        public int DestX { get; private set; }
        public int DestY { get; private set; }

        Player _player;

        public enum TileType
        {
            Empty,
            Wall
        }

        public void Initialize(int size, Player player)
        {
            // 홀수 크기의 맵 생성
            if (size % 2 == 0)
                return;

            Tile = new TileType[size, size];
            Size = size;
            _player = player;

            DestX = Size - 2;
            DestY = Size - 2;

            // 길을 막아버리는 작업
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        Tile[y, x] = TileType.Wall;
                    else
                        Tile[y, x] = TileType.Empty;
                }
            }

            //BinaryTree 미로 생성 알고리즘
            //GenerateByBinaryTree();
            GenerateBySideWinder();
        }

        public void Render()
        {
            ConsoleColor prevColor = Console.ForegroundColor;

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    // 플레이어 좌표를 가지고와서, 그 좌표와 현재 y,x가 일치하면 플레이어 색상으로 표시
                    if (y == _player.PosY && x == _player.PosX)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else if (y == DestY && x == DestX)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                        Console.ForegroundColor = GetTileColor(Tile[y, x]); ;

                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = prevColor;
        }

        private ConsoleColor GetTileColor(TileType type)
        {
            switch (type)
            {
                case TileType.Empty:
                    return ConsoleColor.Green;
                case TileType.Wall:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.Green;
            }
        }

        private void GenerateByBinaryTree()
        {
            // 랜덤으로 우측 혹은 아래로 길을 뚫는 작업
            Random rand = new Random();
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    if (y == Size - 2 && x == Size - 2)
                        continue;

                    // size - 2는 벽 바로 앞의 길을 의미함
                    if (y == Size - 2)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        continue;
                    }

                    if (x == Size - 2)
                    {
                        Tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    if (rand.Next(0, 2) == 0)
                        Tile[y, x + 1] = TileType.Empty;
                    else
                        Tile[y + 1, x] = TileType.Empty;
                }
            }
        }

        private void GenerateBySideWinder()
        {
            // 랜덤으로 우측 혹은 아래로 길을 뚫는 작업
            Random rand = new Random();
            for (int y = 0; y < Size; y++)
            {
                int count = 1;

                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    if (y == Size - 2 && x == Size - 2)
                        continue;

                    // size - 2는 벽 바로 앞의 길을 의미함
                    if (y == Size - 2)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        continue;
                    }

                    if (x == Size - 2)
                    {
                        Tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    if (rand.Next(0, 2) == 0)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        // 우측으로 몇번 길을 뚫었는지 Check
                        count++;
                    }
                    else
                    {
                        int randomIndex = rand.Next(0, count);
                        Tile[y + 1, x - randomIndex * 2] = TileType.Empty;
                        count = 1;
                    }
                }
            }
        }
    }
}
