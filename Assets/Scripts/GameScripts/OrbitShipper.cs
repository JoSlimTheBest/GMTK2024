using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitShipper : MonoBehaviour
{
    public Transform aroundPoint;//вокруг какого объекта крутиться

    public float circle = 1f;//кругов в секунду



    float dist; //радиус
    float circleRadians = Mathf.PI * 2; //движение по радиусу
    public float currentAng = 0; //текущий градус
                                 //public Rigidbody targetmass; //Масса планеты
                                 //public float coefMass = 0.01f; //Отношении массы к скорости вращения


    private void Start()
    {
        //targetmass = GetComponent<Rigidbody>();
        dist = (transform.position - aroundPoint.position).magnitude;
    }

    private void FixedUpdate()
    {
        Vector3 p = aroundPoint.position;
        currentAng += circleRadians * circle * Time.deltaTime; //* targetmass.mass * coefMass;
        p.x += Mathf.Sin(currentAng) * dist;
        p.y += Mathf.Cos(currentAng) * dist;
       transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, p - transform.position), Time.deltaTime);
        transform.position = p;


    }
}
