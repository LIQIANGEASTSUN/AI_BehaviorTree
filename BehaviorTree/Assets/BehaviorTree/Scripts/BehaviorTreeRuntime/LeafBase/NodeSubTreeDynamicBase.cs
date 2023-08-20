using BehaviorTree;

public abstract class NodeSubTreeDynamicBase : NodeSubTreeDynamicAB, IBTActionOwner
{
    protected IBTOwner _owner = null;

    public virtual void SetOwner(IBTOwner owner)
    {
        _owner = owner;
    }

    public virtual IBTOwner GetOwner()
    {
        return _owner;
    }

    protected override void TreeSetOwner(NodeBase subTreeNode)
    {
        BTActionOwnerTool.NodeSetOwner(_owner, subTreeNode);
    }
}
