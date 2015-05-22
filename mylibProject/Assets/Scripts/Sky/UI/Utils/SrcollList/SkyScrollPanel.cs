using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace UI.UIComponent.ScrollList
{
    public class SkyScrollPanel : MonoBehaviour
    {
        public bool AutoScroll = true;
        public SkyElementBase BaseElement;
        public Vector2 ElementLocalSize = new Vector2 (0.8f, 0.8f);
        public int ShowNumber = 1;
        public SkyElementConfig Config;

        // Use this for initialization
        void Start ()
        {
            init ();
        }

        private void init ()
        {
            myScrollPanel = gameObject;
            initScroll ();
            initScrollSize ();
            addElments ();
            SetPosition ();
        }

        void Update ()
        {
            myUpdate ();
        }

        protected virtual void myUpdate ()
        {

        }

        protected void initScroll ()
        {
            myScrollRect = GetComponent<ScrollRect> ();
            myscrollBar = myScrollRect.horizontalScrollbar;
            ((SkyScrollRect)myScrollRect).mySkyOnEndDrag = new SkyScrollRect.SkyOnEndDrag (onEndDrag);
            ((SkyScrollRect)myScrollRect).mySkyOnBeginDrag = new SkyScrollRect.SkyOnBeginDrag (onBeginDrag);
            myScrollList = GameObject.Find (SCROLL_LIST);
            myGridLayoutGroup = myScrollList.GetComponent<GridLayoutGroup> ();
        }

        protected virtual void onBeginDrag ()
        {
            //Debug.Log ("Panel OnBeginDrag");
            AutoScroll = false;
        }

        protected virtual void onEndDrag ()
        {
            //Debug.Log ("Panel OnEndDrag");
            AutoScroll = true;
        }
    
        protected virtual void initScrollSize ()
        {
            RectTransform recTransform = myScrollPanel.transform as RectTransform;
            myGridLayoutGroup.cellSize = new Vector2 (recTransform.rect.width * ElementLocalSize.x / ShowNumber, recTransform.rect.height * ElementLocalSize.y);
            myGridLayoutGroup.spacing = new Vector2 (recTransform.rect.width * (1 - ElementLocalSize.x) / ShowNumber, recTransform.rect.height * (1 - ElementLocalSize.y));
            myGridLayoutGroup.padding.left = myGridLayoutGroup.padding.right = (int)(recTransform.rect.width * (1 - ElementLocalSize.x) / 2f / ShowNumber);
            myGridLayoutGroup.padding.top = myGridLayoutGroup.padding.bottom = (int)(recTransform.rect.height * (1 - ElementLocalSize.y) / 2f);
        }

        private void addElments ()
        {
            for (int i=0; i<Config.getCount(); i++) {
                SkyElementBase element = Instantiate (BaseElement) as SkyElementBase;
                element.transform.SetParent (myScrollList.gameObject.transform, false);
                element.Init (i, this);
            }
        }

        public virtual void SetPosition ()
        {
        }

        public int GetElementCount ()
        {
            if (myScrollList == null)
                return 0;
            return myScrollList.transform.childCount;
        }

        protected GameObject myScrollPanel;
        protected Scrollbar myscrollBar;
        protected GameObject myScrollList;
        protected ScrollRect myScrollRect;
        protected GridLayoutGroup myGridLayoutGroup;
        protected static string SCROLL_LIST = "ScrollList";
        protected int index = 2;
        protected const  int standCount = 5;
        
        public virtual void OnSubPointDown ()
        {
            //Debug.Log ("OnSubPointDown");
            AutoScroll = false;
        }

        public virtual void OnSubPointUp ()
        {
            //Debug.Log ("OnSubPointUp");
            if (!((SkyScrollRect)myScrollRect).IsDraging)
                AutoScroll = true;
        }

        public virtual void NextElement ()
        {

        }

        public virtual void PreElement ()
        {
          
        }
    }
}
