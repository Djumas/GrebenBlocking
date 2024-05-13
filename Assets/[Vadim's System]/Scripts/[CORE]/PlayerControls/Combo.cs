using System;
using System.Collections.Generic;

[Serializable]
public class Combo {
    public List<ComboElement> comboSequence;
    public InputResult finalResult;
    public InputResult flawResult;
}