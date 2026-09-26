using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class WeaponPickUp : Interactable
    {
        public WeaponItem weapon;

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);

            PickUpItem(playerManager);
        }

        private void PickUpItem(PlayerManager playerManager)
        {
            PlayerInventory playerInventory;
            PlayerLocomotion playerLocomotion;
            AnimatorHandler animatorHandler;

            playerInventory = playerManager.GetComponent<PlayerInventory>();
            playerLocomotion = playerManager.GetComponent<PlayerLocomotion>();
            animatorHandler = playerManager.GetComponentInChildren<AnimatorHandler>();

            playerLocomotion.rigidbody.velocity = Vector3.zero;  // 捡东西的时候停止运动喵
            animatorHandler.PlayerTargetAnimation("Pick_up", true);  // 播放捡东西动画喵
            playerInventory.weaponInventory.Add(weapon);
            Destroy(gameObject);

        }
    }
}

