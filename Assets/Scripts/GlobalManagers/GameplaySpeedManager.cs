using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameplaySpeedManager : MonoSingleton<GameplaySpeedManager>
{
    public float timeScale = 0.3f;
    public bool useTimeScale;
    public Gamepad gamepad;

    public void SetGameplaySpeed() => Time.timeScale = timeScale;
    public void ResetGameplaySpeed() => Time.timeScale = 1;
}
