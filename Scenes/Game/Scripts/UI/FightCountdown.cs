using System.Collections;
using UnityEngine;
using TMPro;  // Import TextMeshPro namespace
using DG.Tweening;  // Import DOTween namespace

public class FightCountdown : MonoBehaviour {
    public static FightCountdown I;
    public void Awake(){ I = this; }

    public GameObject go;
    public TextMeshProUGUI countdownText;  // Reference to the TextMeshProUGUI component
    private int countdownValue = 3;  // Starting countdown value
    
    public bool countdownComplete;

    public void setup (){
        go.SetActive (false);
    }

    public void start_count()
    {
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        go.SetActive (true);

        while (countdownValue > 0)
        {
            // Display current countdown value
            countdownText.text = countdownValue.ToString();
            
            // Reset scale and alpha
            countdownText.transform.localScale = Vector3.one;
            countdownText.alpha = 1;

            // Scale and fade effects using DOTween
            countdownText.transform.DOScale(1.5f, 0.5f).SetEase(Ease.OutBack);
            countdownText.DOFade(0, 0.5f).SetEase(Ease.InExpo);

            // Wait for 1 second
            yield return new WaitForSeconds(1f);

            // Decrement the countdown value
            countdownValue--;
        }

        // Show "GO!" text
        countdownText.text = "GO!";
        
        // Reset scale and alpha
        countdownText.transform.localScale = Vector3.one;
        countdownText.alpha = 1;

        // Scale and fade effects for "GO!"
        countdownText.transform.DOScale(1.5f, 0.5f).SetEase(Ease.OutBack);
        countdownText.DOFade(0, 0.5f).SetEase(Ease.InExpo);

        // Wait for a moment before completing
        yield return new WaitForSeconds(1f);

        // Call the function to start the game or any other action
        OnCountdownComplete();
        ContEnemies.I.spawn_enemies ();
    }

    private void OnCountdownComplete()
    {
        go.SetActive (false);
        countdownComplete = true;
    }
}
