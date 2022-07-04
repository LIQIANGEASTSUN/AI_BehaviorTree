using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class NodeRandom : NodeComposite
    {
        private BehaviorRandom behaviorRandom;

        public NodeRandom(NODE_TYPE nodeType) : base(nodeType)
        {

        }

        public override void OnEnter()
        {
            base.OnEnter();
            BehaviorRandom.Init();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        protected BehaviorRandom BehaviorRandom
        {
            get
            {
                if (null == behaviorRandom)
                {
                    behaviorRandom = new BehaviorRandom(nodeChildList.Count);
                }
                return behaviorRandom;
            }
        }

        protected virtual int GetRandom()
        {
            return BehaviorRandom.GetRandom();
        }

        /// <summary>
        /// 移除第index个下标的位置，就是选中一个下标后
        /// 将该位置与后边没有选中的位置互换
        /// 如 idArr = {10, 20, 30, 40, 50, 60} 第一次随机从 0-5 个下标随机
        /// 假设选中了 下标 index = 1 值为 20
        /// 则将 下标 index = 1 的和 下标为 5 的值互换
        /// idArr = {10, 60, 30, 40, 50, 20} 
        /// 下次随机从 0 - 4 个下标随机，其实就是将 20排除在外了，避免重复随机到20
        /// 假设下次随机选中了下标 index = 3 值为 40
        /// 则将 下标 index = 3 的和 下标为 4 的值互换
        /// idArr = {10, 60, 30, 50, 40, 20} 
        /// 这样每次随机后将随机范围缩小一个范围，将随机得到的值移到后边，
        /// 避免了重复随机到同一个值的麻烦
        /// </summary>
        /// <param name="index"></param>
        protected void Remove(int index)
        {
            BehaviorRandom.Remove(index);
        }

    }

    public class BehaviorRandom
    {
        private int _count;
        private int _randomCount;
        private int[] _idArr;
        protected System.Random random;

        public BehaviorRandom(int count)
        {
            _count = count;
            _idArr = new int[_count];
            random = new System.Random();
            Init();
        }

        public void Init()
        {
            _randomCount = 0;
            for (int i = 0; i < _idArr.Length; ++i)
            {
                _idArr[i] = i;
            }
        }

        public void Check()
        {
            if (_randomCount < 0 || _randomCount >= _idArr.Length)
            {
                Init();
            }
        }

        public int GetRandom()
        {
            if (_count <= 0)
            {
                return 0;
            }

            if (_randomCount >= _idArr.Length)
            {
                Init();
            }

            int index = random.Next(0, 10000);
            index %= (_idArr.Length - _randomCount);
            int value = _idArr[index];
            Remove(index);
            return value;
        }

        public int RemainderCount()
        {
            int count = _idArr.Length;
            int remainder = count - _randomCount;
            return remainder;
        }

        public IEnumerable<int> GetRemainder()
        {
            int remainderCount = RemainderCount();
            for (int i = 0; i < remainderCount; ++i)
            {
                yield return _idArr[i];
            }
        }

        public void Remove(int index)
        {
            if (index < 0 || index >= _idArr.Length)
            {
                return;
            }

            if (_randomCount < 0 || _randomCount >= _idArr.Length)
            {
                return;
            }

            int count = _idArr.Length - 1;
            _idArr[index] = _idArr[count - _randomCount];
            ++_randomCount;
        }

    }

}


