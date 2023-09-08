using System;
using System.Collections.Generic;
using System.Text;

namespace EL_DC_Charger.Queue
{
    public class ArrayQueue
    {
        protected List<object> mList = new List<object>();

        // 큐 배열 생성
        public ArrayQueue()
        {

        }

        public int getSize() => mList.Count;

        // 큐가 비어있는지 확인
        public bool empty()
        {
            if (mList.Count > 0) return false;
            else
            {
                clear();
                return true;
            }
        }

        // 큐가 꽉 찼는지 확인
        public bool full()
        {
            if (mList.Count > 700) return true;
            else return false;
        }

        // 큐가 꽉 찼는지 확인
        public void clear() => mList.Clear();

        // 큐 rear에 데이터 등록
        public void insert(object item)
        {
            if (full()) clear();
            mList.Add(item);
        }

        // 큐에서 front 데이터 조회
        public object peek()
        {
            if (empty()) return null;

            return mList[0];
        }


        public object peek(int indexArray)
        {
            if (empty()) return null;

            return mList[indexArray];
        }

        // 큐에서 front 데이터 제거
        public object remove()
        {
            if (empty())
            {
                mList.Clear();
                return null;
            }
            object item = mList[0];
            mList.RemoveAt(0);
            return item;
        }

        // 큐에서 front 데이터 제거
        public object remove(int indexArray)
        {
            if (empty()) return null;

            object item = mList[0];
            mList.RemoveAt(0);
            return item;
        }
    }
}
