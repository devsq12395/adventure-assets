using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameObject : MonoBehaviour {  
    
    [Header("------ UNITY EDITOR EDITABLE PARTS ------")]
    public string name; 
    public string nameUI, type;
    
    [Header("Types: unit, missile, collect. Put dummies as missile")]
    public int owner;
    public int id;

    // Settable in Unity
    public int hp, hpMax, mp, mpMax, mpRegen, armor;
    public int dam, skill, hitDam;
    public string onHitSFX;
    public List<string> tags;
    public float speed, attachScaleMultiplier = 1;
    public float hpBarScaleX = 1;
    
    [Header("------ NON-EDITABLE PARTS ------")]
    public int ultPerc;

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
    public Dictionary<string, string> equipItems;

    // Movement
    public Vector2 curPos = new Vector2(0, 0);
    public Vector2 movInput = new Vector2(0, 0);
    public Vector2 nxtPos = new Vector2(0, 0);
    public string facing;
    public bool isWalk;
    public Vector2 walkTargPos = new Vector2(0, 0);
    public bool checkBorder = true;

    // Move to ang
    public bool constMovAng_isOn = false;
    public float constMovAng_spd, constMovAng_ang, constMovAng_dur, constMovAng_dist, propellDist;
    public Vector2 constMovAng_movTarget;
    public string propellType = "";
    public Vector2 propellFirstPos = new Vector2(0, 0);
    
    // Move to dir
    public bool constMovDir_isOn = false;
    public float constMovDir_spd;
    public Vector2 constMovDir_dir;

    // Move to pos
    public bool moveToPos_isOn = false;
    public Vector2 moveToPos_pos;

    // Instant Move Update
    public bool instMov_isOn = false;
    public float instMov_ang, instMov_spd, instMov_dur, instMov_dist;
    public string instMov_mode = "";

    // Booleans
    public bool isRunning, isAtk, isInvul, isDash;

    // Misc
    public float knockDrag;
    public float timedLife, range;
    public int controllerID;
    public List<int> hitUnitsID;
    public float colorChangeDur;
    public int summonedBy;

    // Buffs
    public List<ContBuffs.buff> buffs;

    // Skills
    public List<SkillTrig> skills;

    // UI
    public HealthBarScript barHP, barSta;

    [Header("Shadow Settings")]
    private Vector2 shadowOffset;
    private Vector2 shadowScaleMultiplier;
    private Color shadowColor;

    private GameObject shadow; // The shadow object
    private SpriteRenderer shadowRenderer; // SpriteRenderer for the shadow

    void Start() {
        renderer = GetComponent<Renderer>();

        // Initialize components
        buffs = new List<ContBuffs.buff>();
        skills = new List<SkillTrig>();
        equipItems = new Dictionary<string, string>();

        // Equip items if hero
        if (tags.Contains("hero")) {
            foreach (string equipSlot in Inv2.I.equipStrList) {
                equipItems[equipSlot] = Inv2.I.get_item_name_in_equipped_slot(equipSlot, name);
            }
        } else {
            foreach (string equipSlot in Inv2.I.equipStrList) {
                equipItems[equipSlot] = "";
            }
        }

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hasAnim = (anim != null);

        // Set ID and default skills
        id = ContObj.I.curID;
        ContObj.I.curID++;
        ContObj.I.set_default_skills(this);

        // Set shadow variables
        shadowOffset = new Vector2(0f, -0.15f); // Offset to adjust shadow position
        shadowScaleMultiplier = new Vector2(1.8f, 0.5f); // Scale multiplier for shadow
        shadowColor = new Color(0, 0, 0, 0.4f); // Darker shadow color for visibility

        // Find the shadow child object
        Transform shadowTransform = transform.Find("Shadow");
        if (shadowTransform != null) {
            shadow = shadowTransform.gameObject;
            shadowRenderer = shadow.GetComponent<SpriteRenderer>();

            if (shadowRenderer != null) {
                // Apply initial color and scale settings
                shadowRenderer.color = shadowColor;
                shadow.transform.localPosition = shadowOffset;
                UpdateShadowScale();
            } else {
                Debug.LogWarning("Shadow GameObject is missing a SpriteRenderer component.");
            }
        } else {
            Debug.LogWarning("Shadow GameObject not found. Please ensure a child object named 'Shadow' is added.");
        }

        InvokeRepeating("update_every_10th_ms", 0.5f, 0.1f);
    }

    void Update() {
        ContObj.I.update_obj(this);
        UpdateShadowPositionAndScale();
    }

    private void UpdateShadowPositionAndScale() {
        if (shadow != null) {
            // Update shadow position and scale based on the game object's scale
            shadow.transform.localPosition = shadowOffset;
            UpdateShadowScale();
        }
    }

    private void UpdateShadowScale() {
        // Scale the shadow based on the game object's current scale and the shadowScaleMultiplier
        Vector2 scaledShadowSize = new Vector2(
            transform.localScale.x * shadowScaleMultiplier.x,
            transform.localScale.y * shadowScaleMultiplier.y
        );
        shadow.transform.localScale = scaledShadowSize;
    }

    private void update_every_10th_ms() {
        ContObj.I.update_every_10th_ms(this);
    }

    // Other methods remain the same...

    public void OnTriggerStay2D(Collider2D _collision) {
        InGameObject _obj2_scrpt = _collision.gameObject.GetComponent<InGameObject>();

        if (_obj2_scrpt != null) {
            ContCollision.I.collision(this, _obj2_scrpt);
        }
    }
}
