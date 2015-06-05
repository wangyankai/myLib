using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI.UIComponent.ScrollList
{
		public class SkyScrollRect : ScrollRect
		{
				public delegate void SkyOnBeginDrag ();

				public SkyOnBeginDrag mySkyOnBeginDrag;

				public delegate void SkyOnEndDrag ();

				public SkyOnEndDrag mySkyOnEndDrag;

				public override void OnBeginDrag (UnityEngine.EventSystems.PointerEventData eventData)
				{
						if (eventData.button != PointerEventData.InputButton.Left) {
								return;
						}
						if (!this.IsActive ()) {
								return;
						}
				
						base.OnBeginDrag (eventData);
						mySkyOnBeginDrag ();
						_isDraging = true;
				}

				public override void OnDrag (UnityEngine.EventSystems.PointerEventData eventData)
				{	
						base.OnDrag (eventData);
				}

				public override void OnEndDrag (UnityEngine.EventSystems.PointerEventData eventData)
				{
						if (eventData.button != PointerEventData.InputButton.Left) {
								return;
						}
						base.OnEndDrag (eventData);
						mySkyOnEndDrag ();
						_isDraging = false;
				}

				public override void OnScroll (UnityEngine.EventSystems.PointerEventData data)
				{
						
				}
	 
				public bool IsDraging {
						get { return _isDraging;}
				}

				private bool _isDraging;
		}
}
