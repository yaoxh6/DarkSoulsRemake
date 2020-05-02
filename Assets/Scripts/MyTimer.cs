using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTimer
{
    public enum STATE
    {
        IDLE,
        RUN,
        FINISHED
    }
    public STATE state;

    public float duration = 0.15f;

    private float elaspedTime = 0.15f;

    public void Tick()
    {
        if(state == STATE.IDLE)
        {

        }
        else if(state == STATE.RUN)
        {
            elaspedTime += Time.deltaTime;
            if (elaspedTime >= duration)
            {
                state = STATE.FINISHED;
            }
        }
        else if(state == STATE.FINISHED)
        {

        }
        else
        {

        }
    }

    public void Go()
    {
        elaspedTime = 0;
        state = STATE.RUN;
    }
}
