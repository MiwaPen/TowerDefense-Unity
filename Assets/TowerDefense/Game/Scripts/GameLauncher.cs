using UnityEngine;

public class GameLauncher : MonoBehaviour {
    [SerializeField] private int _maxFrameRate;
    void Start() {
        Application.targetFrameRate = _maxFrameRate;
    }
}
