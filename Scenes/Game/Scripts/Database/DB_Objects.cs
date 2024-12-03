using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DB_Objects : MonoBehaviour {

    public static DB_Objects I;    
    private Dictionary<string, GameObject> objectMap;

    public void Awake(){
        I = this;
        InitializeObjectMap();
    }

    public GameObject dummy;

    [Header("----- Characters for Player -----")]
    public GameObject tommy;
    public GameObject kazuma, anastasia, seraphine, miguel, anthony;

    [Header("----- Units -----")]
    public GameObject hero;
    public GameObject samurai, greenSlime, orcShaman, kitsune, kitsuneBoss, slimeRed, slimeBlue, slimeOrange, mobster, slimeKing, giantSlime,
        lucaTheTerror, embracedInfantry, embracedMage, prismDrone, warShredder, alphaWarShredder, captainCharles, captainBeatrice,
        centurion, victorianSoldier, mafiaBoss, assassin, assassinCaptain, goblin, goblinMage, hobgoblin, fireSpirit, orc, axeArmAzar,
        frostOrb, barrel, skeletonAxethrower;

    [Header("----- Missiles -----")]
    public GameObject testMissile1;
    public GameObject beam1, beam1Placeholder, kitsuneMissile, molotovMsl, voidSphere, iceMissile01, shotgun, slimeBlueMissile, slimeGreenMissile, slashKazuma, flameWave,
        bulletTommy, bulletMobster, bulletEmbraced, bulletVictorianSoldier, fireWave, blueWave, slashShredder, prismDroneMissile,
        centurionMissile, beatriceMissile, lucaTyphoon, mafiaPunch, mafiaGunShot, shuriken, shurikenSpiral, fireball, alfredExplodeSlash, 
        tommySuperAtk, destroyerSlash, frostOrbMissile, axeSkeleton;

    [Header("----- Effects -----")]
    public GameObject explosion1;
    public GameObject explosion2, explosion3, damTxt, explosion1_mini, molotovEfct, bindChainExp1, bindChainExp2, voidSphereHit, voidSphereCast, frostWaveHit, 
        frostWaveCast, moveSmoke, moveSmokeSpawner, muzzle1, muzzle2, gooGreen, gooBlue, magicSparkSeraphine, smokeExpand, alfredBurningSlash,
        crosshairSkeletonAxe, cloud1, cloud2, cloud3, cloud4, cloud5;

    [Header("----- Buffs -----")]
    public GameObject buf_burn;
    public GameObject buf_bindingChains, buf_voidSphereGrounded;

    [Header("----- Collectibles -----")]
    public GameObject colCoin;
    public GameObject colCoinPack;

    [Header("----- Dummies -----")]
    public GameObject blizzardDummy;

    private void InitializeObjectMap() {
         objectMap = new Dictionary<string, GameObject> {
            { "hero", hero },
            { "samurai", samurai },
            { "tommy", tommy },
            { "kazuma", kazuma },
            { "kazuya", kazuma },
            { "seraphine", seraphine },
            { "sylphine", seraphine },
            { "anastasia", anastasia },
            { "miguel", miguel },
            { "anthony", anthony },
            { "greenSlime", greenSlime },
            { "orcShaman", orcShaman },
            { "orc-shaman", orcShaman },
            { "kitsune", kitsune },
            { "kitsuneBoss", kitsuneBoss },
            { "kitsune-boss", kitsuneBoss },
            { "slimeRed", slimeRed },
            { "slime-red", slimeRed },
            { "slimeBlue", slimeBlue },
            { "slime-blue", slimeBlue },
            { "slimeOrange", slimeOrange },
            { "slime-orange", slimeOrange },
            { "mobster", mobster },
            { "slimeKing", slimeKing },
            { "slime-king", slimeKing },
            { "giantSlime", giantSlime },
            { "giant-slime", giantSlime },
            { "lucaTheTerror", lucaTheTerror },
            { "mafia-captain", lucaTheTerror },
            { "embracedInfantry", embracedInfantry },
            { "embraced-infantry", embracedInfantry },
            { "embracedMage", embracedMage },
            { "embraced-mage", embracedMage },
            { "prismDrone", prismDrone },
            { "prism-drone", prismDrone },
            { "warShredder", warShredder },
            { "war-shredder", warShredder },
            { "alphaWarShredder", alphaWarShredder },
            { "alpha-war-shredder", alphaWarShredder },
            { "captainCharles", captainCharles },
            { "captain-charles", captainCharles },
            { "captainBeatrice", captainBeatrice },
            { "cap-beatrice", captainBeatrice },
            { "centurion", centurion },
            { "victorianSoldier", victorianSoldier },
            { "victorian-soldier", victorianSoldier },
            { "mafiaBoss", mafiaBoss },
            { "mafia-boss", mafiaBoss },
            { "assassin", assassin },
            { "assassinCaptain", assassinCaptain },
            { "assassin-captain", assassinCaptain },
            { "goblin", goblin },
            { "goblinMage", goblinMage },
            { "goblin-mage", goblinMage },
            { "hobgoblin", hobgoblin },
            { "fireSpirit", fireSpirit },
            { "fire-spirit", fireSpirit },
            { "orc", orc },
            { "axeArmAzar", axeArmAzar },
            { "axe-arm-azar", axeArmAzar },
            { "frostOrb", frostOrb },
            { "frost-orb", frostOrb },
            { "barrel", barrel },
            { "skeletonAxethrower", skeletonAxethrower },
            { "skeleton-axethrower", skeletonAxethrower },
            { "testMissile1", testMissile1 },
            { "SampleAssist_Missile", testMissile1 },
            { "beam1", beam1 },
            { "beam1Placeholder", beam1Placeholder },
            { "beam-1-placeholder", beam1Placeholder },
            { "kitsuneMissile", kitsuneMissile },
            { "molotovMsl", molotovMsl },
            { "voidSphere", voidSphere },
            { "iceMissile01", iceMissile01 },
            { "shotgun", shotgun },
            { "slimeBlueMissile", slimeBlueMissile },
            { "slime-blue-missile", slimeBlueMissile },
            { "slimeGreenMissile", slimeGreenMissile },
            { "slime-green-missile", slimeGreenMissile },
            { "slashKazuma", slashKazuma },
            { "slash-kazuma", slashKazuma },
            { "flameWave", flameWave },
            { "flame-wave", flameWave },
            { "fireWave", flameWave },
            { "fire-wave", flameWave },
            { "bulletTommy", bulletTommy },
            { "bullet-tommy", bulletTommy },
            { "bulletMobster", bulletMobster },
            { "bullet-mobster", bulletMobster },
            { "bulletEmbraced", bulletEmbraced },
            { "bullet-embraced", bulletEmbraced },
            { "bulletVictorianSoldier", bulletVictorianSoldier },
            { "bullet-victorian-soldier", bulletVictorianSoldier },
            { "blueWave", blueWave },
            { "blue-wave", blueWave },
            { "slashShredder", slashShredder },
            { "slash-shredder", slashShredder },
            { "prismDroneMissile", prismDroneMissile },
            { "prism-drone-missile", prismDroneMissile },
            { "centurionMissile", centurionMissile },
            { "centurion-missile", centurionMissile },
            { "beatriceMissile", beatriceMissile },
            { "beatrice-missile", beatriceMissile },
            { "lucaTyphoon", lucaTyphoon },
            { "luca-typhoon", lucaTyphoon },
            { "mafiaPunch", mafiaPunch },
            { "mafia-punch", mafiaPunch },
            { "mafiaGunShot", mafiaGunShot },
            { "mafia-gun-shot", mafiaGunShot },
            { "shuriken", shuriken },
            { "shurikenSpiral", shurikenSpiral },
            { "shuriken-spin", shurikenSpiral },
            { "fireball", fireball },
            { "alfredExplodeSlash", alfredExplodeSlash },
            { "alfred-explode-slash", alfredExplodeSlash },
            { "tommySuperAtk", tommySuperAtk },
            { "tommy-super-atk", tommySuperAtk },
            { "destroyerSlash", destroyerSlash },
            { "destroyer-slash", destroyerSlash },
            { "frostOrbMissile", frostOrbMissile },
            { "frost-orb-missile", frostOrbMissile },
            { "axeSkeleton", axeSkeleton },
            { "axe-skeleton", axeSkeleton },
            { "explosion1", explosion1 },
            { "explosion2", explosion2 },
            { "explosion3", explosion3 },
            { "damTxt", damTxt },
            { "explosion1_mini", explosion1_mini },
            { "molotovEfct", molotovEfct },
            { "bindChainExp1", bindChainExp1 },
            { "bindChainExp2", bindChainExp2 },
            { "voidSphereHit", voidSphereHit },
            { "voidSphereCast", voidSphereCast },
            { "frostWaveHit", frostWaveHit },
            { "frostWaveCast", frostWaveCast },
            { "moveSmoke", moveSmoke },
            { "move-smoke", moveSmoke },
            { "moveSmokeSpawner", moveSmokeSpawner },
            { "move-smoke-spawner", moveSmokeSpawner },
            { "muzzle1", muzzle1 },
            { "muzzle-1", muzzle1 },
            { "muzzle2", muzzle2 },
            { "muzzle-2", muzzle2 },
            { "gooGreen", gooGreen },
            { "goo-green", gooGreen },
            { "gooBlue", gooBlue },
            { "goo-blue", gooBlue },
            { "magicSparkSeraphine", magicSparkSeraphine },
            { "magic-spark-seraphine", magicSparkSeraphine },
            { "smokeExpand", smokeExpand },
            { "smoke-expand", smokeExpand },
            { "alfredBurningSlash", alfredBurningSlash },
            { "alfred-burning-slash", alfredBurningSlash },
            { "crosshairSkeletonAxe", crosshairSkeletonAxe },
            { "skeleton-axe-crosshair", crosshairSkeletonAxe },
            { "cloud1", cloud1 },
            { "cloud2", cloud2 },
            { "cloud3", cloud3 },
            { "cloud4", cloud4 },
            { "cloud5", cloud5 },
            { "buf_burn", buf_burn },
            { "buffAtch_burn", buf_burn },
            { "buf_bindingChains", buf_bindingChains },
            { "buffAtch_binding-chains", buf_bindingChains },
            { "buf_voidSphereGrounded", buf_voidSphereGrounded },
            { "buffAtch_void-sphere-grounded", buf_voidSphereGrounded },
            { "colCoin", colCoin },
            { "coin", colCoin },
            { "colCoinPack", colCoinPack },
            { "coin-pack", colCoinPack },
            { "blizzardDummy", blizzardDummy }
        };
    }

    public GameObject get_game_obj(string _name) {
        if (objectMap.TryGetValue(_name, out GameObject obj)) {
            GameObject _retVal = GameObject.Instantiate(obj, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; 
            SceneManager.MoveGameObjectToScene(_retVal, SceneManager.GetSceneByName("Game"));
            return _retVal;
        }
        Debug.LogWarning($"GameObject with name {_name} not found.");
        return null;
    }

    void Start() {
        
    }

    void Update() {
        
    }
}
