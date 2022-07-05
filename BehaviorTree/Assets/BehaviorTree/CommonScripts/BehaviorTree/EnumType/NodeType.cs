namespace BehaviorTree
{
    /// <summary>
    /// 行为树节点类型
    /// </summary>
    public enum NODE_TYPE
    {
        /// <summary>
        /// SelectNode
        /// </summary>
        [EnumAttirbute("SelectNode")]
        SELECT = 0,

        /// <summary>
        /// SequenceNode
        /// </summary>
        [EnumAttirbute("SequenceNode")]
        SEQUENCE = 1,

        /// <summary>
        /// RandomNode
        /// </summary>
        [EnumAttirbute("RandomNode")]
        RANDOM = 2,

        /// <summary>
        /// RandomSequeuece
        /// </summary>
        [EnumAttirbute("RandomSequeuece")]
        RANDOM_SEQUEUECE = 3,

        /// <summary>
        /// RandomPriority
        /// </summary>
        [EnumAttirbute("RandomPriority")]
        RANDOM_PRIORITY = 4,

        /// <summary>
        /// ParallelNode
        /// </summary>
        [EnumAttirbute("ParallelNode")]
        PARALLEL = 5,

        /// <summary>
        /// ParalleSelect
        /// </summary>
        [EnumAttirbute("ParalleSelect")]
        PARALLEL_SELECT = 6,

        /// <summary>
        /// ParalleAll
        /// </summary>
        [EnumAttirbute("ParalleAll")]
        PARALLEL_ALL = 7,

        /// <summary>
        /// IfJudgeParallel
        /// </summary>
        [EnumAttirbute("IfJudgeParallel")]
        IF_JUDEG_PARALLEL = 8,

        /// <summary>
        /// IfJudgeSequence
        /// </summary>
        [EnumAttirbute("IfJudgeSequence")]
        IF_JUDEG_SEQUENCE = 9,

        /// <summary>
        /// DecoratorInverter
        /// </summary>
        [EnumAttirbute("DecoratorInverter")]
        DECORATOR_INVERTER = 100,

        /// <summary>
        /// DecoratorRepeat
        /// </summary>
        [EnumAttirbute("DecoratorRepeat")]
        DECORATOR_REPEAT = 101,

        /// <summary>
        /// DecoratorReturnFail
        /// </summary>
        [EnumAttirbute("DecoratorReturnFail")]
        DECORATOR_RETURN_FAIL = 102,

        /// <summary>
        /// DecoratorReturnSuccess
        /// </summary>
        [EnumAttirbute("DecoratorReturnSuccess")]
        DECORATOR_RETURN_SUCCESS = 103,

        /// <summary>
        /// DecoratorUntilFail
        /// </summary>
        [EnumAttirbute("DecoratorUntilFail")]
        DECORATOR_UNTIL_FAIL = 104,

        /// <summary>
        /// DecoratorUntilSuccess
        /// </summary>
        [EnumAttirbute("DecoratorUntilSuccess")]
        DECORATOR_UNTIL_SUCCESS = 105,

        /// <summary>
        /// Condition
        /// </summary>
        [EnumAttirbute("Condition")]
        CONDITION = 200,

        /// <summary>
        /// Action
        /// </summary>
        [EnumAttirbute("Action")]
        ACTION = 300,

        /// <summary>
        /// SubTreeNode
        /// </summary>
        [EnumAttirbute("SubTreeNode")]
        SUB_TREE = 1000,
    }


    /// <summary>
    /// 节点状态
    /// </summary>
    public enum NODE_STATUS
    {
        /// <summary>
        /// 准备状态
        /// </summary>
        READY = 0,

        /// <summary>
        /// 运行状态
        /// </summary>
        RUNNING = 1,
    }

    /// <summary>
    /// 子树类型
    /// </summary>
    public enum SUB_TREE_TYPE
    {
        /// <summary>
        /// 普通：可编辑子节点
        /// </summary>
        [EnumAttirbute("普通：可编辑子节点")]
        NORMAL = 0,

        /// <summary>
        /// 配置：单独的树配置，不可编辑子节点
        /// </summary>
        [EnumAttirbute("配置：读取配置文件")]
        CONFIG = 1,
    }

    /// <summary>
    /// 判断节点类型
    /// </summary>
    public enum NodeIfJudgeEnum
    {
        /// <summary>
        /// if 判断节点
        /// </summary>
        [EnumAttirbute("if 判断节点")]
        IF = 0,

        /// <summary>
        /// 执行节点
        /// </summary>
        [EnumAttirbute("执行节点")]
        ACTION = 1,
    }
}