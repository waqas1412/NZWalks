using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IMapper mapper;

        public RegionsController(NZWalksDBContext dBContext, IRegionRepositery regionRepositery, IMapper mapper)
        {
            this.dBContext = dBContext;
            this.regionRepositery = regionRepositery;
            this.mapper = mapper;
        }
        [HttpGet]
        [Authorize (Roles = "Reader, Writer")]
        public async Task<IActionResult> GetALL()
        {
            var regions = await regionRepositery.GetAllAsync();
            return Ok(mapper.Map<List<RegionDto>>(regions));
        }

        [HttpGet]
        [Authorize (Roles = "Reader, Writer")]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var region = await regionRepositery.GetByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<RegionDto>(region));
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

            if (ModelState.IsValid)
            {
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);
                regionDomainModel = await regionRepositery.CreateAsync(regionDomainModel);
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);
                return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDomainModel);
            }
            else { return BadRequest(); }


        }

        [HttpPut]
        [Authorize(Roles = "Writer")]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {

            if (ModelState.IsValid)
            {
                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);
                regionDomainModel = await regionRepositery.UpdateAsync(id, regionDomainModel);
                if (regionDomainModel == null)
                {
                    return NotFound();
                }
                return Ok(mapper.Map<RegionDto>(regionDomainModel));
            }
            else { return BadRequest(); }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepositery.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }
    }
}
