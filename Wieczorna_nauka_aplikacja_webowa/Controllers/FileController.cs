using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wieczorna_nauka_aplikacja_webowa.Entities;
using Wieczorna_nauka_aplikacja_webowa.Models;
using Wieczorna_nauka_aplikacja_webowa.Services.Services;

namespace Wieczorna_nauka_aplikacja_webowa.Controllers
{
    [Route("file")]
    //[Authorize]
    public class FileController : ControllerBase
    {
        [HttpGet]
        //parametr, który mówi: przez 1200 sekund możesz pobrać dany plik przez cacha
        [ResponseCache(Duration = 1200, VaryByQueryKeys = new[] {"fileName"})]
        //Klasa odpowiedzialna za zwracanie plików statycznych oraz przesyłanie upload oraz downolad
        public ActionResult GetFile([FromQuery] string fileName)
        {
            //zwraca aktualną ścieżkę katologu, w którym uruchomiona jest aplikacja
            var rootPath = Directory.GetCurrentDirectory();
            //zwraca ścieżkę pliku fileName 
            var filePath = $"{rootPath}/PrivateFiles/{fileName}";
            //sprawdzenie czy dany plik istnieje
            if(!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }
            //utworzenie obiektu który ma metodę sprawdzającą rozszerzenie pliku czy jest .txt czy .word
            var contentProvider = new FileExtensionContentTypeProvider();
            //metoda sprawdzajaca rozszerzenie pliku
            contentProvider.TryGetContentType(filePath, out string contentType);
            //metoda odczytuje wszystkie bajty z pliku o podanej ścieżce 
            var fileContents = System.IO.File.ReadAllBytes(filePath);
            return File(fileContents, contentType, fileName);
        }

        [HttpPost]
        //dzięki atrybutowi FromForm oraz intefejsie IFormFile klient będzie w stanie przesłać plik do 
        //akcji
        public ActionResult Upload([FromForm] IFormFile file)
        {
            //sprawdzam istnieje w ogole taki plik
            if(file !=null && file.Length > 0)
            {
                //załadowanie głównej ścieżki (tak jak w endpoint wyżej)
                var rootPath = Directory.GetCurrentDirectory();
                //przypisanie do zmienej ścieżki, którą ustawimy jako miejsce zapisywania plików
                var fullPath = $"{rootPath}/PrivateFiles/{file.FileName}";
                //utworzenie nowego obiektu, który jest tworzony za pomocą ściezki i tybu FileModeCreate,
                //oznacza ze plik będzietworzony jesli nie isntieje,a jesli istnieje to napisywany
                //jest to tylko utworzenie pliku, bez jego zawartosci
                var stream = new FileStream(fullPath, FileMode.Create);
                {
                    /*kopiowanie zawartości obiektu 'file' do otwartego strumienia pliku pozwala na zapisanie
                     * danych z obiektu 'file' do pliku na dysku twardym*/
                    file.CopyTo(stream);
                }
                    
                return Ok();
            }
            return BadRequest();

        }
    }


}
