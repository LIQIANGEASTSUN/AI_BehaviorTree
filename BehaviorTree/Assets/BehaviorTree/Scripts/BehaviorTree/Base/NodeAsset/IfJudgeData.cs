
namespace BehaviorTree
{
    public class IfJudgeData
    {
        public int nodeId;
        public int ifJudegType = (int)NodeIfJudgeEnum.IF;
        public int ifResult = (int)ResultType.Fail;

        public IfJudgeData Clone()
        {
            IfJudgeData ifJudgeData = new IfJudgeData();
            ifJudgeData.nodeId = nodeId;
            ifJudgeData.ifJudegType = ifJudegType;
            ifJudgeData.ifResult = ifResult;
            return ifJudgeData;
        }
    }
}