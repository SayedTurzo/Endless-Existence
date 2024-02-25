using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] private int coinToAdd= 5;
    [SerializeField] private int coinToRemove = 3;
    
    public bl_Joystick Joystick;
    
    float horizontalInput;
    float verticalInput;

    void Update()
    {
        //horizontalInput = Input.GetAxis("Horizontal");
        //verticalInput = Input.GetAxis("Vertical");
        
        // horizontalInput = Joystick.Horizontal;
        // verticalInput = Joystick.Vertical;

        if (Application.isMobilePlatform)
        {
            Debug.Log("Running on a mobile device");
            horizontalInput = Joystick.Horizontal;
            verticalInput = Joystick.Vertical;
        }
        else
        {
            Debug.Log("Not running on a mobile device");
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
        }
        
        
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);
        movement.Normalize();
        transform.position += movement * speed * Time.deltaTime;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Debug.Log("Collided with coin!");
            GameManager.Instance.AddCoins(coinToAdd);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Virus"))
        {
            Debug.Log("Collided with Virus!");
            GameManager.Instance.RemoveCoins(coinToRemove);
            Destroy(other.gameObject);
        }
    }
    
}
