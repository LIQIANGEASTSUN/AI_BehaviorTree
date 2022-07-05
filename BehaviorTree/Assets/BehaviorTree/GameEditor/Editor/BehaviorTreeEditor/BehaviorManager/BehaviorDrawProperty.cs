using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BehaviorTree
{
    public class BehaviorDrawPropertyController
    {
        private BehaviorFileHandleController _fileHandleController;
        //private BehaviorPlayController _playController;
        private BehaviorPropertyOption _propertyOption;
        private BehaviorDescriptController _descriptController;
        private BehaviorNodeInspectorController _nodeInspectorController;
        private BehaviorParameterController _parameterController;
        private BehaviorRuntimeParameterController _runtimeParameter;

        public void Init()
        {
            _fileHandleController = new BehaviorFileHandleController();
            //_playController = new BehaviorPlayController();
            _propertyOption = new BehaviorPropertyOption();
            _descriptController = new BehaviorDescriptController();
            _nodeInspectorController = new BehaviorNodeInspectorController();
            _parameterController = new BehaviorParameterController();
            _runtimeParameter = new BehaviorRuntimeParameterController();
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
                if (BehaviorDataController.Instance.PlayState == BehaviorPlayType.PLAY
                    || BehaviorDataController.Instance.PlayState == BehaviorPlayType.PAUSE)
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

        private string[] optionArr = new string[3] { "", "", ""};
        public int OnGUI()
        {
            for (int i = 0; i < optionArr.Length; ++i)
            {
                string msg = Localization.GetInstance().Format(optionArrKey[i]);
                optionArr[i] = msg;
            }

            int index = option;
            option = GUILayout.Toolbar(option, optionArr, EditorStyles.toolbarButton);
            return option;
        }

        public void OnDestroy()
        {

        }

    }
}


