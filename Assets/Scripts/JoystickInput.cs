using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInput : IUserInput
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
    public string btnLB = "btn4";
    public string btnLT = "btn6";

    public MyButton buttonA = new MyButton();
    public MyButton buttonB = new MyButton();
    public MyButton buttonC = new MyButton();
    public MyButton buttonD = new MyButton();
    public MyButton buttonLB = new MyButton();
    public MyButton buttonLT = new MyButton();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        buttonA.Tick(Input.GetButton(btnA));
        buttonB.Tick(Input.GetButton(btnB));
        buttonC.Tick(Input.GetButton(btnC));
        buttonD.Tick(Input.GetButton(btnD));
        buttonLB.Tick(Input.GetButton(btnLB));
        buttonLT.Tick(Input.GetButton(btnLT));

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

        bIsRun = buttonA.IsPressing && !buttonA.IsDelaying || buttonA.IsExtending;
        defense = buttonLB.IsPressing;
        jump = buttonA.OnPressed && buttonA.IsExtending;
        roll = buttonA.OnReleased && buttonA.IsDelaying;
        attack = buttonC.OnPressed;
    }

}
