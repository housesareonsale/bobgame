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

public enum TwinBossDialogState
{
    INTRO, INTRO_CONTINUED, BOSS_FIGHT, ONE_DIED, TWO_DIED, FIGHT_END
}

public enum BenDoverDialogState
{
    INTRO, INTRO_CONTINUED, BOSS_FIGHT, HALF_WAY, FIGHT_END
}

public enum MonsterPartyDialogState
{
    INTRO, BOSS_FIGHT, BEAT_BOSS, FIGHT_END
}

public enum EnemyType
{
    BAD_MAN, SPRAY_MAN, WIZ_MAN, HR_MAN
}

public enum FinalBossDialogueState
{
    INTRO, INTRO_CONTINUED, INTRO_NEXT, BOSS_FIGHT, BEAT_BOSS, FIGHT_END
}