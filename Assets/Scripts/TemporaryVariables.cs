using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryVariables
{
    public static CountPlayers CountPlayers = CountPlayers.OnePlayer;
}
public enum CountPlayers
{ 
    OnePlayer,
    TwoPlayers
}
