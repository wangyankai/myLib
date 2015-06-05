using UnityEngine;
using System.Collections;

public class SharedPrefs
{
	public int GetInt (string key)
	{
		return GetInt (key,0);
	}

	public int GetInt (string key,int defaultValue)
	{
		return PlayerPrefs.GetInt (key,defaultValue);
	}


	public float GetFloat (string key)
	{
		return GetFloat (key,0f);
	}

	public float GetFloat (string key,float defaultValue)
	{
		return PlayerPrefs.GetFloat (key,defaultValue);
	}


	public string GetString (string key)
	{
		string empty = string.Empty;
		return GetString (key,string.Empty);
	}

	public string GetString (string key,string defaultValue)
	{
		return PlayerPrefs.GetString (key,defaultValue);
	}

	public static long GetLong (string key){
		return GetLong (key,0L);
	}

	public static long GetLong (string key, long defaultValue)
	{
		string strValue = PlayerPrefs.GetString (key, null);
		if (strValue == null) {
			return defaultValue;
		}
		long result = defaultValue;
		if (long.TryParse (strValue, out result)) {
			return result;
		} else {
			return defaultValue;
		}
	}

	public static void SetFloat (string key, float value)
	{
		PlayerPrefs.SetFloat (key, value);
	}

	public static void SaveFloat (string key, float value)
	{
		PlayerPrefs.SetFloat (key, value);
		Save ();
	}

	public static void SetInt (string key, int value)
	{
		PlayerPrefs.SetInt (key, value);
	}

	public static void SaveInt (string key, int value)
	{
		PlayerPrefs.SetInt (key, value);
		Save ();
	}

	public static void SetString (string key, string value)
	{
		PlayerPrefs.SetString (key, value);
	}

	public static void SaveString (string key, string value)
	{
		PlayerPrefs.SetString (key, value);
		Save ();
	}


	public static void SetLong (string key, long value)
	{
		PlayerPrefs.SetString (key, value.ToString ());
	}

	public static void SaveLong (string key, long value)
	{
		PlayerPrefs.SetString (key, value.ToString ());
		Save ();
	}

	public static  void DeleteAll ()
	{
		PlayerPrefs.DeleteAll ();
	}

	public static void DeleteKey (string key)
	{
		PlayerPrefs.DeleteKey (key);
	}

	public static void Save ()
	{
		PlayerPrefs.Save ();
	}

	public static  bool HasKey (string key)
	{
		return PlayerPrefs.HasKey (key);
	}
}
