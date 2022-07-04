using BehaviorTree;

public abstract class NodeSubTreeDynamicBase : NodeSubTreeDynamicAB, IBTActionOwner
{
    protected ISprite _owner = null;

    public virtual void SetOwner(ISprite owner)
    {
        _owner = owner;
    }

    public virtual ISprite GetOwner()
    {
        return _owner;
    }

    protected override void TreeSetOwner(NodeBase subTreeNode)
    {
        BTActionOwnerTool.NodeSetOwner(_owner, subTreeNode);
    }
}
