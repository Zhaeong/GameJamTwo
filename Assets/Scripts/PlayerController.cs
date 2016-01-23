using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}


    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float rotateSpeed = 3.0f;
    //private Vector3 moveDirection = Vector3.zero;

    public GameObject goFlag;

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Vector3 playerPosition = transform.position;
            Vector3 objectPosition = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z -1);
            Quaternion playerRotation = Quaternion.identity; 
            Instantiate(goFlag, objectPosition, playerRotation);
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
}
