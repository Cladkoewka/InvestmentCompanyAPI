namespace Application.Interfaces;

public interface IService<TGetDto, TCreateDto, TUpdateDto>
{
    Task<TGetDto?> GetByIdAsync(int id);                     
    Task<IEnumerable<TGetDto>> GetAllAsync();                
    Task<TGetDto> AddAsync(TCreateDto dto);                  
    Task<bool> UpdateAsync(int id, TUpdateDto dto);          
    Task<bool> DeleteAsync(int id);                          
}