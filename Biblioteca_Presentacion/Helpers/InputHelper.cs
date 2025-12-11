namespace Biblioteca_Presentacion.Helpers
{

    public static class InputHelper
    {
        public static int LeerEntero(string prompt)
        {
            int val;
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out val))
                Console.Write("Valor inválido. Intente nuevamente: ");
            return val;
        }

        public static long LeerLong(string prompt)
        {
            long val;
            Console.Write(prompt);
            while (!long.TryParse(Console.ReadLine(), out val))
                Console.Write("Valor inválido. Intente nuevamente: ");
            return val;
        }

        public static string LeerTexto(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine() ?? string.Empty;
        }

        public static DateTime LeerFecha(string prompt)
        {
            DateTime d;
            Console.Write(prompt);
            while (!DateTime.TryParse(Console.ReadLine(), out d))
                Console.Write("Fecha inválida. Intente (yyyy-mm-dd): ");
            return d;
        }
    }

}
