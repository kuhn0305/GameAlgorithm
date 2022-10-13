using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
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

            if(head == null)
                head = newRoom;

            if(tail != null)
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

            if(room.Prev != null)
                room.Prev.Next = room.Next;
            
            if(room.Next != null)
                room.Next.Prev = room.Prev;

            count--;
        }
    }

    internal class Board
    {
        public int[] _data = new int[25];   // 배열
        public MyList<int> _data2 = new MyList<int>();  // 동적배열
        public MyLinkedList<int> _data3 = new MyLinkedList<int>(); // 연결리스트

        public void Initialize()
        {
            _data3.AddLast(101);
            _data3.AddLast(102);
            MyLinkedListNode<int> node = _data3.AddLast(103);
            _data3.AddLast(104);
            _data3.AddLast(105);

            _data3.Remove(node);
        }
    }
}
