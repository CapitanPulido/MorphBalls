using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.UI;

public class PolyMorph : MonoBehaviour
{
    public Rigidbody2D rb;
    public Texture2D[] texturas;
    public SpriteRenderer render;
    private int estado = 0; // 0: Madera, 1: Plástico, 2: Metal  
    public bool enAgua = false;
    private float alturaAgua = 0f;
    private int clickCount = 0;
    public Image buttonImage;

    public Sprite spritemadera;  
    public Sprite spriteplastico;  
    public Sprite spritemetal;

    public bool Madera;
    public bool Metal;
    public bool Plastico;

    public BolaSimple ball;

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        

        estado = 0; // Comenzamos en madera
        AplicarPropiedadesMaterial();

        rb.gravityScale = 1; // Restaurar la gravedad normal
        Madera = true;
        Plastico = false;
        Metal = false;

        render.sprite = spritemadera;
        buttonImage.sprite = spritemadera;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            int estadoAnterior = estado;
            estado = (estado + 1) % 3; // Cambia entre 0 (Madera), 1 (Plástico), 2 (Metal)
            AplicarPropiedadesMaterial();

            // Si estaba en el fondo con metal y cambia a otro material, aplicamos impulso
            if (enAgua && estadoAnterior == 2 && estado < 2)
            {
                rb.velocity = new Vector2(rb.velocity.x, 10f); // Expulsión del agua
            }
        }


        if(enAgua == true && Madera == true)
        {
            rb.gravityScale = 0f;
        }

        if (enAgua == true && Plastico == true)
        {
            rb.gravityScale = -1f;
        }

        if (enAgua == true && Metal == true)
        {
            rb.gravityScale = 2f;
        }

    }

   


    public void ChangeMaterial()
    {
        clickCount++;

        switch (clickCount % 3)
        {
            case 1:
                render.sprite = spriteplastico;
                buttonImage.sprite = spriteplastico;

                ball.GravityPlastic();
                Madera = false;
                Plastico = true;
                Metal = false;
                Debug.Log("plastico");
                break;

            case 2:
                render.sprite = spritemetal;
                buttonImage.sprite = spritemetal;

                ball.GravityMetal();
                Madera = false;
                Plastico = false;
                Metal = true;
                Debug.Log("metal");
                break;

            case 0:
                render.sprite = spritemadera;
                buttonImage.sprite = spritemadera;

                ball.GravityWood();
                Madera = true;
                Plastico = false;
                Metal = false;
                Debug.Log("madera");
                break;

        }
    }


    public void AplicarPropiedadesMaterial()
    {
        switch (estado)
        {
            case 0: // Madera (queda en la altura media)
                rb.gravityScale = 0f;
                if (enAgua && transform.position.y < alturaAgua - 1) // Mantenerse en el medio
                {
                    rb.velocity = new Vector2(rb.velocity.x, 2f);
                }
                break;

            case 1: // Plástico (flota en la superficie)
                rb.gravityScale = -0.5f;
                if (enAgua && transform.position.y < alturaAgua) // Mantenerse en la superficie
                {
                    rb.velocity = new Vector2(rb.velocity.x, 3f);
                }
                break;

            case 2: // Metal (se hunde hasta el fondo)
                rb.gravityScale = 2f;
                break;
        }

    }

    public void DetectarAgua()
    {
        enAgua = true;
    }

    public void SaliAgua()
    {
        enAgua = false;
    }


}
