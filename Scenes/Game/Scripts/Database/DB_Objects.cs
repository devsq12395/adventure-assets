using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DB_Objects : MonoBehaviour {

    public static DB_Objects I;
	public void Awake(){ I = this; }

    public GameObject dummy;

    [Header("----- Characters for Player -----")]
    public GameObject tommy;
    public GameObject kazuma, anastasia, seraphine, miguel, anthony;

    [Header("----- Units -----")]
    public GameObject hero;
    public GameObject samurai, greenSlime, orcShaman, kitsune, kitsuneBoss, slimeRed, slimeBlue, slimeOrange, mobster, slimeKing, giantSlime,
        lucaTheTerror, embracedInfantry, embracedMage, prismDrone, warShredder, alphaWarShredder, captainCharles, captainBeatrice,
        centurion, victorianSoldier;

    [Header("----- Missiles -----")]
    public GameObject testMissile1;
    public GameObject beam1, beam1Placeholder, kitsuneMissile, molotovMsl, voidSphere, iceMissile01, shotgun, slimeBlueMissile, slimeGreenMissile, slashKazuma, flameWave,
        bulletTommy, bulletMobster, bulletEmbraced, bulletVictorianSoldier, fireWave, blueWave, slashShredder, prismDroneMissile,
        centurionMissile, beatriceMissile, lucaTyphoon;

    [Header("----- Effects -----")]
    public GameObject explosion1;
    public GameObject explosion2, explosion3, damTxt, explosion1_mini, molotovEfct, bindChainExp1, bindChainExp2, voidSphereHit, voidSphereCast, frostWaveHit, 
        frostWaveCast, moveSmoke, moveSmokeSpawner, muzzle1, muzzle2, gooGreen, gooBlue, magicSparkSeraphine;

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
            case "kazuma":                    _refObj = kazuma; break;
            case "seraphine":case "sylphine":               _refObj = seraphine; break;
            case "anastasia":               _refObj = anastasia; break;
            case "miguel":                  _refObj = miguel; break;
            case "anthony":                 _refObj = anthony; break;

            // Enemies
            case "greenSlime":              _refObj = greenSlime; break;
            case "orc-shaman":               _refObj = orcShaman; break;
            case "kitsune":                 _refObj = kitsune; break;
            case "kitsune-boss":            _refObj = kitsuneBoss; break;
            case "slime-red":            _refObj = slimeRed; break;
            case "slime-blue":            _refObj = slimeBlue; break;
            case "slime-orange":            _refObj = slimeOrange; break;
            case "mobster":            _refObj = mobster; break;
            case "slime-king":            _refObj = slimeKing; break;
            case "giant-slime":            _refObj = giantSlime; break;
            case "luca-the-terror":            _refObj = lucaTheTerror; break;
            case "embraced-infantry":            _refObj = embracedInfantry; break;
            case "embraced-mage":            _refObj = embracedMage; break;
            case "prism-drone":            _refObj = prismDrone; break;
            case "war-shredder":            _refObj = warShredder; break;
            case "alpha-war-shredder":            _refObj = alphaWarShredder; break;
            case "captain-charles":            _refObj = captainCharles; break;
            case "cap-beatrice":            _refObj = captainBeatrice; break;
            case "victorian-soldier":            _refObj = victorianSoldier; break;
            case "centurion":            _refObj = centurion; break;

            // Missiles
            case "testMissile1":            _refObj = testMissile1; break;
            case "SampleAssist_Missile":    _refObj = testMissile1; break;
            case "beam1":                   _refObj = beam1; break;
            case "beam-1-placeholder":        _refObj = beam1Placeholder; break;
            case "kitsuneMissile":          _refObj = kitsuneMissile; break;
            case "molotovMsl":              _refObj = molotovMsl; break;
            case "voidSphere":              _refObj = voidSphere; break;
            case "iceMissile01":              _refObj = iceMissile01; break;
            case "shotgun":                 _refObj = shotgun; break;
            case "slime-blue-missile":                 _refObj = slimeBlueMissile; break;
            case "slime-green-missile":                 _refObj = slimeGreenMissile; break;
            case "slash-kazuma":                 _refObj = slashKazuma; break;
            case "flame-wave":case "fire-wave":                 _refObj = flameWave; break;
            case "bullet-tommy":                 _refObj = bulletTommy; break;
            case "bullet-mobster":                 _refObj = bulletMobster; break;
            case "bullet-embraced":                 _refObj = bulletEmbraced; break;
            case "bullet-victorian-soldier":                 _refObj = bulletVictorianSoldier; break;
            case "blue-wave":                 _refObj = blueWave; break;
            case "slash-shredder":                 _refObj = slashShredder; break;
            case "prism-drone-missile":             _refObj = prismDroneMissile; break;
            case "centurion-missile":             _refObj = centurionMissile; break;
            case "beatrice-missile":             _refObj = beatriceMissile; break;
            case "luca-typhoon":             _refObj = lucaTyphoon; break;

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
            case "move-smoke":             _refObj = moveSmoke; break;
            case "move-smoke-spawner":             _refObj = moveSmokeSpawner; break;
            case "muzzle-1":             _refObj = muzzle1; break;
            case "muzzle-2":             _refObj = muzzle2; break;
            case "goo-green":             _refObj = gooGreen; break;
            case "goo-blue":             _refObj = gooBlue; break;
            case "magic-spark-seraphine":             _refObj = magicSparkSeraphine; break;
                
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
        SceneManager.MoveGameObjectToScene(_retVal, SceneManager.GetSceneByName("Game"));
        return _retVal;
    }
}
