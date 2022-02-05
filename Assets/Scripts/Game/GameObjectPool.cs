using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameObjectPool : MonoBehaviour
    {
        public GameObject prefab;
        private Stack<GameObject> objects;

        public void Initialize(int stackSize = 0)
        {
            objects = new Stack<GameObject>(stackSize);
            for (int i = 0; i < stackSize; i++)
            {
                objects.Push(GetNewInstance());
            }
        }

        public GameObject Pull()
        {
            GameObject jaba = objects.Count > 0 ? objects.Pop() : GetNewInstance();

            jaba.SetActive(true);
            return jaba;
        }
        
        public T Pull<T>() where T : Component
        {
            return Pull().GetComponent<T>();
        }

        public void Push(GameObject jaba)
        {
            jaba.SetActive(false);
            objects.Push(jaba);
        }

        private GameObject GetNewInstance()
        {
            return Instantiate(prefab, transform);
        }
    }
}