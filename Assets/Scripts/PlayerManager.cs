using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SG
{
    public class PlayerManager : MonoBehaviour
    {
        InputHandler inputHandler;
        CameraHandler cameraHandler;
        PlayerLocomotion playerLocomotion;
        Animator anim;

        public bool isInteracting;

        [Header("玩家状态")]
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;
        public bool canDoCombo;

        void Awake()
        {
            cameraHandler = FindObjectOfType<CameraHandler>();
        }

        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
        }

        void Update()
        {
            float delta = Time.deltaTime;
            isInteracting = anim.GetBool("isInteracting");
            canDoCombo = anim.GetBool("canDoCombo");

            inputHandler.TickInput(delta);
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleRollingAndSprinting(delta);
            playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);

            CheckForInteractableObject();
        
        }

        private void FixedUpdate()
        {
            float delta = Time.deltaTime;

            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }
        }

        private void LateUpdate()
        {
            inputHandler.rollFlag = false;
            inputHandler.sprintFlag = false;
            inputHandler.rb_Input = false;
            inputHandler.rt_Input = false;
            inputHandler.d_Pad_Up = false;
            inputHandler.d_Pad_Down = false;
            inputHandler.d_Pad_Left = false;
            inputHandler.d_Pad_Right = false;
            inputHandler.a_Input = false;

            playerLocomotion.inAirTimer = playerLocomotion.inAirTimer + Time.deltaTime;
        }

        public void CheckForInteractableObject()
        {
            RaycastHit hit;

            if(Physics.SphereCast(transform.position,0.3f,transform.forward ,out hit, 1f, cameraHandler.ignoreLayers))
            {
                if(hit.collider.tag == "Interactable")
                {
                    Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                    if(interactableObject != null)
                    {
                        string interactableText = interactableObject.interactableText;
                        //设置ui文本文字为这里的喵
                        //设置一个弹出ui窗口喵

                        if (inputHandler.a_Input)
                        {
                            hit.collider.GetComponent<Interactable>().Interact(this);
                        }
                    }
                }
                Debug.Log($"打到了：{hit.collider.name}, tag = {hit.collider.tag}");
            }
        }
    }
}

