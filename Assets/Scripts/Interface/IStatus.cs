using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatus
{
    void UpdateStatus();
    void SetCoin(float count);
    void SetDiamond(float count);
    void SetEnergy(float count);
    void ShowSubcripe();
}
