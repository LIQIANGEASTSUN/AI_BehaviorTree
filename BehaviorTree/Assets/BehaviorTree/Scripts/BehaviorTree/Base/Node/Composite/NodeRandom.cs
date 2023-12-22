using System.Collections.Generic;

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


