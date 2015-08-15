using UnityEngine;  
using System.Collections;  
using UnityEditor;  

public class MyFont : MonoBehaviour   
{  
	public Font m_myFont;  
	public TextAsset m_data;  
	private BMFont mbFont = new BMFont();
	public float scale = 1;
	void Start ()   
	{  
		BMFontReader.Load(mbFont, m_data.name, m_data.bytes);  // 借用NGUI封装的读取类
		CharacterInfo[] characterInfo = new CharacterInfo[mbFont.glyphs.Count];  
		for (int i = 0; i < mbFont.glyphs.Count; i++)  
		{  
			BMGlyph bmInfo = mbFont.glyphs[i];  
			CharacterInfo info = new CharacterInfo();  
			info.index = bmInfo.index;  
			info.uv.x = (float)bmInfo.x / (float)mbFont.texWidth;  
			info.uv.y = 1-(float)bmInfo.y / (float)mbFont.texHeight;  
			info.uv.width = (float)bmInfo.width / (float)mbFont.texWidth;  
			info.uv.height = -1f * (float)bmInfo.height / (float)mbFont.texHeight;  
			info.vert.x = (float)bmInfo.offsetX*scale;  
			//info.vert.y = (float)bmInfo.offsetY+(float)bmInfo.height-mbFont.charSize;  
			info.vert.y = -((float)bmInfo.offsetY+(float)bmInfo.height)*scale;  
			info.vert.width = (float)bmInfo.width*scale;  
			info.vert.height = (float)bmInfo.height*scale;  
			info.width = (float)bmInfo.advance*scale;  
			characterInfo[i] = info;
		}  
		m_myFont.characterInfo = characterInfo;  
	}  
}  
