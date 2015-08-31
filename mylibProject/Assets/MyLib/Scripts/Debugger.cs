using UnityEngine;
using System.Collections;

public class Debugger  {

	static const public int ALL = 0;
	static const public int DEBUG =1;
	static const public int INFO =2;
	static const public int WARN =3;
	static const public int ERROR =4;
	static const public int FATAL =5;
	static const public int OFF = 100;
	
	static public int DEBUG_LEVEL = ALL;

	static public void Log(object message)
	{
		Log(message,null);
	}
	static public void Log(object message, Object context)
	{
		if(DEBUG>=DEBUG_LEVEL)
		{
			Debug.Log(message,context);
		}
	}

	static public void LogInfo(object message)
	{
		LogInfo(message,null);
	}
	static public void LogInfo(object message, Object context)
	{
		if(INFO>=DEBUG_LEVEL)
		{
			Debug.Log(message,context);
        }
    }

    static public void LogWarning(object message)
    {
        LogWarning(message,null);
    }
    static public void LogWarning(object message, Object context)
    {
		if(WARN>=DEBUG_LEVEL)
        {
            Debug.LogWarning(message,context);
        }
    }


	static public void LogError(object message)
	{
		LogError(message,null);
	}
	static public void LogError(object message, Object context)
	{
		if(ERROR>=DEBUG_LEVEL)
		{
			Debug.LogError(message,context);
		}
	}

	static public void LogFatal(object message)
	{
		LogFatal(message,null);
	}
	static public void LogFatal(object message, Object context)
	{
		if(FATAL>=DEBUG_LEVEL)
		{
			Debug.LogError(message,context);
        }
    }
}