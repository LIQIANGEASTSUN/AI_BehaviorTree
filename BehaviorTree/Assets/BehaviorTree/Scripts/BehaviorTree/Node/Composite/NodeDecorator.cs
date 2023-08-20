namespace BehaviorTree
{
    /// <summary>
    /// decorator node
    /// </summary>
    public abstract class NodeDecorator : NodeComposite
    {
        public NodeDecorator(NODE_TYPE nodeType) : base(nodeType)
        { }

        /// <summary>
        /// The modifier node cannot exist independently, and its function is to modify the child node to get the desired result
        /// The common types of modifier nodes are as follows:
        /// Inverter        Reverse the execution result of the child node
        /// Repeater        Repeat execution of the child node 
        /// Return Fail     Execute the child node and return Fail
        /// Return Success  Execute the child node and return Success
        /// Unitl Failure   Execute child node until the child node returns fail
        /// Until Success   Execute child node until the child node returns fail
        /// </summary>
        /// <returns></returns>
        public override ResultType Execute()
        {
            return ResultType.Success;
        }
    }
}
