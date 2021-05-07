using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_controller : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float speed = 2f;
    public bool grounded;
    public float jumpPoer = 6.5f; 

    private Rigidbody2D rb2d;
    private Animator anim;
    private bool jump;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("Grounded", grounded);


        if (Input.GetKeyDown(KeyCode.UpArrow)&& grounded){
            jump = true;
        }

    }



    void FixedUpdate()
    {

        float h = Input.GetAxis("Horizontal");

        rb2d.AddForce(Vector2.right * speed * h);

        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

//Flip rotacion del player en la direccion
        if (h > 0.1f){
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if(h < -0.1f){
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }


        if (jump){
            rb2d.AddForce(Vector2.up * jumpPoer, ForceMode2D.Impulse);
            jump = false;
        }

        Debug.Log(rb2d.velocity.x);

    }
// Reposicionamiento del player durante las pruebas 
    void OnBecameInvisible(){
        transform.position = new Vector3(0,0,0);
    }
}
