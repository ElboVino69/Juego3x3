using System;
using System.Threading;

class Juego
{
    private static int[,] tablero = new int[3, 3];
    private static int jugadorX = 1, jugadorY = 1;
    private static int puntos = 0;
    private static string nombre;

    static void Main(string[] args)
    {
        Console.WriteLine("**Juego del Tablero 3x3**");
        Console.Write("Ingrese su nombre: ");
        nombre = Console.ReadLine();

        InicializarTablero();
        DibujarTablero();

        Thread hiloMovimiento = new Thread(new ThreadStart(HiloMovimiento));
        hiloMovimiento.Start();

        while (true)
        {
            if (jugadorX == 3 && jugadorY == 3)
            {
                Console.WriteLine("¡Felicidades, {0}! Has ganado.", nombre);
                Console.WriteLine("Puntos: {0}", puntos);

                Console.Write("¿Reiniciar el marcador? (s/n): ");
                string respuesta = Console.ReadLine();

                if (respuesta.ToLower() == "s")
                {
                    puntos = 0;
                }

                InicializarTablero();
                DibujarTablero();
            }

            if (tablero[jugadorX, jugadorY] == 3)
            {
                puntos++;
                Console.WriteLine("¡Has encontrado un premio!");
                tablero[jugadorX, jugadorY] = 0;
            }

            Thread.Sleep(100);
        }
    }

    private static void HiloMovimiento()
    {
        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo tecla = Console.ReadKey(true);

                switch (tecla.Key)
                {
                    case ConsoleKey.W:
                        MoverJugador(-1, 0);
                        break;
                    case ConsoleKey.S:
                        MoverJugador(1, 0);
                        break;
                    case ConsoleKey.A:
                        MoverJugador(0, -1);
                        break;
                    case ConsoleKey.D:
                        MoverJugador(0, 1);
                        break;
                }
            }
        }
    }

    private static void InicializarTablero()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                tablero[i, j] = 0;
            }
        }

        // Posición inicial del jugador
        tablero[jugadorX, jugadorY] = 1;

        // Posición aleatoria del premio
        Random random = new Random();
        int premioX = random.Next(0, 3);
        int premioY = random.Next(0, 3);

        while (tablero[premioX, premioY] == 1)
        {
            premioX = random.Next(0, 3);
            premioY = random.Next(0, 3);
        }

        tablero[premioX, premioY] = 1;
    }

    private static void DibujarTablero()
    {
        Console.Clear();

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                switch (tablero[i, j])
                {
                    case 0:
                        Console.Write(" ");
                        break;
                    case 1:
                        Console.Write("O");
                        break;
                    case 2:
                        Console.Write("X");
                        break;
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine("Puntos: {0}", puntos);
    }

    private static void MoverJugador(int dx, int dy)
    {
        int x = jugadorX + dx, y = jugadorY + dy;

        if (x >= 0 && x < 3 && y >= 0 && y < 3)
        {
            tablero[jugadorX, jugadorY] = 0;
            jugadorX = x;
            jugadorY = y;
            tablero[jugadorX, jugadorY] = 2;
            DibujarTablero();
        }
    }
}
