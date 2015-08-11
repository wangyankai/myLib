var amount : float;
private var inUse : boolean = false;
private var pixelInsetRange : Vector2 = Vector2(-0.215,100);
function Update () {
	var 	sliderPosition : Vector3;
	sliderPosition.x = transform.position.x * Screen.width + guiTexture.pixelInset.x + guiTexture.pixelInset.width * 0.5;
	sliderPosition.y = transform.position.y * Screen.height + guiTexture.pixelInset.y + guiTexture.pixelInset.height * 0.5;
	
	if(Vector3.Distance(sliderPosition, Input.mousePosition) < 12 && Input.GetMouseButtonDown(0)){
		inUse = true;
	}
	if(inUse){
		guiTexture.pixelInset.x = Input.mousePosition.x - transform.position.x * Screen.width - guiTexture.pixelInset.width * 0.5;
		amount = guiTexture.pixelInset.x / (pixelInsetRange.y - pixelInsetRange.x);
		if(Input.GetMouseButtonUp(0)){
			inUse = false;
		}
	}
	amount = Mathf.Clamp01(amount);
	guiTexture.pixelInset.x = Mathf.Lerp(-0.215,100,amount);
}

function InUse(){
	return inUse;
}
