using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float velocidade;
    public float forcapulo;

    public bool isJumping;
    public bool doubleJump;
    private Rigidbody2D pulo;
    private Animator animacao;

    // Start is called before the first frame update
    void Start()
    {
        pulo = GetComponent<Rigidbody2D>();
        animacao = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        jump();
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * velocidade;
        
        if(Input.GetAxis("Horizontal") > 0f)//se Input.GetAxis("Horizontal") for maior que 0 é porque esta indo para direito
        {
            animacao.SetBool("andar", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        if(Input.GetAxis("Horizontal") < 0f)//se Input.GetAxis("Horizontal") for menor que 0 é porque esta indo para esquerdo
        {
            animacao.SetBool("andar", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        if(Input.GetAxis("Horizontal") == 0f)//se Input.GetAxis("Horizontal") for igual a 0 estou parado, então andar false.
        {
            animacao.SetBool("andar", false);
        }
        
    }
    void jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(!isJumping)
            {
                pulo.AddForce(new Vector2(0f, forcapulo), ForceMode2D.Impulse);
                doubleJump = true;
                animacao.SetBool("pulo", true);
            }
            else
            {
                if(doubleJump)
                {
                    pulo.AddForce(new Vector2(0f, forcapulo), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJumping = false;
            animacao.SetBool("pulo", false);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }
}
