using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class TestRepeated : MonoBehaviour
{
    public Slider X_slider;
    public Slider Y_slider;
    public Slider Scale_slider;
    public Slider Length_slider;
    public float timeLenght = 1;
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
    float SpeedBelt_Test;
    float deltaTime = 0.0f;
    float LengthWind;
    float X_position;
    float Y_position;
    float Z_position;

    // Use this for initialization
    void Start()
    {
        SpeedBelt_Test = 10f;
        LengthWind = Window.transform.localScale.x;
        X_position = Base.transform.localPosition.x;
        Y_position = Base.transform.localPosition.y;
        Z_position = Base.transform.localPosition.z;
        CommonDataJT.ControllerWindow = this;
        X_slider.value = CommonDataJT.currentUser.data.Window_X_pos;
        Y_slider.value = CommonDataJT.currentUser.data.Window_Y_pos;
        Scale_slider.value = CommonDataJT.currentUser.data.Window_Scale;
        Length_slider.value = CommonDataJT.currentUser.data.Window_Lenght;


        Rect rec = new Rect(0, 0, CommonDataJT.TextureBelt.width, CommonDataJT.TextureBelt.height);

        Sprite_1.GetComponent<SpriteRenderer>().sprite = Sprite.Create(CommonDataJT.TextureBelt, rec, new Vector2(0.5f, 0.5f), 100f);
        Sprite_2.GetComponent<SpriteRenderer>().sprite = Sprite.Create(CommonDataJT.TextureBelt, rec, new Vector2(0.5f, 0.5f), 100f);
        Sprite_1.AddComponent<BoxCollider2D>();
        Sprite_2.AddComponent<BoxCollider2D>();
        if (isEPS)
        {
            _inputController = new InputController();
            _inputController.controller = this;
            //_inputController.Begin(CommonData.IP_adress, CommonData.port);
            _inputController.Begin(CommonDataJT.currentUser.data.IP_adress, int.Parse(CommonDataJT.currentUser.data.Port));
        }

        currentPosition = 0;
        position_0 = 0;
        // LenghtStart=Sprite_1.GetComponent<BoxCollider2D>().size.x;
        // delta = LenghtStart / timeLenght;

        norm = Vector3.Normalize(Window.transform.position - Point_1.transform.position);
        normInvert = Vector3.Normalize(Point_1.transform.position - Window.transform.position);
        Lenght = Sprite_1.GetComponent<BoxCollider2D>().size.x * Base.transform.localScale.x;
        // Lenght = LenghtStart;
        smoothKoeff = Lenght / CommonDataJT.currentUser.data.LenghtBelt;
        Debug.Log("Lenght=" + Lenght);
        Debug.Log("smoothKoeff=" + smoothKoeff);
        // Debug.Log(norm);
        // Debug.Log(Vector3.Magnitude(norm));
        Vector3 Sprite_2Pos = new Vector3(Sprite_1.transform.position.x - norm.x * Lenght, Sprite_1.transform.position.y - norm.y * Lenght, Sprite_1.transform.position.z - norm.z * Lenght);
        Sprite_2.transform.position = Sprite_2Pos;

        ChangeSizePosition();


    }

    // Update is called once per frame
    void Update()
    {






        // Debug.Log("deltaTime=  " + SpeedBelt);
        if (isEPS)
        {
            // currentPosition += SpeedBelt * smoothKoeff;
            delta = SpeedBelt * Lenght / CommonDataJT.currentUser.data.LenghtBelt;
            // Debug.Log("SpeedBelt=  " + _inputController.CurrentValue);
        }
        else
        {
            //delta = LenghtStart / timeLenght * smoothKoeff;
            // delta = smoothKoeff;
            //currentPosition += delta;

            // currentPosition += SpeedBelt_Test * smoothKoeff;
            delta = SpeedBelt_Test * Lenght / CommonDataJT.currentUser.data.LenghtBelt;
        }
        //Debug.Log("delta=  " + delta);


        // if (currentPosition > LenghtStart)
        // {
        //     currentPosition -= LenghtStart;
        //     currentMove = LenghtStart - position_0 + currentPosition;
        //     position_0 = currentPosition;
        // }
        // else
        // {
        //     currentMove = currentPosition - position_0;
        //     position_0 = currentPosition;
        // }
        //
        // currentMove *= Base.transform.localScale.x;
        //Debug.Log(currentMove);
        norm = Vector3.Normalize(Window.transform.position - Point_1.transform.position);
        Lenght = Sprite_1.GetComponent<BoxCollider2D>().size.x * Base.transform.localScale.x;
        Vector3 current_1 = Sprite_1.transform.position - Window.transform.position;

        // Debug.Log(current_1+"    "+(current_1.x / norm.x) + "    " + Vector3.Magnitude(current_1));
        Vector3 current_2 = Sprite_2.transform.position - Window.transform.position;
        if ((current_1.x / norm.x) > 0 && (current_1.x / norm.x) > Lenght)
        {
            //Debug.Log("sprite_1");
            Vector3 Pos = new Vector3(Sprite_2.transform.position.x - norm.x * Lenght, Sprite_2.transform.position.y - norm.y * Lenght, Sprite_2.transform.position.z - norm.z * Lenght);
            Sprite_1.transform.position = Pos;
        }
        if ((current_2.x / norm.x) > 0 && (current_2.x / norm.x) > Lenght)
        {
            Vector3 Pos = new Vector3(Sprite_1.transform.position.x - norm.x * Lenght, Sprite_1.transform.position.y - norm.y * Lenght, Sprite_1.transform.position.z - norm.z * Lenght);
            Sprite_2.transform.position = Pos;
        }
        //Sprite_1.transform.Translate(norm * currentMove, Space.World);
        //Sprite_2.transform.Translate(norm * currentMove, Space.World);
        Sprite_1.transform.Translate(norm * delta * Time.deltaTime, Space.World);
        Sprite_2.transform.Translate(norm * delta * Time.deltaTime, Space.World);
    }

    void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time + " seconds");
        _inputController.EndConnect();

    }
    public void Setspeed(float speed)
    {
        if (speed < 0)
        {
            Debug.Log("SpeedBelt<0=  " + speed);
        }

        SpeedBelt = speed;
    }
    public void Slider_Changed(float newValue)
    {
        Sprite_1.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, newValue);
        Sprite_2.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, newValue);
    }
    public void ChangeSizePosition()
    {
        Debug.Log(CommonDataJT.currentUser.data.Window_Lenght);
        Debug.Log(CommonDataJT.currentUser.data.Window_Scale);
        Debug.Log(CommonDataJT.currentUser.data.Window_X_pos);
        Debug.Log(CommonDataJT.currentUser.data.Window_Y_pos);
        Window.transform.localScale = new Vector3(LengthWind * CommonDataJT.currentUser.data.Window_Lenght, Window.transform.localScale.y, Window.transform.localScale.z);
        Base.transform.localPosition = new Vector3(X_position + CommonDataJT.currentUser.data.Window_X_pos, Y_position, Z_position + CommonDataJT.currentUser.data.Window_Y_pos);
        Base.transform.localScale = new Vector3(CommonDataJT.currentUser.data.Window_Scale, CommonDataJT.currentUser.data.Window_Scale, CommonDataJT.currentUser.data.Window_Scale);
        // if (X_slider.value != CommonDataJT.currentUser.data.Window_X_pos)
        // {
        //     X_slider.value = CommonDataJT.currentUser.data.Window_X_pos;
        // }
        // if (Y_slider.value != CommonDataJT.currentUser.data.Window_Y_pos)
        //     Y_slider.value = CommonDataJT.currentUser.data.Window_Y_pos;
        // if (Scale_slider.value != CommonDataJT.currentUser.data.Window_Scale)
        //     Scale_slider.value = CommonDataJT.currentUser.data.Window_Scale;
        // if (Length_slider.value != CommonDataJT.currentUser.data.Window_Lenght)
        //     Length_slider.value = CommonDataJT.currentUser.data.Window_Lenght;
    }
    public void Slider_X()
    {
        Debug.Log("Slider_X");
        CommonDataJT.currentUser.data.Window_X_pos = X_slider.value;
        CommonDataJT.currentUser.PushData();
    }
    public void Slider_Y(float newValue)
    {
        //Debug.Log(newValue);
        CommonDataJT.currentUser.data.Window_Y_pos = newValue;
        CommonDataJT.currentUser.PushData();
    }
    public void Slider_Scale(float newValue)
    {
        CommonDataJT.currentUser.data.Window_Scale = newValue;
        CommonDataJT.currentUser.PushData();
    }
    public void Slider_Length(float newValue)
    {
        CommonDataJT.currentUser.data.Window_Lenght = newValue;
        CommonDataJT.currentUser.PushData();
    }
}
