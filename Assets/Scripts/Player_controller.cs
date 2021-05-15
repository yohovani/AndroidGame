using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player_controller : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float speed = 2f;
    public bool grounded;
    public float jumpPower = 6.5f; 


    public Image Corazon;
    public int CantDeCorazon;
    public RectTransform PosicionPrimerCorazon;
    public Canvas MyCanvas;
    public int offSet;



    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer spr;
    private bool jump;
    private bool hit = false;
    private float cd = 0f;
	private bool doubleJump;
    private bool movement = true;
    private float Points = 0f;
    private bool life = true;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        //Corazones del jugador
    
        Transform PosCorazon = PosicionPrimerCorazon;
        for (int i = 0; i < CantDeCorazon; i++)
        {
            Image NewCorazon = Instantiate(Corazon, PosCorazon.position, Quaternion.identity);
            NewCorazon.transform.parent = MyCanvas.transform;
            PosCorazon.position = new Vector2(PosCorazon.position.x + offSet, PosCorazon.position.y);
            
            

        }
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("Grounded", grounded);
        anim.SetBool("Hit", hit);
        anim.SetBool("Life", life);


        if (Input.GetKeyDown(KeyCode.UpArrow)){
			if (grounded){
				jump = true;
				doubleJump = true;
			} else if (doubleJump){
				jump = true;
				doubleJump = false;
			}
		}
        if (Input.GetKeyDown(KeyCode.Space) && cd == 0f){
            hit = true;
            anim.SetBool("Hit", hit);
           if (hit == true){
            hit = false;
            }
        }
        //muerte
        if (CantDeCorazon <= 0){
            //Destroy(gameObject);
            Destroy(Corazon);
            life = false;
            anim.SetBool("Life", life);
            movement = false;
            jump = false;
            Debug.Log(Points);
        }

        
    }



    void FixedUpdate()
    {

        float h = Input.GetAxis("Horizontal");
        if (!movement) h = 0;

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
			rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
			rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
			jump = false;
		}



       // Debug.Log(rb2d.velocity.x);
        
    }
// Reposicionamiento del player durante las pruebas 
    void OnBecameInvisible(){
        transform.position = new Vector3(-4.7f,-3.3f,0f);
        Destroy(MyCanvas.transform.GetChild(CantDeCorazon + 1).gameObject);
        CantDeCorazon -= 1;
    }

    
	public void EnemyJump()
    {
		jump = true;
        Points = Points + 100f;
	}
	public void EnemyKnockBack(float enemyPosX)
    {
	    jump = true;
	    float side = Mathf.Sign(enemyPosX - transform.position.x);
	    rb2d.AddForce(Vector2.left * side * jumpPower, ForceMode2D.Impulse);
	    movement = false;
	    Invoke("EnableMovement", 0.7f);
        
        //Reduccion de la vida por daño
        Destroy(MyCanvas.transform.GetChild(CantDeCorazon + 1).gameObject);
        CantDeCorazon -= 1;
        

        //cambio de color al momento de sufrir daño
	    Color color = new Color(255/255f, 106/255f, 0/255f);
	    spr.color = color;


	}

	void EnableMovement(){
		movement = true;
		spr.color = Color.white;
	}

    public void Coraplus(){
        CantDeCorazon += 1;
        Transform PosCorazon = PosicionPrimerCorazon;
        Image NewCorazon = Instantiate(Corazon, PosCorazon.position, Quaternion.identity);
        NewCorazon.transform.parent = MyCanvas.transform;
        PosCorazon.position = new Vector2(PosCorazon.position.x + offSet, PosCorazon.position.y);
    }
    

}
