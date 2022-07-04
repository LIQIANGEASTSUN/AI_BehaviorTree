using UnityEngine;
using UnityEditor;

namespace BehaviorTree
{
    /// <summary>
    /// 子树节点
    /// </summary>
    public class NodeSubTreeInspector : NodeBaseInspector
    {

        protected override void Common()
        {
            SubTree();
        }

        private void SubTree()
        {
            nodeValue.subTreeValue = EditorGUILayout.LongField("SubTreeValue:", nodeValue.subTreeValue);
            string[] nameArr = EnumNames.GetEnumNames<SUB_TREE_TYPE>();
            int index = EnumNames.GetEnumIndex<SUB_TREE_TYPE>((SUB_TREE_TYPE)nodeValue.subTreeType);
            int result = EditorGUILayout.Popup("子树类型", index, nameArr);
            nodeValue.subTreeType = (int)(EnumNames.GetEnum<SUB_TREE_TYPE>(result));
            nodeValue.subTreeConfig = EditorGUILayout.TextField(new GUIContent("配置文件"), nodeValue.subTreeConfig);
            GUILayout.Space(5);

            if (nodeValue.subTreeType == (int)SUB_TREE_TYPE.CONFIG)
            {
                DataHandler dataHandler = new DataHandler();
                dataHandler.DeleteSubTreeChild(nodeValue.id);

                if (GUILayout.Button("选择子树配置文件"))
                {
                    ConfigFileSelect configFileSelect = new ConfigFileSelect();
                    nodeValue.subTreeConfig = configFileSelect.Select();
                }
            }
            else
            {
                if (GUILayout.Button("将子树存储为配置文件"))
                {
                    ConfigFileSaveSubTree configFileSaveSubTree = new ConfigFileSaveSubTree();
                    configFileSaveSubTree.SaveSubTree(nodeValue.subTreeConfig, nodeValue.id);
                }
            }
        }

    }

}
