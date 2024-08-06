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

	public List<Button> itemsBTN;
	public List<Inv2.Item> items;

	public int page, pageMax, ITEMS_PER_PAGE;

	public void setup (){
		page = 1;
		ITEMS_PER_PAGE = 8;
	}

	public void setup_page (){
		pageMax = Inv2.I.get_max_pages (ITEMS_PER_PAGE);
		items = Inv2.I.get_items_in_page (page, ITEMS_PER_PAGE);

		items.ForEach ((_item) => {
			Inv2_DB.ItemData _data = Inv2_DB.I.get_item_data (_item.name);
		});

		// Disable buttons with no items
		for (int _ind = items.Count; _ind < itemsBTN.Count; _ind++) {
			itemsBTN [_ind].image.sprite = Inv2_DB.I.i_none;
		}
	}

	public void btn_item (int _ind){

	}
}