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

public enum IntroDialogueState
{
    INTRO, INTRO_CONTINUED, OFF_DENVER_COL, INTRO_END
}

public enum GameDialogState
{
    NONE, FIRST_FLOOR_START, FIRST_FLOOR_END, SECOND_FLOOR_START, SECOND_FLOOR_END
}