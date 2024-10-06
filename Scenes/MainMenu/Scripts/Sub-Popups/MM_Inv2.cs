using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MM_Inv2 : MonoBehaviour {
    public static MM_Inv2 I;
	public void Awake(){ I = this; }

	public GameObject go, goBtnAction;
	public CanvasGroup canvasGroup; // Add this for controlling alpha
	public RectTransform imgWindow, imgWindow_itemInfo; // Add this for controlling the y position

	public Image img_window1, img_window2;
	public Sprite spr_invWin1, spr_invWin2, spr_shopWin1, spr_shopWin2;

	public Image i_item;
	public TextMeshProUGUI t_title, t_itemName, t_itemDetails, t_itemDesc, t_gold, t_page, t_action;

	public List<Button> itemsBTN;
	public List<Inv2.Item> itemsShow, itemsSet;

	public int page, pageMax, ITEMS_PER_PAGE;

	public Inv2.Item itemSel;
	public string itemSet, mode;

	private bool isItemInfoShow;

	public void setup (){
		page = 1;
		ITEMS_PER_PAGE = 8;

		itemsSet = new List<Inv2.Item>();
	}

	public void show (string _itemSetMode){
        go.SetActive (true);

        canvasGroup.alpha = 0f;
        imgWindow.anchoredPosition = new Vector2(-300f, 0);

        imgWindow.DOAnchorPosX(263.7f, 0.2f).SetEase(Ease.OutSine);
        canvasGroup.DOFade(1f, 0.2f);

        imgWindow_itemInfo.anchoredPosition = new Vector2(300f, 0);
        isItemInfoShow = false;

        mode = _itemSetMode;
        itemsSet.Clear ();
        switch (_itemSetMode) {
        	case "inventory": 
        		itemsSet = Inv2.I.items; 
        		img_window1.sprite = spr_invWin1;
        		img_window2.sprite = spr_invWin2;

        		goBtnAction.SetActive (false);
        		break;
        	case "equip": 
        		itemsSet = Inv2.I.get_items_with_tag ( itemSet );
        		img_window1.sprite = spr_invWin1;
        		img_window2.sprite = spr_invWin2;

        		goBtnAction.SetActive (true);
        		t_action.text = "Equip";
        		break;
        	case "shop": 
        		itemsSet = Inv2.I.generate_item_set ( Inv2_DB.I.get_shop_items_sold (itemSet) );
        		img_window1.sprite = spr_shopWin1;
        		img_window2.sprite = spr_shopWin2;

        		goBtnAction.SetActive (true);
        		t_action.text = "Buy";
        		break;
        }

        int _gold = ZPlayerPrefs.GetInt ("gold");
        t_gold.text = $"Your Gold: {_gold}";

        itemSel = new Inv2.Item ("empty", 0, 0, "");
        setup_page ();
        change_item_sel (itemSel);

        pageMax = Inv2.I.get_max_pages (ITEMS_PER_PAGE);
        t_page.text = $"{page}/{pageMax}";
    }

    public void hide (){
    	imgWindow.DOAnchorPosX(-300f, 0.2f).SetEase(Ease.OutSine);
    	imgWindow_itemInfo.DOAnchorPosX(300f, 0.2f).SetEase(Ease.OutSine);
        canvasGroup.DOFade(0f, 0.2f).OnComplete(() => go.SetActive(false));
    }

	public void setup_page() {
	    stop_blink_animations ();

	    itemsShow = Inv2.I.get_items_in_page(page, ITEMS_PER_PAGE, itemsSet);

	    int _curInd = 0;
	    itemsShow.ForEach((_item) => {
	        Inv2_DB.ItemData _data = Inv2_DB.I.get_item_data(_item.name);
	        itemsBTN[_curInd].image.sprite = _data.sprite;
	        _curInd++;
	    });

	    // Disable buttons with no items
	    for (int _ind = itemsShow.Count; _ind < itemsBTN.Count; _ind++) {
	        itemsBTN[_ind].image.sprite = Sprites.I.get_sprite("empty");
	    }

	    t_page.text = $"{page}/{pageMax}";

	    // Re-apply the blink effect to the selected item, if it is on the current page
	    if (!itemSel.Equals(default(Inv2.Item)) && itemsShow.Contains(itemSel)) {
	        int index = itemsShow.IndexOf(itemSel);
	        itemsBTN[index].image.DOColor(Color.blue, 0.5f).SetLoops(-1, LoopType.Yoyo);
	    }
	}


	public void btn_change_page (int _inc){
		page = (page + _inc + pageMax) % pageMax;
    	if (page == 0) page = pageMax;
		setup_page ();
	}

	public void btn_item (int _index){
		if (_index < itemsShow.Count) {
			change_item_sel (itemsShow [_index]);
		}
	}

	public void change_item_sel(Inv2.Item _item) {
		itemSel = _item;

	    if (_item.name == "empty") {
	    	// Clear out UI on first open - This section will be used on first open only
	        t_itemName.text = "";
	        t_itemDetails.text = "";
	        t_itemDesc.text = "";
	        i_item.sprite = Sprites.I.get_sprite("empty");

	        stop_blink_animations ();
	    } else {
	    	if (!isItemInfoShow){
	    		isItemInfoShow = true;
	    		imgWindow_itemInfo.DOAnchorPosX(-266.3f, 0.2f).SetEase(Ease.OutSine);
	    	}

	        Inv2_DB.ItemData _data = Inv2_DB.I.get_item_data(_item.name);

	        t_itemName.text = _data.nameUI;
	        t_itemDetails.text = _data.details;
	        t_itemDesc.text = _data.desc;
	        i_item.sprite = _data.sprite;

	        stop_blink_animations ();

	        // Start the blink animation for the selected item
	        int index = itemsShow.IndexOf(_item);
	        if (index >= 0 && index < itemsBTN.Count) {
	            itemsBTN[index].image.DOColor(Color.blue, 0.5f).SetLoops(-1, LoopType.Yoyo);
	        }
	    }
	}

	public void update_gold (int _newGold){
		if (go.activeSelf) {
			t_gold.text = $"Your Gold: {_newGold}";
		}
	}

	private void stop_blink_animations (){
        itemsBTN.ForEach(btn => {
        	btn.image.color = Color.white;
        	btn.image.DOKill();
        });
	}

	public void btn_action (){
		if (itemSel.name == "empty") return;

		int gold = ZPlayerPrefs.GetInt("gold");
		Inv2_DB.ItemData _itemInfo = Inv2_DB.I.get_item_data (itemSel.name);

		switch (mode){
			case "equip":
				Inv2.I.set_item_equip (itemSel, MM_Char.I.curChar);
				MMCont_Dialog.I.create_dialog ("change-equip-success");
				break;
			case "buy": case "shop":
				if (gold >= _itemInfo.cost) {
					Inv2.I.add_item (itemSel.name);
					MainMenu.I.update_gold (-_itemInfo.cost);
					
					MMCont_Dialog.I.create_dialog ("buy-success");
				} else {
					MMCont_Dialog.I.create_dialog ("buy-not-enough-gold");
				}
				break;
		}
	}
}
