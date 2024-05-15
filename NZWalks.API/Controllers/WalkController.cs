using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly NZWalksDBContext dBContext;

        public WalkController(NZWalksDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        //GetWalk
        [HttpGet]
        public IActionResult GetAll()
        {
            var Walks= dBContext.Walks.ToList();

            var WalkDTO = new List<WalkDTO>();
            foreach (var Walk in Walks)
            {
                WalkDTO.Add(new WalkDTO
                {
                    ID = Walk.ID,
                    Name = Walk.Name,
                    Description = Walk.Description,
                    LengthInKM = Walk.LengthInKM,
                    WalkImageURL = Walk.WalkImageURL,
                    DifficultyID=Walk.DifficultyID,
                    RegionID=Walk.RegionID
                    
                });
            }
            return Ok(WalkDTO);
        }

        //Get Walks by ID
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetByID([FromRoute]Guid id)
        {
            var walk= dBContext.Walks.FirstOrDefault(x => x.ID == id);
            if(walk == null)
                return NotFound();
            var WalkDTO = new WalkDTO
            {
                ID = walk.ID,
                Name = walk.Name,
                Description = walk.Description,
                LengthInKM = walk.LengthInKM,
                WalkImageURL = walk.WalkImageURL,
                DifficultyID = walk.DifficultyID,
                RegionID = walk.RegionID
              
            };
            return Ok(WalkDTO);
        }

        //Create Walk
        [HttpPost]
        public IActionResult Create([FromBody]WalkRequestDTO walkRequestDTO)
        {
            var walk = new Walk
            {
                Name = walkRequestDTO.Name,
                Description = walkRequestDTO.Description,
                LengthInKM = walkRequestDTO.LengthInKM,
                WalkImageURL = walkRequestDTO.WalkImageURL,
                DifficultyID= walkRequestDTO.DifficultyID,
                RegionID= walkRequestDTO.RegionID
             
            };
            dBContext.Walks.Add(walk);
            dBContext.SaveChanges(); // getting an error here at data updatation.

            var walkDTO = new WalkDTO
            {

                Name = walkRequestDTO.Name,
                Description = walkRequestDTO.Description,
                LengthInKM = walkRequestDTO.LengthInKM,
                WalkImageURL = walkRequestDTO.WalkImageURL,
                DifficultyID=walkRequestDTO.DifficultyID,
                RegionID= walkRequestDTO.RegionID
               
            };

            return CreatedAtAction(nameof(GetByID), new {id=walk.ID},walkDTO);
        }
    }
}
