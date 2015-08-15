using UnityEngine;
using System.Collections;

public interface SkyAction
{
	bool Loop {
		get;
		set;
	}

	bool AutoRun {
		get;
		set;
	}

	float PlayTime {
		get;
		set;
	}

	float DelayTime {
		get;
		set;
	}

	float AutoStartDelayTime {
		get;
		set;
	}

	SkyAniDuration PositionSkyAniDuration {
		get;
		set;
	}

	SkyAniCallBack DelayCallBack {
		get;
		set;
	}

	SkyAniCallBack PlayCallBack {
		get;
		set;
	}

	SkyBaseSequence  ParentAction {
		get;
		set;
	}

	void Init ();

	void PlayLoop ();
	
	void Play ();
	
	void Delay ();

	void PlayNext ();

	void RemoveFromParent ();
}
