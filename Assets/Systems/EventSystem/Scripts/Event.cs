using UnityEngine;
using DG.Tweening;

public class Event : MonoBehaviour
{
    [Header("Show Text Variables")]
    [SerializeField] string[] eventTextArray;
    [SerializeField] float textDisplayTime = 1f;

    private EventManager eventManager;

    private void OnEnable()
    {
        ShowTextsByTime();
    }

    private int currentTextIndex = 0;
    private void ShowTextsByTime()
    {
        if (eventTextArray.Length == 0)
            return;

        ShowTextDelayed(currentTextIndex);
    }

    private void ShowTextDelayed(int index)
    {
        if (index < 0 || index >= eventTextArray.Length)
            return;

        eventManager.eventTMPro.text = eventTextArray[index];

        currentTextIndex++;
        if (currentTextIndex < eventTextArray.Length)
        {
            DOTween.Sequence()
                .AppendInterval(textDisplayTime)
                .OnComplete(() => ShowTextDelayed(currentTextIndex));
        }
    }

    public void Init(EventManager eventManager)
    {
        this.eventManager = eventManager;
    }
}
