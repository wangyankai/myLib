using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UI.UIComponent.ScrollList;

public class ElementButton : SkyElementBase
{

    public override void Init (int index, SkyScrollPanel mySkyScrollPanel)
    {
        base.Init (index, mySkyScrollPanel);
        gameObject.name = "ElementButton" + index;
        Button b = gameObject.transform.parent.Find (gameObject.name).GetComponent<Button> ();
        if (((ElementButtonConfig)(MySkyScrollPanel.Config)).ConfigInfs [index].sprite == null)
            gameObject.transform.Find ("Text").GetComponent<Text> ().text = ((ElementButtonConfig)(MySkyScrollPanel.Config)).ConfigInfs [index].desc;
        else
            b.image.sprite = ((ElementButtonConfig)(MySkyScrollPanel.Config)).ConfigInfs [index].sprite;
        b.onClick.AddListener (() => ElementButtonConfig.ChoiseEvent (((ElementButtonConfig)(MySkyScrollPanel.Config)).ConfigInfs [index].buttonConfigType));
    }
}
