using System.Collections.Generic;
using GraphicTree;

namespace BehaviorTree
{
    /// <summary>
    /// 自定义节点配置
    /// </summary>
    public class BehaviorConfigNode : AbstractConfigNode
    {
        public readonly static BehaviorConfigNode Instance = new BehaviorConfigNode();

        public BehaviorConfigNode() : base()
        {
            Init();
        }

        public override void Init()
        {
            base.Init();
#if true
            #region 行为节点
            Config<PlayerAttackAction>("Player/攻击");
            Config<PlayerMoveAction>("Player/移动");
            Config<PlayerPatrolAction>("Player/巡逻");
            Config<PlayerReplenishEnergyAction>("Player/补充能量");
            Config<PlayerSearchEnemyAction>("Player/搜索敌人");


            Config<NumberActionDo1>("Number/Do1");
            Config<NumberActionDo2>("Number/Do2");
            Config<NumberActionDo3>("Number/Do3");
            Config<NumberActionDo4>("Number/Do4");
            #endregion

            #region 条件节点
            Config<PlayerEnougthEnergyCondition>("Player/能量是否足够判断");
            #endregion
#endif

            Config<NodeConditionCustom>("通用条件节点");


            #region DefaultParameter
            ConfigDefaultParameter<PlayerEnougthEnergyCondition>(new List<string>() { BTConstant.Energy });
            ConfigDefaultParameter<PlayerSearchEnemyAction>(new List<string>() { BTConstant.EnergyMin, BTConstant.IsSurvial });
            #endregion
        }

        private void ConfigDefaultParameter<T>(List<string> parameterList) where T : NodeBase, new()
        {
            string identificationName = CustomIdentification.GetIdentification<T>();
            CustomIdentification info = GetIdentification(identificationName);
            info.DefaultParameterList.AddRange(parameterList);
        }

        protected override void PrimaryNode()
        {
            #region 组合节点
            
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

            #region 修饰节点
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