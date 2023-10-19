using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GhostContactState
{
    protected ContactWithPlayer context;

    public void SetContext(ContactWithPlayer context)
    {
        this.context = context;
    }

    public abstract void OnContactWithPlayer(GameObject player);
}