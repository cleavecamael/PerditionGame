using UnityEngine;

[CreateAssetMenu(fileName = "OutOfOxygenConstants", menuName = "ScriptableObjects/OutOfOxygenConstants", order = 7)]
public class OutOfOxygenConstants : ScriptableObject
{
    public float chromaticAberrationMax = 0.8f;
    public float chromaticAberrationRate = 0.05f;
    public float bloomMax = 1f;
    public float bloomRate = 0.01f;
    public float paniniProjectionMax = 1f;
    public float paniniProjectionRate = 0.01f;
    public float lensDistortionMax = 0.7f;
    public float lensDistortionRate = 0.01f;
    public float filmGrainMax = 1f;
    public float maxTimeTaken = 15f;
    public float maxCanvasAlpha = 0.05f;
    public float canvasRate = 0.01f;
}