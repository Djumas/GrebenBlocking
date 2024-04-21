using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;


public enum InputCommand {
    KICK,
    PUNCH,
    MODIFIER,
    UP,
    DOWN,
    FORWARD,
    BACKWARD,
    GRAB
}

public enum InputResult {
    KICK,
    HEAVY_KICK,
    PUNCH,
    HEAVY_PUNCH,
    UPPERCOAT,
    LEG_KNOCK_OVER,
    HEADBUTT,
    BACKFIST,
    HUMMER_LEG,
    LOW_KICK,
    FRONT_KICK,
    BACK_KICK,
    GRAB,
    CHOKE,
    THROW_BACK,
    THROW_FRONT,
    BEAT_UP,
    STOMP,
    NONE       
}

[Serializable]
public class ComboElement {
    public InputCommand command;
    public bool evaluateOnPress;
    public bool evaluateOnRelease;
    public float evaluationDelay;
}


[Serializable]
public class Combo {
    public List<ComboElement> comboSequence;
    public InputResult finalResult;
    public InputResult flawResult;
}



public class PlayerInputResponse : MonoBehaviour
{
    [SerializeField] MovementComponent movementComponent;
    [SerializeField] RotationComponent rotationComponent;
    [SerializeField] Camera currentCamera;

    [SerializeField] ComboConfig comboConfig;

    private List<InputCommand> pressedCommands = new List<InputCommand>();

    public void AddCommand(InputCommand command) {
        pressedCommands.Add(command);
    }

    private void Awake()
    {
        currentCamera = Camera.main;
    }

    public void OnMove(InputValue value)
    {
        var readValue = value.Get<Vector2>();
        var direction = readValue.x * currentCamera.transform.right + readValue.y * (currentCamera.transform.forward - (currentCamera.transform.forward.y) * (new Vector3(0, 1, 0)).normalized);
        movementComponent.SetMovement(direction);
        rotationComponent.SetRotation(direction);
    }

    public void OnKick(InputValue value)
    {
        
    }
    

    public void OnPunch(InputValue value)
    {
        
    }


    public void OnModifier(InputValue value)
    {
        
    }

}
