/*
 * File:			GAFAssetPostProcessor.cs
 * Version:			2.0
 * Last changed:	2015/2/17 9:59
 * Author:			Niktin.Nikolay
 * Copyright:		© GAFMedia
 * Project:			GAF Unity plugin
 */

using UnityEditor;
using UnityEngine;

using System.IO;
using System.Linq;

using GAF.Assets;
using GAFInternal.Reader;

using GAFEditorInternal.Assets;

namespace GAFEditor.Assets
{
	public class GAFAssetPostProcessor : AssetPostprocessor
	{
		public static void OnPostprocessAllAssets(
			  string[] importedAssets
			, string[] deletedAssets
			, string[] movedAssets
			, string[] movedFromAssetPaths)
		{
			foreach (string assetName in importedAssets)
			{
				if (assetName.EndsWith(".gaf"))
				{
					byte[] fileBytes = null;
					using (BinaryReader freader = new BinaryReader(File.OpenRead(assetName)))
					{
						fileBytes = freader.ReadBytes((int)freader.BaseStream.Length);
					}

					if (fileBytes.Length > sizeof(int))
					{
						int header = System.BitConverter.ToInt32(fileBytes.Take(4).ToArray(), 0);
						if (GAFHeader.isCorrectHeader((GAFHeader.CompressionType)header))
						{
							var path = Path.GetDirectoryName(assetName) + "/" + Path.GetFileNameWithoutExtension(assetName) + ".asset";
							var asset = AssetDatabase.LoadAssetAtPath(path, typeof(GAFAnimationAsset)) as GAFAnimationAsset;
							if (asset == null)
							{
								asset = ScriptableObject.CreateInstance<GAFAnimationAsset>();
								AssetDatabase.CreateAsset(asset, path);
								asset = AssetDatabase.LoadAssetAtPath(path, typeof(GAFAnimationAsset)) as GAFAnimationAsset;
							}

							asset.name = Path.GetFileNameWithoutExtension(assetName);
							asset.initialize(fileBytes, AssetDatabase.AssetPathToGUID(path));
							EditorUtility.SetDirty(asset);
							AssetDatabase.SaveAssets();

							GAFResourceManagerInternal.instance.createResources<GAFTexturesResource>(asset);

							EditorUtility.SetDirty(asset);
							AssetDatabase.SaveAssets();
						}
					}
				}
			}
		}
	}
}