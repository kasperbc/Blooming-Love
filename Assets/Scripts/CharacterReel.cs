using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterReel : MonoBehaviour
{
    [SerializeField]
    private int itemCount = 7;

    private float xPerItem;
    int index = 0;

    float xPos = 0;
    RectTransform rectTransform;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        xPerItem = rectTransform.rect.width / itemCount;
    }

    void Update()
    {
        xPos = Mathf.Lerp(xPos, -xPerItem * index - (rectTransform.rect.width / (itemCount * 2)), 0.01f);

        rectTransform.localPosition = new Vector3(xPos, 0, 0);
    }

    public void NextSlide()
    {
        index++;
        index = Mathf.Clamp(index, 0, itemCount - 1);
    }

    public void PrevSlide() 
    {
        index--;
        index = Mathf.Clamp(index, 0, itemCount - 1);
    }
}
