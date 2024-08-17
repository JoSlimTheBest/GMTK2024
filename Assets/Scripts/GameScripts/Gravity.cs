using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{

    public List<Rigidbody> gravityBodies = new List<Rigidbody>(); //лист объектов которые притягиваются к планете
    private static List<Rigidbody> sunBodies = new List<Rigidbody>(); //лист объектов которые притягиваються к солнцу
    private Rigidbody componentRigidbody;
  //  public PlanetCharacter thisPlanet;
    public float G = 6.667f; //гравитационная постоянная
    public float strenghtAccelerationMin = 0.00001f; //минимальная сила притяжения
    public int rotate = 1;
    public int howMuch;
    // public float strenghtCentrAcceleration = 1f; // изменение формулы притяжения, использовать если хотим неправдивую физику
    //static bool inPlanetGravity = false;
    //public Text CHAT;

    private void Start()
    {
        componentRigidbody = GetComponent<Rigidbody>();
        if (gameObject.name != "Sun")
        {
          //  thisPlanet = GetComponentInChildren<PlanetCharacter>();
        }


    }

    private void OnTriggerEnter(Collider other)
    {

        // Debug.Log("ontrigerENTER: " + this.name);
        if (other.attachedRigidbody != null)
        {
            if (other.attachedRigidbody.isKinematic == false)
            {
                if (this.name != "Sun")
                {

                    gravityBodies.Add(other.attachedRigidbody);
                    sunBodies.Remove(other.attachedRigidbody);

                    /* SDES
                   if (other.attachedRigidbody.GetComponentInChildren<ShipCharacter>() != null && other.attachedRigidbody.GetComponentInChildren<ShipCharacter>().mainPlayerIn == true)
                   {
                       _console.NewTextEvent("Detected Planet Gravity \n", 0, "Обнаружено притяжение планеты \n"); // TextTODO add planet name
                       _mainPlayer.GetComponent<MyCameraManager>().CameraNow(1);
                       _mainPlayer.GetComponent<MenuPlanetControl>().PlanetIs(gameObject);
                       if (_mainPlayer.GetComponent<MainPlayer>().shipNow.GetComponent<ShipController>().destroyMode == true)
                       {
                           _mainPlayer.GetComponent<MyCameraManager>().CameraNow(5);
                       }
                       // cosmosCamera.Priority = 0;
                       // Debug.Log("стандарт");
                   }

                   if (other.gameObject.GetComponent<Asteroid>() != null || other.gameObject.GetComponent<Comet>() != null)
                   {
                       Debug.Log("Asteroid take");
                       if (thisPlanet.mainStation == true)
                       {
                           _mainUImanager.GetComponent<MinimapImage>().AddAsteroid(other.gameObject);
                           Debug.Log("Asteroid use");
                       }
                   }
                     */

               }
               else
               {
                   // gravityBodies.Remove(other.attachedRigidbody);
                   sunBodies.Add(other.attachedRigidbody);
                    /* SDES
                 if (other.attachedRigidbody.GetComponentInChildren<ShipCharacter>() != null && other.attachedRigidbody.GetComponentInChildren<ShipCharacter>().mainPlayerIn == true)
                 {
                     _mainPlayer.GetComponent<MyCameraManager>().CameraNow(0);
                     _mainPlayer.GetComponent<MenuPlanetControl>().PlanetIs(null);

                     // cosmosCamera.Priority = 1;
                 }
                       */
                }
            }


     }



     // if (this.name != "Sun" && other.name == "MainPlayer")
     // {
     //inPlanetGravity = true;

     // Debug.Log("TRUE: " + this.name );
     // }

 }

 private void OnTriggerExit(Collider other)
 {
     if (other.attachedRigidbody == null)
     {
         gravityBodies.Remove(other.attachedRigidbody);
         sunBodies.Remove(other.attachedRigidbody);
         return;
     }

     if (other.attachedRigidbody.isKinematic == false)
     {
         // Debug.Log("ontrigerEXIT: " + this.name);
         if (other.attachedRigidbody != null)
         {

             if (this.name != "Sun")
             {

                 gravityBodies.Remove(other.attachedRigidbody);
                 if (!sunBodies.Contains(other.attachedRigidbody)) //|| !gravityBodies.Contains(other.attachedRigidbody)
                 {
                     sunBodies.Add(other.attachedRigidbody);
                 }
                   /* SDES
                 if (other.attachedRigidbody.GetComponentInChildren<ShipCharacter>() != null && other.attachedRigidbody.GetComponentInChildren<ShipCharacter>().mainPlayerIn == true)
                 {
                     _mainPlayer.GetComponent<MyCameraManager>().CameraNow(0);
                     _mainPlayer.GetComponent<MenuPlanetControl>().PlanetIs(null);

                     //cosmosCamera.Priority = 1;
                 }
                   */

                }
                else
                {

                    sunBodies.Remove(other.attachedRigidbody);
                   // Camera.main.GetComponent<MyCameraManager>().CameraNow(2);
                    /* SDES
                   if (other.attachedRigidbody.GetComponentInChildren<ShipCharacter>() != null && other.attachedRigidbody.GetComponentInChildren<ShipCharacter>().mainPlayerIn == true)
                   {
                       _mainPlayer.GetComponent<MyCameraManager>().CameraNow(1);
                       //_mainPlayer.GetComponent<MenuPlanetControl>().PlanetIs(gameObject);
                       Debug.Log("StandEXIT");
                       //cosmosCamera.Priority = 1;
                   }
                    */
                }
            }
            if (other.attachedRigidbody == null)
            {
                gravityBodies.Remove(other.attachedRigidbody);
                sunBodies.Remove(other.attachedRigidbody);

            }
        }
       


    }


  
    private void FixedUpdate()

    {
        howMuch = sunBodies.Count;
        List<Rigidbody> attachedBodies;
        if (this.name == "Sun")
        {
            attachedBodies = sunBodies;

        }
        else
        {
            attachedBodies = gravityBodies;
        }


        for (int i = 0; i < attachedBodies.Count; i++)
        {
            Rigidbody body = attachedBodies[i];


            if (body != null) // &&(inPlanetGravity == true && this.name != "Sun" ||  inPlanetGravity == false && this.name == "Sun"))

            {

                Vector3 directionToPlanet = (transform.position - body.position).normalized;

                Vector3 normal = transform.forward.normalized;

                Vector3 forceCentr = Vector3.Cross(directionToPlanet, normal).normalized;

                float distance = (transform.position - body.position).sqrMagnitude;
                float strenght = (G * body.mass * componentRigidbody.mass) / distance;

                if (strenght < strenghtAccelerationMin)
                    strenght = strenghtAccelerationMin;

                var forceGravity = (-forceCentr * rotate + directionToPlanet) * strenght;
                body.AddForce(forceGravity);

               


            }
            else
            {

                

                attachedBodies.Remove(body);





            

            }
        }
      
    }
}
