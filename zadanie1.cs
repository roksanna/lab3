using System;

public class RGB
{
    public int R_value { get; set; }
    public int G_value { get; set; }
    public int B_value { get; set; }
    
    public RGB(int r, int g, int b)
    {
        R_value = r;
        G_value = g;
        B_value = b;
    }
}

public class RGBController
{
    // Metoda inicjująca zestaw składowych barw
    public void InitColor(RGB color, int r, int g, int b)
    {
        color.R_value = r;
        color.G_value = g;
        color.B_value = b;
    }
    
    public void DisplayColor(RGB color)
    {
        Console.WriteLine($"[{color.R_value}, {color.G_value}, {color.B_value}]");
    }

    // Metoda mieszająca kolory
    public RGB MixColors(RGB color1, RGB color2)
    {
        int r = (color1.R_value + color2.R_value) / 2;
        int g = (color1.G_value + color2.G_value) / 2;
        int b = (color1.B_value + color2.B_value) / 2;
        
        return new RGB(r, g, b);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        RGB color1 = new RGB(255, 0, 0); // Czerwony
        RGB color2 = new RGB(0, 0, 255); // Niebieski

        RGBController controller = new RGBController();

        controller.DisplayColor(color1);
        controller.DisplayColor(color2);
        RGB mixedColor = controller.MixColors(color1, color2);
        controller.DisplayColor(mixedColor);
    }
}
