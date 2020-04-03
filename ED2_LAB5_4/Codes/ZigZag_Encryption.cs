using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Web;

namespace ED2_LAB5_4.Codes
{
    public class ZigZag_Encryption
    {
        //Lectura para el cifrado.
        public List<byte> encryption(string file, int length)
        {
            //Lista para bytes.
            var list = new List<byte>();
            using (var stream = new FileStream(file, FileMode.Open))
            {
                //Lectura.
                using (var reading = new BinaryReader(stream))
                {
                    //Almacenamiento.
                    var bytes = new byte[length];
                    while (reading.BaseStream.Position != reading.BaseStream.Length)
                    {
                        //Lee los bytes.
                        bytes = reading.ReadBytes(length);
                        foreach (byte bit in bytes)
                        {
                            //Agregando a la lista.
                            list.Add(bit);
                        }
                    }
                }
            }
            return list;
        }
        //Crear matriz.
        public byte[,] matrix(int counter, int level, ref int caracter)
        {
            caracter = counter;
            var array = new byte[level, counter];
            var return_ = false;
            var x = 0;
            for (int y = 0; y < counter; y++)
            {
                if (return_)
                {
                    //Convertir en byte.
                    array[x, y] = Convert.ToByte('_');
                    x--;
                    if (x < 0)
                    {
                        return_ = false;
                        x += 2;
                    }
                }
                else
                {
                    array[x, y] = Convert.ToByte('_');
                    x++;
                    if (x == level)
                    {
                        return_ = true;
                        x -= 2;
                    }
                }
            }
            //Si convierte a byte, que devuelva el array.
            if (array[0, counter - 1] == Convert.ToByte('_'))
            {
                return array;
            }
            else
            {
                array = matrix(counter + 1, level, ref caracter);
            }
            return array;
        }
        //Añadir cualquier tipo de caracter extra.
        public List<byte> add_extra_c(List<byte> list, int counter, ref byte extra_c)
        {
            bool found = true;
            var x = 1;
            while (!found)
            {
                if (list.Contains(Convert.ToByte(x)))
                {
                    x++;
                }
                else
                {
                    //Encuentra el valor.
                    found = true;
                }
            }
            extra_c = Convert.ToByte(x);
            while (list.Count() != counter)
            {
                //Se añade al listado.
                list.Add(Convert.ToByte(x));
            }
            return list;
        }
        public void message(byte[,] array, int level, string route, List<byte> list, byte extra_c)
        {
            //Introduce los bytes.
            var list_position = 0;
            var return_ = false;
            var x = 0;
            for (int y = 0; y < list.Count(); y++)
            {
                if (return_)
                {
                    array[x, y] = list[list_position];
                    list_position++;
                    x--;
                    if (x < 0)
                    {
                        return_ = false;
                        x += 2;
                    }
                }
                else
                {
                    array[x, y] = list[list_position];
                    list_position++;
                    x++;
                    if (x == level)
                    {
                        return_ = true;
                        x -= 2;
                    }
                }
            }
            //Obtiene los bytes cifrados.
            var bytes = new byte[list.Count()];
            var position = 0;
            for (x = 0; x < level; x++)
            {
                for (int y = 0; y < list.Count(); y++)
                {
                    if (array[x, y] != 0)
                    {
                        bytes[position] = array[x, y];
                        position++;
                    }
                }
            }
            using (var stream = new FileStream(route + "\\..\\Files\\ArchivoCifradoZigZag.cif", FileMode.Create))
            {
                using (var writing = new BinaryWriter(stream))
                {
                    writing.Write(extra_c);
                    writing.Seek(0, SeekOrigin.End);
                    writing.Write(bytes);
                }
            }
        }
        //Lectura para el descifrado.
        public List<byte> desncryption(string file, int length, ref byte extra_c)
        {
            //Lista para bytes.
            var list = new List<byte>();
            using (var stream = new FileStream(file, FileMode.Open))
            {
                //Lectura.
                using (var reading = new BinaryReader(stream))
                {
                    //Almacenamiento.
                    var counter = 0;
                    var bytes = new byte[length];
                    while (reading.BaseStream.Position != reading.BaseStream.Length)
                    {
                        //Lee los bytes.
                        bytes = reading.ReadBytes(length);
                        foreach (byte bit in bytes)
                        {
                            bytes = reading.ReadBytes(length);
                            if (counter != 0)
                            {
                                //Agregando a la lista.
                                list.Add(bit);
                            }
                            else
                            {
                                extra_c = bit;
                                counter++;
                            }
                        }
                    }
                }
            }
            return list;
        }
    }
}
