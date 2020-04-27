using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("----- key settings -----")]
    public string KeyUp;
    public string KeyDown;
    public string KeyLeft;
    public string KeyRight;

    public string keyA;
    public string keyB;
    public string keyC;
    public string keyD;

    public string keyJRight;
    public string keyJLeft;
    public string keyJUp;
    public string keyJDown;

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



    void Start()
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
        Jup = (Input.GetKey(keyJUp) ? 1.0f : 0.0f) - (Input.GetKey(keyJDown) ? 1.0f : 0.0f);
        Jright = (Input.GetKey(keyJRight) ? 1.0f : 0.0f) - (Input.GetKey(keyJLeft) ? 1.0f : 0.0f);

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

        bIsRun = Input.GetKey(keyA);

        bool newJump = Input.GetKey(keyB);
        if(newJump != lastjump && newJump == true)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }
        lastjump = newJump;

        bool newAttack = Input.GetKey(keyC);
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
