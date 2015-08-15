using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class TimeRecord
{
	public class TimeRecordElement
	{
		public string Name;
		public long Value;

		public TimeRecordElement (string name)
		{
			this.Name = name;
			this.Value = SharedPrefs.GetLong (name);
		}

		public void Save (long value)
		{
			this.Value = value;
			Save ();
		}

		public void Save ()
		{
			SharedPrefs.SaveLong (Name, Value);
		}

		public override string ToString ()
		{
			return (Name + "  value  " + Value);
		}
	}

	public TimeRecordElement LastSysTime;
	public TimeRecordElement LastPhoneRunTime;
	public TimeRecordElement LastCountTime;
	public TimeRecordElement IsActive; //value: 0,disable;1 active
	public long DelayTime;
	public bool Overlapped;
	private string preFixed;
	private static readonly string LAST_SYS_TIME = "LastSysTime";
	private static readonly string LAST_PHONE_RUN_TIME = "LastPhoneRunTime";
	private static readonly string LAST_COUNT_TIME = "LastCountTime";
	private static readonly string IS_ACTIVE = "IsActive";

	public TimeRecord (string preFixed, long delayTime, bool overlapped)
	{
		this.preFixed = preFixed;
		LastSysTime = new TimeRecordElement (this.preFixed + LAST_SYS_TIME);
		LastPhoneRunTime = new TimeRecordElement (this.preFixed + LAST_PHONE_RUN_TIME);
		LastCountTime = new TimeRecordElement (this.preFixed + LAST_COUNT_TIME);
		IsActive = new TimeRecordElement (this.preFixed + IS_ACTIVE);
		this.DelayTime = delayTime;
		this.Overlapped = overlapped;
		OnStartCheck ();
	}

	public void OnStartCheck ()
	{
		if (IsEffective ()) {
			checkWithSameWeight ();
		}
	}

	private void checkWithSameWeight ()
	{
		long diffLocalTime = SkyTime.ConvertDateToLong (DateTime.Now) - LastSysTime.Value;
		long diffPhoneStartTime = SkyTime.realTimeSinceOn () - LastPhoneRunTime.Value;
		long MaxDiff = diffLocalTime > diffPhoneStartTime ? diffLocalTime : diffPhoneStartTime;
		MaxDiff = LastCountTime.Value > MaxDiff ? LastCountTime.Value : MaxDiff;
		if (MaxDiff > DelayTime) {
			IsActive.Save (0L);
		} else {
			if (MaxDiff > LastCountTime.Value) {
				LastCountTime.Save (MaxDiff);
			}
            
			if (MaxDiff > diffLocalTime) {
				LastSysTime.Value -= (MaxDiff - diffLocalTime);
				LastSysTime.Save ();
			}
            
			if (MaxDiff > diffPhoneStartTime) {
				LastPhoneRunTime.Value -= (MaxDiff - diffPhoneStartTime);
				LastPhoneRunTime.Save ();
			}
            
		}
	}

	private void checkUptimeFirst ()
	{
		long diffPhoneStartTime = SkyTime.realTimeSinceOn () - LastPhoneRunTime.Value;
		long MaxDiff = diffPhoneStartTime;
		long diffLocalTime = SkyTime.ConvertDateToLong (DateTime.Now) - LastSysTime.Value;
		if (MaxDiff < LastCountTime.Value) {
			MaxDiff = LastCountTime.Value;
			MaxDiff = diffLocalTime > MaxDiff ? diffLocalTime : MaxDiff;
		}


		if (MaxDiff > DelayTime) {
			IsActive.Save (0L);
		} else {
			if (MaxDiff > LastCountTime.Value) {
				LastCountTime.Save (MaxDiff);
			}
            
			if (MaxDiff > diffLocalTime) {
				LastSysTime.Value -= (MaxDiff - diffLocalTime);
				LastSysTime.Save ();
			}
            
			if (MaxDiff > diffPhoneStartTime) {
				LastPhoneRunTime.Value -= (MaxDiff - diffPhoneStartTime);
				LastPhoneRunTime.Save ();
			}
            
		}
	}
    
	public bool IsEffective ()
	{
		return IsActive.Value == 1L;
	}

	public bool CheckIsEffective ()
	{
		if (!IsEffective ()) {
			return false;
		} else {
			long MaxDiff = SkyTime.realTimeSinceOn () - LastPhoneRunTime.Value;
			if (MaxDiff > DelayTime) {
				IsActive.Save (0L);
				return false;
			} else {
				return true;
			}
		}
		return false;
	}

	public void CheckActivityStart ()
	{
		if (CheckIsEffective ()) {
			if (Overlapped) {
				ActivityStart ();
			} else {
				IsActive.Save (0L);
			}
		} else {
			ActivityStart ();
		}
	}

	public void ActivityStart ()
	{
		IsActive.Save (1L);
		LastSysTime.Save (SkyTime.ConvertDateToLong (DateTime.Now));
		LastPhoneRunTime.Save (SkyTime.realTimeSinceOn ());
		LastCountTime.Save (0L);
	}

	public void OnQuit ()
	{
		if (IsEffective ()) {
			long diffPhoneStartTime = SkyTime.realTimeSinceOn () - LastPhoneRunTime.Value;
			long MaxDiff = diffPhoneStartTime;
			if (MaxDiff > DelayTime) {
				IsActive.Save (0L);
			} else {
				LastCountTime.Save (MaxDiff);
			}
		}
	}
        
	public long GetLeftTime ()
	{
		if (CheckIsEffective ()) {
			long diffPhoneStartTime = SkyTime.realTimeSinceOn () - LastPhoneRunTime.Value;
			return DelayTime - diffPhoneStartTime;
		}
		return 0;
	}

	public override string ToString ()
	{
		return (LastSysTime.ToString() + "\n" + LastPhoneRunTime.ToString() +"\n" + LastCountTime.ToString()+"\n" + IsActive.ToString());
	}
}
