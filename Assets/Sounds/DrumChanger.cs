using UnityEngine;

public class DrumChanger : MonoBehaviour
{
    public GameObject withDrum;
    public GameObject withoutDrum;
    public float timeToChangeTrack;
    public float maxVolume;

    private AudioSource _withDrum;
    private AudioSource _withoutDrum;
    private bool _fadingOut = false;


    private int _addingDrumStatus = 0;
    // Start is called before the first frame update
    void Start()
    {
        _withDrum = withDrum.GetComponent<AudioSource>();
        _withoutDrum = withoutDrum.GetComponent<AudioSource>();
    }

    void ChangeBeat()
    {
        if (_addingDrumStatus == 0)
        {
            return;
        }
        else if (_addingDrumStatus == 1)
        {
            float deltaVolume = (maxVolume / timeToChangeTrack) * Time.deltaTime;
            float newDrumVolume = _withDrum.volume + deltaVolume;
            if (newDrumVolume >= maxVolume)
            {
                _withDrum.volume = maxVolume;
                _withoutDrum.volume = 0;
            }
            else
            {
                _withDrum.volume = newDrumVolume;
                _withoutDrum.volume = _withoutDrum.volume - deltaVolume;
            }
        }
        else
        {
            float deltaVolume = (maxVolume / timeToChangeTrack) * Time.deltaTime;
            float newDrumVolume = _withoutDrum.volume + deltaVolume;
            if (newDrumVolume >= maxVolume)
            {
                _withoutDrum.volume = maxVolume;
                _withDrum.volume = 0;
            }
            else
            {
                _withoutDrum.volume = newDrumVolume;
                _withDrum.volume = _withDrum.volume - deltaVolume;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChangeBeat();

        if (_fadingOut)
        {
            FadeOut();
        }

       
    }

    public void AddDrum()
    {
        _addingDrumStatus = 1;
    }

    public void RemoveDrum()
    {
        _addingDrumStatus = -1;
    }

    public void StartFadeOut()
    {
        _fadingOut = true;
    }

    void FadeOut()
    {
        float deltaVolume = (maxVolume / timeToChangeTrack) * Time.deltaTime;

        if (_withoutDrum.volume - deltaVolume <= 0)
        {
            _withoutDrum.volume = 0;
        }
        else
        {
            _withoutDrum.volume -= deltaVolume;
        }

        if (_withDrum.volume - deltaVolume <= 0)
        {
            _withDrum.volume = 0;
        }
        else
        {
            _withDrum.volume -= deltaVolume;
        }
    }

}
