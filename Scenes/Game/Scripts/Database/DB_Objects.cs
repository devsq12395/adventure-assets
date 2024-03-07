using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_Objects : MonoBehaviour {

    public static DB_Objects I;
	public void Awake(){ I = this; }

    public GameObject dummy;

    [Header("----- Characters for Player -----")]
    public GameObject tommy;
    public GameObject anastasia, seraphine, miguel, anthony;

    [Header("----- Units -----")]
    public GameObject hero;
    public GameObject samurai, greenSlime, orcShaman, kitsune;

    [Header("----- Missiles -----")]
    public GameObject testMissile1;
    public GameObject beam1, kitsuneMissile, molotovMsl, voidSphere, iceMissile01;

    [Header("----- Effects -----")]
    public GameObject explosion1;
    public GameObject explosion2, explosion3, damTxt, explosion1_mini, molotovEfct, bindChainExp1, bindChainExp2, voidSphereHit, voidSphereCast, frostWaveHit, 
        frostWaveCast;

    [Header("----- Buffs -----")]
    public GameObject buf_burn;
    public GameObject buf_bindingChains, buf_voidSphereGrounded;

    [Header("----- Dummies -----")]
    public GameObject blizzardDummy;

    void Start() {
        
    }

    void Update() {
        
    }

    public GameObject get_game_obj (string _name) {
        GameObject _refObj = GameObject.Instantiate(dummy, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
        Destroy (_refObj);
        
        switch (_name) {
            // Player
            case "hero":                    _refObj = hero; break;
            case "samurai":                 _refObj = samurai; break;
            case "tommy":                   _refObj = tommy; break;
            case "seraphine":               _refObj = seraphine; break;
            case "anastasia":               _refObj = anastasia; break;
            case "miguel":                  _refObj = miguel; break;
            case "anthony":                 _refObj = anthony; break;

            // Enemies
            case "greenSlime":              _refObj = greenSlime; break;
            case "orcShaman":               _refObj = orcShaman; break;
            case "kitsune":                 _refObj = kitsune; break;

            // Missiles
            case "testMissile1":            _refObj = testMissile1; break;
            case "SampleAssist_Missile":    _refObj = testMissile1; break;
            case "beam1":                   _refObj = beam1; break;
            case "kitsuneMissile":          _refObj = kitsuneMissile; break;
            case "molotovMsl":              _refObj = molotovMsl; break;
            case "voidSphere":              _refObj = voidSphere; break;
            case "iceMissile01":              _refObj = iceMissile01; break;

            // Effects
            case "explosion1":              _refObj = explosion1; break;
            case "explosion1_mini":         _refObj = explosion1_mini; break;
            case "explosion2":              _refObj = explosion2; break;
            case "explosion3":         _refObj = explosion3; break;
            case "molotovEfct":             _refObj = molotovEfct; break;
            case "bindChainExp1":             _refObj = bindChainExp1; break;
            case "bindChainExp2":             _refObj = bindChainExp2; break;
            case "voidSphereHit":             _refObj = voidSphereHit; break;
            case "voidSphereCast":             _refObj = voidSphereCast; break;
            case "frostWaveHit":             _refObj = frostWaveHit; break;
            case "frostWaveCast":             _refObj = frostWaveCast; break;
                
            case "damTxt":                  _refObj = damTxt; break;
                
            // Buffs
            case "buffAtch_burn":                       _refObj = buf_burn; break;
            case "buffAtch_binding-chains":             _refObj = buf_bindingChains; break;
            case "buffAtch_void-sphere-grounded":             _refObj = buf_voidSphereGrounded; break;

            // Dummies
            case "blizzardDummy":             _refObj = blizzardDummy; break;

            default: _refObj = dummy; break;
        }

        GameObject _retVal = GameObject.Instantiate(_refObj, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; 
        return _retVal;
    }
}
