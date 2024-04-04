using System; // Importa el espacio de nombres System que contiene funciones fundamentales de C#.
using System.Threading; // Importa el espacio de nombres System.Threading para trabajar con hilos.

class Juego // Define una nueva clase llamada Juego.
{
    private static int[,] tablero = new int[3, 3]; // Declara una matriz estática privada para representar el tablero del juego.
    private static int jugadorX = 1, jugadorY = 1; // Declara las coordenadas iniciales del jugador en el tablero.
    private static int puntos = 0; // Declara una variable para llevar la cuenta de los puntos del jugador.
    private static string nombre; // Declara una variable para almacenar el nombre del jugador.

    static void Main(string[] args) // Método principal del programa.
    {
        Console.WriteLine("**Juego del Tablero 3x3**"); // Imprime el título del juego.
        Console.Write("Ingrese su nombre: "); // Solicita al usuario que ingrese su nombre.
        nombre = Console.ReadLine(); // Almacena el nombre del usuario.
        Console.Clear(); // Limpia la consola.
        InicializarTablero(); // Llama a la función para inicializar el tablero.
        DibujarTablero(); // Llama a la función para dibujar el tablero.

        Thread hiloMovimiento = new Thread(new ThreadStart(HiloMovimiento)); // Crea un nuevo hilo para manejar el movimiento del jugador.
        hiloMovimiento.Start(); // Inicia el hilo de movimiento.

        while (true) // Bucle infinito para mantener el juego en funcionamiento.
        {
            if (jugadorX == 2 && jugadorY == 2) // Si el jugador llega a la posición (3,3) en el tablero entonces...
            {
                Console.WriteLine("¡Felicidades, {0}! Has ganado.", nombre); // Imprime un mensaje de felicitación.
                Console.WriteLine("Puntos: {0}", puntos); // Imprime los puntos del jugador.

                Console.Write("¿Reiniciar el marcador? si/no: "); // Pregunta al jugador si desea reiniciar el marcador.
                string respuesta = Console.ReadLine().ToLower(); // Almacena la respuesta del jugador y la convierte a minúsculas.
                switch (respuesta) // Dependiendo de la respuesta...
                {
                    case "si": // Si la respuesta es 'si'...
                        puntos = 0; // Reinicia los puntos.
                        jugadorX = 1; // Reinicia la posición del jugador en el eje X.
                        jugadorY = 1; // Reinicia la posición del jugador en el eje Y.
                        InicializarTablero(); // Reinicia el tablero.
                        DibujarTablero(); // Dibuja el tablero.
                        break; // Sale del switch.
                    case "no": // Si la respuesta es 'no'...
                        Console.WriteLine("Gracias por jugar, {0}. ¡Hasta la próxima!", nombre); // Agradece al jugador por jugar.
                        return; // Termina el juego.
                    default: // Si la respuesta no es ni 'si' ni 'no'...
                        Console.WriteLine("Respuesta no reconocida. Por favor, responde con 'SI' o 'no'."); // Pide al jugador que introduzca una respuesta válida.
                       break; // Sale del switch.
                }
            }
        }
    }


    private static void HiloMovimiento() // Método para manejar el movimiento del jugador.
    {
        while (true) // Bucle infinito para escuchar constantemente las teclas presionadas por el jugador.
        {
            if (Console.KeyAvailable) // Si se presiona una tecla...
            {
                ConsoleKeyInfo tecla = Console.ReadKey(true); // Almacena la información de la tecla presionada.

                switch (tecla.Key) // Dependiendo de la tecla presionada...
                {
                    case ConsoleKey.W: // Si se presiona 'W'...
                        MoverJugador(-1, 0); // Mueve al jugador hacia arriba.
                        break;
                    case ConsoleKey.S: // Si se presiona 'S'...
                        MoverJugador(1, 0); // Mueve al jugador hacia abajo.
                        break;
                    case ConsoleKey.A: // Si se presiona 'A'...
                        MoverJugador(0, -1); // Mueve al jugador hacia la izquierda.
                        break;
                    case ConsoleKey.D: // Si se presiona 'D'...
                        MoverJugador(0, 1); // Mueve al jugador hacia la derecha.
                        break;
                }
            }
        }
    }

    private static void InicializarTablero() // Método para inicializar el tablero.
    {
        for (int i = 0; i < 3; i++) // Itera sobre las filas del tablero.
        {
            for (int j = 0; j < 3; j++) // Itera sobre las columnas del tablero.
            {
                tablero[i, j] = 0; // Establece cada celda del tablero a 0.
            }
        }

        // Posición inicial del jugador
        tablero[jugadorX, jugadorY] = 1; // Establece la posición inicial del jugador en el tablero.

        // Posición aleatoria del premio
        Random random = new Random(); // Crea un nuevo objeto Random.
        int premioX = random.Next(0, 3); // Genera una posición aleatoria para el premio en el eje X.
        int premioY = random.Next(0, 3); // Genera una posición aleatoria para el premio en el eje Y.

        while (tablero[premioX, premioY] == 1) // Mientras la posición del premio sea la misma que la del jugador...
        {
            premioX = random.Next(0, 3); // Genera una nueva posición para el premio en el eje X.
            premioY = random.Next(0, 3); // Genera una nueva posición para el premio en el eje Y.
        }

        tablero[premioX, premioY] = 2; // Establece la posición del premio en el tablero.
    }

    private static void DibujarTablero() // Método para dibujar el tablero.
    {
        Console.Clear(); // Limpia la consola.

        for (int i = 0; i < 3; i++) // Itera sobre las filas del tablero.
        {
            for (int j = 0; j < 3; j++) // Itera sobre las columnas del tablero.
            {
                switch (tablero[i, j]) // Dependiendo del valor de la celda...
                {
                    case 0: // Si la celda está vacía...
                        Console.Write("[ ]"); // Imprime '[ ]' para representar una celda vacía.
                        break;
                    case 1: // Si la celda contiene al jugador...
                        Console.Write("[x]"); // Imprime '[X]' para representar al jugador.
                        break;
                    case 2: // Si la celda contiene un premio...
                        Console.Write("[O]"); // Imprime '[O]' para representar un premio.
                        break;
                }
            }

            Console.WriteLine(); // Imprime una nueva línea después de cada fila.
        }

        Console.WriteLine("Puntos: {0}", puntos); // Imprime los puntos del jugador.
    }

    private static void MoverJugador(int dx, int dy) // Método para mover al jugador.
    {
        int x = jugadorX + dx, y = jugadorY + dy; // Calcula la nueva posición del jugador.

        if (x >= 0 && x < 3 && y >= 0 && y < 3) // Si la nueva posición está dentro del tablero...
        {
            if (tablero[x, y] == 2) // Si el jugador encuentra un premio...
            {
                puntos++; // Incrementa los puntos.
                Console.WriteLine("¡Has encontrado un premio!"); // Imprime un mensaje.
            }
            tablero[jugadorX, jugadorY] = 0; // Borra la posición actual del jugador en el tablero.
            jugadorX = x; // Actualiza la posición del jugador en el eje X.
            jugadorY = y; // Actualiza la posición del jugador en el eje Y.
            tablero[jugadorX, jugadorY] = 1; // Marca la nueva posición del jugador en el tablero.
            DibujarTablero(); // Dibuja el tablero.
        }
    }
}

