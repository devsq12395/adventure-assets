using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evt_BossKill : EvtTrig
{

    public override void use()
    {
        ContEnemies.I.trigger_boss_kill ();
    }
}
