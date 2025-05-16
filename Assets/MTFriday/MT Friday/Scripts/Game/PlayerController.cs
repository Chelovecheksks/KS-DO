using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Image _screamerImage;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
        Initzialize();
    }

    private void Initzialize()
    {
        /*_screamerImage.gameObject.SetActive(false);
        _screamerImage.sprite = null;
        _audioSource.Stop();
        _audioSource.clip = null;*/
    }

    public IEnumerator Scream(Sprite screamerSprite, float screamerTime, AudioClip screamerAudio)
    {
        _screamerImage.sprite = screamerSprite;
        _screamerImage.gameObject.SetActive(true);
        _audioSource.clip = screamerAudio;
        _audioSource.Play();

        yield return new WaitForSeconds(screamerTime);

        _screamerImage.gameObject.SetActive(false);
        _screamerImage.sprite = null;
        _audioSource.Stop();
        _audioSource.clip = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            Debug.Log("YYYYYYYYYYYYYYYYYYYYRRRRRRRRRRRRRRRRRRRAAAAAAAAAAAAAAAAAAAAAAAAAAA POLYCHILOSS!!!!!!!!!");
            FindObjectOfType<PlayerManager>().CollectItem();
            Destroy(item.gameObject);
        }
    }
}
