using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour{
    Vector3 touchStart;
    //limite de zoom in
    public float zoomLowerLimit = 1;
    //limite de zoom out
    public float zoomHigherLimit = 12;

    float zoomMultiplier = 1.25f;

    // Update is called once per frame
    void Update(){
        //cuando el usuario hace tap/clic
        if (Input.GetMouseButtonDown(0)) {
            //obtenemos la posicion donde esta
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        //si el usuario "pinchea" la pantalla. digamos, para zoom in o zoom out tactil
        if (Input.touchCount == 2) {
            //conseguimos el touch de un dedo
            Touch touchZero = Input.GetTouch(0);
            //y el del otro
            Touch touchOne = Input.GetTouch(1);

            //queremos saber donde estuvo, para saber si esta haciendo zoom out o zoom in
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevDistance = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentDistance = (touchZero.position - touchOne.position).magnitude;

            float difference = currentDistance - prevDistance;
            zoom(difference * 0.01f);
         //si sigue "sosteniendo" el touch. digamos, para arrastrar la camara.
        }else if (Input.GetMouseButton(0)) {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }

        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    //util para zoom, puesto a que cambia el tamano de la camara.
    void zoom(float increment) {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment * zoomMultiplier, zoomLowerLimit, zoomHigherLimit);
    }
}
