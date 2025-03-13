using UnityEngine;

namespace LastBreakthrought.Logic.InteractionZone
{
    [RequireComponent(typeof(SphereCollider))]
    public class ZoneActivator : MonoBehaviour
    {
        [SerializeField] private InterationZoneView _interationZoneView;

        private void OnValidate()
        {
            GetComponent<SphereCollider>().radius = 1.25f * _interationZoneView.transform.localScale.x;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                _interationZoneView.Show();
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                _interationZoneView.Hide();
        }
    }
}
