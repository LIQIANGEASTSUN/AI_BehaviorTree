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
        numberSprite.BTConcrete.GetParameterValue("A", ref a);

        float b = 0;
        numberSprite.BTConcrete.GetParameterValue("B", ref b);

        long c = 0;
        numberSprite.BTConcrete.GetParameterValue("C", ref c);

        long d = 0;
        numberSprite.BTConcrete.GetParameterValue("D", ref d);

        bool e = false;
        numberSprite.BTConcrete.GetParameterValue("E", ref e);

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
