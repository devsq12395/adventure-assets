
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MiniDialog : MonoBehaviour {

	public GameObject go;
	public TextMeshProUGUI tName, tDesc;
	public Image port;
	public List<Image> windows, windows_showOnTweenDone;
	public bool isTweenOut;

	public List<GameObject> inputs;

	public string ID, storage1, storage2, storage3, storage4, storage5;
	public string textToShow, textShowing, onContinueCallbackID, tweenOutCallbackID;

	private float TEXT_DELAY, textDelayCur;
	public bool tweenInText_default, tweenInText, onClickContinue, tweeningOut;

	void Start (){
		TEXT_DELAY = 0.01f;

		windows.ForEach ((_window) => _window.transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f));
		windows_showOnTweenDone.ForEach ((_window) => _window.transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f));

		if (!tweenInText_default) {
			textShowing = textToShow;
			tDesc.text = textShowing;
		}

		SoundHandler.I.play_sfx ("chat");
	}

	void Update (){
		if (textToShow.Length > textShowing.Length && tweenInText) {
			textDelayCur -= Time.deltaTime;

			if (textDelayCur <= 0) {
				textShowing += textToShow [textShowing.Length];
				textDelayCur = TEXT_DELAY;

				tDesc.text = textShowing;

				if (textShowing.Length >= textToShow.Length) {
					tweenInText = false;
					tween_in_options ();
				}
			}

		}

		if (Input.GetMouseButtonDown(0)) {
			if (onClickContinue) {
				// Texts are finished tweening, go to next phase

				tweenOutCallbackID = onContinueCallbackID;
				if (this.isTweenOut) {
					tween_out ();
				} else {
					tweeningOut = false;
					onClickContinue = false;
					on_tween_out_callback ();
				}
			} else if (textToShow.Length > textShowing.Length) {
				// Texts are not finished tweening, force it to finish

				textShowing = textToShow;
				tDesc.text = textShowing;
				tweenInText = false;
				tween_in_options ();
			}
		}
	}

	public MiniDialog_Input get_input_obj (string _inputName){
		return inputs.FirstOrDefault(input => input.GetComponent<MiniDialog_Input>().inputName == _inputName).GetComponent<MiniDialog_Input>();
	}

	public void tween_in (){
		windows.ForEach ((_window) => {
            _window.transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
            _window.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack).OnComplete(() => {
            	tweenInText = true;
            });
        });
	}

	private void tween_in_options (bool _skipTween = false){
		int _inputsOn = inputs.Aggregate (0, (acc, btn) => {
			if (btn.activeSelf) acc++;
			return acc;
		});

		onClickContinue = _inputsOn == 0;
		if (onClickContinue) {

		} else {
			windows_showOnTweenDone.ForEach ((_window) => {
				_window.gameObject.SetActive (true);
            	_window.transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
	            _window.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack).OnComplete(() => {
	            	tweenInText = true;
	            });
	        });
		}
	}

	public void tween_out (){
		windows.ForEach ((_window) => {
            _window.transform.DOScale(new Vector3 (0.8f, 0.8f, 0.8f), 0.2f).SetEase(Ease.InBack).OnComplete(() => on_tween_out_callback ());
        });
        windows_showOnTweenDone.ForEach ((_window) => _window.transform.DOScale(new Vector3 (0.8f, 0.8f, 0.8f), 0.2f).SetEase(Ease.InBack));
	}

	public void on_tween_out_callback (){ 
		if (tweeningOut) return;

		tweeningOut = true;
		bool _destroyGo = this.isTweenOut;
		
		MMCont_Dialog.I.input (this, tweenOutCallbackID);

		if (_destroyGo && go != null) Destroy (go);
	}
}