using UnityEngine;
using System.Collections;
using DG.Tweening;

public static class ExtendClass{
	public static void ExtendMethod(this TestExtend testExtend){
	}

	public static Tweener DOX(this TestExtend testExtend){
		return DOTween.To (() => testExtend.testX, delegate (float x)
		                   {
			testExtend.testX = x;
		}, 1, 1).SetTarget (testExtend);
	}

}
