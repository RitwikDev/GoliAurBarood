using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Properties
{
    public const string PLAYER_GAME_OBJECT_NAME = "Player";
    public const string GROUND_CHECK_NAME = "GroundCheck";
    public const string PLAYER_TAG = "Player";
    public const string PLAYER_COLLIDERS_GAME_OBJECT = "Colliders";
    public const string PLAYER_BULLET_TAG = "Player Bullet";
    public const string PLAYER_INVINCIBILITY_BARRIER = "InvincibilityBarrier";
    public const string PLAYER_INVINCIBILITY_BARRIER_TAG = "InvincibilityBarrier";
    public const int PLAYER_BULLET_SORTING_ORDER = 1;
    public const int PLAYER_LAYER = 3;

    public const string GROUND_TAG_CANNOT_FALL_NAME = "Ground";
    public const string GROUND_TAG_CAN_FALL_NAME = "Ground - Can Fall";

    public const int GROUND_LAYER = 6;

    public const string ENEMY_BOUNDARY = "EnemyBoundary";
    public const string ENEMY_TAG = "Enemy";
    public const string ENEMY_GAME_OBJECT_NAME = "Enemy";
    public const int ENEMY_LAYER = 7;
    public const string ENEMY_FALL_GROUND_CHECK = "FallGroundCheck";

    public const string CAMERA_BOUNDARY = "CameraBoundary";

    public const string ENEMY_SPAWN_LOCATION_GAME_OBJECT = "EnemySpawnLocation";
    public const string ENEMY_SPAWN_GAME_OBJECT = "EnemySpawn";
    public const string ENEMY_SPAWN_LEFT_NAME = "Left";
    public const string ENEMY_SPAWN_RIGHT_NAME = "Right";

    public const int DEAD_LAYER = 8;
    public const string PLAYER_RESPAWN_GAME_OBJECT = "PlayerRespawnPoint";

    public const string POWER_UP_GAME_OBJECT = "PowerUp";
    public const string POWER_UP_BOX_GAME_OBJECT = "Box";
    public const string POWER_UP_ITEM_GAME_OBJECT = "Item";
    public const string POWER_UP_TAG = "PowerUp";
    public const int POWER_UP_LAYER = 9;

    public const string UI_CANVAS_GAME_OBJECT = "Canvas";
    public const string UI_LIFE_GAME_OBJECT = "Life";
    public const string UI_LIFE_IMAGE = "LifeImage";
}