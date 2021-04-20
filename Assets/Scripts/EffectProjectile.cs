using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectProjectile : Projectile
{
    public int level;
    public override void DoExtra(GameObject target)
    {                
        Enemy enemy = target.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.InflictBurn(level * 2);
        }

        Player player = target.GetComponent<Player>();
            
        if(player != null)
        {
            player.InflictBurn(level);
        }
    }
}
