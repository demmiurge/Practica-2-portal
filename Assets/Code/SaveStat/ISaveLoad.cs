using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveLoad
{
    void SetCurrentAttributesAsDefault();
    void LoadDefaultAttributes();
    bool CanAttributesBeSet();
}
