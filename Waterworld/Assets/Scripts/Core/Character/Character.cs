using UnityEngine;
using System.Collections.Generic;
using System;

public class CharacterDataContainer: DataContainer<CharacterData>{
	[SerializeField] CharacterData data;
	public override CharacterData GetData() {
        return data;
    }
}

[Serializable]
public class CharacterData{
	//def props

	//def skills

	//def params count based on props and skills
	//ap


}

public class Character : MonoBehaviour {

}
