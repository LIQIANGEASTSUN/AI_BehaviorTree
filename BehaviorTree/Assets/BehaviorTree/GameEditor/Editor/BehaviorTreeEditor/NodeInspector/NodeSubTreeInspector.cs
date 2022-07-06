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
            string subTreeValue = Localization.GetInstance().Format("SubTreeValue");
            nodeValue.subTreeValue = EditorGUILayout.LongField(subTreeValue, nodeValue.subTreeValue);
            string[] nameArr = EnumNames.GetEnumNames<SUB_TREE_TYPE>();
            int index = EnumNames.GetEnumIndex<SUB_TREE_TYPE>((SUB_TREE_TYPE)nodeValue.subTreeType);
            string subTreeType = Localization.GetInstance().Format("SubTreeType");
            int result = EditorGUILayout.Popup(subTreeType, index, nameArr);
            nodeValue.subTreeType = (int)(EnumNames.GetEnum<SUB_TREE_TYPE>(result));
            string configFile = Localization.GetInstance().Format("ConfigFile");
            nodeValue.subTreeConfig = EditorGUILayout.TextField(new GUIContent(configFile), nodeValue.subTreeConfig);
            GUILayout.Space(5);

            if (nodeValue.subTreeType == (int)SUB_TREE_TYPE.CONFIG)
            {
                DataHandler dataHandler = new DataHandler();
                dataHandler.DeleteSubTreeChild(nodeValue.id);

                string selectSubTreeConfig = Localization.GetInstance().Format("SelectSubTreeConfig");
                if (GUILayout.Button(selectSubTreeConfig))
                {
                    ConfigFileSelect configFileSelect = new ConfigFileSelect();
                    string filePath = configFileSelect.Select();
                    nodeValue.subTreeConfig = System.IO.Path.GetFileNameWithoutExtension(filePath);
                }
            }
            else
            {
                string changeSubtreeToConfig = Localization.GetInstance().Format("ChangeSubtreeToConfig");
                if (GUILayout.Button(changeSubtreeToConfig))
                {
                    ConfigFileSaveSubTree configFileSaveSubTree = new ConfigFileSaveSubTree();
                    configFileSaveSubTree.SaveSubTree(nodeValue.subTreeConfig, nodeValue.id);
                }
            }
        }

    }

}
