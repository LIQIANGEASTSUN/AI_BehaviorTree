using UnityEditor;
using UnityEditor.Compilation;

[InitializeOnLoad]
public class BehaviorRegisterNodeEditor : Editor
{
    static BehaviorRegisterNodeEditor()
    {
        CompilationPipeline.compilationFinished += OnCompilationFinished;
        BehaviorRegisterNode.Instance.RegisterNode();
    }

    private static void OnCompilationFinished(object obj)
    {
        BehaviorRegisterNode.Instance.RegisterNode();
    }
}