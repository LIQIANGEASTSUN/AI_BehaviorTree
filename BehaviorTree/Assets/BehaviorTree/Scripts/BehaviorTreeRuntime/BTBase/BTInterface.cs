using System.Collections.Generic;
using BehaviorTree;

public interface IBTActionOwner
{
    void SetOwner(IBTOwner owner);

    IBTOwner GetOwner();
}

public class BTActionOwnerTool
{
    public static void NodeSetOwner(IBTOwner owner, NodeBase nodeBase)
    {
        Queue<NodeBase> queue = new Queue<NodeBase>();
        queue.Enqueue(nodeBase);
        while (queue.Count > 0)
        {
            NodeBase node = queue.Dequeue();
            if (null == node)
            {
                continue;
            }
            if (typeof(IBTActionOwner).IsAssignableFrom(node.GetType()))
            {
                IBTActionOwner bTActionOwner = node as IBTActionOwner;
                bTActionOwner.SetOwner(owner);
            }

            if (node.NodeType() == (int)NODE_TYPE.ACTION && node.NodeType() == (int)NODE_TYPE.CONDITION)
            {
                continue;
            }
            NodeComposite nodeComposite = node as NodeComposite;
            if (null == nodeComposite)
            {
                continue;
            }
            foreach (var childNode in nodeComposite.GetChilds())
            {
                queue.Enqueue(childNode);
            }
        }
    }
}