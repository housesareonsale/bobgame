using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OpeningDirection
{
    DOWN, UP, LEFT, RIGHT 
}

public enum EnemyState
{
    ROAMING, CHASING, ATTACKING
}

public enum EnemyPart
{
    FACE, BODY, LEGS
} 

public enum UpgradeType
{
    FIRE, NUM_PROJECTILES, BIGGER_PROJECTILES
}