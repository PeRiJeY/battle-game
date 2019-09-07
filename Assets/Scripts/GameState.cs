using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    private static GameState gameState;

    public static readonly int initialHealth = 100;

    public int playerHealth { get; set; }

    private GameState() {
        playerHealth = initialHealth;
    }

    public static GameState getSingleton()
    {
        if (gameState == null)
        {
            gameState = new GameState();
        }
        return gameState;
    }
}
