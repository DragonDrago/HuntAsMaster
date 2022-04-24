using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{

    void DestroyEnemy();
    void OnPlay();
    void OnPause();
    void OnFaild();
    void OnBackward();
}
