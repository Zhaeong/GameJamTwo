using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {



    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float rotateSpeed = 3.0f;
    private int playerNum = 1;
    //private Vector3 moveDirection = Vector3.zero;

    public GameObject goFlag;

    private int count;
    public Text countText;

    void Start()
    {
        count = 5;
        SetCountText();
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && playerNum == 1)
        {
            Vector3 playerPosition = transform.position;
            Vector3 objectPosition = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z -1);
            Quaternion playerRotation = Quaternion.identity; 
            Instantiate(goFlag, objectPosition, playerRotation);
            count = count - 1;
            SetCountText();
        }
        if (Input.GetKeyDown("c"))
        {
            transform.position = new Vector3(0, 0, 0);
            if (playerNum == 1)
                playerNum = 2;
            else if (playerNum == 2)
                playerNum = 1;
        }


    }

    void FixedUpdate()
    {
        CharacterController controller = GetComponent<CharacterController>();
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        controller.SimpleMove(forward * curSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FlagObject") && playerNum == 2)
        {
            Destroy(other.gameObject);
            Debug.Log("collided with flag");
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }
}
