using GraphicTree;
using System.Collections.Generic;

namespace BehaviorTree
{
    /// <summary>
    /// Custom node configuration
    /// </summary>
    public class BehaviorConfigNode : SingletonObject<BehaviorConfigNode>
    {
        private GraphicConfigNode GraphicConfigNode { get; set; }
        private BehaviorConfigNode()
        {
            GraphicConfigNode = new GraphicConfigNode();
        }

        public void Init()
        {
            GraphicConfigNode.Config<NodeConditionCustom>("Custom Condition");

            #region Composite Node
            Config<NodeSelect>(NODE_TYPE.SELECT);
            Config<NodeSequence>(NODE_TYPE.SEQUENCE);
            Config<NodeRandomSelect>(NODE_TYPE.RANDOM);
            Config<NodeRandomSequence>(NODE_TYPE.RANDOM_SEQUEUECE);
            Config<NodeRandomPriority>(NODE_TYPE.RANDOM_PRIORITY);
            Config<NodeParallel>(NODE_TYPE.PARALLEL);
            Config<NodeParallelSelect>(NODE_TYPE.PARALLEL_SELECT);
            Config<NodeParallelAll>(NODE_TYPE.PARALLEL_ALL);
            Config<NodeIfJudgeParallel>(NODE_TYPE.IF_JUDEG_PARALLEL);
            Config<NodeIfJudgeSequence>(NODE_TYPE.IF_JUDEG_SEQUENCE);
            #endregion

            #region Decorator Node
            Config<NodeDecoratorInverter>(NODE_TYPE.DECORATOR_INVERTER);
            Config<NodeDecoratorRepeat>(NODE_TYPE.DECORATOR_REPEAT);
            Config<NodeDecoratorReturnFail>(NODE_TYPE.DECORATOR_RETURN_FAIL);
            Config<NodeDecoratorReturnSuccess>(NODE_TYPE.DECORATOR_RETURN_SUCCESS);
            Config<NodeDecoratorUntilFail>(NODE_TYPE.DECORATOR_UNTIL_FAIL);
            Config<NodeDecoratorUntilSuccess>(NODE_TYPE.DECORATOR_UNTIL_SUCCESS);
            #endregion

            #region SubTree
            Config<NodeSubTree>(NODE_TYPE.SUB_TREE);
            #endregion
        }

        public void Config<T>(NODE_TYPE nodeType) where T : AbstractNode, new()
        {
            string name = EnumNames.GetEnumName<NODE_TYPE>(nodeType);
            GraphicConfigNode.Config<T>(name, (int)nodeType);
        }

        public void Config<T>(string name) where T : AbstractNode, new()
        {
            GraphicConfigNode.Config<T>(name);
        }

        public AbstractNode GetNode(string identificationName)
        {
            return GraphicConfigNode.GetNode(identificationName);
        }

        public Dictionary<string, CustomIdentification> GetNodeDic()
        {
            return GraphicConfigNode.GetNodeDic();
        }

        public CustomIdentification GetIdentification(string identificationName)
        {
            return GraphicConfigNode.GetIdentification(identificationName);
        }
    }
}