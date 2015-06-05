using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace UI.UIComponent.ScrollList
{
		public class SkyElementBase : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IPointerClickHandler
		{

				protected SkyScrollPanel MySkyScrollPanel;
			
				public virtual void Init (int index, SkyScrollPanel mySkyScrollPanel)
				{
						this.MySkyScrollPanel = mySkyScrollPanel;
				}

				public virtual void OnPointerDown (PointerEventData eventData)
				{
						MySkyScrollPanel.OnSubPointDown ();
				}

				public virtual void OnPointerUp (PointerEventData eventData)
				{
						MySkyScrollPanel.OnSubPointUp ();
				}

				public virtual void OnPointerClick (PointerEventData eventData)
				{
						//Debug.Log ("OnPointerClick");
				}
		}
}
