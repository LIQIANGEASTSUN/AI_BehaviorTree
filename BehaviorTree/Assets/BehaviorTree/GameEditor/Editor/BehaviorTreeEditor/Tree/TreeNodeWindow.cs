using UnityEngine;
using UnityEditor;
using System;

namespace BehaviorTree
{

    public class TreeNodeWindow : EditorWindow
    {
        public static TreeNodeWindow window;
        private static Rect windowsPosition = new Rect(10, 30, 1236, 864);

        private const string BehaviorTreeX = "BehaviorTreeX";
        private const string BehaviorTreeY = "BehaviorTreeY";
        private const string BehaviorTreeWidth = "BehaviorTreeWidth";
        private const string BehaviorTreeHeight = "BehaviorTreeHeight";

        private BehaviorManager _behaviorManager = null;
        public delegate void DrawWindowCallBack(Action callBack);
        public static DrawWindowCallBack _drawWindowCallBack;

        [MenuItem("Window/BehaviorTree")]
        public static void ShowWindow()
        {
            window = EditorWindow.GetWindow<TreeNodeWindow>();
            window.position = windowsPosition;
            window.autoRepaintOnSceneChange = true;
            window.Show();

            EditorPrefs.SetFloat(BehaviorTreeX, windowsPosition.x);
            EditorPrefs.SetFloat(BehaviorTreeY, windowsPosition.y);
            EditorPrefs.SetFloat(BehaviorTreeWidth, windowsPosition.width);
            EditorPrefs.SetFloat(BehaviorTreeHeight, windowsPosition.height);
        }

        public static void Closed()
        {
            if (null != window)
            {
                window.Close();
                window = null;
            }
        }

        private void OnEnable()
        {
            _behaviorManager = new BehaviorManager();
            EditorApplication.update += OnFrame;
            _drawWindowCallBack += DrawWindow;
        }

        private void OnDisable()
        {
            _behaviorManager.OnDestroy();
            EditorApplication.update -= OnFrame;
            _drawWindowCallBack -= DrawWindow;
        }

        private void OnFrame()
        {
            _behaviorManager.Update();
        }

        private void OnGUI()
        {
            if (null == window)
            {
                Closed();
                ShowWindow();
                return;
            }
            windowsPosition = window.position;

            _behaviorManager.OnGUI();


            Repaint();
        }

        private void DrawWindow(Action callBack)
        {
            // 开始绘制节点 
            // 注意：必须在  BeginWindows(); 和 EndWindows(); 之间 调用 GUI.Window 才能显示
            BeginWindows();
            {
                if (null != callBack)
                {
                    callBack();
                }
            }
            EndWindows();
        }

        public void ShowNotification(string meg)
        {
            ShowNotification(new GUIContent(meg));
            //RemoveNotification();
        }
    }
}
