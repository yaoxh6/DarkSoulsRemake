using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{

    public GameObject model;
    public PlayerInput playerInput;
    public float walkSpeed = 2.0f;
    public float runMultiplier = 2.7f;
    public float jumpVelocity = 3.5f;

    [SerializeField]
    private Animator anim;
    private Rigidbody rigidbody;
    private Vector3 planarVec;
    private Vector3 thrustVec;

    private bool lockPlanar = false;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        anim = model.GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("forward", playerInput.Dmag * Mathf.Lerp(anim.GetFloat("forward"), (playerInput.bIsRun ? 2.0f : 1.0f), 0.5f));
        if(playerInput.jump)
        {
            anim.SetTrigger("jump");
        }
        if (playerInput.Dmag > 0.1f)
        {
            model.transform.forward = Vector3.Slerp(model.transform.forward, playerInput.Dvec, 0.3f);
        }

        if(lockPlanar == false)
        {
            planarVec = model.transform.forward * playerInput.Dmag * walkSpeed * (playerInput.bIsRun ? runMultiplier : 1.0f);
        }

    }

    void FixedUpdate()
    {
        // rigidbody.position += planarVec * Time.fixedDeltaTime;
        rigidbody.velocity = new Vector3(planarVec.x, rigidbody.velocity.y, planarVec.z) + thrustVec;
        thrustVec = Vector3.zero;
    }

    public void OnJumpEnter()
    {
        playerInput.inputEnabled = false;
        lockPlanar = true;
        thrustVec = new Vector3(0, jumpVelocity, 0);
    }

    public void OnJumpExit()
    {
        playerInput.inputEnabled = true;
        lockPlanar = false;
    }
}
