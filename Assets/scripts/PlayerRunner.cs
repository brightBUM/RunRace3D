using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunner : MonoBehaviour
{
    [SerializeField] CharacterController cc;
    Vector3 move;
    public float speed;
    public Vector3 velocity;
    public float jumpheight = 5f;
    public float gravity = 3;
    public float slidingspeed = 2f;
    [SerializeField] Transform footpos;
    [SerializeField] Transform centrepos;
    [SerializeField] LayerMask groundlayer;
    [SerializeField] LayerMask Walllayer;
    [SerializeField] float groundradius;
    [SerializeField] bool grounded;
    [SerializeField] bool sliding;
    [SerializeField] Animator anim;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        grounded = Physics.CheckSphere(footpos.position, groundradius, groundlayer);
        sliding = Physics.Raycast(centrepos.position, centrepos.forward, 3f, Walllayer);
        if (grounded )
        {
            anim.SetBool("Grounded", true);
            velocity.y = 0;
        }
        else
        {
            
            anim.SetBool("Grounded", false);
            velocity.y += gravity * Time.deltaTime;
            anim.SetBool("WallSlide", false);
        }
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(-2 * gravity * jumpheight);
        }
        Debug.DrawRay(centrepos.position, centrepos.forward*2, Color.red);
        if(sliding)
        {
            velocity.y = -slidingspeed;
            anim.SetBool("Grounded", false);
            anim.SetBool("WallSlide", true);
        }
        if(sliding && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump");
            transform.Rotate(0, 180, 0);
            velocity.y = Mathf.Sqrt(-2 * gravity * jumpheight);
        }
        

        move = transform.forward;
        cc.Move(move * speed * Time.deltaTime);
        cc.Move(velocity * Time.deltaTime);

    }
}
