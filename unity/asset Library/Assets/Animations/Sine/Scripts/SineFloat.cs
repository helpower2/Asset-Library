using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SineEventFloat : UnityEvent<float>  { }
public class SineFloat : MonoBehaviour
{
    public float sinePulseValue, speed;
    public SineEventFloat Sine = new SineEventFloat();
    public float Speed { get { return speed; } set { speed = value; } }
    public float SinePulseValue { get { return sinePulseValue; } set { sinePulseValue = value; } }
    private float time;

    private void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        Sine.Invoke((CalculateSine(sinePulseValue, speed)));
    }

    public float CalculateSine(float _sinePulseValue, float _speed, float _baseSize = 1f)
    {
        return (_baseSize + Mathf.Sin(time * _speed) * _sinePulseValue / _speed);
    }
}