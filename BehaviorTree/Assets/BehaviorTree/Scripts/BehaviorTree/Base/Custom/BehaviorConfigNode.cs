using GraphicTree;

namespace BehaviorTree
{
    /// <summary>
    /// Custom node configuration
    /// </summary>
    public class BehaviorConfigNode : AbstractConfigNode
    {
        private static BehaviorConfigNode _instance;
        private static object _lock = new object();
        public static BehaviorConfigNode Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (_lock)
                    {
                        if (null == _instance)
                        {
                            _instance = new BehaviorConfigNode();
                            _instance.Init();
                        }
                    }
                }
                return _instance;
            }
        }

        public BehaviorConfigNode() : base()
        {
        }

        protected override void Init()
        {
            base.Init();
            BehaviorRegisterNode.RegisterNode();
        }

        /// <summary>
        /// The initial composite node
        /// </summary>
        protected override void PrimaryNode()
        {
            #region Composite Node
            Config<NodeSelect>(EnumNames.GetEnumName<NODE_TYPE>(NODE_TYPE.SELECT), (int)NODE_TYPE.SELECT);
            Config<NodeSequence>(EnumNames.GetEnumName<NODE_TYPE>(NODE_TYPE.SEQUENCE), (int)NODE_TYPE.SEQUENCE);
            Config<NodeRandomSelect>(EnumNames.GetEnumName<NODE_TYPE>(NODE_TYPE.RANDOM), (int)NODE_TYPE.RANDOM);
            Config<NodeRandomSequence>(EnumNames.GetEnumName<NODE_TYPE>(NODE_TYPE.RANDOM_SEQUEUECE), (int)NODE_TYPE.RANDOM_SEQUEUECE);
            Config<NodeRandomPriority>(EnumNames.GetEnumName<NODE_TYPE>(NODE_TYPE.RANDOM_PRIORITY), (int)NODE_TYPE.RANDOM_PRIORITY);
            Config<NodeParallel>(EnumNames.GetEnumName<NODE_TYPE>(NODE_TYPE.PARALLEL), (int)NODE_TYPE.PARALLEL);
            Config<NodeParallelSelect>(EnumNames.GetEnumName<NODE_TYPE>(NODE_TYPE.PARALLEL_SELECT), (int)NODE_TYPE.PARALLEL_SELECT);
            Config<NodeParallelAll>(EnumNames.GetEnumName<NODE_TYPE>(NODE_TYPE.PARALLEL_ALL), (int)NODE_TYPE.PARALLEL_ALL);
            Config<NodeIfJudgeParallel>(EnumNames.GetEnumName<NODE_TYPE>(NODE_TYPE.IF_JUDEG_PARALLEL), (int)NODE_TYPE.IF_JUDEG_PARALLEL);
            Config<NodeIfJudgeSequence>(EnumNames.GetEnumName<NODE_TYPE>(NODE_TYPE.IF_JUDEG_SEQUENCE), (int)NODE_TYPE.IF_JUDEG_SEQUENCE);
            #endregion

            #region Decorator Node
            Config<NodeDecoratorInverter>(EnumNames.GetEnumName<NODE_TYPE>(NODE_TYPE.DECORATOR_INVERTER), (int)NODE_TYPE.DECORATOR_INVERTER);
            Config<NodeDecoratorRepeat>(EnumNames.GetEnumName<NODE_TYPE>(NODE_TYPE.DECORATOR_REPEAT), (int)NODE_TYPE.DECORATOR_REPEAT);
            Config<NodeDecoratorReturnFail>(EnumNames.GetEnumName<NODE_TYPE>(NODE_TYPE.DECORATOR_RETURN_FAIL), (int)NODE_TYPE.DECORATOR_RETURN_FAIL);
            Config<NodeDecoratorReturnSuccess>(EnumNames.GetEnumName<NODE_TYPE>(NODE_TYPE.DECORATOR_RETURN_SUCCESS), (int)NODE_TYPE.DECORATOR_RETURN_SUCCESS);
            Config<NodeDecoratorUntilFail>(EnumNames.GetEnumName<NODE_TYPE>(NODE_TYPE.DECORATOR_UNTIL_FAIL), (int)NODE_TYPE.DECORATOR_UNTIL_FAIL);
            Config<NodeDecoratorUntilSuccess>(EnumNames.GetEnumName<NODE_TYPE>(NODE_TYPE.DECORATOR_UNTIL_SUCCESS), (int)NODE_TYPE.DECORATOR_UNTIL_SUCCESS);
            #endregion

            #region SubTree
            Config<NodeSubTree>(EnumNames.GetEnumName<NODE_TYPE>(NODE_TYPE.SUB_TREE), (int)NODE_TYPE.SUB_TREE);
            #endregion
        }
    }
}