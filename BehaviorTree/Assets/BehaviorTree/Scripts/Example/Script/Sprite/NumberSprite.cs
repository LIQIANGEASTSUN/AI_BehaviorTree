using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class NumberSprite : BaseSprite
{

    public override void Init(Vector3 position)
    {
        Create();
        base.Init(position);
    }

    protected override string AIConfigFile()
    {
        return "TTTTT";
        return "number_tree";
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.D))
        {
            string msg = GetParameterValue(this);
            Debug.LogError("parameter:" + msg);
        }
    }

    public static string GetParameterValue(NumberSprite numberSprite)
    {
        StringBuilder sb = new StringBuilder();

        int a = 0;
        numberSprite.BTBase.GetParameterValue("A", ref a);

        float b = 0;
        numberSprite.BTBase.GetParameterValue("B", ref b);

        long c = 0;
        numberSprite.BTBase.GetParameterValue("C", ref c);

        long d = 0;
        numberSprite.BTBase.GetParameterValue("D", ref d);

        bool e = false;
        numberSprite.BTBase.GetParameterValue("E", ref e);

        sb.Append("  a:" + a);
        sb.Append("  b:" + b);
        sb.Append("  c:" + c);
        sb.Append("  d:" + d);
        sb.Append("  e:" + e);

        return sb.ToString();
    }

    private void Create()
    {
        _gameObject = new GameObject("Number");
        _gameObject.transform.position = Vector3.zero;
    }

}
