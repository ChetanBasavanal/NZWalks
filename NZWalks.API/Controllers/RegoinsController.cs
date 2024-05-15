using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using System.Data.Entity;
using System.Drawing;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegoinsController : ControllerBase
    {
        private readonly NZWalksDBContext dBContext;
        private readonly IRegionRepository regionRepository;

        public RegoinsController(NZWalksDBContext dBContext,IRegionRepository regionRepository)
        {
            this.dBContext = dBContext;
            this.regionRepository = regionRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {   //Here above async and Task<IActionResult> is way to make it as Asyncronous.
            //regoins getting data from domain models. And await and ToListAsync.
            var regoins = await regionRepository.GetAllAsync();
            //regoinDTO getting data from regoins.
            var regoinDTO= new List<RegoinDTO>();
            foreach(var regoin in regoins)
            {
                regoinDTO.Add(new RegoinDTO()
                {
                    Id = regoin.Id,
                    Code = regoin.Code,
                    Name = regoin.Name,
                    RegionImageURL = regoin.RegionImageURL

                }); 
            }
            return Ok(regoinDTO);       //here will be presenting the data from DTO while we hide our domain model safe.
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            //getting data from regoin domain model
            //var regoin = dBContext.Regions.Find(Id);                            this is used only when we want to search by ID.
            var region = await regionRepository.GetByIdAsync(Id);      //better to use this, since it is flexible/ versatile.
            if(region == null)
            {
                return NotFound();
            }

            var regionDTO = new RegoinDTO
            {
                Id=region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageURL=region.RegionImageURL
            };
            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]RegionRequestDTO addregionDTO)
        {
            //Map or convert DTO to domain model
            var regions = new RegionDomain
            {
                Code= addregionDTO.Code,
                Name= addregionDTO.Name,
                RegionImageURL=addregionDTO.RegionImageURL
            };
            //use domain model to create regoin
            regions= await regionRepository.CreateAsync(regions);
            

            //Map domain model back to DTO
            var regionDTO = new RegoinDTO
            {
                Id = regions.Id,
                Code = regions.Code,
                Name = regions.Name,
                RegionImageURL = regions.RegionImageURL
            };

            return CreatedAtAction(nameof(GetById), new {id= regions.Id}, regionDTO);
        }
        //Update region
        
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody]UpdateRegionReqestDTO updateRegionReqestDTO)
        {
            var regionDomainModel = new RegionDomain
            {
                Code = updateRegionReqestDTO.Code,
                Name = updateRegionReqestDTO.Name,
                RegionImageURL = updateRegionReqestDTO.RegionImageURL
            };
            //Check if region exist
              regionDomainModel= await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
           

            //Convert Domain model to DTO
            var RegionDTO = new RegoinDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageURL = regionDomainModel.RegionImageURL
            };

            return Ok(RegionDTO);
        }

        //Delete Region
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task< IActionResult> Delete([FromRoute] Guid id)
        {
            var regions = await regionRepository.DeleteAsync(id);
            if (regions == null)
                return NotFound();

            //Delete region
            

            return Ok();
        }
    }
}
