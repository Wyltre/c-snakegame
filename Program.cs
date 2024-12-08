using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading; 

class Program 
{ 
    static int ekranGenisligi = 30; 
    static int ekranYuksekligi = 20; 
    static int skor = 0; 
    static bool oyunBitti = false; 

    enum Yon { Yukari, Asagi, Sol, Sag } 
    static Yon mevcutYon = Yon.Sag; 

    static List<Nokta> yilan = new List<Nokta> { new Nokta(10, 10) }; 
    static Nokta yemek = new Nokta(new Random().Next(1, ekranGenisligi), new Random().Next(1, ekranYuksekligi)); 

    static void Main() 
    { 
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(@"
                     ###       ##
                     ##       ##
 ##   ##  ##  ##     ##      #####   ######    ####
 ## # ##  ##  ##     ##       ##      ##  ##  ##  ##
 #######  ##  ##     ##       ##      ##      ######
 #######   #####     ##       ## ##   ##      ##
  ## ##       ##    ####       ###   ####      #####
          #####
            ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nOyuna Başlamak İçin Hazır Mısın?");
            Console.WriteLine("Başlamak için herhangi bir tuşa bas!");
            Console.ReadKey();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("3");
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("2");
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("1");
            Thread.Sleep(1000);
            Console.Clear();

            skor = 0;
            yilan = new List<Nokta> { new Nokta(10, 10) };
            mevcutYon = Yon.Sag;
            oyunBitti = false;
            yemek = new Nokta(new Random().Next(1, ekranGenisligi), new Random().Next(1, ekranYuksekligi));

            Console.SetWindowSize(ekranGenisligi, ekranYuksekligi); 
            Console.CursorVisible = false; 

            while (!oyunBitti) 
            { 
                Cizim(); 
                Girdi(); 
                Mantik(); 
                Thread.Sleep(100);  
            } 

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Oyun Bitti!");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Skorun: {skor}");
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nNe yapmak istersin?");
            Console.WriteLine("1 - Çıkış");
            Console.WriteLine("5 - Yeniden Oyna");

            string secim = Console.ReadLine();
            if (secim == "1")
            {
                Console.WriteLine("Oyundan çıkılıyor. Tekrar görüşmek üzere!");
                break;
            }
            else if (secim != "5")
            {
                Console.WriteLine("Geçersiz seçim. Çıkış yapılıyor.");
                break;
            }
        }
    } 

    static void Cizim() 
    { 
        Console.Clear(); 

        foreach (var nokta in yilan) 
        { 
            Console.SetCursorPosition(nokta.X, nokta.Y); 
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("■"); 
        } 

        Console.SetCursorPosition(yemek.X, yemek.Y); 
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("●"); 

        Console.SetCursorPosition(0, ekranYuksekligi - 1); 
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"Skor: {skor}"); 
    } 

    static void Girdi() 
    { 
        if (Console.KeyAvailable) 
        { 
            var tus = Console.ReadKey(true).Key; 

            if (tus == ConsoleKey.UpArrow && mevcutYon != Yon.Asagi) 
                mevcutYon = Yon.Yukari; 
            if (tus == ConsoleKey.DownArrow && mevcutYon != Yon.Yukari) 
                mevcutYon = Yon.Asagi; 
            if (tus == ConsoleKey.LeftArrow && mevcutYon != Yon.Sag) 
                mevcutYon = Yon.Sol; 
            if (tus == ConsoleKey.RightArrow && mevcutYon != Yon.Sol) 
                mevcutYon = Yon.Sag; 
        } 
    } 

    static void Mantik() 
    { 
        Nokta yeniBasNokta = new Nokta(yilan[0].X, yilan[0].Y); 

        switch (mevcutYon) 
        { 
            case Yon.Yukari: yeniBasNokta.Y--; break; 
            case Yon.Asagi: yeniBasNokta.Y++; break; 
            case Yon.Sol: yeniBasNokta.X--; break; 
            case Yon.Sag: yeniBasNokta.X++; break; 
        } 

        if (yeniBasNokta.X == yemek.X && yeniBasNokta.Y == yemek.Y) 
        { 
            yemek = new Nokta(new Random().Next(1, ekranGenisligi), new Random().Next(1, ekranYuksekligi)); 
            skor += 10;  
        } 
        else 
        { 
            yilan.RemoveAt(yilan.Count - 1);  
        } 

        if (yilan.Any(p => p.X == yeniBasNokta.X && p.Y == yeniBasNokta.Y)) 
        { 
            oyunBitti = true; 
            return; 
        } 

        if (yeniBasNokta.X < 0 || yeniBasNokta.X >= ekranGenisligi || yeniBasNokta.Y < 0 || yeniBasNokta.Y >= ekranYuksekligi) 
        { 
            oyunBitti = true; 
            return; 
        } 

        yilan.Insert(0, yeniBasNokta);  
    } 
} 

struct Nokta 
{ 
    public int X; 
    public int Y; 

    public Nokta(int x, int y) 
    { 
        X = x; 
        Y = y; 
    } 
}