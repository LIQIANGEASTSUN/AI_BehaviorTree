using BehaviorTree;

public class BehaviorRegisterNode : SingletonObject<BehaviorRegisterNode>
{
    
    /// <summary>
    /// 注册自定义节点
    /// </summary>
    public void RegisterNode()
    {
        BehaviorConfigNode.Instance.Config<PlayerAttackAction>("Player/Attack");
        BehaviorConfigNode.Instance.Config<PlayerMoveAction>("Player/Move");
        BehaviorConfigNode.Instance.Config<PlayerPatrolAction>("Player/Patrol");
        BehaviorConfigNode.Instance.Config<PlayerReplenishEnergyAction>("Player/Replenish Energy");
        BehaviorConfigNode.Instance.Config<PlayerSearchEnemyAction>("Player/Search Enemy");
        BehaviorConfigNode.Instance.Config<PlayerEnougthEnergyCondition>("Player/Enougth Energy Condition");

        BehaviorConfigNode.Instance.Config<NodeConditionCustom>("Custom Condition");
    }
}
