using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MyEditor : EditorWindow
{
    public Texture tex;
    private GameObject obj;
    private Color colorLeft;
    private Color colorRight;
    private List<Pixel> pixels = new List<Pixel>();
    private Event eventa;

    [MenuItem("MyTools/Paint -_0")]
    private static void OpenMyEditor()
    {
        GetWindow<MyEditor>();
    }

    private void OnEnable()
    {
        pixels.Add(new Pixel(new Rect(10, 10, 60, 60), Color.white));
        pixels.Add(new Pixel(new Rect(80, 10, 60, 60), Color.white));
        pixels.Add(new Pixel(new Rect(150, 10, 60, 60), Color.white));
        pixels.Add(new Pixel(new Rect(220, 10, 60, 60), Color.white));
        pixels.Add(new Pixel(new Rect(10, 80, 60, 60), Color.white));
        pixels.Add(new Pixel(new Rect(80, 80, 60, 60), Color.white));
        pixels.Add(new Pixel(new Rect(150, 80, 60, 60), Color.white));
        pixels.Add(new Pixel(new Rect(220, 80, 60, 60), Color.white));
        pixels.Add(new Pixel(new Rect(10, 150, 60, 60), Color.white));
        pixels.Add(new Pixel(new Rect(80, 150, 60, 60), Color.white));
        pixels.Add(new Pixel(new Rect(150, 150, 60, 60), Color.white));
        pixels.Add(new Pixel(new Rect(220, 150, 60, 60), Color.white));
        pixels.Add(new Pixel(new Rect(10, 220, 60, 60), Color.white));
        pixels.Add(new Pixel(new Rect(80, 220, 60, 60), Color.white));
        pixels.Add(new Pixel(new Rect(150, 220, 60, 60), Color.white));
        pixels.Add(new Pixel(new Rect(220, 220, 60, 60), Color.white));
        colorLeft = Color.white;
        colorRight = Color.white;
    }

    private void OnGUI()
    {
        eventa = Event.current;
        if (!tex)
        {
            Debug.LogError("ПОСТАВБ ТЕКСТУРУ !11!1!");
            return;
        }

        colorLeft = EditorGUI.ColorField(new Rect(10, 290, 270, 15), colorLeft);
        colorRight = EditorGUI.ColorField(new Rect(10, 310, 270, 15), colorRight);
        
        obj = (GameObject) EditorGUI.ObjectField(new Rect(3, 360, position.width - 6, 20), obj,
            typeof(GameObject));

        if (GUI.Button(new Rect(10, 330, 100, 25), "Залить все"))
        {
            SetAllColor();
        }
        
        if (GUI.Button(new Rect(120, 330, 150, 25), "Забахать текстуру"))
        {
            if (obj)
            {
                Material mat = obj.GetComponent<MeshRenderer>().material;
                Texture2D texture2D = new Texture2D(4, 4);
                texture2D.SetPixel(0,0,pixels[0].Color);
                texture2D.SetPixel(0,1,pixels[1].Color);
                texture2D.SetPixel(0,2,pixels[2].Color);
                texture2D.SetPixel(0,3,pixels[3].Color);
                texture2D.SetPixel(1,0,pixels[4].Color);
                texture2D.SetPixel(1,1,pixels[5].Color);
                texture2D.SetPixel(1,2,pixels[6].Color);
                texture2D.SetPixel(1,3,pixels[7].Color);
                texture2D.SetPixel(2,0,pixels[8].Color);
                texture2D.SetPixel(2,1,pixels[9].Color);
                texture2D.SetPixel(2,2,pixels[10].Color);
                texture2D.SetPixel(2,3,pixels[11].Color);
                texture2D.SetPixel(3,0,pixels[12].Color);
                texture2D.SetPixel(3,1,pixels[13].Color);
                texture2D.SetPixel(3,2,pixels[14].Color);
                texture2D.SetPixel(3,3,pixels[15].Color);
                texture2D.filterMode = FilterMode.Point;
                texture2D.Apply();
                mat.mainTexture = texture2D;
            }
            else
            {
                Debug.Log("Засеть объект");
            }
        }

        for (int i = 0; i < pixels.Count; i++)
        {
            DrawPixel(pixels[i]);
        }

        switch (eventa.type)
        {
            case EventType.MouseDown when eventa.button == 1:
                SetColor(colorRight);
                break;
            case EventType.MouseDown when eventa.button == 0:
                SetColor(colorLeft);
                break;
        }
    }

    private void SetAllColor()
    {
        foreach (var pixel in pixels)
        {
            pixel.Color = colorLeft;
        }
    }

    private void SetColor(Color color)
    {
        for (int i = 0; i < pixels.Count; i++)
        {
            if (pixels[i].Rect.Contains(eventa.mousePosition))
            {
                pixels[i].Color = color;
                eventa.Use();
            }
        }
    }

    private void DrawPixel(Pixel pixel)
    {
        GUI.color = pixel.Color;
        GUI.DrawTexture(pixel.Rect, tex);
    }

    private class Pixel
    {
        public Rect Rect;
        public Color Color;

        public Pixel(Rect rect, Color color)
        {
            Rect = rect;
            Color = color;
        }
    }
}