// <copyright file="BitmapFont.cs" company="Boris Brock Softwareentwicklung">
// Copyright (c) 2014 All Rights Reserved
// </copyright>
// <author>Boris Brock</author>
// <date>2014-06-08 20:41:58</date>
// <summary>Class representing a bitmap font</summary>
// <version>1.5</version

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

[ExecuteInEditMode]
public class BitmapFont : MonoBehaviour
{
  #region Inspector Variables

  /// <summary>
  /// The font description.
  /// </summary>
  public TextAsset FontDescription;

  /// <summary>
  /// The font texture.
  /// </summary>
  public Texture2D FontTexture;

  /// <summary>
  /// The retina font description.
  /// </summary>
  public TextAsset RetinaFontDescription;

  /// <summary>
  /// The retina font texture.
  /// </summary>
  public Texture2D RetinaFontTexture;

  /// <summary>
  /// Sets if retina mode is enabled.
  /// </summary>
  public bool UseRetinaFont;

  #endregion

  #region Private Variables

  // Helper variables for change detection
  private TextAsset mOldFontDescription;
  private Texture2D mOldFontTexture;
  private TextAsset mOldRetinaFontDescription;
  private Texture2D mOldRetinaFontTexture;

  // Font information
  [HideInInspector]
  [SerializeField]
  private bool mIsInitialized;

  [HideInInspector]
  [SerializeField]
  private int mCharBaseline;

  [HideInInspector]
  [SerializeField]
  private int mLineHeight;

  [HideInInspector]
  [SerializeField]
  private int mCharBaselineRetina;

  [HideInInspector]
  [SerializeField]
  private int mLineHeightRetina;

  [HideInInspector]
  [SerializeField]
  public List<CharInfo> mChars = new List<CharInfo> ();

  [HideInInspector]
  [SerializeField]
  public List<CharInfo> mRetinaChars = new List<CharInfo> ();

  #endregion

  #region Nested Classes

  [System.Serializable]
  public class CharInfo
  {
    // Public members that describe the characters size/placement information
    public Int32 x;
    public Int32 y;
    public Int32 width;
    public Int32 height;
    public Int32 xoffset;
    public Int32 yoffset;
    public Int32 xadvance;
  }

  #endregion

  #region Public Methods

  /// <summary>
  /// Returns the current font texture (nromal or retina)
  /// </summary>
  /// <returns>The font texture.</returns>
  public Texture2D GetFontTexture ()
  {
    return UseRetinaFont ? RetinaFontTexture : FontTexture;
  }

  /// <summary>
  /// Returns the info struct for the given cahracter
  /// </summary>
  /// <returns>The char info.</returns>
  /// <param name="c">C.</param>
  public CharInfo GetCharInfo (char c)
  {
    if (UseRetinaFont) {
      if (c < mRetinaChars.Count)
        return mRetinaChars [c];
    } else {
      if (c < mChars.Count)
        return mChars [c];
    }
    
    return null;
  }

  /// <summary>
  /// Gets the character baseline.
  /// </summary>
  /// <returns>The char baseline.</returns>
  public int GetCharBaseline ()
  {
    return UseRetinaFont ? mCharBaselineRetina : mCharBaseline;
  }

  /// <summary>
  /// Gets the height of a line.
  /// </summary>
  /// <returns>The line height.</returns>
  public int GetLineHeight ()
  {
    return UseRetinaFont ? mLineHeightRetina : mLineHeight;
  }

  /// <summary>
  /// Gets an identifier for this font.
  /// </summary>
  /// <returns>The identifier.</returns>
  public string GetId ()
  {
    string retval = "";
    if (FontTexture != null)
      retval += FontTexture.name;
    if (RetinaFontTexture != null)
      retval += RetinaFontTexture.name;
    return retval;
  }

  #endregion

  #region Private Methods

  /// <summary>
  /// Called when the font object starts
  /// </summary>
  void Start ()
  {
    if (!mIsInitialized)
      OnFontChanged ();
  }

  /// <summary>
  /// Can be called to recreate the fonts if possible
  /// </summary>
  void OnFontChanged ()
  {
    if (FontTexture != mOldFontTexture || FontDescription != mOldFontDescription) {
      mOldFontTexture = FontTexture;
      mOldFontDescription = FontDescription;
      if (FontTexture != null && FontDescription != null) {
        if (mChars == null)
          mChars = new List<CharInfo> ();
        RecreateBitmapFont (mChars, FontDescription.text, false);
      }
    }
		
    if (RetinaFontTexture != mOldRetinaFontTexture || RetinaFontDescription != mOldRetinaFontDescription) {
      mOldRetinaFontTexture = RetinaFontTexture;
      mOldRetinaFontDescription = RetinaFontDescription;
			
      if (RetinaFontTexture != null && RetinaFontDescription != null) {
        if (mRetinaChars == null)
          mRetinaChars = new List<CharInfo> ();
        RecreateBitmapFont (mRetinaChars, RetinaFontDescription.text, true);
      }
    }
  }
	
  #if UNITY_EDITOR
  void Update ()
  {
    if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode) {
      this.enabled = false;
    } else {
			OnFontChanged();
    }
  }
  #endif
	
  /// <summary>
  /// Gets the current text asset.
  /// </summary>
  /// <returns>The current text asset.</returns>
  private TextAsset GetCurrentTextAsset ()
  {
    return UseRetinaFont ? RetinaFontDescription : FontDescription;
  }

  /// <summary>
  /// Re-creates the bitmap font
  /// </summary>
  private void RecreateBitmapFont (List<CharInfo> chars, string text, bool retina)
  {
    // Prepare the character list
    chars.Clear ();
    while (chars.Count < 256)
      chars.Add (new CharInfo ());

    // Read fnt file
    if (string.IsNullOrEmpty (text))
      return;
    StringReader strReader = new StringReader (text);
    while (true) {
      string line = strReader.ReadLine ();
      if (line == null)
        break;

      try {
        if (line.StartsWith ("char "))
          ProcessCharLine (line, chars);
        else if (line.StartsWith ("common "))
          ProcessCommonLine (line, retina);
      } catch (Exception ex) {
        Debug.LogError ("Error while loading bitmap font: " + ex);
      }
    }

    mIsInitialized = true;
  }

  /// <summary>
  /// Processes a common line.
  /// </summary>
  /// <param name="line">Line.</param>
  private void ProcessCommonLine (string line, bool retina)
  {
    var tokens = line.Split (new char[]{ ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
    int i = 0;
    foreach (var token in tokens) {
      var innerTokens = token.Split (new char[]{ '=' }, System.StringSplitOptions.RemoveEmptyEntries);

      if (innerTokens [0].ToLower () == "linehight") {
        if (retina)
          mLineHeightRetina = Int32.Parse (innerTokens [1]);
        else
          mLineHeight = Int32.Parse (innerTokens [1]);
      } else if (innerTokens [0].ToLower () == "base") {
        if (retina)
          mCharBaselineRetina = Int32.Parse (innerTokens [1]);
        else
          mCharBaseline = Int32.Parse (innerTokens [1]);
      } else if (innerTokens [0].ToLower () == "scalew") {
        if (retina && Int32.Parse (innerTokens [1]) != mOldRetinaFontTexture.width)
          Debug.LogError ("BitmapFontPro Error: the retina texture's width does not match the font settings file. Have you set the Texture Type to Sprite/2D?");
        else if (!retina && Int32.Parse (innerTokens [1]) != mOldFontTexture.width)
          Debug.LogError ("BitmapFontPro Error: the texture's width does not match the font settings file. Have you set the Texture Type to Sprite/2D?");
         
      } else if (innerTokens [0].ToLower () == "scaleh") {
        if (retina && Int32.Parse (innerTokens [1]) != mOldRetinaFontTexture.height)
          Debug.LogError ("BitmapFontPro Error: the retina texture's height does not match the font settings file. Have you set the Texture Type to Sprite/2D?");
        else if (!retina && Int32.Parse (innerTokens [1]) != mOldFontTexture.height)
          Debug.LogError ("BitmapFontPro Error: the texture's height does not match the font settings file. Have you set the Texture Type to Sprite/2D?"); 
      }
	
      i++;
    }
  }

  /// <summary>
  /// Processes a character line.
  /// </summary>
  /// <param name="line">Line.</param>
  private void ProcessCharLine (string line, List<CharInfo> chars)
  {
    var tokens = line.Split (new char[]{ ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
    int i = 0;
    int c = 0;
    foreach (var token in tokens) {
      var innerTokens = token.Split (new char[]{ '=' }, System.StringSplitOptions.RemoveEmptyEntries);

      if (i == 1)
        c = Int32.Parse (innerTokens [1]);
      else if (i == 2)
        chars [c].x = Int32.Parse (innerTokens [1]);
      else if (i == 3)
        chars [c].y = Int32.Parse (innerTokens [1]);
      else if (i == 4)
        chars [c].width = Int32.Parse (innerTokens [1]);
      else if (i == 5)
        chars [c].height = Int32.Parse (innerTokens [1]);
      else if (i == 6)
        chars [c].xoffset = Int32.Parse (innerTokens [1]);
      else if (i == 7)
        chars [c].yoffset = Int32.Parse (innerTokens [1]);
      else if (i == 8)
        chars [c].xadvance = Int32.Parse (innerTokens [1]);

      i++;
    }
  }

  #endregion
}
