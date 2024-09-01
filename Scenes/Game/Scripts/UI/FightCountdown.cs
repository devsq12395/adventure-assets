using System.Collections;
using UnityEngine;
using TMPro;  // Import TextMeshPro namespace
using DG.Tweening;  // Import DOTween namespace

public class FightCountdown : MonoBehaviour {
    public static FightCountdown I;
    public void Awake() { I = this; }

    public GameObject go;
    public TextMeshProUGUI txtCountdown, txtSub;
    private int countdownValue = 3;

    public bool countdownComplete;

    public string mode; // start, end

    public void setup() {
        go.SetActive(false);
    }

    public void start_count(string _mode) {
        mode = _mode;
        countdownComplete = false;

        switch (_mode) {
            case "start": txtSub.text = ""; break;
            case "end": txtSub.text = "Next Area In"; break;
        }

        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown() {
        go.SetActive(true);
        countdownValue = 3; // Reset countdown value if needed

        while (countdownValue > 0) {
            // Display current countdown value
            txtCountdown.text = countdownValue.ToString();

            // Reset scale and alpha
            txtCountdown.transform.localScale = Vector3.one;
            txtCountdown.alpha = 1;

            // Scale and fade effects using DOTween
            txtCountdown.transform.DOScale(1.5f, 0.5f).SetEase(Ease.OutBack);
            txtCountdown.DOFade(0, 0.5f).SetEase(Ease.InExpo);

            // Wait for 1 second
            yield return new WaitForSeconds(1f);

            // Decrement the countdown value
            countdownValue--;
        }

        // Start the coroutine for OnCountComplete
        StartCoroutine(OnCountComplete());
    }

    private IEnumerator OnCountComplete() {
        if (mode == "start") {
            // Show "GO!" text
            txtCountdown.text = "GO!";

            // Reset scale and alpha
            txtCountdown.transform.localScale = Vector3.one;
            txtCountdown.alpha = 1;

            // Scale and fade effects for "GO!"
            txtCountdown.transform.DOScale(1.5f, 0.5f).SetEase(Ease.OutBack);
            txtCountdown.DOFade(0, 0.5f).SetEase(Ease.InExpo);

            // Wait for a moment before completing
            yield return new WaitForSeconds(1f);

            // Call the function to start the game or any other action
            OnOneMoreSecond();
        } else if (mode == "end") {
            Transition_Game.I.change_state("toNextMap");
        }
    }

    private void OnOneMoreSecond() {
        go.SetActive(false);
        countdownComplete = true;

        ContEnemies.I.spawn_enemies();

        if (PlayerPrefs.GetInt("combat-tut-state") == 0) {
            MUI_Tutorial.I.show("move");
        }
    }
}
