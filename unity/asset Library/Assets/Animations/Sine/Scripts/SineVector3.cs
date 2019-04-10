using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SineEventVector3 : UnityEvent<Vector3>  { }
public enum Exes { X, Y, Z, XY, XZ, YZ, All}
public class SineVector3 : MonoBehaviour
{
    [SerializeField] private float sinePulseValue, speed;
    [SerializeField] private Exes exes;
    public float Speed { get { return speed; } set { speed = value; } }
    public float SinePulseValue { get { return sinePulseValue; }set { sinePulseValue = value; } }
    public SineEventVector3 Sine = new SineEventVector3();

    private float time;

    private void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        switch (exes)
        {
            case Exes.X:
                Sine.Invoke(Vector3.right * CalculateSine(sinePulseValue, speed));
                break;
            case Exes.Y:
                Sine.Invoke(Vector3.up * CalculateSine(sinePulseValue, speed));
                break;
            case Exes.Z:
                Sine.Invoke(Vector3.forward * CalculateSine(sinePulseValue, speed));
                break;
            case Exes.XY:
                Sine.Invoke(new Vector3(1, 1, 0) * CalculateSine(sinePulseValue, speed));
                break;
            case Exes.XZ:
                Sine.Invoke(new Vector3(1, 0, 1) * CalculateSine(sinePulseValue, speed));
                break;
            case Exes.YZ:
                Sine.Invoke(new Vector3(0, 1, 1) * CalculateSine(sinePulseValue, speed));
                break;
            case Exes.All:
                Sine.Invoke(Vector3.one * CalculateSine(sinePulseValue, speed));
                break;
        }
        
    }

    public float CalculateSine(float _sinePulseValue, float _speed, float _baseSize = 1f)
    {
        return (_baseSize + Mathf.Sin(time * _speed) * _sinePulseValue / _speed);
    }

}