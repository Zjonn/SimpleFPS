using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int enemiesToSpawn = 1;

    static float _playerTopAccuracy = 0;
    public static float PlayerTopAccuracy
    {
        set { _playerTopAccuracy = value; }
        get { return _playerTopAccuracy; }
    }
}
