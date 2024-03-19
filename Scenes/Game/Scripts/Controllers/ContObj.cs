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
    private void on_create_set_obj_stats (InGameObject _comp){
        if (_comp.tags.Contains ("hero")) _comp.mp = 0;
    }

    public GameObject create_obj (string _name, Vector2 _pos, int _player) {
        GameObject _obj = DB_Objects.I.get_game_obj (_name);
        InGameObject _comp = _obj.GetComponent <InGameObject>();

        _obj.transform.position = _pos;
        _comp.owner = _player;
        on_create_set_obj_stats (_comp);
        
        set_default_skills (_comp);
        setup_events (_comp);
        if (_comp.tags.Contains ("boss")) MUI_HPBars.I.set_boss (_comp);

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

        _obj.curPos.x = _obj.gameObject.transform.position.x;
        _obj.curPos.y = _obj.gameObject.transform.position.y;

        calc_z_pos (_obj);

        input_move_update (_obj);
        const_move_ang_update (_obj);
        const_move_dir_update (_obj);
        move_walk_to_pos_update (_obj);
        move_bounds (_obj);

        check_anim (_obj);
        
        propell_update (_obj);

        pos_limit (_obj);

        ContBuffs.I.update_buffs (_obj);
        
        skill_cd (_obj);
        evt_on_update (_obj);
        timed_life_update (_obj);

        _obj.gameObject.transform.position = new Vector3 (_obj.curPos.x, _obj.curPos.y, _obj.zPos);

        update_render (_obj);
    }
    
    public void update_every_10th_ms (InGameObject _obj){
        mp_regen (_obj);
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
        if (Vector2.Distance(_obj.gameObject.transform.position, Camera.main.transform.position) > GameConstants.RENDER_DIST) {
            _obj.gameObject.GetComponent<Renderer>().enabled = false;
        } else {
            _obj.gameObject.GetComponent<Renderer>().enabled = true;
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

    public void move_walk_to_pos (InGameObject _obj, Vector2 _dir){
        _obj.moveToPos_isOn = true;
        _obj.moveToPos_pos = _dir;
    }

    public void move_walk_to_pos_stop (InGameObject _obj){
        _obj.moveToPos_isOn = false;
    }

    public void move_walk_to_pos_update (InGameObject _obj){
        if (!_obj.moveToPos_isOn || !DB_Conditions.I.can_move (_obj)) return;

        _obj.curPos = Vector2.MoveTowards (_obj.curPos, _obj.moveToPos_pos, _obj.speed * Time.deltaTime);

        if (Calculator.I.get_dist_from_2_points (_obj.moveToPos_pos, _obj.curPos) <= 0.1f) {
            _obj.moveToPos_isOn = false;
        }
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

    public void const_move_ang_update (InGameObject _obj){
        if (!_obj.constMovAng_isOn || !DB_Conditions.I.can_move (_obj)) return;

        Vector3 _curPos = _obj.curPos;
        float _yPos = _curPos.y + _obj.constMovAng_spd * Mathf.Sin(_obj.constMovAng_ang * Mathf.Deg2Rad);
        Vector3 _newPos = new Vector3(
            _curPos.x + _obj.constMovAng_spd * Mathf.Cos(_obj.constMovAng_ang * Mathf.Deg2Rad), 
            _yPos,
            _obj.zPos
        );

        if (_obj.isRotate) {
            _obj.gameObject.transform.rotation = Quaternion.Euler (0, 0, _obj.constMovAng_ang);
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
        _obj.gameObject.transform.position = new Vector3 (_pos.x, _pos.y, _obj.zPos);
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
        if (!DB_Conditions.I.can_mp_regen (_obj)) return;
        
        _obj.mp += _obj.mpRegen;
        if (_obj.mp > _obj.mpMax) _obj.mp = _obj.mpMax;
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
            ContDamage.I.kill (_obj);
        }
    }
}
