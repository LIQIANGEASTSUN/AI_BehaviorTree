using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberTest : MonoBehaviour
{

    public static NumberTest Instance = null;
    private NumberSprite _numberSprite;

    public int A;
    public float B;
    public long C;
    public long D;
    public bool E;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _numberSprite.BTBase.UpdateParameter("A", A);
            _numberSprite.BTBase.UpdateParameter("B", B);
            _numberSprite.BTBase.UpdateParameter("C", C);
            _numberSprite.BTBase.UpdateParameter("D", D);
            _numberSprite.BTBase.UpdateParameter("E", E);
        }
    }

    public void SetNumberSprite(NumberSprite numberSprite)
    {
        _numberSprite = numberSprite;
    }



}
