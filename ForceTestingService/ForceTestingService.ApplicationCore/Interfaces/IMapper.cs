using System;
using System.Collections.Generic;

namespace ForceTestingService.ApplicationCore.Interfaces
{
    public interface IMapper <TEntity, TDto>
    {
        TEntity Map(TDto dto);
        TDto Map(TEntity entity);

        IEnumerable<TEntity> Map(IEnumerable<TDto> dataTransferObjects, Action<TEntity> callback = null);
        IEnumerable<TDto> Map(IEnumerable<TEntity> entities, Action<TDto> callback = null);
    }
}