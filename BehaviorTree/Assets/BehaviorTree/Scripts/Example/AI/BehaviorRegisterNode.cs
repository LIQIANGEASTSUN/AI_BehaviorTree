using BehaviorTree;

public class BehaviorRegisterNode
{
    
    /// <summary>
    /// 注册自定义节点
    /// </summary>
    public static void RegisterNode()
    {
        // Add the custom node
        BehaviorConfigNode config = BehaviorConfigNode.Instance;

        config.Config<PlayerAttackAction>("Player/Attack");
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
    }
}
