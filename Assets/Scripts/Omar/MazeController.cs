// @desc Maze Controller Algorithm
// @author Omar Huseynov
// @date 17th May 2020

using System.Collections.Generic;
using UnityEngine;

public interface Observable
{
        void addObserver(Observer o);
        void removeObserver(Observer o);
        void notifyObservers();
}

public class MazeController : MonoBehaviour, Observable
{
        public float m_speed;
        public float m_smoothness;

        private List<Observer> m_observers;

        public void Start()
        {
                m_observers = new List<Observer>();
        }

        public void addObserver(Observer o)
        {
                if (!m_observers.Contains(o))
                {
                        m_observers.Add(o);
                        m_observers.Sort();
                }
        }

        public void removeObserver(Observer o)
        {
                int index = m_observers.BinarySearch(o);
                if (-1 != index)
                        m_observers.RemoveAt(index);
        }

        public void notifyObservers()
        {
                foreach (Observer o in m_observers)
                        o.update(transform.rotation.eulerAngles);
        }

        public void Update()
        {
                float hmovement = Input.GetAxis("Horizontal");
                float vmovement = Input.GetAxis("Vertical");

                if (hmovement != 0.0f || vmovement != 0.0f)
                {
                        Vector3 rotator = new Vector3(vmovement, 0.0f, -hmovement);
                        transform.Rotate(rotator * m_speed * Time.deltaTime);
                        notifyObservers();
                }
        }
}
