using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : IUserInput
{
    // Start is called before the first frame update

    [Header("----- key settings -----")]
    public string KeyUp = "w";
    public string KeyDown = "s";
    public string KeyLeft = "a";
    public string KeyRight = "d";

    public string keyA = "left shift";
    public string keyB = "q";
    public string keyRB = "mouse 0";
    public string keyD = "mouse 1";
    public string keyRT = "";
    public string keyLB = "";
    public string keyLT = "";

    public string keyJRight;
    public string keyJLeft;
    public string keyJUp;
    public string keyJDown;

    public MyButton buttonA = new MyButton();//run
    public MyButton buttonB = new MyButton();//lockon
    public MyButton buttonC = new MyButton();//attack
    public MyButton buttonD = new MyButton();//defense
    public MyButton buttonLB = new MyButton();
    public MyButton buttonLT = new MyButton();
    public MyButton buttonRB = new MyButton();
    public MyButton buttonRT = new MyButton();

    [Header("----- Mouse Setting -----")]
    public bool mouseEnable = true;
    public float mouseSensitivityX = 1.0f;
    public float mouseSensitivityY = 1.0f;

    void Awake()
    {
        KeyUp = "w";
        KeyDown = "s";
        KeyLeft = "a";
        KeyRight = "d";

        inputEnabled = true;

    }

    // Update is called once per frame
    void Update()
    {

        buttonA.Tick(Input.GetKey(keyA));
        buttonB.Tick(Input.GetKey(keyB));
        buttonRB.Tick(Input.GetKey(keyRB));
        buttonRT.Tick(Input.GetKey(keyRT));
        buttonLT.Tick(Input.GetKey(keyLT));
        buttonLB.Tick(Input.GetKey(keyLB));
        buttonD.Tick(Input.GetKey(keyD));


        if (mouseEnable == true)
        {
            Jup = Input.GetAxis("Mouse Y") * mouseSensitivityY * 3.0f;
            Jright = Input.GetAxis("Mouse X") * mouseSensitivityX * 2.5f;
        }
        else
        {
            Jup = (Input.GetKey(keyJUp) ? 1.0f : 0.0f) - (Input.GetKey(keyJDown) ? 1.0f : 0.0f);
            Jright = (Input.GetKey(keyJRight) ? 1.0f : 0.0f) - (Input.GetKey(keyJLeft) ? 1.0f : 0.0f);
        }




        TargetDup = (Input.GetKey(KeyUp) ? 1.0f : 0.0f) - (Input.GetKey(KeyDown) ? 1.0f : 0.0f);
        TargetDright = (Input.GetKey(KeyRight) ? 1.0f : 0.0f) - (Input.GetKey(KeyLeft) ? 1.0f : 0.0f);

        if(inputEnabled == false)
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
        defense = buttonD.IsPressing;
        jump = buttonA.OnPressed && buttonA.IsExtending;
        roll = buttonA.OnReleased && buttonA.IsDelaying;
        //attack = buttonC.OnPressed;
        lockon = buttonB.OnPressed;
        rb = buttonRB.OnPressed;
        rt = buttonRT.OnPressed;
        lb = buttonLB.OnPressed;
        lt = buttonLT.OnPressed;
    }

}
