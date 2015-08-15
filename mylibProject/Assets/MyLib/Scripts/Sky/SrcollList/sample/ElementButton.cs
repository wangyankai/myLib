using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UI.UIComponent.ScrollList;

public class ElementButton : SkyElementBase
{
	private Text mytext;
	private Button b;
	private TimeRecord timeRecord;
	bool isSpecial = false;

	public override void Init (int index, SkyScrollPanel mySkyScrollPanel)
	{
		base.Init (index, mySkyScrollPanel);
		ElementButtonConfig config = ((ElementButtonConfig)(MySkyScrollPanel.Config));
		gameObject.name = "ElementButton" + index;
		b = gameObject.transform.parent.Find (gameObject.name).GetComponent<Button> ();
		mytext = gameObject.transform.Find ("Text").GetComponent<Text> ();
		isSpecial = config.ConfigInfs [index].IsSpecial;
		if (((ElementButtonConfig)(MySkyScrollPanel.Config)).ConfigInfs [index].sprite == null)
			mytext.text = config.ConfigInfs [index].desc;
		else
			b.image.sprite = config.ConfigInfs [index].sprite;
		b.onClick.AddListener (() => ElementButtonConfig.ChoiseEvent (config.ConfigInfs [index].buttonConfigType));

		if (isSpecial) {
			timeRecord = new TimeRecord ("test", SkyTime.MINUTE * 10, false);
		}
	}

	void Update ()
	{
		if (isSpecial) {
			letTime ();
		}
	}

	long lastLeftTime = 0;
	
	public void letTime ()
	{
		long leftTime = timeRecord.GetLeftTime ();
		if (leftTime > 0) {
			if (leftTime != lastLeftTime) {
				lastLeftTime = leftTime;
				long leftHour = leftTime / SkyTime.HOUR;
				leftTime -= leftHour * SkyTime.HOUR;
				long leftMinute = leftTime / SkyTime.MINUTE;
				leftTime -= leftMinute * SkyTime.MINUTE;
				long leftSecond = leftTime;
				string s = leftHour + " : " + leftMinute + " : " + leftSecond;
				mytext.text = "Special \n" + s;
			}
		} else {
			mytext.text = "NoSpecial";
		}
	}
}
