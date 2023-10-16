using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GhostStateManagerObserver
{
    public void UpdateState(GhostStateAbstractFactory factory);
}