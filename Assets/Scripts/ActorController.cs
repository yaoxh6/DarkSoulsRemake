using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{

    public GameObject model;
    public PlayerInput playerInput;
    public float walkSpeed = 2.0f;
    public float runMultiplier = 2.7f;

    [SerializeField]
    private Animator anim;
    private Rigidbody rigidbody;
    private Vector3 movingVec;

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
        movingVec = model.transform.forward * playerInput.Dmag * walkSpeed * (playerInput.bIsRun ? runMultiplier : 1.0f);
    }

    void FixedUpdate()
    {
        rigidbody.position += movingVec * Time.fixedDeltaTime;
    }
}
