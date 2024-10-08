using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_Dialogs_TheOldTavern : MonoBehaviour {
    public static DB_Dialogs_TheOldTavern I;
    public void Awake() { 
        I = this;
    }

    public DB_Dialog.DialogData get_dialog_data(string _dialogId, DB_Dialog.DialogData) {
        switch (_dialogId) {
            
        }
        _new.name = "Bartender";
                _new.desc = "The tavern today is so merry! Which drink do you want, mister?";
                _new.portImg = "npc-woman-2";
                _new.isTweenOut = "1";
                _new.inputEmptyContinue = "";
                _new.isMini = "0";
                _new.inputScaler_enable = "0";
                _new.posX = 0;
                _new.posY = 0;
                _new.input1 = new InputData("recruit-anastasia-1", "Recruit Anastasia");
                _new.input2 = new InputData("recruit-sylphine-1", "Recruit Sylphine");
                _new.input3 = new InputData("shopCancel", "Nevermind");
                _new.input4 = new InputData("", "");
                _new.inputTxtBox = new InputData("", "");
    }
}