using System;
using System.Collections.Generic;
using ForceTestingService.ApplicationCore.Interfaces;

namespace ForceTestingService.ApplicationCore.Mappers
{
    public abstract class GenericMapper<TEntity, TDto> : IMapper<TEntity, TDto>
    {
        public abstract TEntity Map(TDto dto);
        public abstract TDto Map(TEntity entity);

        public IEnumerable<TEntity> Map(IEnumerable<TDto> dataTransferObjects, Action<TEntity> callback = null)
        {
            var mappingResult = new List<TEntity>();
            foreach (var dto in dataTransferObjects)
            {
                var mappedEntity = Map(dto);
                if (mappedEntity != null)
                {
                    callback?.Invoke(mappedEntity);
                    mappingResult.Add(mappedEntity);
                }
            }

            return mappingResult;
        }

        public IEnumerable<TDto> Map(IEnumerable<TEntity> entities, Action<TDto> callback = null)
        {
            var mappingResult = new List<TDto>();
            foreach (var entity in entities)
            {
                var mappedDto = Map(entity);
                if (mappedDto != null)
                {
                    callback?.Invoke(mappedDto);
                    mappingResult.Add(mappedDto);
                }
            }

            return mappingResult;
        }
    }
}