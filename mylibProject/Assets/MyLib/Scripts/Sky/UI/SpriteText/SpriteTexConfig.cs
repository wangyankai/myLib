using UnityEngine;
using System.Collections;
using System;

public class SpriteTexConfig : MonoBehaviour {

	[Serializable]
	public class SpriteItem
	{
		public string MIndex;
		public Sprite MSprite;
		public float WidthFactor = 1f;
	}

	public Vector2 ImageSize = new Vector2 (1f, 1f);

	public SpriteItem[] sprites;
	public float NullItemWidthFactor = 0.2f;
}
