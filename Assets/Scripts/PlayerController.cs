﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float rotateSpeed = 3.0f;
    private int playerNum = 1;
    private Vector3 moveDirection = Vector3.zero;
    private CountDownScript countDownscript;
    public GameObject goFlag;

    private int count;
    public Text countText;

    void Start()
    {
        count = 5;
    }
    
    void Update()
    {
       

        if (Input.GetKeyDown("e") && playerNum == 1)
        {
            Vector3 playerPosition = transform.position;
            Vector3 objectPosition = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z - 1);
            Quaternion playerRotation = Quaternion.identity;
            Instantiate(goFlag, objectPosition, playerRotation);

        }
        if (Input.GetKeyDown("c"))
        {
            ChangePlayer();
        }


    }
    
    void FixedUpdate()
    {
        CharacterController controller = GetComponent<CharacterController>();
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

    }
    

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FlagObject") && playerNum == 2)
        {
            Destroy(other.gameObject);
            Debug.Log("collided with flag");
            count = count + 1;
            SetCountText();
        }
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count == 0)
        {
            ChangePlayer();
            countDownscript = gameObject.GetComponent<CountDownScript>();
            countDownscript.Awake();
        }
    }
    public void ChangePlayer() {
        if (playerNum == 1)
        {
            transform.position = new Vector3(0, 1, 0);
            playerNum = 2;
        }
        else if (playerNum == 2)
        {
            playerNum = 1;
        }
    }
}
