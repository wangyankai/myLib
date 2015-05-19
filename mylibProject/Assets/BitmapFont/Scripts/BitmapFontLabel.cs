// <copyright file="BitmapFontLabel.cs" company="Boris Brock Softwareentwicklung">
// Copyright (c) 2014 All Rights Reserved
// </copyright>
// <author>Boris Brock</author>
// <date>2014-06-08 20:41:58</date>
// <summary>Class representing a bitmap font label</summary>
// <version>1.5</version

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[ExecuteInEditMode]
public class BitmapFontLabel : MonoBehaviour
{
  #region Inspector Variables

  /// <summary>
  /// The font that is used to draw the text.
  /// </summary>
  public GameObject Font;

  /// <summary>
  /// The text that is shown on the label.
  /// </summary>
  public string Text;

  /// <summary>
  /// The scale of the label.
  /// </summary>
  public float Scale = 1.0f;

  /// <summary>
  /// The line spacing scale (1 means default spacing).
  /// </summary>
  public float LineSpacingScale = 1.0f;

  /// <summary>
  /// The vertical alignment.
  /// </summary>
  public VerticalTextAlign VerticalAlign;

  /// <summary>
  /// The horizontal alignment.
  /// </summary>
  public HorizontalTextAlign HorizontalAlign;

  /// <summary>
  /// The color of the text.
  /// </summary>
  public Color TextColor = Color.white;

  #endregion

  #region Private Variables

  // Used for change detection
  private GameObject mOldFont;
  private string mOldText;
  private float mOldScale = 1.0f;
  private float mOldLineSpacingScale = 1.0f;
  private VerticalTextAlign mOldVerticalAlign;
  private HorizontalTextAlign mOldHorizontalAlign;
  private Color mOldTextColor = Color.white;

  [HideInInspector]
  [SerializeField]
  private bool mWasCreatedRetina;

  // Helper objects (children)
  private GameObject mCharHolder;
  private GameObject mCharPrefab;

  #endregion

  #region Nested Classes and Enums

  public enum VerticalTextAlign
  {
    Top,
    Center,
    Bottom
  }

  public enum HorizontalTextAlign
  {
    Left,
    Center,
    Right
  }

  #endregion

  #region Public Methods

  /// <summary>
  /// Sets the color.
  /// </summary>
  /// <param name="color">Color.</param>
  public void SetColor (Color color)
  {
    this.TextColor = color;
    this.mOldTextColor = color;

    if (mCharHolder == null)
      return;
    foreach (Transform childTransform in mCharHolder.transform) {
      SpriteRenderer r = (SpriteRenderer)childTransform.GetComponent (typeof(SpriteRenderer));
      if (r != null)
        r.color = color;
    }
  }

  /// <summary>
  /// Sets the font.
  /// </summary>
  /// <param name="font">Font.</param>
  public void SetFont (GameObject font)
  {
    Font = font;
    RecreateText (Text);
  }

  /// <summary>
  /// Sets the text.
  /// </summary>
  /// <param name="text">Text.</param>
  public void SetText (string text)
  {
    Text = text;
    RecreateText (Text);
  }

  /// <summary>
  /// Sets the scale.
  /// </summary>
  /// <param name="scale">Scale.</param>
  public void SetScale (float scale)
  {
    Scale = scale;
    RecreateText (Text);
  }

  /// <summary>
  /// Sets the line spacing scale.
  /// </summary>
  /// <param name="scale">Scale.</param>
  public void SetLineSpacingScale (float scale)
  {
    LineSpacingScale = scale;
    RecreateText (Text);
  }

  /// <summary>
  /// Sets the vertical align.
  /// </summary>
  /// <param name="align">Align.</param>
  public void SetVerticalAlign (VerticalTextAlign align)
  {
    VerticalAlign = align;
    RecreateText (Text);
  }

  /// <summary>
  /// Sets the horizontal align.
  /// </summary>
  /// <param name="align">Align.</param>
  public void SetHorizontalAlign (HorizontalTextAlign align)
  {
    HorizontalAlign = align;
    RecreateText (Text);
  }

  #endregion

  #region Private Methods

  /// <summary>
  /// Called when this instance starts
  /// </summary>
  void Start ()
  {
    mOldText = Text;
    mOldScale = Scale;
    mOldLineSpacingScale = LineSpacingScale;
    mOldVerticalAlign = VerticalAlign;
    mOldHorizontalAlign = HorizontalAlign;
    mOldTextColor = TextColor;
  }

  #if UNITY_EDITOR
  void Update ()
  {
		if(Font == null) return;
      // Runtime
      var parentFont = (BitmapFont)Font.GetComponent (typeof(BitmapFont));
			if (parentFont == null)
				return;
      if (mOldText != Text ||
          Scale != mOldScale ||
          HorizontalAlign != mOldHorizontalAlign ||
          VerticalAlign != mOldVerticalAlign ||
          LineSpacingScale != mOldLineSpacingScale ||
          mWasCreatedRetina != parentFont.UseRetinaFont) {

        mOldText = Text;
        mOldScale = Scale;
        mOldLineSpacingScale = LineSpacingScale;
        mOldVerticalAlign = VerticalAlign;
        mOldHorizontalAlign = HorizontalAlign;
        RecreateText (Text);
      }

      if (TextColor.r != mOldTextColor.r ||
          TextColor.g != mOldTextColor.g ||
          TextColor.b != mOldTextColor.b ||
          TextColor.a != mOldTextColor.a) {
        mOldTextColor.r = TextColor.r;
        mOldTextColor.g = TextColor.g;
        mOldTextColor.b = TextColor.b;
        mOldTextColor.a = TextColor.a;
        SetColor (TextColor);
    }
  }
  

#else
  void Update ()
  {
    // Runtime on devices
    if (Font == null)
      return;
    var parentFont = (BitmapFont)Font.GetComponent (typeof(BitmapFont));
    if (parentFont == null)
      return;
    if (mOldText != Text ||
        Scale != mOldScale ||
        HorizontalAlign != mOldHorizontalAlign ||
        VerticalAlign != mOldVerticalAlign ||
        LineSpacingScale != mOldLineSpacingScale ||
        mWasCreatedRetina != parentFont.UseRetinaFont) {

      mOldText = Text;
      mOldScale = Scale;
      mOldLineSpacingScale = LineSpacingScale;
      mOldVerticalAlign = VerticalAlign;
      mOldHorizontalAlign = HorizontalAlign;
      RecreateText (Text);
    }

    if (TextColor.r != mOldTextColor.r ||
        TextColor.g != mOldTextColor.g ||
        TextColor.b != mOldTextColor.b ||
        TextColor.a != mOldTextColor.a) {
      mOldTextColor.r = TextColor.r;
      mOldTextColor.g = TextColor.g;
      mOldTextColor.b = TextColor.b;
      mOldTextColor.a = TextColor.a;
      SetColor (TextColor);
    }
  }
  #endif

  /// <summary>
  /// Recreates the text.
  /// </summary>
  /// <param name="text">Text.</param>
  public void RecreateText (string text)
  {
    var mFont = (BitmapFont)Font.GetComponent (typeof(BitmapFont));

    mWasCreatedRetina = mFont.UseRetinaFont;
    float curScale = mOldScale;
    float retinaScale = mWasCreatedRetina ? 0.5f : 1.0f;

    var c1 = transform.FindChild ("Characters");
    if (c1 != null)
      DestroyImmediate (c1.gameObject);

    mCharHolder = new GameObject ("Characters");
    mCharHolder.transform.parent = transform;
    mCharHolder.transform.localPosition = Vector3.zero;

    if (mCharPrefab == null) {
			
      // delet all children
      var c2 = transform.FindChild ("CharacterPrefab");
      if (c2 != null)
        DestroyImmediate (c2.gameObject);
						
      mCharPrefab = new GameObject ("CharacterPrefab");
      mCharPrefab.AddComponent<SpriteRenderer> ();
      mCharPrefab.transform.parent = transform;
    }

    var lines = text.Split (new char[]{ '\n' });
    float pixelsToUnit = 100.0f / curScale;
		
    // Draw the text
    float posy = 0;
		
    if (VerticalAlign == VerticalTextAlign.Bottom)
      posy += (lines.Length - 1) * mFont.GetLineHeight () * retinaScale * LineSpacingScale / pixelsToUnit;
    else if (VerticalAlign == VerticalTextAlign.Center)
      posy = -mFont.GetLineHeight () * retinaScale * 0.5f / pixelsToUnit + (lines.Length - 1) * mFont.GetLineHeight () * retinaScale * LineSpacingScale * 0.5f / pixelsToUnit;
    else if (VerticalAlign == VerticalTextAlign.Top)
      posy = -mFont.GetLineHeight () * retinaScale / pixelsToUnit;
		
    foreach (var line in lines) {
      float posX = 0;
			
      // Calc line width
      float totalWidth = 0;
      if (HorizontalAlign == HorizontalTextAlign.Center || HorizontalAlign == HorizontalTextAlign.Right) {
        foreach (char c in line) {
          var info = mFont.GetCharInfo (c);
          totalWidth += info.xadvance * retinaScale / pixelsToUnit;
        }
      }
      if (HorizontalAlign == HorizontalTextAlign.Center) {
        posX = -totalWidth * 0.5f;
      } else if (HorizontalAlign == HorizontalTextAlign.Right) {
        posX = -totalWidth;
      }
			
			
      foreach (char c in line) {
        var info = mFont.GetCharInfo (c);
        if (info == null)
          continue;
				
        GameObject character = (GameObject)Instantiate (mCharPrefab);
        character.transform.parent = mCharHolder.transform;
        SpriteRenderer sprite = (SpriteRenderer)character.GetComponent (typeof(SpriteRenderer));

        int y = (int)(mFont.GetFontTexture ().height - info.y - info.height);
        sprite.sprite = Sprite.Create (mFont.GetFontTexture (), new Rect (info.x, y, info.width, info.height), new Vector2 (0, 1), pixelsToUnit / retinaScale);
        sprite.transform.parent = mCharHolder.transform;
				
        Vector3 p = sprite.transform.localPosition;
        p.x = posX;
        p.y = posy;
        p.z = 0.0f;
				
        p.x += info.xoffset * retinaScale / pixelsToUnit;
        p.y += (mFont.GetCharBaseline () * retinaScale - info.yoffset * retinaScale) / pixelsToUnit;
				
        character.transform.localPosition = p;
				
        posX += info.xadvance * retinaScale / pixelsToUnit;
      }

      posy -= mFont.GetLineHeight () * retinaScale * LineSpacingScale / pixelsToUnit;
      SetColor (TextColor);
    }
  }

  #endregion
}
