using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitShipper : MonoBehaviour
{
    public Transform aroundPoint;//������ ������ ������� ���������

    public float circle = 1f;//������ � �������



    float dist; //������
    float circleRadians = Mathf.PI * 2; //�������� �� �������
    public float currentAng = 0; //������� ������
                                 //public Rigidbody targetmass; //����� �������
                                 //public float coefMass = 0.01f; //��������� ����� � �������� ��������


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
