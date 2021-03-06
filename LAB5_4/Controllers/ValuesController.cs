﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using LAB5_4.Codes;
namespace LAB5_4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/Cesar
        [Route("CifradoCesar")]
        [HttpPost]
        public void PostCifradoCesar([FromBody] string file, string key)
        {
            Cesar_Encryption CESAR = new Cesar_Encryption();
            var lecture= Path.GetFullPath("Cifrado");
            var lecture2 = Path.GetFullPath(file);
            CESAR.message(lecture, file, key);
        }

        [Route("DescifradoCesar")]
        [HttpPost]
        public void PostDescifradoCesar([FromBody]string file, string key)
        {
            Cesar_Encryption CESAR = new Cesar_Encryption();
            var lecture = Path.GetFullPath("Descifrado");
            var lecture2 = Path.GetFullPath(file);
            CESAR.message_d(file, lecture, key);

        }
        // POST api/ZigZag
        [Route("CifradoZigZag")]
        [HttpPost]
        public void PostCifradoZigZag([FromBody] string file, int level)
        {
           ZigZag_Encryption ZigZag = new ZigZag_Encryption();
            var lecture = Path.GetFullPath("Cifrado");
            var lecture2 = Path.GetFullPath(file);
            var BytesList = ZigZag.encryption(file, level);
            var AddExtraC = 0;
            var Matrix = ZigZag.matrix(BytesList.Count(), level, ref AddExtraC);
            var ExtraCharacter = new byte();
            if (AddExtraC > BytesList.Count())
            {
                BytesList = ZigZag.add_extra_c(BytesList, AddExtraC, ref ExtraCharacter);
            }
            ZigZag.message(Matrix, level, lecture, BytesList, ExtraCharacter);

        }
        [Route("DescifradoZigZag")]
        [HttpPost]
        public void PostDescifradoZigZag([FromBody] string file, int level)
        {
            ZigZag_Encryption ZigZag = new ZigZag_Encryption();
            var lecture = Path.GetFullPath("Descifrado");
            var lecture2= Path.GetFullPath(file);
            var ExtraC = new byte();
            var BytesList = ZigZag.decryption(file, level, ref ExtraC);
            var AddExtraC = 0;
            var Matrix = ZigZag.matrix_dec(BytesList.Count(), level, ref AddExtraC);
            var AddCharacter2 = new byte();
            if (AddExtraC > BytesList.Count())
            {
                BytesList = ZigZag.add_extra_c(BytesList, AddExtraC, ref AddCharacter2);
            }
            
            ZigZag.message_dec(lecture, level, BytesList, Matrix, ExtraC);

        }
        // POST api/Spiral
        [Route("CifradoSpiral")]
        [HttpPost]
        public void PostCifradoSpiral([FromBody] string file, int l, bool direction)
        {
            Spiral_Encryption Spiral = new Spiral_Encryption();
            var lecture = Path.GetFullPath("Cifrado");
            var archivoleido2 = Path.GetFullPath(file);
            Spiral.message(lecture, file,l, direction);

        }
        [Route("DescifradoSpiral")]
        [HttpPost]
        public void PostDescifradoSpiral([FromBody] string file, int l, bool direction)
        {
            Spiral_Encryption Spiral = new Spiral_Encryption();
            var lecture = Path.GetFullPath("Descifrado");
            var archivoleido2 = Path.GetFullPath(file);
            Spiral.message_d(lecture, file, l, direction);
        }

    }
}
