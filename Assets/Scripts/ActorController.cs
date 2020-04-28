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
    public float rollVelocity = 3.0f;
    public float jabMultiplier = 2.0f;

    [Space(10)]
    [Header("===== friction Settings =====")]
    public PhysicMaterial frictionOne;
    public PhysicMaterial frictionZero;

    private Animator anim;
    private Rigidbody rigidbody;
    private Vector3 planarVec;
    private Vector3 thrustVec;
    private bool canAttack;
    private bool lockPlanar = false;
    private CapsuleCollider col;
    private float lerpTarget;


    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        anim = model.GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("forward", playerInput.Dmag * Mathf.Lerp(anim.GetFloat("forward"), (playerInput.bIsRun ? 2.0f : 1.0f), 0.5f));
        if(rigidbody.velocity.magnitude > 5.0f)
        {
            anim.SetTrigger("roll");
        }

        if (playerInput.jump)
        {
            anim.SetTrigger("jump");
            canAttack = false;
        }

        if (playerInput.attack && CheckState("ground") && canAttack)
        {
            anim.SetTrigger("attack");
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

    private bool CheckState(string stateName, string layerName = "Base Layer")
    {
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsName(stateName);
    }


    //
    //
    //
    public void OnJumpEnter()
    {
        playerInput.inputEnabled = false;
        lockPlanar = true;
        thrustVec = new Vector3(0, jumpVelocity, 0);
    }

    //public void OnJumpExit()
    //{
    //    playerInput.inputEnabled = true;
    //    lockPlanar = false;
    //}

    public void IsGround()
    {
        // print("IsOnGround");
        anim.SetBool("isGround", true);
    }
    
    public void IsNotGround()
    {
        // print("IsNotGround");
        anim.SetBool("isGround", false);
    }

    public void OnGroundEnter()
    {
        playerInput.inputEnabled = true;
        lockPlanar = false;
        canAttack = true;
        col.material = frictionOne;
    }

    public void OnGroundExit()
    {
        col.material = frictionZero;
    }

    public void OnFallEnter()
    {
        playerInput.inputEnabled = false;
        lockPlanar = true;
    }

    public void OnRollEnter()
    {
        playerInput.inputEnabled = false;
        lockPlanar = true;
        thrustVec = new Vector3(0, rollVelocity, 0);
    }

    public void OnJabEnter()
    {
        playerInput.inputEnabled = false;
        lockPlanar = true;
    }

    public void OnJabUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("jabVelocity") * jabMultiplier;
    }

    public void OnAttack1hAEnter()
    {
        playerInput.inputEnabled = false;
        //lockPlanar = true;
        lerpTarget = 1.0f;
    }

    public void OnAttack1hAUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("attack1hAVelocity") * jabMultiplier;
        float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("attack"));
        currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.1f);
        anim.SetLayerWeight(anim.GetLayerIndex("attack"), currentWeight);
    }

    public void OnAttackIdleEnter()
    {
        playerInput.inputEnabled = true;
        //lockPlanar = false;
        //anim.SetLayerWeight(anim.GetLayerIndex("attack"), 0.0f);
        lerpTarget = 0.0f;
    }

    public void OnAttackIdleUpdate()
    {
        float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("attack"));
        currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.1f);
        anim.SetLayerWeight(anim.GetLayerIndex("attack"), currentWeight);
    }
}
