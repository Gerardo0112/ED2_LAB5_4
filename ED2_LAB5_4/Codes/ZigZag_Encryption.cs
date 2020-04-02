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
            using(var stream = new FileStream(file, FileMode.Open))
            {
                //Lectura.
                using(var reading = new BinaryReader(stream))
                {
                    //Almacenamiento.
                    var bytes = new byte[length];
                    while(reading.BaseStream.Position != reading.BaseStream.Length)
                    {
                        //Lee los bytes.
                        bytes = reading.ReadBytes(length);
                        foreach(byte bit in bytes)
                        {
                            //Agregando a la lista.
                            list.Add(bit);
                        }
                    }
                }
            }
            return list;
        }
    }
}
