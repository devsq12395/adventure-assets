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

				case "marcos-tavern":
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
				    _new.input1 = new InputData("", "");
				    _new.input2 = new InputData("", "");
				    _new.input3 = new InputData("", "");
				    _new.input4 = new InputData("back-to-inventory", "Back");
				    _new.inputTxtBox = new InputData("", "");
				    break;

				case "wooster-square-house-1":
				    _new.name = "Old Man";
				    _new.desc = "Our nation of Victoria was founded by the great heroes who defeated the Demon Lord 150 years ago.";
				    _new.portImg = "npc-man-2";
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
				    _new.name = "Woman";
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
				    _new.portImg = "npc-man-2";
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
				    _new.name = "Woman";
				    _new.desc = "I think a war between the families will erupt soon.";
				    _new.portImg = "npc-woman-2";
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
				    _new.desc = "Welcome to Luppino House, the base of operations for the Luppino Family.";
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
				    _new.name = "Vic";
				    _new.desc = "I have a job for you two if you are interested.";
				    _new.portImg = "vic";
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
				    _new.desc = "Alfred, my man, you're finally here! And you must be Tommy.";
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

				case "dialog-vic-6":
				    _new.name = "Vic";
				    _new.desc = "Luca the Terror, one of the muscles of the Rossi Family, is spotted on our turf with some of his men, selling narcotics.";
				    _new.portImg = "vic";
				    _new.isTweenOut = "0";
				    _new.inputEmptyContinue = "show-dialog-vic-7";
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

				case "dialog-vic-7":
				    _new.name = "Alfred";
				    _new.desc = "Finally some action! Tell us what to do, Vic.";
				    _new.portImg = "kazuma";
				    _new.isTweenOut = "0";
				    _new.inputEmptyContinue = "show-dialog-vic-8";
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

				case "dialog-vic-4":
				    _new.name = "Vic";
				    _new.desc = "There are monsters spotted near our turf. Deal with them and show us how you two fight.";
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
				    _new.desc = "Tommy, Alfred, there you are! I have an urgent job for you.";
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

				case "dialog-vic-8":
				    _new.name = "Vic";
				    _new.desc = "Your goal is to beat down Luca and his boys and make sure they never come back.";
				    _new.portImg = "vic";
				    _new.isTweenOut = "0";
				    _new.inputEmptyContinue = "show-dialog-vic-9";
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

				case "dialog-vic-9":
				    _new.name = "Vic";
				    _new.desc = "Boss Vincenzo wants you to show that this is our territory and no one crosses the Luppino family.";
				    _new.portImg = "vic";
				    _new.isTweenOut = "1";
				    _new.inputEmptyContinue = "";
				    _new.isMini = "0";
				    _new.inputScaler_enable = "0";
				    _new.posX = 0;
				    _new.posY = 0;
				    _new.input1 = new InputData("start-mission-vic-2", "Let's go!");
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
				    _new.input1 = new InputData("", "");
				    _new.input2 = new InputData("", "");
				    _new.input3 = new InputData("", "");
				    _new.input4 = new InputData("back", "Back");
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

				case "bella-vita":
				    _new.name = "Old Woman";
				    _new.desc = "We have some scraps and crafting essentials for sale here.";
				    _new.portImg = "npc-woman-3";
				    _new.isTweenOut = "1";
				    _new.inputEmptyContinue = "";
				    _new.isMini = "0";
				    _new.inputScaler_enable = "0";
				    _new.posX = 0;
				    _new.posY = 0;
				    _new.input1 = new InputData("shop-bella-vita", "Buy");
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
				    _new.input1 = new InputData("", "");
				    _new.input2 = new InputData("", "");
				    _new.input3 = new InputData("", "");
				    _new.input4 = new InputData("back", "Back");
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
				    _new.input1 = new InputData("", "");
				    _new.input2 = new InputData("", "");
				    _new.input3 = new InputData("", "");
				    _new.input4 = new InputData("back-craft-success", "Back");
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

				case "dialog-vic-10":
				    _new.name = "Vic";
				    _new.desc = "Tommy, welcome back! Our associate from the Victorian Guards, Captain Beatrice, wants to see you.";
				    _new.portImg = "vic";
				    _new.isTweenOut = "0";
				    _new.inputEmptyContinue = "show-dialog-vic-11";
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

				case "dialog-vic-11":
				    _new.name = "Captain Beatrice";
				    _new.desc = "You must be Vincenzo's new boys. I'm Captain Beatrice, commander of the Victorian Guards here in New Haven.";
				    _new.portImg = "beatrice";
				    _new.isTweenOut = "0";
				    _new.inputEmptyContinue = "show-dialog-vic-12";
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

				case "dialog-vic-12":
				    _new.name = "Captain Beatrice";
				    _new.desc = "Me and Vincenzo shares the same interest in fighting the spread of narcotics.";
				    _new.portImg = "beatrice";
				    _new.isTweenOut = "0";
				    _new.inputEmptyContinue = "show-dialog-vic-13";
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

				case "dialog-vic-13":
				    _new.name = "Captain Beatrice";
				    _new.desc = "There are five powerful families here in New Haven, Luppino Family being one of them.";
				    _new.portImg = "beatrice";
				    _new.isTweenOut = "0";
				    _new.inputEmptyContinue = "show-dialog-vic-14";
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

				case "dialog-vic-14":
				    _new.name = "Captain Beatrice";
				    _new.desc = "The other four families are allied in their interests in the narcotics business. If they are left unchecked, they will dominate the city.";
				    _new.portImg = "beatrice";
				    _new.isTweenOut = "0";
				    _new.inputEmptyContinue = "show-dialog-vic-17";
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

				case "dialog-vic-15":
				    _new.name = "Boss Vincenzo";
				    _new.desc = "UNUSED";
				    _new.portImg = "vincenzo";
				    _new.isTweenOut = "0";
				    _new.inputEmptyContinue = "show-dialog-vic-16";
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

				case "dialog-vic-16":
				    _new.name = "Boss Vincenzo";
				    _new.desc = "UNUSED";
				    _new.portImg = "vincenzo";
				    _new.isTweenOut = "0";
				    _new.inputEmptyContinue = "show-dialog-vic-17";
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

				case "dialog-vic-17":
				    _new.name = "Captain Beatrice";
				    _new.desc = "So if you are to fight with us, I will have to see what you are capable of.";
				    _new.portImg = "beatrice";
				    _new.isTweenOut = "0";
				    _new.inputEmptyContinue = "show-dialog-vic-18";
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

				case "dialog-vic-18":
				    _new.name = "Captain Beatrice";
				    _new.desc = "Are you up to spar with me?";
				    _new.portImg = "beatrice";
				    _new.isTweenOut = "1";
				    _new.inputEmptyContinue = "";
				    _new.isMini = "0";
				    _new.inputScaler_enable = "0";
				    _new.posX = 0;
				    _new.posY = 0;
				    _new.input1 = new InputData("start-mission-vic-3", "Sure!");
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
