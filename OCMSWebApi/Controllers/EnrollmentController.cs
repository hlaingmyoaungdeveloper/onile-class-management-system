using Domain.features.Enrollment;
using Domain.models;
using Microsoft.AspNetCore.Mvc;

namespace OCMSWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnrollmentController : Controller
{
    private readonly EnrollmentService _service;

    public EnrollmentController()
    {
        _service = new EnrollmentService();
    }

    [HttpGet]
    public IActionResult GetEnrollment()
    {
       
        var result = _service.GetEnrollments(new EnrollmentListRequestModel());
        if (result.IsSuccess)
        {
           return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPost]
    public IActionResult CreateEnrollment([FromBody] EnrollmentCreateRequestModel model)
    {
        
        var result = _service.CreateEnrollment(model);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpGet("{EnrollmentId}")]
    public IActionResult GetEnrollment([FromRoute] EnrollmentEditRequestModel model)
    {
        
        var result = _service.GetEnrollment(model);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}
