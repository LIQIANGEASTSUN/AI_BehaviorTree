﻿using System.Collections.Generic;
using UnityEngine;

namespace GraphicTree
{
    public struct NodeRuning
    {
        public int nodeId;
        public float time;
        public int resultType;
    }

    public static class NodeNotify
    {
        private static Dictionary<int, NodeRuning> _nodeRunTimeDic = new Dictionary<int, NodeRuning>();
        private static Dictionary<int, int> _nodeDrawDic = new Dictionary<int, int>();

        private static int _playState = -1;

        private static int _currentDebugEntityId;

        public static void SetCurrentEndityId(int id)
        {
            _currentDebugEntityId = id;
        }

        public static void NotifyExecute(int entityId, int nodeId, int resultType, float time)
        {
#if UNITY_EDITOR
            if (entityId != _currentDebugEntityId)
            {
                return;
            }

            NodeRuning nodeRuning = new NodeRuning();
            nodeRuning.nodeId = nodeId;
            nodeRuning.time = time;
            nodeRuning.resultType = resultType;

            _nodeRunTimeDic[nodeId] = nodeRuning;
#endif
        }

        public static void SetPlayState(int state)
        {
            _playState = state;
        }

        public static float NodeDraw(int nodeId, ref int resultType)
        {
            NodeRuning nodeRuning;
            if (!_nodeRunTimeDic.TryGetValue(nodeId, out nodeRuning))
            {
                return 0;
            }

            resultType = nodeRuning.resultType;

            if (_playState == 1)
            {
                if (!_nodeDrawDic.ContainsKey(nodeId))
                {
                    _nodeDrawDic[nodeId] = 0;
                }
            }
            else
            {
                float offset = Time.realtimeSinceStartup - nodeRuning.time;
                if (offset > (0.5f / Time.timeScale))
                {
                    _nodeDrawDic[nodeId] = 0;
                    return 0;
                }

                if (!_nodeDrawDic.ContainsKey(nodeId))
                {
                    _nodeDrawDic[nodeId] = 0;
                }

                _nodeDrawDic[nodeId] += 1;
                _nodeDrawDic[nodeId] %= 100;
            }

            return _nodeDrawDic[nodeId] * 0.01f;
        }

        public static void Clear()
        {
            _nodeDrawDic.Clear();
        }

    }
}

