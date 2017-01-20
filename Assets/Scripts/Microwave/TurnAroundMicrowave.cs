using UnityEngine;

namespace Microwave
{
    public class TurnAroundMicrowave : MonoBehaviour
    {
        public GameObject microwaveFront;
        public GameObject microwaveBack;

        public void FlipAround()
        {
            microwaveFront.SetActive(microwaveBack.activeSelf);
            microwaveBack.SetActive(!microwaveBack.activeSelf);
        }
    }
}