using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Web;

namespace ED2_LAB5_4.Codes
{
    public class Cesar_Encryption
    {
        static Dictionary<string, int> original = new Dictionary<string, int>();
        static Dictionary<string, int> encryption = new Dictionary<string, int>();
        string route = string.Empty;
        static bool empty = true;
        //Crear diccionario original.
        public void original_generate()
        {
            if(empty)
            {
                var upper = 32;
                var counter = 1;
                for(int x = 0; x < 256; x++)
                {
                    original.Add(Convert.ToString((char)upper), counter);
                    counter++;
                    upper++;
                }
                //Suma al contador del diccionario.
                original.Add("\r\n", counter + 1);
                empty = false;
            }
        }
        //Crear el diccionario para cifrado.
        public void encryption_generate(string key)
        {
            var counter = 1;
            //Añadir clave al diccionario.
            if (key != null)
            {
                foreach (var letter in key)
                {
                    if (!encryption.ContainsKey(Convert.ToString(letter)))
                    {
                        encryption.Add(Convert.ToString(letter), counter);
                        counter++;
                    }
                }
            }
            //Comparación de diccionario original con el de cifrado para el ingreso de letras.
            foreach (var item in original.Keys)
            {
                if (!encryption.ContainsKey(item))
                {
                    encryption.Add(item, counter);
                    counter++;
                }
            }
        }
    }
}
