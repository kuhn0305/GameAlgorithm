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
        public TileType[,] _tile;
        public int _size;

        public enum TileType
        {
            Empty,
            Wall
        }

        public void Initialize(int size)
        {
            _tile = new TileType[size, size];
            _size = size;

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (x == 0 || x == size - 1 || y == 0 || y == size - 1)
                        _tile[y, x] = TileType.Wall;
                    else
                        _tile[y, x] = TileType.Empty;
                }
            }
        }

        public void Render()
        {
            ConsoleColor prevColor = Console.ForegroundColor;

            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    Console.ForegroundColor = GetTileColor(_tile[y, x]); ;
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
    }
}
