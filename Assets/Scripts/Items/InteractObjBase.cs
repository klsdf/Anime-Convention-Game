using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractObjBase : MonoBehaviour
{
    public virtual void Interact()
    {

    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<DemoCharacterController>().SetInteractObject(this.gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<DemoCharacterController>().ClearInteractObject();
        }
    }
}