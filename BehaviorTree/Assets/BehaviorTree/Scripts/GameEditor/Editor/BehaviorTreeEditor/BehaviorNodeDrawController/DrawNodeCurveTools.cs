using GraphicTree;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviorTree
{
    public class DrawNodeCurveTools
    {
        // Draw the line
        public static void DrawNodeCurve(RectT start, RectT end)
        {
            Handles.color = Color.black;
            Vector3 startPos = new Vector3(start.x + start.width / 2, start.y + start.height, 0);
            Vector3 endPos = new Vector3(end.x + end.width / 2, end.y, 0);
            //Handles.DrawLine(startPos, endPos);

            Vector3 middle = (startPos + endPos) * 0.5f;
            DrawArrow(startPos, endPos, Color.black);
            Handles.color = Color.white;
        }

        private static void DrawArrow(Vector2 from, Vector2 to, Color color)
        {
            Handles.BeginGUI();
            Handles.color = color;
            Handles.DrawAAPolyLine(3, from, to);
            Vector2 v0 = from - to;
            v0 *= 10 / v0.magnitude;
            Vector2 v1 = new Vector2(v0.x * 0.866f - v0.y * 0.5f, v0.x * 0.5f + v0.y * 0.866f);
            Vector2 v2 = new Vector2(v0.x * 0.866f + v0.y * 0.5f, v0.x * -0.5f + v0.y * 0.866f);
            Vector2 middle = (from + to) * 0.5f;
            Handles.DrawAAPolyLine(3, middle + v1, middle, middle + v2);
            Handles.EndGUI();
        }

    }
}
