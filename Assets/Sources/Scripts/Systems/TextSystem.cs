using Leopotam.Ecs;
using UnityEngine;
using Client;
using UnityEngine.Assertions.Must;

sealed class TextSystem : IEcsRunSystem
{
    // auto-injected fields.
    readonly EcsWorld world = null;
    readonly EcsFilter<TextBlockComponent> textBlockFilter = null;
    readonly EcsFilter<SetTextComponent> setTextFilter = null;


    void IEcsRunSystem.Run()
    {

        foreach (var i in textBlockFilter)
        {
            ref var com = ref textBlockFilter.Get1(i);

            if (!com.GameObject.activeSelf) { continue; }

            if (com.TypedWords == com.TypingWords.Length)
            {
                com.SpandedSwitchOffTime += Time.deltaTime;
                Debug.Log(1 + (int)com.SpandedSwitchOffTime);
                if (Input.GetKeyDown(KeyCode.Space) || com.SpandedSwitchOffTime > com.SwitchOffTime)
                {
                    com.SpandedSwitchOffTime = 0f;
                    com.GameObject.SetActive(false);
                }
                continue;

            }
            if (com.TypeLatterSpandedTime < com.TypingLatterTime)
            {
                com.TypeLatterSpandedTime += Time.deltaTime;
            }
            else
            {
                com.TypeLatterSpandedTime = 0f;
                com.Text.text += com.TypingWords[com.TypedWords] + " ";
                com.TypedWords++;

            }
        }


        foreach (var i in setTextFilter)
        {
            ref var com = ref setTextFilter.Get1(i);

            var freeTextBlockIndex = GetFreeTextBlockIndex(out bool thereIsBlock);
            if (thereIsBlock)
            {
                ref var textBlock = ref textBlockFilter.Get1(freeTextBlockIndex);

                textBlock.Text.text = "";
                textBlock.TypingWords = com.Text.Split(' ');
                textBlock.TypingLatterTime = com.TypingLatterTime;
                textBlock.TypedWords = 0;
                textBlock.SwitchOffTime = com.SwitchOffTime;
                // Set Block
                CountText(com.Text, out int maxLattersOnLine, out int linesCount);
                textBlock.Form.size = new Vector2(textBlock.WidthStart + textBlock.WidthStep * (maxLattersOnLine + 1), textBlock.HeightStart + textBlock.HeightStep * (linesCount - 1));
                textBlock.GameObject.SetActive(true);
                Vector2 posButtomTag;
                posButtomTag = new Vector2(textBlock.Form.size.x * com.ButtomTagOffset, -textBlock.Form.size.y);
                textBlock.ButtomTag.anchoredPosition = posButtomTag;
                textBlock.GameObject.transform.parent = com.Taraget;
                textBlock.GameObject.transform.localPosition = -new Vector3(textBlock.ButtomTag.anchoredPosition.x, textBlock.ButtomTag.anchoredPosition.y) + textBlock.StartOffset + com.Offcet;
                setTextFilter.GetEntity(i).Destroy();
            }
            else
            {
                break;
            }
        }
    }

    private void CountText(string text, out int maxLattersOnLine, out int linesCount)
    {
        var splittedText = text.Split('\n');
        linesCount = splittedText.Length;
        int max = 0;
        foreach (var item in splittedText)
        {
            int length = item.Length;
            if (length > max)
            {
                max = length;
            }
        }
        maxLattersOnLine = max;
    }

    private int GetFreeTextBlockIndex(out bool thereIsBlock)
    {
        foreach (var i in textBlockFilter)
        {
            ref var com = ref textBlockFilter.Get1(i);
            if (!com.GameObject.activeSelf)
            {
                thereIsBlock = true;
                return i;

            }
        }
        thereIsBlock = false;
        return -1;
    }
}