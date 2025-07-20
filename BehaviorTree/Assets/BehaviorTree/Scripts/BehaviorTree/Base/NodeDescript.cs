
namespace BehaviorTree
{
    public class NodeDescript
    {
        public static string GetFunction(NODE_TYPE nodeType)
        {
            if (nodeType == NODE_TYPE.SELECT)
            {
                return NodeSelect.descript;
            }

            if (nodeType == NODE_TYPE.SEQUENCE)
            {
                return NodeSequence.descript;
            }

            if (nodeType == NODE_TYPE.RANDOM)
            {
                return NodeRandomSelect.descript;
            }
            
            if (nodeType == NODE_TYPE.IF_JUDEG_PARALLEL)
            {
                return NodeIfJudgeParallel.descript;
            }

            if (nodeType == NODE_TYPE.IF_JUDEG_SEQUENCE)
            {
                return NodeIfJudgeSequence.descript;
            }

            if (nodeType == NODE_TYPE.RANDOM_SEQUEUECE)
            {
                return NodeRandomSequence.descript;
            }

            if (nodeType == NODE_TYPE.RANDOM_PRIORITY)
            {
                return NodeRandomPriority.descript;
            }

            if (nodeType == NODE_TYPE.PARALLEL)
            {
                return NodeParallel.descript;
            }

            if (nodeType == NODE_TYPE.PARALLEL_SELECT)
            {
                return NodeParallelSelect.descript;
            }

            if (nodeType == NODE_TYPE.PARALLEL_ALL)
            {
                return NodeParallelAll.descript;
            }

            if (nodeType == NODE_TYPE.DECORATOR_INVERTER)
            {
                return NodeDecoratorInverter.descript;
            }

            if (nodeType == NODE_TYPE.DECORATOR_REPEAT)
            {
                return NodeDecoratorRepeat.descript;
            }

            if (nodeType == NODE_TYPE.DECORATOR_RETURN_FAIL)
            {
                return NodeDecoratorReturnFail.descript;
            }

            if (nodeType == NODE_TYPE.DECORATOR_RETURN_SUCCESS)
            {
                return NodeDecoratorReturnSuccess.descript;
            }

            if (nodeType == NODE_TYPE.DECORATOR_UNTIL_FAIL)
            {
                return NodeDecoratorUntilFail.descript;
            }

            if (nodeType == NODE_TYPE.DECORATOR_UNTIL_SUCCESS)
            {
                return NodeDecoratorUntilSuccess.descript;
            }

            return string.Empty;
        }
    }
}