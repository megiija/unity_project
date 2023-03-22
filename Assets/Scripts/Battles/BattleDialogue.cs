using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] int textSpeed;

    [SerializeField] GameObject actionSelect;
    [SerializeField] GameObject moveSelect;
    [SerializeField] GameObject moveInfo;

    [SerializeField] List<TextMeshProUGUI> actionText;
    [SerializeField] List<TextMeshProUGUI> moveText;
    [SerializeField] TextMeshProUGUI ppText;
    [SerializeField] TextMeshProUGUI typeText;

    public void SetText(string dialogue)
    {
        dialogueText.text = dialogue;
    }

    public IEnumerator TypeDialogue(string dialogue)
    {
        dialogueText.text = "";
        foreach (var i in dialogue.ToCharArray())
        {
            dialogueText.text += i;
            yield return new WaitForSeconds(1f / textSpeed);
        }
    }

    //enable/disable options
    public void EnableDialogue(bool enabled)
    {
        dialogueText.enabled = enabled;
    }

    public void EnableActions(bool enabled)
    {
        actionSelect.SetActive(enabled);
    }

    public void EnableMoveSelect(bool enabled)
    {
        moveSelect.SetActive(enabled);
        moveInfo.SetActive(enabled);
    }

    public void UpdateMoves(Move move)
    {
        ppText.text = $"PP:  {move.PP}/{move.Base.PP}";
        typeText.text = $"Type:  {move.Base.Type}";
    }


    public void setMoves(List<Move> moves, GameObject move1, GameObject move2, GameObject move3)
    {
        for (int i=0; i < moves.Count; i++)
        {
            if (i < moves.Count)
                moveText[i].text = moves[i].Base.name;
            else
            {
                moveText[i].text = "-";
            }
        }

        if (moveText[1].text == "-")
            move1.SetActive(false);
        if (moveText[2].text == "-")
            move2.SetActive(false);
        if (moveText[3].text == "-")
            move3.SetActive(false);
    }
}
