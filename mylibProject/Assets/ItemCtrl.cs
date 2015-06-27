using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemCtrl : MonoBehaviour 
{
    public float Height
    {
        get { return m_LayoutElement.preferredHeight; }
    }
    [SerializeField] LayoutElement m_LayoutElement;
    public Text m_Text;
    public int m_Index;

    float m_Height;
	void Start()
    {
        m_Height = m_LayoutElement.preferredHeight;
    }
}
