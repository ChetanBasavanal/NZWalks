using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultyController : ControllerBase
    {
        //Creating instance of DB
        private readonly NZWalksDBContext dBContext;

        //Initialze the constructor to this controller
        public DifficultyController(NZWalksDBContext dBContext)
        {
            this.dBContext = dBContext;

        }
        //Get Difficulty
        [HttpGet]
        public IActionResult GetAll()
        {
            //Collectiing the data from Domain model and storing in Difficulties
            var difficulties = dBContext.Difficulties.ToList();
            //maping data from difficulties to DifficultyDTO
            var DifficultyDTO = new List<DifficultyDTO>();
            foreach (var difficulty in difficulties)
            {
                DifficultyDTO.Add(new DifficultyDTO()
                {
                    ID= difficulty.Id,
                    Name = difficulty.Name
                });
            }

            return Ok(DifficultyDTO);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetByID(Guid id)
        {
            var Difficulties= dBContext.Difficulties.FirstOrDefault(x=>x.Id == id);
            if(Difficulties == null)
                return NotFound();
            var DifficultyDTO = new DifficultyDTO
            {
                ID= Difficulties.Id,
                Name = Difficulties.Name
            };

            return Ok(DifficultyDTO);

        }
        [HttpPost]
        public IActionResult Create([FromBody]DifficultyRequestDTO difficultyRequestDTO)
        {
            var Difficulty = new Difficulty
            {
                
                Name = difficultyRequestDTO.Name,
            };
            dBContext.Difficulties.Add(Difficulty);
            dBContext.SaveChanges();

            var DifficultyDTO = new DifficultyDTO
            {
                Name = difficultyRequestDTO.Name,
            };
            return CreatedAtAction(nameof(GetByID), new { id = Difficulty.Id }, DifficultyDTO);
        }
        //Update Diffuculty
        [HttpPut]
        [Route("{id=Guid}")]
        public IActionResult Update([FromRoute]Guid id, [FromBody]UpdateDifficultyRequestDTO updateDifficultyRequestDTO)
        {
            //checking whether there is data available or not.
            var Difficulty= dBContext.Difficulties.FirstOrDefault(x=>x.Id==id); 
            if(Difficulty == null) return NotFound();
            //map or convert DTO to Domain model
            Difficulty.Name=updateDifficultyRequestDTO.Name;
            dBContext.SaveChanges(); //Data saved in DB

            //map or convert Domain model to DTO.
            var DifficultyDTO = new DifficultyDTO
            {
                Name = Difficulty.Name,
            };
            return Ok(DifficultyDTO);
        }

        //Delete Difficulty.
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute]Guid id)
        {
            //Look for data availablity
            var Difficulty = dBContext.Difficulties.FirstOrDefault(x => x.Id == id);
            if(Difficulty == null) return NotFound();
            //remove data from domainModel
            dBContext.Difficulties.Remove(Difficulty);
            dBContext.SaveChanges(); //save change

            return Ok();
        }
    }
}
