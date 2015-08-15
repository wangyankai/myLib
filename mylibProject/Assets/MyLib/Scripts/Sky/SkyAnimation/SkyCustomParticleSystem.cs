using UnityEngine;
using System.Collections;

[RequireComponent( typeof(ParticleSystem) )]
public class SkyCustomParticleSystem : MonoBehaviour
{
	public float  EmissionRate = 10f;
	float          _timeToEmission = 0f;
	ParticleSystem _particleSystem;
	
	float EmissionPeriod { get { return 1f / EmissionRate; } }
	
	void Awake ()
	{
		_particleSystem = GetComponent<ParticleSystem> ();
		_particleSystem.emissionRate = 0f;
		_timeToEmission = EmissionPeriod;
	}

	float ratio = 1f;
	private float parentHight = 1;
	private float parentWidth = 1;
	private float fixedBorad = 0.75f;

	void Start ()
	{ 
		getNewSize ();
	}
	
	void Update ()
	{
		if (_particleSystem.isPlaying) {
			GenrateParticle ();
		}
	}

	public virtual  void GenrateParticle ()
	{
		_timeToEmission -= Time.deltaTime;
		if (_timeToEmission <= 0f) {
			_timeToEmission = EmissionPeriod - _timeToEmission;
			
			Vector3 randomPosition = new Vector3 (Random.Range (-ratio, ratio), Random.Range (-fixedBorad, fixedBorad), 0);
			_particleSystem.Emit (
				randomPosition,
				Vector3.zero, 
				_particleSystem.startSize,
				_particleSystem.startLifetime,
				_particleSystem.startColor
			);
		}
	}

	public void getNewSize ()
	{
		RectTransform parentTransform = transform.parent.transform as RectTransform;
		parentHight = parentTransform.rect.height;
		parentWidth = parentTransform.rect.width;
		ratio = (parentWidth / parentHight) * fixedBorad;
	}
}
