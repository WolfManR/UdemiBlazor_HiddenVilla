using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Business.Repository
{
    public class HotelAmenityRepository : IHotelAmenityRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public HotelAmenityRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<HotelAmenityDTO> Create(HotelAmenityDTO amenityDTO)
        {
            var amenity = _mapper.Map<HotelAmenity>(amenityDTO);
            var added = await _db.HotelAmenities.AddAsync(amenity);
            await _db.SaveChangesAsync();
            return _mapper.Map<HotelAmenityDTO>(added.Entity);
        }

        public async Task<HotelAmenityDTO> Update(int amenityId, HotelAmenityDTO amenityDTO)
        {
            if(amenityId != amenityDTO.Id) return null;

            try
            {
                var amenityDetails = await _db.HotelAmenities.FindAsync(amenityId);
                var amentiy = _mapper.Map(amenityDTO, amenityDetails);
                var updated = _db.HotelAmenities.Update(amentiy);
                await _db.SaveChangesAsync();
                return _mapper.Map<HotelAmenityDTO>(updated.Entity);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<int> Delete(int amenityId)
        {
            var amenityDetails = await _db.HotelAmenities.FindAsync(amenityId);
            if(amenityDetails is null) return 0;

            _db.HotelAmenities.Remove(amenityDetails);
            return await _db.SaveChangesAsync();
        }
        
        public async Task<HotelAmenityDTO> Get(int amenityId)
        {
            try
            {
                var amenity = await _db.HotelAmenities.FirstOrDefaultAsync(x => x.Id == amenityId);
                return _mapper.Map<HotelAmenityDTO>(amenity);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<HotelAmenityDTO> Get()
        {
            try
            {
                var amenities = _db.HotelAmenities.ProjectTo<HotelAmenityDTO>(_mapper.ConfigurationProvider);
                return amenities;
            }
            catch
            {
              return null;
            }
        }
    }
}