using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    // set your data here
    // for Scoring system
    private int _currentScore;
    private int _currentPlayerHealth;
    
    // Mario basic starting values
    public int playerMaxSpeed = 10;
    public int playerJumpSpeed = 50;
    public int playerDefaultForce = 150;

    // for Reset values
    Vector3 _goombaSpawnPointStart = new(2.5f, -0.45f, 0); // hardcoded location
    // .. other reset values 

    // for Consume.cs
    public int consumeTimeStep = 10;
    public int consumeLargestScale = 4;

    // for Break.cs
    public int breakTimeStep = 30;
    public int breakDebrisTorque = 10;
    public int breakDebrisForce = 10;

    // for SpawnDebris.cs
    public int spawnNumberOfDebris = 10;

    // for Rotator.cs
    public int rotatorRotateSpeed = 6;

    // for EnemyController.cs
    public int enemyMoveSpeed = 5;
    public float groundSurface = -1.5f;

    // for testing
    public int testValue;
}