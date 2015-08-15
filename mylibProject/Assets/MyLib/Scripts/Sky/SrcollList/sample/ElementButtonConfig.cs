using UnityEngine;
using System.Collections;
using UI.UIComponent.ScrollList;
using System;

public enum ButtonConfigType{
	NODefine,BuyCoins
};


public class ElementButtonConfig : SkyElementConfig
{
	[Serializable]
	public  class ConfigInf{
		public Sprite sprite;
		public ButtonConfigType buttonConfigType;
        public string desc ;
		public bool IsSpecial = false;

		public ConfigInf(Sprite sprite,ButtonConfigType buttonConfigType){
			this.sprite = sprite;
			this.buttonConfigType = buttonConfigType;
		}

		public ConfigInf(Sprite sprite,String buttonConfigType){
			this.sprite = sprite;
			this.buttonConfigType = (ButtonConfigType)Enum.Parse(typeof(ButtonConfigType), buttonConfigType);
		}
	}

		public ConfigInf[] ConfigInfs;

		public override int getCount ()
		{ 
		if (ConfigInfs != null)
			return ConfigInfs.Length;
				return 0;
		}



	   private  static void OpenShopPanel(){
//		   LobbyController.GetInstance ().BuyCoinsClicked();
		}



	public static void ChoiseEvent(ButtonConfigType buttonConfigType){
		switch(buttonConfigType){
		case ButtonConfigType.BuyCoins:
			OpenShopPanel();
			break;
		default:
			Debug.Log ("Click for nothing");
			break;
		}
	}
}
