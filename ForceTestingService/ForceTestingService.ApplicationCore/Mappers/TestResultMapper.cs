﻿using ForceTestingService.ApplicationCore.DTO;
using ForceTestingService.Infrastructure.Entities;

namespace ForceTestingService.ApplicationCore.Mappers
{
    public class TestResultMapper: GenericMapper<TestResult, TestResultDto>
    {
        public override TestResult Map(TestResultDto dto)
        {
            return new TestResult()
            {
                Id = dto.Id,
                Score = dto.Score,
                Date = dto.Date,
                TestBaseId = dto.TestBaseId,
                StudentUserId = dto.StudentUserId
            };
        }

        public override TestResultDto Map(TestResult entity)
        {
            return new TestResultDto()
            {
                Id = entity.Id,
                Score = entity.Score,
                Date = entity.Date,
                TestBaseId = entity.TestBaseId,
                StudentUserId = entity.StudentUserId,
                StudentFullName = entity.StudentUser.LastName + " " + entity.StudentUser.FirstName
            };
        }
    }
}