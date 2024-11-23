using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContObj : MonoBehaviour {

    public static ContObj I;
	public void Awake(){ I = this; }

    public int curID = 0;

    void Start() {
        
    }

    void Update() {
        
    }

    // Create
    private void on_create_set_obj_stats(InGameObject _comp, string _name) {
        if (!_comp.tags.Contains("hero")) return;

        _comp.mp = 0;

        // Get all character stats at once
        Dictionary<string, int> _charStats = StatCalc.I.get_all_stats_of_char(_name);
        DB_Chars.CharData _charData = DB_Chars.I.get_char_data(_name);

        // Set stats from the dictionary
        _comp.statHP = _charStats["hp"];
        _comp.statAttack = _charStats["attack"];
        _comp.statRange = _charStats["range"];
        _comp.statSkill = _charStats["skill"];
        _comp.statSpeed = _charStats["speed"];
        _comp.statArmor = _charStats["armor"];
        _comp.statCritRate = _charStats["crit-rate"];
        _comp.statCritDam = _charStats["crit-dam"];

        // Clear and set tags (ensure this is appropriate based on your implementation)
        _comp.tags.Clear();
        _comp.tags.AddRange(_charData.tags); 

        // Set max HP and other combat-related properties
        _comp.hpMax = _comp.statHP;
        _comp.hp = _comp.statHP;
        _comp.armor = _comp.statArmor;
        _comp.dam = _comp.statAttack;
        _comp.skill = _comp.statSkill;
        _comp.speed = 6 + (_comp.statSpeed * 0.25f);
    }

    private void on_create_set_boss (InGameObject _comp){
        if (!_comp.tags.Contains ("boss")) return;

        MUI_HPBars.I.set_boss (_comp);
    }

    private void on_create_set_missile (InGameObject _comp){
        if (_comp.type != "missile") return;

        if (_comp.timedLife <= 0)  _comp.timedLife = 10;
    }

    public GameObject create_obj (string _name, Vector2 _pos, int _player) {
        GameObject _obj = DB_Objects.I.get_game_obj (_name);
        InGameObject _comp = _obj.GetComponent <InGameObject>();

        if (!_comp) Debug.Log ($"WARNING: InGameObject not found for spawned object {_name}");
        _obj.transform.position = _pos;
        _comp.owner = _player;
        on_create_set_obj_stats (_comp, _name);
        
        set_default_skills (_comp);
        setup_events (_comp);
        on_create_set_boss (_comp);
        on_create_set_missile (_comp);

        if (_comp.type != "collect"){
            _comp.barHP = _obj.AddComponent<HealthBarScript>();
            _comp.barHP.Setup ("health");
        }

        // On spawn events
        List<EvtTrig> _evts = get_evts_with_trigger_name (_comp, "spawn");
        foreach (EvtTrig _evt in _evts) {
            _evt.use ();
        }

        return _obj;
    }

    public GameObject create_obj_spawner (string _name, Vector2 _pos, int _player){
        GameObject _obj = create_obj ("move-smoke-spawner", _pos, _player);
        _obj.GetComponent<Evt_SpawnerDth>().toSpawn = _name;
        _obj.GetComponent<Evt_SpawnerDth>().owner = _player;
        _obj.GetComponent<InGameObject>().timedLife = 1;

        return _obj;
    }

    public GameObject create_missile (string _name, Vector2 _pos, int _player, float _ang) {
        GameObject _obj = create_obj (_name, _pos, _player);
        InGameObject _comp = _obj.GetComponent <InGameObject> ();
        
        change_facing (_comp, (Mathf.Abs (_ang) < 180) ? "left" : "right");

        const_move_ang_set (_comp, _ang, _comp.speed);

        return _obj;
    }
    
    // Update
    public void update_obj (InGameObject _obj){
        if (Game.I.isPaused) return;

        calc_z_pos (_obj);

        if (!_obj.forcedMovement) {
            input_move_update (_obj);
            const_move_ang_update (_obj);
            const_move_dir_update (_obj);
            move_walk_to_pos_update (_obj);
            instant_move_upd_update (_obj);
            move_bounds (_obj);
        }

        jump_update (_obj);

        check_anim (_obj);
        color_obj_dur (_obj);
        
        propell_update (_obj);

        pos_limit (_obj);

        ContBuffs.I.update_buffs (_obj);
        
        skill_cd (_obj);
        evt_on_update (_obj);
        timed_life_update (_obj);

        // Set the position of the gameObject
        if (!_obj.forcedMovement) {
            // Calculate the zPos based on the curPos.y relative to the details.size.y
            float normalizedY = (_obj.curPos.y + ContMap.I.details.size.y) / (2 * ContMap.I.details.size.y);
            _obj.zPos = Mathf.Lerp(-9, -1, normalizedY);
        
            _obj.gameObject.transform.position = new Vector3(
                _obj.curPos.x, 
                _obj.curPos.y + _obj.jumpHeight, 
                _obj.zPos
            );
        } else {
            // Calculate the zPos based on the curPos.y relative to the details.size.y
            float normalizedY = (_obj.transform.position.y + ContMap.I.details.size.y) / (2 * ContMap.I.details.size.y);
            _obj.zPos = Mathf.Lerp(-9, -1, normalizedY);
            
            _obj.gameObject.transform.position = new Vector3(
                _obj.transform.position.x, 
                _obj.transform.position.y + _obj.jumpHeight, 
                _obj.zPos
            );
        }

        update_render (_obj);
    }
    
    public void update_every_10th_ms (InGameObject _obj){
        // mp_regen (_obj);
    }

    // Animation
    private void calc_z_pos (InGameObject _obj) {
        Vector3 _pos = _obj.curPos;
        switch (_obj.type) {
            case "missile":
                _obj.zPos = (_pos.y - 110) / 100;
                break;

            default:
                _obj.zPos = (_pos.y - 100) / 100;
                break;
        }
        _pos.z = _obj.zPos;

        _obj.curPos = _pos;
    }
    
    public void check_anim (InGameObject _obj) {
        if (!_obj.hasAnim) return;
        
        // Running / At`ck
        if (_obj.anim.parameters.Any(param => param.name == "isRunning")) { // Check if "isRunning" animation exists in the object
            _obj.isRunning = (((_obj.isWalk || _obj.moveToPos_isOn) && _obj.propellType == "") ? true : false);
            
            _obj.anim.SetBool ("isRunning", _obj.isRunning);
            _obj.anim.SetBool ("isAtk", _obj.isAtk);
            _obj.anim.SetInteger ("toAnim", _obj.toAnim);
        }
        
        // Dash
        if (_obj.anim.parameters.Any(param => param.name == "isDash")) {
            _obj.isDash = (_obj.propellType == "dash");
            
            _obj.anim.SetBool ("isDash", _obj.isDash);
        }
    }

    public void change_facing (InGameObject _obj, string _facing){
        if (_obj.facing == _facing) return;
        _obj.facing = _facing;

        Vector2 _curScale = _obj.gameObject.transform.localScale;
        float scaleX = Mathf.Abs (_curScale.x);
        _curScale.x = ((_obj.facing == "left") ? -scaleX : scaleX);
        _obj.gameObject.transform.localScale = _curScale;
    }

    public void change_obj_angle (InGameObject _obj, float _ang) {
        _obj.gameObject.transform.rotation = Quaternion.Euler (0f, 0f, _ang);
    }

    private void update_render (InGameObject _obj){
        Renderer _renderer = _obj.gameObject.GetComponent<Renderer>();
        if (_renderer == null) return;
        
        _renderer.enabled = Vector2.Distance(_obj.gameObject.transform.position, ContPlayer.I.player.transform.position) <= 35f;
    }

    public void change_color (InGameObject _obj, Color _color, float _dur) {
        _obj.gameObject.GetComponent<SpriteRenderer>().color = _color;
        _obj.colorChangeDur = _dur;
    }

    public void color_obj_dur (InGameObject _obj) {
        if (_obj.colorChangeDur <= 0) return;

        _obj.colorChangeDur -= Time.deltaTime;
        if (_obj.colorChangeDur <= 0) {
            _obj.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    // Movement
    /*
        LIST OF MOVEMENT TYPES:
        - input_move - uses "Vector2.MoveTowards"
        - move_walk_to_pos - uses "Vector2.MoveTowards"
        - propell_to_angle (InGameObject _obj, float _ang, float _spd, float _drag, float _distLim, bool _changeAng = false)
        - change_velocity
        - const_move_ang_set
        - const_move_dir_set
        - move_instant
        - move_forward_instant
        - stop_obj
        - instant_move_upd_start_dur (InGameObject _obj, float _ang, float _spd, float _dur)
            - will be used for knockbacks, etc.
        - instant_move_upd_update
    */
    public void input_move (InGameObject _obj, Vector2 _value){
        if (!_obj) return;
        if (_obj.isAtk || !DB_Conditions.I.can_move (_obj)) {
            _obj.isWalk = false;
            return; 
        }
        else if (_obj.propellType == "knocked") {
            if (_obj.rb.velocity.magnitude < 1f){
                _obj.propellType = "";
                change_velocity (_obj, new Vector3 (0, 0, 0));
            }
            return;
        }
        else if (_obj.propellType != ""){
            _obj.isWalk = false;
            return;
        }
        
        _obj.movInput.x = (_value.x > 0f) ? 1 : (_value.x < 0f) ? -1 : 0;
        _obj.movInput.y = (_value.y > 0f) ? 1 : (_value.y < 0f) ? -1 : 0;

        if (_obj.movInput.x != 0) {
            change_facing (_obj, (_obj.movInput.x < 0) ? "left" : "right");
        }

        _obj.isWalk = (_obj.movInput.x != 0 || _obj.movInput.y != 0);
    }

    public void input_move_update (InGameObject _obj){
        if (!_obj.isWalk || !DB_Conditions.I.can_move (_obj)) return;

        _obj.nxtPos.x = _obj.movInput.x * 100;
        _obj.nxtPos.y = _obj.movInput.y * 100;

        _obj.walkTargPos.x = _obj.gameObject.transform.position.x + _obj.nxtPos.x;
        _obj.walkTargPos.y = _obj.gameObject.transform.position.y + _obj.nxtPos.y;
        _obj.curPos = Vector2.MoveTowards (_obj.curPos, _obj.walkTargPos, _obj.speed * Time.deltaTime);
        InGameCamera.I.point_to_target ();
    }

    public void move_walk_to_pos(InGameObject _obj, Vector2 _dir) {
        _obj.moveToPos_isOn = true;
        _obj.moveToPos_pos = _dir;
    }

    public void move_walk_to_pos_stop(InGameObject _obj) {
        _obj.moveToPos_isOn = false;
    }

    public void move_walk_to_pos_update(InGameObject _obj) {
        if (!_obj.moveToPos_isOn || !DB_Conditions.I.can_move(_obj)) return;

        Vector2 currentPos = _obj.curPos;
        Vector2 targetPos = _obj.moveToPos_pos;
        Vector2 direction = (targetPos - currentPos).normalized;

        float rayDistance = 0.5f; // Adjust this value based on the desired ray length
        Debug.DrawRay(currentPos, direction * rayDistance, Color.red);
        Debug.DrawLine(currentPos, targetPos, Color.green);

        RaycastHit2D hit = Physics2D.Raycast(currentPos, direction, rayDistance);

        if (hit.collider != null && !hit.collider.isTrigger && hit.collider.gameObject != _obj.gameObject)
        {
            Vector2 avoidanceDir = new Vector2(-direction.y, direction.x); // Perpendicular direction
            float newAngle = Mathf.Atan2(avoidanceDir.y, avoidanceDir.x) * Mathf.Rad2Deg;

            newAngle += 45.0f;

            direction = new Vector2(Mathf.Cos(newAngle * Mathf.Deg2Rad), Mathf.Sin(newAngle * Mathf.Deg2Rad));
        }

        _obj.curPos = Vector2.MoveTowards(currentPos, currentPos + direction, _obj.speed * Time.deltaTime);

        if (Calculator.I.get_dist_from_2_points(_obj.moveToPos_pos, _obj.curPos) <= 0.1f)
        {
            _obj.moveToPos_isOn = false;
        }
    }

    public void const_mov_ang_with_dur (InGameObject _obj, float _ang, float _dur){

    }

    public void propell_to_angle (InGameObject _obj, float _ang, float _spd, float _drag, float _distLim, string _propellType, bool _changeAng = false) {
        /*
            KNOWN PROPELL TYPES:
            - "knocked", "dash"
        */
        if (_changeAng) change_obj_angle (_obj, _ang);

        _obj.propellType = _propellType;
        Vector3 _vel = new Vector3(_spd * Mathf.Cos(_ang * Mathf.Deg2Rad), _spd * Mathf.Sin(_ang * Mathf.Deg2Rad), 0);
        _obj.knockDrag = _drag;
        _obj.propellDist = _distLim;
        _obj.propellFirstPos = new Vector2 (_obj.transform.position.x, _obj.transform.position.y);
        change_velocity (_obj, _vel);
    }

    public void change_velocity (InGameObject _obj, Vector3 _newVel) {
        _obj.rb.velocity = _newVel;
    }

    public void propell_update (InGameObject _obj) {
        if (_obj.propellType == "" || _obj.propellType == "missile") return;

        _obj.rb.velocity -= _obj.rb.velocity * _obj.knockDrag * Time.fixedDeltaTime;
        if (_obj.rb.velocity.magnitude <= 0.25f){
            propell_stop (_obj);
        }
        
        Vector2 _newPos = new Vector2 (_obj.transform.position.x, _obj.transform.position.y);
        
        float _dist = Vector2.Distance (_obj.propellFirstPos, _newPos);
        if (_dist >= _obj.propellDist) {
            propell_stop (_obj);
        }
    }
    
    private void propell_stop (InGameObject _obj){
        _obj.propellType = "";
        change_velocity (_obj, new Vector3 (0, 0, 0));
    }

    public void const_move_ang_set (InGameObject _obj, float _ang, float _spd = 0f){
        _obj.constMovAng_isOn = true;
        if (_spd == 0f) _spd = _obj.speed;
        _obj.constMovAng_spd = _spd;
        _obj.constMovAng_ang = _ang;
    }

    public void const_move_ang_with_dur (InGameObject _obj, float _ang, float _dur, float _spd = 0f){
        const_move_ang_set (_obj, _ang, _spd);
        _obj.constMovAng_dur = _dur;
    }

    public void const_move_ang_with_dist (InGameObject _obj, float _ang, float _dist, float _spd = 0f){
        const_move_ang_set (_obj, _ang, _spd);
        _obj.constMovAng_dist = _dist;
    }

    public void const_move_ang_update (InGameObject _obj){
        if (!_obj.constMovAng_isOn || !DB_Conditions.I.can_move (_obj)) return;

        Vector3 _curPos = _obj.curPos;
        float   _yPos = _curPos.y + _obj.constMovAng_spd * Mathf.Sin(_obj.constMovAng_ang * Mathf.Deg2Rad);
        Vector3 _newPos = new Vector3(
            _curPos.x + _obj.constMovAng_spd * Mathf.Cos(_obj.constMovAng_ang * Mathf.Deg2Rad), 
            _yPos,
            _obj.zPos
        );

        if (_obj.isRotate && _obj.gameObject.GetComponent<SpinObject>() == null) {
            _obj.gameObject.transform.rotation = Quaternion.Euler (0, 0, _obj.constMovAng_ang);
        }

        if (_obj.range > 0) {
            _obj.range -= _obj.constMovAng_spd;
            if (_obj.range <= 0) {
                ContDamage.I.kill (_obj);
            }
        }

        if (_obj.constMovAng_dur > 0) {
            _obj.constMovAng_dur -= Time.deltaTime;

            if (_obj.constMovAng_dur <= 0) {
                _obj.constMovAng_isOn = false;
            }
        }

        if (_obj.constMovAng_dist > 0) {
            _obj.constMovAng_dist -= _obj.constMovAng_spd;

            if (_obj.constMovAng_dist <= 0) {
                _obj.constMovAng_isOn = false;
            }
        }

        _obj.curPos = _newPos;
    }

    public void const_move_dir_set (InGameObject _obj, Vector2 _dir, float _spd = 0f){
        _obj.constMovDir_isOn = true;
        if (_spd == 0f) _spd = _obj.speed;
        _obj.constMovDir_spd = _spd;
        _obj.constMovDir_dir = _dir;
    }

    public void const_move_dir_update (InGameObject _obj){
        if (!_obj.constMovDir_isOn || !DB_Conditions.I.can_move (_obj)) return;

        Vector2 _pos = _obj.curPos;
        // transform.Translate(_dir * speed * Time.deltaTime);
        _obj.curPos = Vector2.MoveTowards (_pos, _obj.constMovDir_dir, _obj.speed * Time.deltaTime);
    }

    private void move_bounds (InGameObject _obj){
        if (!DB_Conditions.I.is_check_border (_obj)) return;
        
        DB_Maps.mapDetails _details = ContMap.I.details;
        float _xL = _details.size.x, _yL = _details.size.y;
        if (_obj.curPos.x > _xL)                _obj.curPos.x = _xL - 0.1f;
        else if (_obj.curPos.x < -_xL)          _obj.curPos.x = -_xL + 0.1f;
        if (_obj.curPos.y > _yL)                _obj.curPos.y = _yL - 0.1f;
        else if (_obj.curPos.y < -_yL)          _obj.curPos.y = -_yL + 0.1f;
    }

    public void move_instant (InGameObject _obj, Vector2 _pos) {
        _obj.curPos = new Vector3 (_pos.x, _pos.y, _obj.zPos);
    }

    public void move_forward_instant (InGameObject _obj, float _ang, float _dis) {
        move_instant (_obj, Calculator.I.get_next_point_in_direction (_obj.curPos, _ang, _dis));
    }

    public void pos_limit (InGameObject _obj){
        if (!DB_Conditions.I.is_kill_on_border_pass (_obj)) return;
        
        Vector2 _pos = _obj.curPos;
        float _lim = 3000f;
        
        if (_pos.x > _lim || _pos.x < -_lim || _pos.y > _lim || _pos.y < -_lim) {
            Destroy (_obj.gameObject);
        }
    }
    
    public void stop_obj (InGameObject _obj){
        _obj.rb.velocity = Vector3.zero;
        _obj.walkTargPos = Vector3.zero;
    }

    public void instant_move_upd_start_dur (InGameObject _obj, float _ang, float _spd, float _dur, string _mode = ""){
        _obj.instMov_dur = _dur;
        _obj.instMov_dist = 0;

        instant_move_upd_start (_obj, _ang, _spd, _mode);
    }

    public void instant_move_upd_start_dist (InGameObject _obj, float _ang, float _spd, float _dist, string _mode = ""){
        _obj.instMov_dur = 0;
        _obj.instMov_dist = _dist;

        instant_move_upd_start (_obj, _ang, _spd, _mode);
    }

    public void instant_move_upd_start (InGameObject _obj, float _ang, float _spd, string _mode){
        _obj.instMov_isOn = true;
        _obj.instMov_ang = _ang;
        _obj.instMov_spd = _spd;
        _obj.instMov_mode = _mode;
    }

    public void instant_move_upd_update (InGameObject _obj){
        if (!_obj.instMov_isOn) return;

        move_forward_instant (_obj, _obj.instMov_ang, _obj.instMov_spd);

        if (_obj.instMov_dur > 0) {
            // Duration
            _obj.instMov_dur -= Time.deltaTime;
            if (_obj.instMov_dur <= 0) {
                _obj.instMov_isOn = false;
                _obj.instMov_mode = "";
            }

        } else if (_obj.instMov_dist > 0){
            // Distance
            _obj.instMov_dist -= _obj.instMov_spd;
            if (_obj.instMov_dist <= 0) {
                _obj.instMov_isOn = false;
                _obj.instMov_mode = "";
            }
        }
    }

    // Jump
    public void set_jump(InGameObject _obj, float _height, float _toMaxHeightDur) {
        if (!_obj.isJumping)
        {
            _obj.jumpTargetHeight = _height;
            _obj.jumpDuration = _toMaxHeightDur;
            _obj.jumpStartTime = Time.time;
            _obj.isJumping = true;
        }
    }

    private void jump_update(InGameObject _obj) {
        if (_obj.isJumping)
        {
            float elapsedTime = Time.time - _obj.jumpStartTime;
            float halfDuration = _obj.jumpDuration / 2; // Half duration for both ascending and descending

            if (elapsedTime < _obj.jumpDuration)
            {
                if (elapsedTime < halfDuration) // Ascending phase
                {
                    float progress = elapsedTime / halfDuration;
                    _obj.jumpHeight = Mathf.Lerp(0, _obj.jumpTargetHeight, progress);
                }
                else // Descending phase
                {
                    float fallElapsedTime = elapsedTime - halfDuration;
                    float fallProgress = fallElapsedTime / halfDuration;
                    _obj.jumpHeight = Mathf.Lerp(_obj.jumpTargetHeight, 0, fallProgress);
                }
            }
            else // End of jump
            {
                _obj.jumpHeight = 0;
                _obj.isJumping = false;
            }
        }
    }

    // GETs
    public List<InGameObject> get_objs_in_area (Vector2 _pos, float _range){
        List<InGameObject> _ret = new List<InGameObject>();

        foreach (InGameObject _o in GameObject.FindObjectsOfType<InGameObject>()) {
            if (Vector2.Distance (_o.gameObject.transform.position, _pos) <= _range) {
                _ret.Add (_o);
            }
        }
        return _ret;
    }

    public InGameObject get_obj_with_id (int _id){
        foreach (InGameObject _o in GameObject.FindObjectsOfType<InGameObject>()) {
            if (_o.id == _id) {
                return _o;
            }
        }
        return null;
    }
    
    // Stats
    private void mp_regen (InGameObject _obj){
        // if (!DB_Conditions.I.can_mp_regen (_obj)) return;
        
        // _obj.mp += _obj.mpRegen;
        // if (_obj.mp > _obj.mpMax) _obj.mp = _obj.mpMax;
    }

    // Skill / Attack
    public void use_skill_active (InGameObject _obj, string _skillName){
        foreach (SkillTrig _skill in _obj.skills) {
            if (_skill.skillName != _skillName)     continue;

            _skill.use_active ();
            break;
        }
    }

    public SkillTrig get_skill_with_skill_slot (InGameObject _obj, string _skillSlot) {
        SkillTrig _ret = null;

        foreach (SkillTrig _skill in _obj.skills) {
            if (_skill.skillSlot == _skillSlot) {
                _ret = _skill;
                break;
            }
        }

        return _ret;
    }

    public void set_default_skills (InGameObject _obj){
        foreach (SkillTrig _skill in _obj.gameObject.GetComponents <SkillTrig> ()) {
            if (_skill.skillSlot == "Skill1") _obj.skill1 = _skill;
            else if (_skill.skillSlot == "Skill2") _obj.skill2 = _skill;
            
            _obj.skills.Add (_skill);
        }
    }
    
    // Events
    public void setup_events (InGameObject _obj){
        List<EvtTrig> _evts = new List<EvtTrig> ();
        _evts.AddRange (_obj.gameObject.GetComponents <EvtTrig> ());
        
        foreach (EvtTrig _evt in _evts) {
            _evt.setup ();
        }
    }

    public List<EvtTrig> get_evts_with_trigger_name (InGameObject _obj, string _trigName){
        List<EvtTrig> _ret = new List<EvtTrig> ();
        List<EvtTrig> _evts = new List<EvtTrig> ();
        _evts.AddRange (_obj.gameObject.GetComponents <EvtTrig> ());
        
        foreach (EvtTrig _evt in _evts) {
            if (_evt.evtTrigger == _trigName) {
                _ret.Add (_evt);
            }
        }
        
        return _ret;
    }
    
    public void evt_on_death (InGameObject _obj){
        List<EvtTrig> _evts = get_evts_with_trigger_name (_obj, "death");
        foreach (EvtTrig _evt in _evts) {
            _evt.use ();
        }
    }

    private void skill_cd (InGameObject _obj){
        SkillTrig[] _skills = _obj.GetComponentsInChildren<SkillTrig>();
        foreach (SkillTrig _s in _skills) {
            if (_s.cd <= 0) continue;
            _s.cd -= Time.deltaTime;
            if (_s.cd < 0) _s.cd = 0;
        }
    }

    public void evt_on_update (InGameObject _obj){
        List<EvtTrig> _evts = get_evts_with_trigger_name (_obj, "update");
        foreach (EvtTrig _evt in _evts) {
            _evt.use ();
        }
    }

    private void timed_life_update (InGameObject _obj){
        if (_obj.timedLife <= 0) return;
        
        _obj.timedLife -= Time.deltaTime;
        if (_obj.timedLife <= 0) {
            evt_on_death (_obj);
            ContDamage.I.kill (_obj);
        }
    }

    // Other
    public void face_player (InGameObject _obj){
        InGameObject _p = ContPlayer.I.player;
        Vector2 _pPos = _p.gameObject.transform.position,
                _objPos = _obj.gameObject.transform.position;

        change_facing (_obj, ((_objPos.x > _pPos.x) ? "left" : "right"));
    }
}
