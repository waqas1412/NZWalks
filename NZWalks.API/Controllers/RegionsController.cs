using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositeries;

namespace NZWalks.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDBContext dBContext;
        private readonly IRegionRepositery regionRepositery;

        public RegionsController(NZWalksDBContext dBContext, IRegionRepositery regionRepositery)
        {
            this.dBContext = dBContext;
            this.regionRepositery = regionRepositery;
        }
        [HttpGet]
        public async Task<IActionResult> GetALL()
        {
            var regions = await regionRepositery.GetAllAsync();
            var regionsDto = new List<RegionDto>();
            foreach (var region in regions)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl,
                });
            }
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) {
            //var region = dBContext.Regions.Find(id); //Works only for primary key
            //var region = await dBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            var region = await regionRepositery.GetByIdAsync(id);
            
            if (region == null)
            {
                return NotFound();
            }
            var regionDto = new RegionDto {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl,
            };
            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto) {
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name    = addRegionRequestDto.Name, 
                RegionImageUrl   = addRegionRequestDto.RegionImageUrl,
            };
            //await dBContext.Regions.AddAsync(regionDomainModel);
            //await dBContext.SaveChangesAsync();
           regionDomainModel = await regionRepositery.CreateAsync(regionDomainModel);

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id}, regionDomainModel);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto) {
            var regionDomainModel = new Region {
                Code = updateRegionRequestDto.Code,
                Name = updateRegionRequestDto.Name,
                RegionImageUrl = updateRegionRequestDto.RegionImageUrl
        };
              regionDomainModel =  await regionRepositery.UpdateAsync(id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
         
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };
            return Ok(regionDto); 
        }

        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id) {
            var regionDomainModel = await regionRepositery.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return Ok(regionDto);
        }
    }
}
