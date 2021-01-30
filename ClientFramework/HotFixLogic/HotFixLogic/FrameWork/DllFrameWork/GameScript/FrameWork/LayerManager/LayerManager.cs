using UnityEngine;
using System.Collections;

public class LayerManager
{
	public static int Default = LayerMask.NameToLayer ("Default"); 
	public static int CharacterLayer = LayerMask.NameToLayer("CharacterLayer");
    public static int NonPlayerCharacterLayer = LayerMask.NameToLayer("NonPlayerCharacterLayer"); 
	public static int SenceLayer=LayerMask.NameToLayer("SenceLayer"); 
	public static int TerrainLayer=LayerMask.NameToLayer("TerrainLayer"); 	
    public static int UILayer = LayerMask.NameToLayer("UI");

	public static int LayerToMask(int layer)
	{
		return 1 << layer;
	}
}