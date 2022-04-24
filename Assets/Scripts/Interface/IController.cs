using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController
{
    void OnLoaded();
    void OnPlay();
    void OnPause();
    void OnTargetBegin();
    void OnTargeting();
    void OnTargetEnd();
    void OnBackward();
    void OnFaild();
    void OnVectory();
    void OnFinish();
    void OnRestart();
}
