using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

[ExecuteInEditMode]
public class SpriteTextPanel : MonoBehaviour
{
	public enum LayoutType
	{
		LEFT,
		MIDDLE,
		RIGHT
	}

	[SerializeField]
	public LayoutType MLayoutType = LayoutType.MIDDLE;

	[SerializeField]
	public GameObject SpriteTextElement;

	public string TextContent{
		get{ return _textContent;}
		set{ RelayoutPanel(value);}
	}

	[SerializeField]
	private string _textContent;

	[SerializeField]
	public SpriteTexConfig MSpriteTexConfig;

	void Start ()
	{
		RelayoutPanel (TextContent);
	}

	public void myUpdate ()
	{
		RelayoutPanel(TextContent);
	}

	void OnEnable ()
	{
		myUpdate ();
	}
	
	void Reset ()
	{
		myUpdate ();
	}

	private void RelayoutPanel (string coinsCount)
	{
		this._textContent = coinsCount;
		for (int i = 0; i<gameObject.transform.childCount; i++) {
			GameObject go = gameObject.transform.GetChild (i).gameObject;
			arrayList.Add(go);
		}

		for (int i=0; i<arrayList.Count; i++) {
			DestroyImmediate (arrayList[i]);
		}
		arrayList.Clear ();

		if (coinsCount.Length < 1 || MSpriteTexConfig == null)
			return;
      

		RectTransform rectTransform = transform as RectTransform;
		Rect rect = rectTransform.rect;
		float width = rect.width / coinsCount.Length;
		float height = rect.height;

		if (width * MSpriteTexConfig.ImageSize.y > height * MSpriteTexConfig.ImageSize.x) {
			width = height * MSpriteTexConfig.ImageSize.x / MSpriteTexConfig.ImageSize.y / rect.width;
			height = 1f;
		} else {
			height = width * MSpriteTexConfig.ImageSize.y / MSpriteTexConfig.ImageSize.x / rect.height;
			width = 1f / coinsCount.Length;
		}

		float totalWidth = 0;

		for (int i=0; i<coinsCount.Length; i++) {
			string index = coinsCount [i] + "";
			bool hasSprite = false;
			SpriteTexConfig.SpriteItem spriteItemForThis = null;
			float widthFactorForThis = MSpriteTexConfig.NullItemWidthFactor;
			foreach (SpriteTexConfig.SpriteItem spriteItem in MSpriteTexConfig.sprites) {
                
				if (index.Equals (spriteItem.MIndex)) {
					spriteItemForThis = spriteItem;
					hasSprite = true;
					break;
				}
			}
          
			if (hasSprite) {

				widthFactorForThis = spriteItemForThis.WidthFactor;
                
			} 
			totalWidth += width * widthFactorForThis;
		}


		float startX = 0;
		switch (MLayoutType) {
		case LayoutType.LEFT:
			startX = 0;
			break;
		case LayoutType.MIDDLE:
			startX = (1 - totalWidth) / 2f;
			break;
		case LayoutType.RIGHT:
			startX = (1 - totalWidth);
			break;
		default:
			break;
		}

		float startY = (1 - height) / 2f;
		float endY = startY + height;
		float endX = startX + width;

		for (int i=0; i<coinsCount.Length; i++) {
			string index = coinsCount [i] + "";
			bool hasSprite = false;
			SpriteTexConfig.SpriteItem spriteItemForThis = null;
			float widthFactorForThis = MSpriteTexConfig.NullItemWidthFactor;
			foreach (SpriteTexConfig.SpriteItem spriteItem in MSpriteTexConfig.sprites) {
                
				if (index.Equals (spriteItem.MIndex)) {
					spriteItemForThis = spriteItem;
					hasSprite = true;
					break;
				}
			}
			GameObject number = Instantiate (SpriteTextElement) as GameObject;
			number.transform.SetParent (transform, false);
			Image image = number.GetComponent<Image> ();
			if (hasSprite) {
				image.sprite = spriteItemForThis.MSprite;
				image.color = new Color (1f, 1f, 1f, 1f);
				widthFactorForThis = spriteItemForThis.WidthFactor;

			} else {
				image.color = new Color (1f, 1f, 1f, 0f);
			}
          
			RectTransform rectNumber = number.transform as RectTransform;
			Vector2 anchorMin = rectNumber.anchorMin;
			Vector2 anchorMax = rectNumber.anchorMax;
			endY = startY + height;
			endX = startX + width * widthFactorForThis;

			anchorMin.x = startX;
			anchorMin.y = startY;
			anchorMax.x = endX;
			anchorMax.y = endY;
			startX = endX;
           
			rectNumber.anchorMin = anchorMin;
			rectNumber.anchorMax = anchorMax;
		}

	}

	private List<GameObject> arrayList = new List<GameObject> ();
}
