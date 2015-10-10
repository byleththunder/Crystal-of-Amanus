using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Game States/States")]
public class GameStates : MonoBehaviour {

    public enum CharacterState { Playing, DontMove, Defense };
}
