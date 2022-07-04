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
            Config<NodeSelect>("选择节点", (int)NODE_TYPE.SELECT);
            Config<NodeSequence>("顺序节点", (int)NODE_TYPE.SEQUENCE);
            Config<NodeRandomSelect>("随机选择节点", (int)NODE_TYPE.RANDOM);
            Config<NodeRandomSequence>("随机顺序节点", (int)NODE_TYPE.RANDOM_SEQUEUECE);
            Config<NodeRandomPriority>("随机权重节点", (int)NODE_TYPE.RANDOM_PRIORITY);
            Config<NodeParallel>("并行节点", (int)NODE_TYPE.PARALLEL);
            Config<NodeParallelSelect>("并行选择节点", (int)NODE_TYPE.PARALLEL_SELECT);
            Config<NodeParallelAll>("并行执行所有节点", (int)NODE_TYPE.PARALLEL_ALL);
            Config<NodeIfJudgeParallel>("IF 判断并行节点", (int)NODE_TYPE.IF_JUDEG_PARALLEL);
            Config<NodeIfJudgeSequence>("IF 判断顺序节点", (int)NODE_TYPE.IF_JUDEG_SEQUENCE);
            #endregion

            #region 修饰节点
            Config<NodeDecoratorInverter>("修饰节点_取反", (int)NODE_TYPE.DECORATOR_INVERTER);
            Config<NodeDecoratorRepeat>("修饰节点_重复", (int)NODE_TYPE.DECORATOR_REPEAT);
            Config<NodeDecoratorReturnFail>("修饰_返回Fail", (int)NODE_TYPE.DECORATOR_RETURN_FAIL);
            Config<NodeDecoratorReturnSuccess>("修饰_返回Success", (int)NODE_TYPE.DECORATOR_RETURN_SUCCESS);
            Config<NodeDecoratorUntilFail>("修饰_直到Fail", (int)NODE_TYPE.DECORATOR_UNTIL_FAIL);
            Config<NodeDecoratorUntilSuccess>("修饰_直到Success", (int)NODE_TYPE.DECORATOR_UNTIL_SUCCESS);
            #endregion

            #region SubTree
            Config<NodeSubTree>("子树", (int)NODE_TYPE.SUB_TREE);
            #endregion
        }
    }
}