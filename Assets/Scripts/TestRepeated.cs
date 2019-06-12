using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TestRepeated : MonoBehaviour
{
    public float timeLenght=1;
    public float FPS;
    public GameObject Sprite_1;
    public GameObject Window;
    public GameObject Sprite_2;
    public GameObject Point_1;
    private Vector3 norm;
    public GameObject Base;
    private Vector3 normInvert;
    private float Lenght;
    private float delta;
    private float currentPosition;
    private float LenghtStart;
    private float currentMove;
    private float position_0;
    private float smoothKoeff;
    InputController _inputController;
    public bool isEPS;
    float SpeedBelt;
    float deltaTime = 0.0f;
    


    // Use this for initialization
    void Start ()
	{
        if(isEPS)
        {
            _inputController = new InputController();
            _inputController.controller = this;
            //_inputController.Begin(CommonData.IP_adress, CommonData.port);
           _inputController.Begin("192.168.1.10", 80);
        }
        
        currentPosition = 0;
        position_0 = 0;
        LenghtStart=Sprite_1.GetComponent<BoxCollider2D>().size.x;
        delta = LenghtStart / timeLenght;
        Debug.Log("LenghtStart=" + LenghtStart);
        norm = Vector3.Normalize(Window.transform.position - Point_1.transform.position);
        normInvert = Vector3.Normalize(Point_1.transform.position - Window.transform.position);
        // Lenght = Sprite_1.GetComponent<BoxCollider2D>().size.x*Base.transform.localScale.x;
        Lenght = LenghtStart;
        smoothKoeff = Lenght / CommonDataJT.currentUser.data.LenghtBelt;
        Debug.Log("Lenght=" + Lenght);
        Debug.Log("smoothKoeff=" + smoothKoeff);
        // Debug.Log(norm);
        // Debug.Log(Vector3.Magnitude(norm));
        Vector3 Sprite_2Pos = new Vector3(Sprite_1.transform.position.x - norm.x * Lenght, Sprite_1.transform.position.y - norm.y * Lenght, Sprite_1.transform.position.z - norm.z * Lenght);
	    Sprite_2.transform.position = Sprite_2Pos;


        
    }
	
	// Update is called once per frame
	void Update () {

        



        
       // Debug.Log("deltaTime=  " + SpeedBelt);
        if (isEPS)
        {
            currentPosition += SpeedBelt* smoothKoeff;
            delta = SpeedBelt;
            // Debug.Log("Input=  " + _inputController.CurrentValue);
        }
        else
        {
            //delta = LenghtStart / timeLenght * smoothKoeff;
            delta = smoothKoeff;
            currentPosition += delta;
        }
        Debug.Log("delta=  " + delta);


        if (currentPosition>LenghtStart)
        {
            currentPosition -= LenghtStart;
            currentMove = LenghtStart - position_0 + currentPosition;
            position_0 = currentPosition;
        }
        else
        {
            currentMove = currentPosition - position_0;
            position_0 = currentPosition;
        }
        
        currentMove*=Base.transform.localScale.x;
        //Debug.Log(currentMove);
        norm = Vector3.Normalize(Window.transform.position - Point_1.transform.position);
        //Lenght = Sprite_1.GetComponent<BoxCollider2D>().size.x * Base.transform.localScale.x;
	    Vector3 current_1= Sprite_1.transform.position - Window.transform.position;
        
       // Debug.Log(current_1+"    "+(current_1.x / norm.x) + "    " + Vector3.Magnitude(current_1));
        Vector3 current_2 = Sprite_2.transform.position - Window.transform.position;
        if ((current_1.x / norm.x) > 0 && (current_1.x / norm.x) > Lenght)
	    {
            //Debug.Log("sprite_1");
            Vector3 Pos = new Vector3(Sprite_2.transform.position.x - norm.x * Lenght, Sprite_2.transform.position.y - norm.y * Lenght, Sprite_2.transform.position.z - norm.z * Lenght);
            Sprite_1.transform.position = Pos;
        }
        if ((current_2.x / norm.x) > 0 && (current_2.x / norm.x) > Lenght )
        {
            Vector3 Pos = new Vector3(Sprite_1.transform.position.x - norm.x * Lenght, Sprite_1.transform.position.y - norm.y * Lenght, Sprite_1.transform.position.z - norm.z * Lenght);
            Sprite_2.transform.position = Pos;
        }
        //Sprite_1.transform.Translate(norm * currentMove, Space.World);
        //Sprite_2.transform.Translate(norm * currentMove, Space.World);
        Sprite_1.transform.Translate(norm * delta*Time.deltaTime, Space.World);
        Sprite_2.transform.Translate(norm * delta * Time.deltaTime, Space.World);
    }
    
    void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time + " seconds");
        _inputController.EndConnect();
       
    }
    public void Setspeed(float speed)
    {
        if(speed<0)
        {
            Debug.Log(speed);
        }

        SpeedBelt = speed;
    }
    public void Slider_Changed(float newValue)
    {
        Sprite_1.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, newValue);
        Sprite_2.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, newValue);
    }
}
