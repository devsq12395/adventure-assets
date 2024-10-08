using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_Dialogs_TheOldTavern : MonoBehaviour {
    public static DB_Dialogs_TheOldTavern I;
    public void Awake() { 
        I = this;
    }

    public DB_Dialogs.DialogData get_dialog_data(string _dialogId, DB_Dialogs.DialogData _new) {
        switch (_dialogId) {
            case "the-old-tavern": case "the-old-tavern-close-stories":
                _new.name = "Bartender";
                switch (_dialogId) {
                    case "the-old-tavern": _new.desc = "The tavern today is so merry! Which drink do you want, mister?"; break;
                    case "the-old-tavern-close-stories": _new.desc = "Ok then. If you need anything I'm just here in this bar."; break;
                }
                _new.portImg = "npc-man-2";
                _new.isTweenOut = "1";
                _new.inputEmptyContinue = "";
                _new.isMini = "0";
                _new.inputScaler_enable = "0";
                _new.posX = 0;
                _new.posY = 0;
                _new.input1 = new DB_Dialogs.InputData("recruit-anastasia-1", "Recruit Anastasia");
                _new.input2 = new DB_Dialogs.InputData("recruit-sylphine-1", "Recruit Sylphine");
                _new.input3 = new DB_Dialogs.InputData("old-tavern-stories", "Ask Questions");
                _new.input4 = new DB_Dialogs.InputData("shopCancel", "Nevermind");
                _new.inputTxtBox = new DB_Dialogs.InputData("", "");
                break;
            case "old-tavern-stories": case "old-tavern-stories-end":
                _new.name = "Bartender";
                switch (_dialogId) {
                    case "old-tavern-stories": _new.desc = "Ah, you must be new in town. What do you want to know?"; break;
                    case "old-tavern-stories-end": _new.desc = "Anything else you want to know about?"; break;
                }
                _new.portImg = "npc-man-2";
                _new.isTweenOut = "1";
                _new.inputEmptyContinue = "";
                _new.isMini = "0";
                _new.inputScaler_enable = "0";
                _new.posX = 0;
                _new.posY = 0;
                _new.input1 = new DB_Dialogs.InputData("old-tavern-stories-new-haven-1", "New Haven");
                _new.input2 = new DB_Dialogs.InputData("old-tavern-stories-4-families-1", "The 4 Families");
                _new.input3 = new DB_Dialogs.InputData("the-old-tavern-close-stories", "Nevermind");
                _new.input4 = new DB_Dialogs.InputData("", "");
                _new.inputTxtBox = new DB_Dialogs.InputData("", "");
                break;

            case "old-tavern-stories-new-haven-1":
                _new.name = "Bartender";
                _new.desc = "Well, this city is part of the nation of Victoria, as you probably already know.";
                _new.portImg = "npc-man-2";
                _new.isTweenOut = "0";
                _new.inputEmptyContinue = "old-tavern-stories-new-haven-2";
                _new.isMini = "0";
                _new.inputScaler_enable = "0";
                _new.posX = 0;
                _new.posY = 0;
                _new.input1 = new DB_Dialogs.InputData("", "");
                _new.input2 = new DB_Dialogs.InputData("", "");
                _new.input3 = new DB_Dialogs.InputData("", "");
                _new.input4 = new DB_Dialogs.InputData("", "");
                _new.inputTxtBox = new DB_Dialogs.InputData("", "");
                break;
            case "old-tavern-stories-new-haven-2":
                _new.name = "Bartender";
                _new.desc = "With the tension with Rhode Island in the west, most Victorian troops here have been moved to the border, so the millitary don't have a strong presence here nowadays.";
                _new.portImg = "npc-man-2";
                _new.isTweenOut = "0";
                _new.inputEmptyContinue = "old-tavern-stories-new-haven-3";
                _new.isMini = "0";
                _new.inputScaler_enable = "0";
                _new.posX = 0;
                _new.posY = 0;
                _new.input1 = new DB_Dialogs.InputData("", "");
                _new.input2 = new DB_Dialogs.InputData("", "");
                _new.input3 = new DB_Dialogs.InputData("", "");
                _new.input4 = new DB_Dialogs.InputData("", "");
                _new.inputTxtBox = new DB_Dialogs.InputData("", "");
                break;
            case "old-tavern-stories-new-haven-3":
                _new.name = "Bartender";
                _new.desc = "We sure could use more policing in this place, especially these days where the 4 biggest crime families are eager to start a war with each other.";
                _new.portImg = "npc-man-2";
                _new.isTweenOut = "0";
                _new.inputEmptyContinue = "old-tavern-stories-new-haven-4";
                _new.isMini = "0";
                _new.inputScaler_enable = "0";
                _new.posX = 0;
                _new.posY = 0;
                _new.input1 = new DB_Dialogs.InputData("", "");
                _new.input2 = new DB_Dialogs.InputData("", "");
                _new.input3 = new DB_Dialogs.InputData("", "");
                _new.input4 = new DB_Dialogs.InputData("", "");
                _new.inputTxtBox = new DB_Dialogs.InputData("", "");
                break;
            case "old-tavern-stories-new-haven-4":
                _new.name = "Bartender";
                _new.desc = "I tell you, I've been in this city for over 40 years and the families have not gotten this close to war as these days.";
                _new.portImg = "npc-man-2";
                _new.isTweenOut = "0";
                _new.inputEmptyContinue = "old-tavern-stories-end";
                _new.isMini = "0";
                _new.inputScaler_enable = "0";
                _new.posX = 0;
                _new.posY = 0;
                _new.input1 = new DB_Dialogs.InputData("", "");
                _new.input2 = new DB_Dialogs.InputData("", "");
                _new.input3 = new DB_Dialogs.InputData("", "");
                _new.input4 = new DB_Dialogs.InputData("", "");
                _new.inputTxtBox = new DB_Dialogs.InputData("", "");
                break;

            case "old-tavern-stories-4-families-1":
                _new.name = "Bartender";
                _new.desc = "They're the top four strongest crime organization in New Haven.";
                _new.portImg = "npc-man-2";
                _new.isTweenOut = "0";
                _new.inputEmptyContinue = "old-tavern-stories-4-families-2";
                _new.isMini = "0";
                _new.inputScaler_enable = "0";
                _new.posX = 0;
                _new.posY = 0;
                _new.input1 = new DB_Dialogs.InputData("", "");
                _new.input2 = new DB_Dialogs.InputData("", "");
                _new.input3 = new DB_Dialogs.InputData("", "");
                _new.input4 = new DB_Dialogs.InputData("", "");
                _new.inputTxtBox = new DB_Dialogs.InputData("", "");
                break;
            case "old-tavern-stories-4-families-2":
                _new.name = "Bartender";
                _new.desc = "Word is, three of these families have been doing business with the New York Triads to sell their narcotics here in New Haven.";
                _new.portImg = "npc-man-2";
                _new.isTweenOut = "0";
                _new.inputEmptyContinue = "old-tavern-stories-4-families-3";
                _new.isMini = "0";
                _new.inputScaler_enable = "0";
                _new.posX = 0;
                _new.posY = 0;
                _new.input1 = new DB_Dialogs.InputData("", "");
                _new.input2 = new DB_Dialogs.InputData("", "");
                _new.input3 = new DB_Dialogs.InputData("", "");
                _new.input4 = new DB_Dialogs.InputData("", "");
                _new.inputTxtBox = new DB_Dialogs.InputData("", "");
                break;
            case "old-tavern-stories-4-families-3":
                _new.name = "Bartender";
                _new.desc = "The Luppino Family don't like narcotics, or getting involved with the Triads, so Boss Vincenzo is standing his ground against the other three families.";
                _new.portImg = "npc-man-2";
                _new.isTweenOut = "0";
                _new.inputEmptyContinue = "old-tavern-stories-4-families-4";
                _new.isMini = "0";
                _new.inputScaler_enable = "0";
                _new.posX = 0;
                _new.posY = 0;
                _new.input1 = new DB_Dialogs.InputData("", "");
                _new.input2 = new DB_Dialogs.InputData("", "");
                _new.input3 = new DB_Dialogs.InputData("", "");
                _new.input4 = new DB_Dialogs.InputData("", "");
                _new.inputTxtBox = new DB_Dialogs.InputData("", "");
                break;
            case "old-tavern-stories-4-families-4":
                _new.name = "Bartender";
                _new.desc = "There might be a gang war coming if this keeps up. And let me tell you, The Luppino Family is not backing down that easily.";
                _new.portImg = "npc-man-2";
                _new.isTweenOut = "0";
                _new.inputEmptyContinue = "old-tavern-stories-end";
                _new.isMini = "0";
                _new.inputScaler_enable = "0";
                _new.posX = 0;
                _new.posY = 0;
                _new.input1 = new DB_Dialogs.InputData("", "");
                _new.input2 = new DB_Dialogs.InputData("", "");
                _new.input3 = new DB_Dialogs.InputData("", "");
                _new.input4 = new DB_Dialogs.InputData("", "");
                _new.inputTxtBox = new DB_Dialogs.InputData("", "");
                break;
        }
        
        return _new;
    }
}