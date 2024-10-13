using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_FrostOrbs : SkillTrig
{
    public string missileObj; // The fire spirit prefab
    public float radius = 2f; // Distance from the owner at which fire spirits should spawn

    public override void use_active()
    {
        InGameObject _ownerComp = gameObject.GetComponent <InGameObject> ();
        Vector3 ownerPosition = gameObject.transform.position; // The position of the script owner

        _ownerComp.ultPerc = 0;

        for (int i = 0; i < 4; i++)
        {
            // Calculate the angle for each fire spirit (90 degrees apart)
            float angle = i * 90f;
            Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * radius;

            // Create fire spirit around the owner
            GameObject _fireSpirit = ContObj.I.create_obj(missileObj, ownerPosition + offset, _ownerComp.owner);
            InGameObject _fireSpiritScript = _fireSpirit.GetComponent<InGameObject>();
            _fireSpiritScript.summonedBy = _ownerComp.id;

            // Add the SpinAroundObject script and set the target to the script owner
            SpinAroundObject spinScript = _fireSpirit.AddComponent<SpinAroundObject>();
            spinScript.target = gameObject.transform; // Set the owner as the target
            spinScript.initialAngle = angle;
            spinScript.SetInitialPosition ();

            _fireSpirit.GetComponent<InGameObject>().timedLife = 7f;
        }

        SoundHandler.I.play_sfx("dash");
        SoundHandler.I.play_sfx("magic");
        ContEffect.I.create_effect ("smoke-expand", gameObject.transform.position);
        ContEffect.I.create_effect ("magic-spark-seraphine", gameObject.transform.position);
        MUI_Overlay.I.show_overlay ("ult");
    }
}
