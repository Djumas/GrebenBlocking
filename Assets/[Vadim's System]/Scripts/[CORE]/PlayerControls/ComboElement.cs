using System;

[Serializable]
public class ComboElement {
    public InputCommand command;
    public bool evaluateOnPress;
    public bool evaluateOnRelease;
    public float evaluationDelay;
}