using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ListView : MonoBehaviour 
{
    float gap = 0.0f;
    [SerializeField] Transform m_Content;
    [SerializeField] VerticalLayoutGroup m_Layout;
    [SerializeField] GameObject[] m_Prefabs;
    [SerializeField] float m_DestroyLine;
    [SerializeField] int m_DefaultCnt;

    List<ItemCtrl> m_DataSource = new List<ItemCtrl>();
    static int cnt = 0;
    GameObject m_ContentParent;
    void Start()
    {
        gap = m_Layout.spacing;
        m_ContentParent = m_Content.transform.parent.gameObject;
        for (int i = 0; i < m_DefaultCnt; ++i)
        {
            int index = Random.Range(0, 3);
            GameObject go = GameObject.Instantiate(m_Prefabs[index]) as GameObject;
            go.transform.SetParent(m_Content.transform);
            go.transform.localScale = Vector3.one;
            ItemCtrl itemCtrl = go.GetComponent<ItemCtrl>();
            itemCtrl.m_Text.text = (cnt).ToString();
            itemCtrl.m_Index = cnt;
            ++cnt;
            m_DataSource.Add(itemCtrl);
        }
    }

    public void OnValueChange(Vector2 pos)
    {
        if(flag)
        {
            return;
        }
        
        if(m_DataSource.Count>0)
        {
            GameObject go = m_DataSource[0].gameObject;
            float y = m_ContentParent.transform.InverseTransformPoint(go.transform.position).y;
            ItemCtrl itemCtrl = go.GetComponent<ItemCtrl>();
            //向上滑动
            if(direction==1&&(y-itemCtrl.Height)>m_DestroyLine)
            {
                //删除顶部一个元素
                Destroy(go);
                m_DataSource.RemoveAt(0);
                m_Content.localPosition = m_Content.localPosition - new Vector3(0,(itemCtrl.Height+gap),0);
                //在底部添加一个元素
                int index = Random.Range(0, 3);
                GameObject newItem = GameObject.Instantiate(m_Prefabs[index]) as GameObject;
                newItem.transform.SetParent(m_Content.transform);
                newItem.transform.localScale = Vector3.one;
                ItemCtrl newItemCtrl = newItem.GetComponent<ItemCtrl>();
                GameObject lastItem = m_DataSource[m_DataSource.Count - 1].gameObject;
                ItemCtrl lastItemCtrl = lastItem.GetComponent<ItemCtrl>();
                newItemCtrl.m_Text.text = (lastItemCtrl.m_Index+1).ToString();
                newItemCtrl.m_Index = lastItemCtrl.m_Index+1;
                m_DataSource.Add(newItemCtrl);
            }

            //向下滑动
            if(direction==-1&& (m_Content.localPosition.y < m_DestroyLine))
            {
                if(itemCtrl.m_Index>=1)
                {
                    //顶部添加一个元素
                    int index = Random.Range(0, 3);
                    GameObject newItem = GameObject.Instantiate(m_Prefabs[index]) as GameObject;
                    newItem.transform.SetParent(m_Content.transform);
                    newItem.transform.SetAsFirstSibling();
                    newItem.transform.localScale = Vector3.one;
                    ItemCtrl newItemCtrl = newItem.GetComponent<ItemCtrl>();
                    newItemCtrl.m_Text.text = (itemCtrl.m_Index-1).ToString();
                    newItemCtrl.m_Index = itemCtrl.m_Index - 1;
                    m_DataSource.Insert(0, newItemCtrl);
                    m_Content.localPosition = m_Content.localPosition + new Vector3(0, (newItemCtrl.Height + gap), 0);
                    //底部删除一个
                    Destroy(m_DataSource[m_DataSource.Count-1].gameObject);
                    m_DataSource.RemoveAt(m_DataSource.Count - 1);
                }
            }
        }
    }

    bool flag = false;
    Vector3 beginPos;
    int direction = 0;
    public void OnBeginDrag()
    {
        beginPos = Input.mousePosition;
        flag = true;
    }

    public void OnEndDrag()
    {
        if(Input.mousePosition.y-beginPos.y>0)
        {
            direction = 1;
        }
        else if(Input.mousePosition.y-beginPos.y<0)
        {
            direction = -1;
        }
        else
        {
            direction = 0;
        }
        flag = false;
    }

}
