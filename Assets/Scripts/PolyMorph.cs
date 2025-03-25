using UnityEngine;
using UnityEngine.UI;

public class PolyMorph : MonoBehaviour
{
    public Sprite spritewood;  
    public Sprite spritemetal;  
    public Sprite spriteplastic;  

    private int clickCount = 0;
    private Image buttonImage;

    public BolaSimple Ball;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = spritewood;  // Iniciar con sprite normal
    }

    public void ChangeGravity()
    {
        clickCount++;

        switch (clickCount % 3)
        {
            case 1:  
                buttonImage.sprite = spritemetal;
                Ball.GravityMetal();
                Debug.Log("MetalBall");
                break;
            case 2:
                buttonImage.sprite = spriteplastic;
                Ball.GravityPlastic();
                Debug.Log("PlasticBall");
                break;
            case 0:
                buttonImage.sprite = spritewood;
                Ball.GravityWood();
                Debug.Log("WoodBall");

                break;
        }
    }
}
