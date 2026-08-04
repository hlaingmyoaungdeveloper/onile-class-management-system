using Domain.models;
using OnlineClassManagementSystem.Database.AppDbContextModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.features.Enrollment;

public class EnrollmentService
{
    private readonly AppDbContext _db;

    public EnrollmentService()
    {
        _db = new AppDbContext();
    }

    public EnrollmentListResponseModel GetEnrollments(EnrollmentListRequestModel model)
    {
        try
        {
            var enrollments = _db.TblEnrollments.Select(x => new EnrollmentModel
            {
                SubClassId = x.SubClassId,
                StudentName = x.StudentName,
                StudentContact = x.StudentContact,
                PaymentInfo = x.PaymentInfo,
                FatherName = x.FatherName,
                CreatedDateTime = x.CreatedDateTime,
                ModifiedDateTime = x.ModifiedDateTime,
            }).ToList();

            return new EnrollmentListResponseModel
            {
                IsSuccess = true,
                Message = "Successfully get Enrollment",
                Enrollments = enrollments
            };
        }
        catch (Exception ex)
        {
            return new EnrollmentListResponseModel
            {
                IsSuccess = false,
                Message = ex.ToString()
            };
        }
    }

    public EnrollmentEditResponseModel GetEnrollment(EnrollmentEditRequestModel model)
    {
        try
        {
            var enrollment = _db.TblEnrollments.FirstOrDefault(x => x.EnrollmentId == model.EnrollmentId);
            if (enrollment is null)
            {
                return new EnrollmentEditResponseModel
                {
                    Message = "Enrollment doesn't exist"
                };
            }
            return new EnrollmentEditResponseModel
            {
                IsSuccess = true,
                Message = "Successfully get Enrollment",
                SubClassId = enrollment.SubClassId,
                StudentName = enrollment.StudentName,
                StudentContact = enrollment.StudentContact,
                PaymentInfo = enrollment.PaymentInfo,
                FatherName = enrollment.FatherName,
                CreatedDateTime = enrollment.CreatedDateTime,
                ModifiedDateTime = enrollment.ModifiedDateTime,
            };
        }
        catch (Exception ex)
        {
            return new EnrollmentEditResponseModel
            {
                IsSuccess = false,
                Message = ex.ToString()
            };
        }
    }

    public EnrollmentCreateResponseModel CreateEnrollment(EnrollmentCreateRequestModel model)
    {
        try
        {
            var subClass = _db.TblSubClasses.FirstOrDefault(x => x.SubClassId  == model.SubClassId);
            if (subClass is null)
            {
                return new EnrollmentCreateResponseModel
                {
                    IsSuccess = false,
                    Message = "SubClass doesn't exist"
                };
            }
            
            if (subClass.IsDelete)
            {
                return new EnrollmentCreateResponseModel
                {
                    IsSuccess = false,
                    Message = "Cannot enroll in a deleted SubClass."
                };
            }
            
            if (subClass.StudentLimit <= subClass.StudentCount)
            {
                return new EnrollmentCreateResponseModel
                {
                    IsSuccess = false,
                    Message = "StudentLimit is full"
                };
            }
            TblEnrollment enrollment = new TblEnrollment()
            {
                SubClassId = model.SubClassId,
                StudentName = model.StudentName,
                StudentContact = model.StudentContact,
                PaymentInfo = model.PaymentInfo,
                FatherName = model.FatherName,
                CreatedDateTime = DateTime.Now,
                ModifiedDateTime = DateTime.Now,
            };
            _db.Add(enrollment);
            subClass.StudentCount += 1;
            int result = _db.SaveChanges();
        
            return new EnrollmentCreateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Successfully created Enrollment" : "Failed to create Enrollment"
            };
        }
        catch (Exception ex)
        {
            return new EnrollmentCreateResponseModel
            {
                IsSuccess = false,
                Message = ex.ToString()
            };
        }
    }

}
