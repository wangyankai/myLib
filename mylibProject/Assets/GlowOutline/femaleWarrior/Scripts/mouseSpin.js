var slider1Script : slider1;

function Update () {
	if(Input.GetMouseButton(0) && !slider1Script.InUse()){
		transform.rotation.eulerAngles.y += Input.GetAxis("Mouse X") * Time.deltaTime * -100.0;
	}
}