using UnityEngine;
using UnityEditor;

namespace BehaviorTree
{
    public class BehaviorDrawPropertyController
    {
        private FileHandleController _fileHandleController;
        //private BehaviorPlayController _playController;
        private BehaviorPropertyOption _propertyOption;
        private BehaviorDescriptController _descriptController;
        private NodeInspectorController _nodeInspectorController;
        private ParameterController _parameterController;
        private RuntimeParameterController _runtimeParameter;

        public void Init()
        {
            _fileHandleController = new FileHandleController();
            //_playController = new BehaviorPlayController();
            _propertyOption = new BehaviorPropertyOption();
            _descriptController = new BehaviorDescriptController();
            _nodeInspectorController = new NodeInspectorController();
            _parameterController = new ParameterController();
            _runtimeParameter = new RuntimeParameterController();
        }

        public void OnDestroy()
        {
            _fileHandleController.OnDestroy();
            //_playController.OnDestroy();
            _propertyOption.OnDestroy();
            _descriptController.OnDestroy();
            _nodeInspectorController.OnDestroy();
            _parameterController.OnDestroy();
            _runtimeParameter.OnDestroy();
        }

        public void OnGUI()
        {
            _fileHandleController.OnGUI();

            //_playController.OnGUI();
            GUILayout.Space(3);

            int option = _propertyOption.OnGUI();
            if (option == 0)
            {
                _descriptController.OnGUI();
            }
            else if (option == 1)
            {
                _nodeInspectorController.OnGUI();
            }
            else if (option == 2)
            {
                if (DataController.Instance.PlayState == BehaviorPlayType.PLAY
                    || DataController.Instance.PlayState == BehaviorPlayType.PAUSE)
                {
                    _runtimeParameter.OnGUI();
                }
                else
                {
                    _parameterController.OnGUI();
                }
            }
        }
    }

    public class BehaviorPropertyOption
    {
        private int option = 1;
        private readonly string[] optionArrKey = new string[] { "Descript", "Inspect", "Parameter" };

        private string[] optionArr = new string[3] { "Descript", "Inspect", "Parameter"};
        public int OnGUI()
        {
            option = GUILayout.Toolbar(option, optionArr, EditorStyles.toolbarButton);
            return option;
        }

        public void OnDestroy()
        {

        }
    }
}


