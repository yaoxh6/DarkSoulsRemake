using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInput : MonoBehaviour
{
    [Header("----- Joystick setting -----")]
    public string axisX = "axisX";
    public string axisY = "axisY";
    public string axisJright = "axis4";
    public string axisJup = "axis5";
    public string btnA = "btn0";
    public string btnB = "btn1";
    public string btnC = "btn2";
    public string btnD = "btn3";

    [Header("----- Output signals -----")]
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec;
    public float Jup;
    public float Jright;


    public bool bIsRun;

    public bool jump;
    private bool lastjump;

    public bool attack;
    private bool lastattack;

    [Header("----- others -----")]
    public bool inputEnabled;

    private float TargetDup;
    private float TargetDright;
    private float VelocityDup;
    private float VelocityRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Jup = -Input.GetAxis(axisJup);
        Jright = Input.GetAxis(axisJright);

        TargetDup = Input.GetAxis(axisY);
        TargetDright = Input.GetAxis(axisX);

        if (inputEnabled == false)
        {
            TargetDup = 0;
            TargetDright = 0;
        }


        Dup = Mathf.SmoothDamp(Dup, TargetDup, ref VelocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, TargetDright, ref VelocityRight, 0.1f);

        Vector2 tempDAxis = SquareToCircle(new Vector2(Dright, Dup));
        float Dright2 = tempDAxis.x;
        float Dup2 = tempDAxis.y;

        Dmag = Mathf.Sqrt(Mathf.Sqrt(Dup2 * Dup2 + Dright2 * Dright2));
        Dvec = Dup * transform.forward + Dright * transform.right;

        bIsRun = Input.GetButton(btnA);

        bool newJump = Input.GetButton(btnB);
        if (newJump != lastjump && newJump == true)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }
        lastjump = newJump;

        bool newAttack = Input.GetButton(btnC);
        if (newAttack != lastattack && newAttack == true)
        {
            attack = true;
        }
        else
        {
            attack = false;
        }
        lastattack = newAttack;
    }

    Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);
        return output;
    }
}
