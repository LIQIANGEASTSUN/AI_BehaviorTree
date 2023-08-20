using System.Collections.Generic;
using BehaviorTree;

public class BehaviorRegisterNode
{
    
    public static void RegisterNode()
    {
        // Add the custom node
        BehaviorConfigNode config = BehaviorConfigNode.Instance;

        BehaviorConfigNode.Instance.Config<PlayerAttackAction>("Player/Attack");
        config.Config<PlayerMoveAction>("Player/Move");
        config.Config<PlayerPatrolAction>("Player/Patrol");
        config.Config<PlayerReplenishEnergyAction>("Player/Replenish Energy");
        config.Config<PlayerSearchEnemyAction>("Player/Search Enemy");
        config.Config<PlayerEnougthEnergyCondition>("Player/Enougth Energy Condition");

        config.Config<NumberActionDo1>("Number/Do1");
        config.Config<NumberActionDo2>("Number/Do2");
        config.Config<NumberActionDo3>("Number/Do3");
        config.Config<NumberActionDo4>("Number/Do4");

        config.Config<NodeConditionCustom>("Custom Condition");

        #region DefaultParameter
        config.ConfigDefaultParameter<PlayerEnougthEnergyCondition>(new List<string>() { BTConstant.Energy });
        config.ConfigDefaultParameter<PlayerSearchEnemyAction>(new List<string>() { BTConstant.EnergyMin, BTConstant.IsSurvial });
        #endregion
    }

}
