using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class UGUIEventListener : EventTrigger {

	public delegate void Delegate_OnBeginDrag(GameObject go, PointerEventData eventData);
	public delegate void Delegate_OnCancel (GameObject go, BaseEventData eventData);
	public delegate void Delegate_OnDeselect (GameObject go, BaseEventData eventData);
	public delegate void Delegate_OnDrag (GameObject go, PointerEventData eventData);
	public delegate void Delegate_OnDrop (GameObject go, PointerEventData eventData);
	public delegate void Delegate_OnEndDrag (GameObject go, PointerEventData eventData);
	public delegate void Delegate_OnInitializePotentialDrag (GameObject go, PointerEventData eventData);
	public delegate void Delegate_OnMove (GameObject go, AxisEventData eventData);
	public delegate void Delegate_OnPointerClick (GameObject go, PointerEventData eventData);
	public delegate void Delegate_OnPointerDown (GameObject go, PointerEventData eventData);
	public delegate void Delegate_OnPointerEnter (GameObject go, PointerEventData eventData);
	public delegate void Delegate_OnPointerExit (GameObject go, PointerEventData eventData);
	public delegate void Delegate_OnPointerUp (GameObject go, PointerEventData eventData);
	public delegate void Delegate_OnScroll (GameObject go, PointerEventData eventData);
	public delegate void Delegate_OnSelect (GameObject go, BaseEventData eventData);
	public delegate void Delegate_OnSubmit (GameObject go, BaseEventData eventData);
	public delegate void Delegate_OnUpdateSelected (GameObject go, BaseEventData eventData);


	public List<Delegate_OnBeginDrag> OnBeginDrags = new List<Delegate_OnBeginDrag> ();
	public List<Delegate_OnCancel> OnCancels = new List<Delegate_OnCancel> ();
	public List<Delegate_OnDeselect> OnDeselects = new List<Delegate_OnDeselect> ();
	public List<Delegate_OnDrag> OnDrags = new List<Delegate_OnDrag> ();
	public List<Delegate_OnDrop> OnDrops = new List<Delegate_OnDrop> ();
	public List<Delegate_OnEndDrag> OnEndDrags = new List<Delegate_OnEndDrag> ();
	public List<Delegate_OnInitializePotentialDrag> OnInitializePotentialDrags = new List<Delegate_OnInitializePotentialDrag> ();
	public List<Delegate_OnMove> OnMoves = new List<Delegate_OnMove> ();
	public List<Delegate_OnPointerClick> OnPointerClicks = new List<Delegate_OnPointerClick> ();
	public List<Delegate_OnPointerDown> OnPointerDowns = new List<Delegate_OnPointerDown> ();
	public List<Delegate_OnPointerEnter> OnPointerEnters = new List<Delegate_OnPointerEnter> ();
	public List<Delegate_OnPointerExit> OnPointerExits = new List<Delegate_OnPointerExit> ();
	public List<Delegate_OnPointerUp> OnPointerUps = new List<Delegate_OnPointerUp> ();
	public List<Delegate_OnScroll> OnScrolls = new List<Delegate_OnScroll> ();
	public List<Delegate_OnSelect> OnSelects = new List<Delegate_OnSelect> ();
	public List<Delegate_OnSubmit> OnSubmits = new List<Delegate_OnSubmit> ();
	public List<Delegate_OnUpdateSelected> OnUpdateSelecteds = new List<Delegate_OnUpdateSelected> ();


	public override void OnBeginDrag (PointerEventData eventData)
	{
		for (int k = 0; k < OnBeginDrags.Count; k++) {
			OnBeginDrags [k] (gameObject, eventData);
		}
	}

	public override void OnCancel (BaseEventData eventData)
	{
		for (int k = 0; k < OnCancels.Count; k++) {
			OnCancels [k] (gameObject, eventData);
		}
	}

	public override void OnDeselect (BaseEventData eventData)
	{
		for (int k = 0; k < OnDeselects.Count; k++) {
			OnDeselects [k] (gameObject, eventData);
		}
	}

	public override void OnDrag (PointerEventData eventData)
	{
		for (int k = 0; k < OnDrags.Count; k++) {
			OnDrags [k] (gameObject, eventData);
		}
	}

	public override void OnDrop (PointerEventData eventData)
	{
		for (int k = 0; k < OnDrops.Count; k++) {
			OnDrops [k] (gameObject, eventData);
		}
	}

	public override void OnEndDrag (PointerEventData eventData)
	{
		for (int k = 0; k < OnEndDrags.Count; k++) {
			OnEndDrags [k] (gameObject, eventData);
		}
	}

	public override void OnInitializePotentialDrag (PointerEventData eventData)
	{
		for (int k = 0; k < OnInitializePotentialDrags.Count; k++) {
			OnInitializePotentialDrags [k] (gameObject, eventData);
		}
	}

	public override void OnMove (AxisEventData eventData)
	{
		for (int k = 0; k < OnMoves.Count; k++) {
			OnMoves [k] (gameObject, eventData);
		}
	}

	public override void OnPointerClick (PointerEventData eventData)
	{
		for (int k = 0; k < OnPointerClicks.Count; k++) {
			OnPointerClicks [k] (gameObject, eventData);
		}
	}

	public override void OnPointerDown (PointerEventData eventData)
	{
		for (int k = 0; k < OnPointerDowns.Count; k++) {
			OnPointerDowns [k] (gameObject, eventData);
		}
	}

	public override void OnPointerEnter (PointerEventData eventData)
	{
		for (int k = 0; k < OnPointerEnters.Count; k++) {
			OnPointerEnters [k] (gameObject, eventData);
		}
	}

	public override void OnPointerExit (PointerEventData eventData)
	{
		for (int k = 0; k < OnPointerExits.Count; k++) {
			OnPointerExits [k] (gameObject, eventData);
		}
	}

	public override void OnPointerUp (PointerEventData eventData)
	{
		for (int k = 0; k < OnPointerUps.Count; k++) {
			OnPointerUps [k] (gameObject, eventData);
		}
	}

	public override void OnScroll (PointerEventData eventData)
	{
		for (int k = 0; k < OnScrolls.Count; k++) {
			OnScrolls [k] (gameObject, eventData);
		}
	}

	public override void OnSelect (BaseEventData eventData)
	{
		for (int k = 0; k < OnSelects.Count; k++) {
			OnSelects [k] (gameObject, eventData);
		}
	}

	public override void OnSubmit (BaseEventData eventData)
	{
		for (int k = 0; k < OnSubmits.Count; k++) {
			OnSubmits [k] (gameObject, eventData);
		}
	}

	public override void OnUpdateSelected (BaseEventData eventData)
	{
		for (int k = 0; k < OnUpdateSelecteds.Count; k++) {
			OnUpdateSelecteds [k] (gameObject, eventData);
		}
	}


	public static UGUIEventListener Get(GameObject go)
	{
		UGUIEventListener listener = go.GetComponent<UGUIEventListener> ();
		if (listener == null) {
			listener = go.AddComponent<UGUIEventListener> ();
		}
		return listener;
	}
}
