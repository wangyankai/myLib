var stickTo : Transform;
var disabled : boolean;
private var resetPosition : Vector3;

function Start(){
	resetPosition = transform.localPosition;
}

function Update () {
	if(disabled){
		transform.localPosition = resetPosition;
	}
	else{
		transform.position = stickTo.position;
	}
}