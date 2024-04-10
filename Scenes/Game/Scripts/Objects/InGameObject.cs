using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameObject : MonoBehaviour {  
    
    [Header("------ UNITY EDITOR EDITABLE PARTS ------")]
    public string name; 
    public string nameUI, type;
    
    [Header("Types: unit, missile, collect. Put dummies as missile")]
        // TYPES: unit, missile, collect
        // To be set in Unity GameObject's component
    public int owner;
    public int id;

    // Settable in Unity
    public int hp, hpMax, mp, mpMax, mpRegen, armor;
    public int hitDam;
    public string onHitSFX;
    public List<string> tags;
    public float speed, attachScaleMultiplier = 1;
    
    [Header("------ NON-EDITABLE PARTS ------")]
    public float zPos;
    public SkillTrig skill1, skill2;

    // Animation
    public Animator anim;
    public bool hasAnim;
    public bool isRotate = false;
    public int toAnim;
    public Renderer renderer;

    // Components
    public Rigidbody2D rb;

    // Stats
    public int statHP, statMP, statAttack, statRange, statSkill, statSpeed, statArmor, statCritRate, statCritDam;

    // Movement
    public Vector2 curPos = new Vector2 (0, 0);
    public Vector2 movInput = new Vector2 (0, 0);
    public Vector2 nxtPos = new Vector2 (0, 0);
    public string facing;
    public bool isWalk;
    public Vector2 walkTargPos = new Vector2 (0, 0);
    public bool checkBorder = true;

    // Move to ang
    public bool constMovAng_isOn = false;
    public float constMovAng_spd, constMovAng_ang, propellDist;
    public string propellType = "";
    public Vector2 propellFirstPos = new Vector2 (0, 0);
    
    // Move to dir
    public bool constMovDir_isOn = false;
    public float constMovDir_spd;
    public Vector2 constMovDir_dir;

    // Move to pos
    public bool moveToPos_isOn = false;
    public Vector2 moveToPos_pos;

    // Booleans
    public bool isRunning, isAtk, isInvul, isDash;

    // Misc
    public float knockDrag;
    public float timedLife, range;
    public int controllerID;
    public List<int> hitUnitsID;

    // Buffs
    public List <ContBuffs.buff> buffs;

    // Skills
    public List <SkillTrig> skills;

    void Start() {
        renderer = GetComponent <Renderer> ();

        buffs = new List<ContBuffs.buff> ();
        skills = new List<SkillTrig> ();

        rb = GetComponent <Rigidbody2D> ();
        anim = GetComponent <Animator> ();
        hasAnim = (anim != null);

        id = ContObj.I.curID;
        ContObj.I.curID++;

        ContObj.I.set_default_skills (this);
        
        InvokeRepeating("update_every_10th_ms", 0.5f, 0.1f);
    }

    void Update() {
        ContObj.I.update_obj (this);
    }
    
    private void update_every_10th_ms (){
        ContObj.I.update_every_10th_ms (this);
    }

    public virtual void change_anim (string _anim) {
        
    }

    // Collision
    public void OnTriggerStay2D (Collider2D _collision){
        InGameObject _obj2_scrpt = _collision.gameObject.GetComponent <InGameObject> ();

        if (_obj2_scrpt != null) {
            ContCollision.I.collision (this, _obj2_scrpt);
        }
    }
}
