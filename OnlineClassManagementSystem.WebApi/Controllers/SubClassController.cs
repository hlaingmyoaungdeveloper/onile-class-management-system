using Domain.features.SubClass;
using Domain.models;
using Microsoft.AspNetCore.Mvc;

namespace OCMSWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubClassController : Controller
{
    private readonly SubClassService _service;

    public SubClassController()
    {
        _service = new SubClassService();
    }

    [HttpGet]
    public IActionResult GetSubClass()
    {
        var result = _service.GetSubClasses(new SubClassListRequestModel());
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPost]
    public IActionResult CreateSubClass([FromBody] SubClassCreateRequestModel model)
    {
        var result = _service.CreateSubClass(model);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpGet("{SubClassId}")]
    public IActionResult GetSubClass([FromRoute] SubClassEditRequestModel model)
    {
        var result = _service?.GetSubClass(model);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPatch("{id}")]
    public IActionResult PatchSubClass(int id , SubClassPatchRequestModel model)
    {
        var result = _service.PatchSubClass(id, model);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpDelete("{SubClassId}")]
    public IActionResult DeleteSubClass([FromRoute] SubClassDeleteRequestModel model)
    {
        var result = _service.DeleteSubClass(model);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}
