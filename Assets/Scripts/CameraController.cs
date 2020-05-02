using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public PlayerInput playerinput;
    private IUserInput playerinput;
    public float horizontalSpeed = 100.0f;
    public float verticalSpeed = 100.0f;
    public float cameraDampValue = 0.05f;
    public Vector3 cameraDampVelocity;


    private GameObject playerHandle;
    private GameObject cameraHanle;
    private float tempEulerX;
    private GameObject model;
    private GameObject camera;
    [SerializeField]
    private GameObject lockTarget;

    void Start()
    {
        cameraHanle = transform.parent.gameObject;
        playerHandle = cameraHanle.transform.parent.gameObject;
        tempEulerX = 20.0f;
        ActorController ac = playerHandle.GetComponent<ActorController>();
        model = ac.model;
        playerinput = ac.playerInput;
        camera = Camera.main.gameObject;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(lockTarget == null)
        {
            Vector3 tempModelEuler = model.transform.eulerAngles;

            playerHandle.transform.Rotate(Vector3.up, playerinput.Jright * horizontalSpeed * Time.fixedDeltaTime);
            //cameraHanle.transform.Rotate(Vector3.right, playerinput.Jup * -verticalSpeed * Time.deltaTime);
            tempEulerX -= playerinput.Jup * verticalSpeed * Time.fixedDeltaTime;
            cameraHanle.transform.localEulerAngles = new Vector3(Mathf.Clamp(tempEulerX, -40, 30), 0, 0);

            model.transform.eulerAngles = tempModelEuler;
        }
        else
        {
            Vector3 tempForward = lockTarget.transform.position - model.transform.position;
            tempForward.y = 0;
            playerHandle.transform.forward = tempForward;
        }
        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, transform.position, ref cameraDampVelocity, cameraDampValue);
        //camera.transform.eulerAngles = transform.eulerAngles;
        camera.transform.LookAt(cameraHanle.transform);
    }

    public void LockUnlock()
    {
        //if(lockTarget == null)
        //{
            Vector3 modelOrigin1 = model.transform.position;
            Vector3 mpdelOrigin2 = modelOrigin1 + new Vector3(0, 1, 0);
            Vector3 boxCenter = mpdelOrigin2 + model.transform.forward * 5.0f;
            Collider[] cols = Physics.OverlapBox(boxCenter, new Vector3(0.5f, 0.5f, 5f),model.transform.rotation,LayerMask.GetMask("Enemy"));

        if(cols.Length == 0)
        {
            lockTarget = null;
        }
        else
        {
            foreach (var col in cols)
            {
                if(lockTarget == col.gameObject)
                {
                    lockTarget = null;
                    break;
                }
                lockTarget = col.gameObject;
                break;
            }
        }
        //}
        //else
        //{
        //    lockTarget = null;
        //}
    }
}
