using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : Entity, IInteractables
{
    public string text = "";

    public void Interact()
    {
        Debug.Log(text);
    }
    protected override void Die()
    {
        Debug.Log("Sing Died");
        RestoreHP();
    }
}
