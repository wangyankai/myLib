using UnityEngine;
using System.Collections;

public interface SkyAction
{
	void Init ();

	void PlayLoop ();
	
	void Play ();
	
	void DelayAction ();

	void PlayNextAction ();

	void SetAniamtionSeqence (SkyBaseSequence skyAniSequence);

	void RemoveFromSeqence ();

	bool IsLoop ();

	void SetLoop (bool isLoop);

	bool IsAutoRun ();

	void SetAutoRun (bool isAutoRun);

	float GetPlayTime ();

	void SetPlayTime (float playTime);

	float GetDelayTime ();

	void SetDelayTime (float delayTime);

}
