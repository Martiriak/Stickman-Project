using System.Collections;
using UnityEngine;

namespace Stickman
{
    public class TMP_Platform : MonoBehaviour
    {
        private void Start() => StartCoroutine(Gianni());

        private IEnumerator Gianni()
        {
            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
        }
    }
}
