var direction : Vector3;
function Update () {
		var zLocalRotation : float = transform.localRotation.eulerAngles.z;
		transform.localRotation.eulerAngles.z = 0.0;
		transform.position += transform.TransformDirection(direction) *  Time.deltaTime;
		transform.localRotation.eulerAngles.z = zLocalRotation;		
}