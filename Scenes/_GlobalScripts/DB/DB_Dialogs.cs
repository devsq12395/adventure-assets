using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_Dialogs : MonoBehaviour {
    public static DB_Dialogs I;
    public void Awake() { 
        I = this;
    }

    public struct DialogData {
        public string name, desc, portImg, isTweenOut;
        public string inputEmptyContinue, isMini, inputScaler_enable;
        public int posX, posY;
        public InputData input1, input2, input3, input4, inputTxtBox;

        public DialogData(string _dialogId) {
            name = "";
            desc = "";
            portImg = "";
            isTweenOut = "0";
            inputEmptyContinue = "";
            isMini = "0";
            inputScaler_enable = "0";
            posX = 0;
            posY = 0;
            input1 = new InputData();
            input2 = new InputData();
            input3 = new InputData();
            input4 = new InputData();
            inputTxtBox = new InputData();
        }
    }

    public struct InputData {
        public string id;
        public string text;

        public InputData(string _id = "", string _text = "") {
            id = _id;
            text = _text;
        }
    }

    public DialogData get_dialog_data(string _dialogId) {
        DialogData _new = new DialogData(_dialogId);
        switch (_dialogId) {
            case "intro-1":
                _new.name = "Alfred";
                _new.desc = "We're finally here in New Haven, Tommy!";
                _new.portImg = "kazuma";
                _new.isTweenOut = "0";
                _new.inputEmptyContinue = "show-intro-2";
                _new.isMini = "0";
                _new.inputScaler_enable = "0";
                _new.posX = 0;
                _new.posY = 0;
                _new.input1 = new InputData("", "");
                _new.input2 = new InputData("", "");
                _new.input3 = new InputData("", "");
                _new.input4 = new InputData("", "");
                _new.inputTxtBox = new InputData("", "");
                break;

			case "dialog-area-locked":
			    _new.name = "Alfred";
			    _new.desc = "Let's explore this area first before moving.";
			    _new.portImg = "kazuma";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
				break;

           case "buy-not-enough-reqs":
			    _new.name = "";
			    _new.desc = "You lack the item requirements!";
			    _new.portImg = "";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "1";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("back", "Back");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "sell-success":
			    _new.name = "";
			    _new.desc = "Items sold!";
			    _new.portImg = "";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "1";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("back-to-inventory", "Back");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "the-old-tavern": case "the-old-tavern-close-stories":
			case "old-tavern-stories":
			case "old-tavern-stories-new-haven-1":case "old-tavern-stories-new-haven-2":case "old-tavern-stories-new-haven-3":case "old-tavern-stories-new-haven-4":
			case "old-tavern-stories-4-families-1":case "old-tavern-stories-4-families-2":case "old-tavern-stories-4-families-3":case "old-tavern-stories-4-families-4":
			case "old-tavern-stories-end":
			    _new = DB_Dialogs_TheOldTavern.I.get_dialog_data (_dialogId, _new);
			    break;

			case "buy-success":
			    _new.name = "";
			    _new.desc = "Purchase complete!";
			    _new.portImg = "";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "1";

			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("back", "Back");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("=", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "wooster-square-house-1":
			    _new.name = "Old Man";
			    _new.desc = "Our nation of Victoria was founded by the great heroes who defeated the Demon Lord 150 years ago.";
			    _new.portImg = "npc-woman-1";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "wooster-square-house-2";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "dialog-vic-20":
			    _new.name = "Boss Vincenzo";
			    _new.desc = "I just want to say, you are now a part of Luppino Family. So if you need anything, we are always here.";
			    _new.portImg = "vincenzo";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "wooster-square-house-3":
			    _new.name = "Man";
			    _new.desc = "The Rossi Family is spotted here on Wooster Square recently, acting like they own the place.";
			    _new.portImg = "npc-man-1";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "wooster-square-house-4";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "wooster-square-house-2":
			    _new.name = "Old Man";
			    _new.desc = "We honor their legacy every day.";
			    _new.portImg = "npc-woman-1";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "wooster-square-house-5":
			    _new.name = "Woman";
			    _new.desc = "You don't look from around here. Oh, I know! You must be a student in New Yale University.";
			    _new.portImg = "npc-woman-3";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "wooster-square-house-4";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "wooster-square-house-4":
			    _new.name = "Man";
			    _new.desc = "I think a war between the families will erupt soon.";
			    _new.portImg = "npc-man-1";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "intro-2":
			    _new.name = "Alfred";
			    _new.desc = "We should look for my friend Vic. He's well connected and will help us settle in.";
			    _new.portImg = "kazuma";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("back", "Sounds Good");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "strega":
			    _new.name = "Woman";
			    _new.desc = "Welcome to Giovanni's crafting shop. How can I help?";
			    _new.portImg = "npc-woman-1";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("shopStregaBuy", "Buy");
			    _new.input2 = new InputData("shopCancel", "Nevermind");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "dialog-vic-2":
			    _new.name = "Vic";
			    _new.desc = "I recommended you guys to my boss, Mr. Vincenzo. But we'll have to see first if you have what it takes.";
			    _new.portImg = "vic";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "show-dialog-vic-3";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "dialog-vic-3":
			    _new.name = "Alfred";
			    _new.desc = "Tell us what we need to do and we'll do it, Vic.";
			    _new.portImg = "kazuma";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "show-dialog-vic-4";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "dialog-vic-1":
			    _new.name = "Vic";
			    _new.desc = "Alfred, my man, you're finally here! And you must be Tommy. Welcome to New Haven!";
			    _new.portImg = "vic";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("show-dialog-vic-2", "Nice to meet you!");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "dialog-vic-4":
			    _new.name = "Vic";
			    _new.desc = "There are reports of monsters spotted near our turf. Deal with them and show us how you two fight.";
			    _new.portImg = "vic";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("start-mission-vic-1", "Let's go!");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "dialog-vic-5":
			    _new.name = "Vic";
			    _new.desc = "Tommy, Alfred, there you are! Well done with beating the monster.";
			    _new.portImg = "vic";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "show-dialog-vic-6";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

		   case "dialog-vic-6":
			    _new.name = "Vic";
			    _new.desc = "You two should meet Boss Vincenzo at our base, the Luppino Diner. It's just a few meters north from here.";
			    _new.portImg = "vic";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

		   case "dialog-vincenzo-1":
			    _new.name = "Boss Vincenzo";
			    _new.desc = "You must be Tommy and Alfred. I heard amazing things about you from Vic.";
			    _new.portImg = "vincenzo";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "show-dialog-vincenzo-2";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "dialog-vincenzo-2":
			    _new.name = "Boss Vincenzo";
			    _new.desc = "I have a job for you if you are interested.";
			    _new.portImg = "vincenzo";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "show-dialog-vincenzo-3";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "dialog-vincenzo-3":
			    _new.name = "Boss Vincenzo";
			    _new.desc = "Some boys from the Rossi Family has been spotted trying to sell narcotics on our turf.";
			    _new.portImg = "vincenzo";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "show-dialog-vincenzo-4";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "dialog-vincenzo-4":
			    _new.name = "Boss Vincenzo";
			    _new.desc = "You see, we the Luppino Family want nothing to do with narcotics. And most of the other families do not like it.";
			    _new.portImg = "vincenzo";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "show-dialog-vincenzo-5";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "dialog-vincenzo-5":
			    _new.name = "Boss Vincenzo";
			    _new.desc = "If this tension between the families keeps up, there will be war soon. For now, let's show these intruders from the Rossi Family that this is our turf.";
			    _new.portImg = "vincenzo";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("start-mission-vincenzo-1", "Sure, Boss Vincenzo.");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "dialog-vincenzo-6":
			    _new.name = "Boss Vincenzo";
			    _new.desc = "Well done dealing with Rossi's boys. I will have a job for you soon. Feel free to explore around for now.";
			    _new.portImg = "vincenzo";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "dialog-field-1":
			    _new.name = "Gun-Arm Azar";
			    _new.desc = "I heard the vampire lady who cut my arm is spotted here. Once I see her, she's dead!";
			    _new.portImg = "azar";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "show-dialog-field-2";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "dialog-field-2":
			    _new.name = "Orc Henchman";
			    _new.desc = "We're with you, warchief! She will pay!";
			    _new.portImg = "orc-1";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "dialog-field-3":
			    _new.name = "Anastasia";
			    _new.desc = "There you are, Azar. I knew you were involved at the raid on New Yale last week.";
			    _new.portImg = "anastasia";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "show-dialog-field-4";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "dialog-field-4":
			    _new.name = "Anastasia";
			    _new.desc = "What is it you're after? And who are you working for this time?";
			    _new.portImg = "anastasia";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "show-dialog-field-5";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "dialog-field-5":
			    _new.name = "Gun-Arm Azar";
			    _new.desc = "Anastasia, the vampire lady. Today, you will pay for what you did to my arm! Get her boys!";
			    _new.portImg = "orc-1";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "show-dialog-field-4";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("start-mission-field-1", "Prepare to fight");
			    _new.input2 = new InputData("back", "Let's retreat");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "dialog-field-6":
			    _new.name = "Anastasia";
			    _new.desc = "Azar and the BloodAxe gang got away. No matter, they won't get that far.";
			    _new.portImg = "anastasia";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "recruit-sylphine-1":
			    _new.name = "Sylphine";
			    _new.desc = "Hi, I'm Sylphine. Student at New Yale University.";
			    _new.portImg = "sylphine";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "recruit-sylphine-2";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "recruit-sylphine-2":
			    _new.name = "Sylphine";
			    _new.desc = "I'm here at Wooster Square to do some research work. As you know, this place can be dangerous at times.";
			    _new.portImg = "sylphine";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "recruit-sylphine-3";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "recruit-sylphine-3":
			    _new.name = "Sylphine";
			    _new.desc = "If you're looking for a companion, I'm one of the best spellcasters in New Yale.";
			    _new.portImg = "sylphine";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("recruit-sylphine", "Let's see...");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "hero-recruited":
			    _new.name = "";
			    _new.desc = "This character is already recruited!";
			    _new.portImg = "";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "1";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("back", "Back");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "buy-craft-success":
			    _new.name = "";
			    _new.desc = "Crafting successful!";
			    _new.portImg = "";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "1";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("back", "Back");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "anthony-1":
			    _new.name = "Anthony";
			    _new.desc = "You must be Tommy, right? I heard a lot about you from Vic.";
			    _new.portImg = "anthony";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "show-dialog-anthony-2";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "anthony-2":
			    _new.name = "Anthony";
			    _new.desc = "I'm Anthony, the best car mechanic in New Haven and also loyal to the Luppino Family.";
			    _new.portImg = "anthony";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "show-dialog-anthony-3";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "anthony-3":
			    _new.name = "Anthony";
			    _new.desc = "We have some business that needs your help. The pay will be good, of course.";
			    _new.portImg = "anthony";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "show-dialog-anthony-4";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "anthony-4":
			    _new.name = "Anthony";
			    _new.desc = "Just help me clear out some War Shredders. They've been encroaching nearby leately, and it's bad for business.";
			    _new.portImg = "anthony";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("start-mission-anthony-1", "I'll handle it.");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "anthony-5":
			    _new.name = "Anthony";
			    _new.desc = "Tommy! There you are, my friend!";
			    _new.portImg = "anthony";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "show-dialog-anthony-6";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "anthony-6":
			    _new.name = "Anthony";
			    _new.desc = "Business is booming and it's all thanks to you. If you're having trouble with cars, come find me. Repairs are on the house, just for you!";
			    _new.portImg = "anthony";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "bryans-armory":
			    _new.name = "Bryan";
			    _new.desc = "We sell affordable weapons here.";
			    _new.portImg = "npc-man-1";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("shop-bryans-armory", "Buy");
			    _new.input2 = new InputData("shopCancel", "Nevermind");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "buy-not-enough-gold":
			    _new.name = "";
			    _new.desc = "Not enough gold!";
			    _new.portImg = "";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "1";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("back", "Back");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "change-equip-success":
			    _new.name = "";
			    _new.desc = "Item Equipped!";
			    _new.portImg = "";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "1";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("close-inventory-after-equip", "Back");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("close-inventory-after-equip", "Back");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "buy-recruit-success":
			    _new.name = "";
			    _new.desc = "Character is recruited! Set your members on the Character window.";
			    _new.portImg = "";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "1";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("back-craft-success", "Back");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "shopTest":
			    _new.name = "Test's shop";
			    _new.desc = "TestDesc";
			    _new.portImg = "tommy";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("shopTest01", "Buy");
			    _new.input2 = new InputData("shopCancel", "Nevermind");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "dialog-beatrice-1":
			    _new.name = "Captain Beatrice";
			    _new.desc = "You must be Tommy, I presume? Vincenzo told me that you will come.";
			    _new.portImg = "beatrice";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "show-dialog-beatrice-2";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "dialog-beatrice-2":
			    _new.name = "Captain Beatrice";
			    _new.desc = "I'm Captain Beatrice, commander of the Victorian Guards here in New Haven.";
			    _new.portImg = "beatrice";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "show-dialog-beatrice-3";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "dialog-beatrice-3":
			    _new.name = "Captain Beatrice";
			    _new.desc = "Me and Vincenzo share the same interest in fighting the spread of narcotics. That's why currently, we're on an uneasy alliance.";
			    _new.portImg = "beatrice";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "show-dialog-beatrice-4";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "dialog-beatrice-4":
			    _new.name = "Captain Beatrice";
			    _new.desc = "There are five powerful families here in New Haven, Vincenzo's Luppino Family being one of them.";
			    _new.portImg = "beatrice";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "show-dialog-beatrice-5";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "dialog-beatrice-5":
			    _new.name = "Captain Beatrice";
			    _new.desc = "The other four families are allied in their interests in the narcotics business. If they are left unchecked, they will dominate the city.";
			    _new.portImg = "beatrice";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "show-dialog-beatrice-6";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "dialog-beatrice-6":
			    _new.name = "Captain Beatrice";
			    _new.desc = "So if you are to fight with us, I will have to see what you are capable of.";
			    _new.portImg = "beatrice";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "show-dialog-beatrice-7";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "dialog-beatrice-7":
			    _new.name = "Captain Beatrice";
			    _new.desc = "Are you up to spar with me?";
			    _new.portImg = "beatrice";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("start-mission-beatrice-1", "Sure!");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "dialog-beatrice-8":
			    _new.name = "Captain Beatrice";
			    _new.desc = "I see that you are a capable fighter. The people of New Haven will be grateful if you will support us in the fight against narcotics.";
			    _new.portImg = "beatrice";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "dialog-vic-19":
			    _new.name = "Boss Vincenzo";
			    _new.desc = "Tommy, my boy! I hope you are faring well.";
			    _new.portImg = "vincenzo";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "show-dialog-vic-20";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "recruit-anastasia-1":
			    _new.name = "Anastasia";
			    _new.desc = "You must be the new travellers everyone in town is talking about.";
			    _new.portImg = "anastasia";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "recruit-anastasia-2";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "recruit-anastasia-2":
			    _new.name = "Anastasia";
			    _new.desc = "I'm Anastasia. Like you, I came from afar.";
			    _new.portImg = "anastasia";
			    _new.isTweenOut = "0";
			    _new.inputEmptyContinue = "recruit-anastasia-3";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("", "");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

			case "recruit-anastasia-3":
			    _new.name = "Anastasia";
			    _new.desc = "If you're looking for strong companions, I'm available. For a price, of course.";
			    _new.portImg = "anastasia";
			    _new.isTweenOut = "1";
			    _new.inputEmptyContinue = "";
			    _new.isMini = "0";
			    _new.inputScaler_enable = "0";
			    _new.posX = 0;
			    _new.posY = 0;
			    _new.input1 = new InputData("recruit-anastasia", "Let's see...");
			    _new.input2 = new InputData("", "");
			    _new.input3 = new InputData("", "");
			    _new.input4 = new InputData("", "");
			    _new.inputTxtBox = new InputData("", "");
			    break;

        }

        return _new;
    }
}
