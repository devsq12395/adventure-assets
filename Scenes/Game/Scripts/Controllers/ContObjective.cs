using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class ContObjective : MonoBehaviour {

    public static ContObjective I;
    public void Awake(){ I = this; }

    public TextMeshProUGUI objectiveText;

    public void setup_and_set_starting_objective(string objective){
        set_objective (objective);
    }

    public void set_objective(string objective){
        objectiveText.text = objective;
    }

}