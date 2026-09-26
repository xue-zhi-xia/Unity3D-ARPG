using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class Interactable : MonoBehaviour
    {
        public float radius = 0.6f;
        public string interactableText;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        public virtual void Interact(PlayerManager playerManager)
        {
            Debug.Log("你与一个对象进行了交互喵");
        }
    }

}
