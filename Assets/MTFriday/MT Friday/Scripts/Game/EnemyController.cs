using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private Sprite _screamerImage;
    [SerializeField] private AudioClip _screamerAudioClip;
    [SerializeField] private float _screamerDuration;
    [SerializeField] private bool _canKillPlayer;
    [SerializeField] private float _catchDistance;

    [SerializeField] private float _detectionDistance;
    private GameObject _player;

    private EnemyMovement _enemyMovement;
    private PhotonView _photonView;

    private void Start()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
        _photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (_player == null)
            _enemyMovement.SetHaveTarget(false);
        else
            _enemyMovement.SetHaveTarget(true);

        CheckPlayerInDetectionDistance();
    }

    private void CheckPlayerInDetectionDistance()
    {
        if (_player == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _detectionDistance);

            foreach (Collider collider in colliders)
            {
                //if (collider.GetComponent<FirstPersonContoller>)
                if (collider.gameObject.GetComponent<PlayerMovement>())
                {
                    _player = collider.gameObject;
                    return;
                }
            }
        }
        else
        {
            float distance = Vector3.Distance(_player.transform.position, transform.position);
            if (distance > _detectionDistance)
            {
                _player = null;
                return;
            }
            else if (distance < _catchDistance)
            {
                CaughtPlayer(_player);
            }
            _enemyMovement.SetDestination(_player.transform.position);
        }
    }

    private void CaughtPlayer(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();

        playerController.Scream(_screamerImage, _screamerDuration, _screamerAudioClip);

        if (_canKillPlayer && _photonView.IsMine)
        {
            PlayerManager playerManager = FindObjectOfType<PlayerManager>();
            playerManager.KillPlayer();
        }
        FindObjectOfType<EnemyManager>().KillEnemy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _detectionDistance);
    }
}
