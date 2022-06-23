using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Victor.Hololens.HandFocusExample
{
    public class GameManager : MonoBehaviour, IMixedRealityPointerHandler
    {
        public GameObject MyPrefab;

        
        public void OnPointerClicked(MixedRealityPointerEventData eventData)
        {
            Debug.Log("OnPointerClicked");
            var result = eventData.Pointer.Result;
            var spawnPosition = result.Details.Point;
            var spawnRotation = Quaternion.LookRotation(result.Details.Normal);
            Instantiate(MyPrefab, spawnPosition, spawnRotation);
        }

        public void OnPointerDown(MixedRealityPointerEventData eventData)
        {
            Debug.Log("OnPointerDown");
        }

        public void OnPointerDragged(MixedRealityPointerEventData eventData)
        {
            Debug.Log("OnPointerDragged");
        }

        public void OnPointerUp(MixedRealityPointerEventData eventData)
        {
            Debug.Log("OnPointerUp");
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
