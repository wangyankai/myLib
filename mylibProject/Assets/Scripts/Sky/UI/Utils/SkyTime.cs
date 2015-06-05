using UnityEngine;
using System.Collections;
using System;

public class SkyTime
{
	public static long SECOND = 1L;
	public static long MINUTE = SECOND * 60L;
	public static long HOUR = MINUTE * 60L;
	public static long DAY = HOUR * 24L;

	public  static int realTimeSinceOn ()
	{
		return (int)Time.realtimeSinceStartup;
	}
	
	public static long ConvertDateToLong (System.DateTime time)
	{  
		DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime (new DateTime (1970, 1, 1));  
		return (long)((time - startTime).TotalSeconds);  
	}

	public static DateTime ConvertLongToDate (long time)
	{
		DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime (new DateTime (1970, 1, 1));
		long lTime = long.Parse (time + "0000000");
		TimeSpan toNow = new TimeSpan (lTime);
		DateTime dtResult = dtStart.Add (toNow);
		return dtResult;
	}
}
